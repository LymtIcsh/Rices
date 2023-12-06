using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Suture
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerThirdPersonController : MonoBehaviour
    {
        [Header("玩家")] [Tooltip("角色的移动速度，单位为m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("角色的冲刺速度，单位为m/s")] public float SprintSpeed = 5.335f;

        [Tooltip("加速和减速")] public float SpeedChangeRate = 10.0f;
        
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Tooltip("角色转向面部移动方向的速度有多快")] [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Space(10)] [Tooltip("玩家可以跳跃的高度")] public float JumpHeight = 1.2f;

        [Tooltip("角色使用自己的重力值。引擎的默认值是-9.81f")] public float Gravity = -15.0f;

        [Space(10)] [Tooltip("在能够再次跳跃之前所需要的时间。设置为0f，立即再次跳跃")]
        public float JumpTimeout = 0.50f;

        [Tooltip("进入坠落状态所需要的时间。下楼梯时很方便")] public float FallTimeout = 0.15f;


        [Header("玩家落地")] [Tooltip("角色是否接地。不是CharacterController内置接地检查的一部分")]
        public bool Grounded = true;

        [Tooltip("适用于粗糙地面")] public float GroundedOffset = -0.14f;

        [Tooltip("接地止回阀半径。应该匹配CharacterController的半径吗")]
        public float GroundedRadius = 0.28f;

        [Tooltip("角色使用什么层作为地面")] public LayerMask GroundLayers;


        [Header("摄像机")] [Tooltip("在Cinemachine Virtual Camera中设置的跟随目标，摄像机将跟随")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("你可以把摄像机向上移动多远")] public float TopClamp = 70.0f;

        [Tooltip("你可以把摄像机向下移动多远")] public float BottomClamp = -30.0f;

        [Tooltip("额外的度来覆盖摄像头。有用的微调相机位置锁定时")] public float CameraAngleOverride = 0.0f;

        [Tooltip("用于锁定相机在所有轴上的位置")] public bool LockCameraPosition = false;


        // 摄像机
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        //玩家
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        private Collider[] physicsColliders = new Collider[10];

        //  【超时增量】
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // 动画id
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;


#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private PlayerAssetsInputs _playerAssetsInputs;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        /// <summary>
        /// 当前是鼠标设备吗?
        /// </summary>
        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }

        private void Awake()
        {
            _mainCamera ??= GameObject.FindGameObjectWithTag("MainCamera");
        }

        // Start is called before the first frame update
        void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _playerAssetsInputs = GetComponent<PlayerAssetsInputs>();

#if ENABLE_INPUT_SYSTEM
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "未添加 PlayerInput 组件");
#endif

            AssignAnimationIDs();

            //重新设置开始时的超时时间
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }


        // Update is called once per frame
        void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        /// <summary>
        /// 分配动画id
        /// </summary>
        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        /// <summary>
        /// 是否在地面
        /// </summary>
        void GroundedCheck()
        {
            // //设置球体位置，带有偏移量
            // Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            //     transform.position.z);
            //
            // Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            //     QueryTriggerInteraction.Ignore);

            //设置检测盒子
            int raycastAll =
                Physics.OverlapBoxNonAlloc(transform.position, new Vector3(0.15f, 0.1f, 0.15f), physicsColliders,
                    quaternion.identity, GroundLayers);

            Grounded = raycastAll > 0;

            //如果使用动画状态机，更新落地
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        void CameraRotation()
        {
            //如果有输入且摄像机位置不固定
            if (_playerAssetsInputs.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //不要将鼠标输入乘以Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _playerAssetsInputs.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _playerAssetsInputs.look.y * deltaTimeMultiplier;
            }

            //限制我们的旋转，所以我们的值被限制为360度
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine将遵循此目标
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        /// <summary>
        /// 角度限制
        /// </summary>
        /// <param name="lfAngle"></param>
        /// <param name="lfMin"></param>
        /// <param name="lfMax"></param>
        float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f)
                lfAngle += 360f;
            if (lfAngle > 360f)
                lfAngle -= 360f;

            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        /// <summary>
        /// 玩家移动
        /// </summary>
        private void Move()
        {
            //设置目标速度基于移动速度，冲刺速度，如果冲刺被按下
            float targetSpeed = _playerAssetsInputs.sprint ? SprintSpeed : MoveSpeed;

            //一种简单的加速和减速设计，易于删除，替换或迭代

            //注意:Vector2的==运算符使用近似值，因此不容易出现浮点错误，并且比幅度方便
            //若无输入，则将目标转速设为0
            if (_playerAssetsInputs.move == Vector2.zero)
                targetSpeed = 0.0f;

            //参考玩家当前的水平速度
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _playerAssetsInputs.analogMovement ? _playerAssetsInputs.move.magnitude : 1f;

            //加速或减速到目标速度
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                //创造曲线的结果，而不是线性的，给更有机的速度变化
                //注意Lerp中的T是夹住的，所以我们不需要夹住我们的速度
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                //将速度四舍五入到小数点后三位
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            //归一化输入方向
            Vector3 inputDirection = new Vector3(_playerAssetsInputs.move.x, 0, _playerAssetsInputs.move.y).normalized;

            //注意:Vector2的!=运算符使用近似值，因此不容易出现浮点错误，并且比幅度便宜
            //如果有一个移动输入，当玩家移动时旋转玩家
            if (_playerAssetsInputs.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                //旋转到相对于摄像机位置的面输入方向
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            //移动玩家
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            //如果使用动画状态机，更新速度 和 移动速率
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        /// <summary>
        /// 跳跃和重力
        /// </summary>
        private void JumpAndGravity()
        {
            if (Grounded)
            {
                //重置坠落超时定时器
                _fallTimeoutDelta = FallTimeout;

                //如果使用动画状态机，更新跳跃和自由落体
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                //停止我们的速度在着陆时无限下降
                if (_verticalVelocity < 0.0f)
                    _verticalVelocity = -2f;

                //跳跃
                if (_playerAssetsInputs.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // H * -2 * G的平方根=达到期望高度所需的速度
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2 * Gravity);

                    //如果使用动画状态机，更新跳跃
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                //跳跃超时
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                //重置跳转超时定时器
                _jumpTimeoutDelta = JumpTimeout;

                //坠落超时
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    //如果使用动画状态机，更新自由落体
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                //如果我们没有落地，就不要跳
                _playerAssetsInputs.jump = false;
            }


            //如果在终端下，将重力随时间施加(乘以时间两次以线性加速)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        #region 动画事件

        void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length>0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index],transform.TransformPoint(_controller.center),FootstepAudioVolume);
                }
            }
 
        }

        
        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
        #endregion


        // private void OnDrawGizmosSelected()
        // {
        //     Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        //     Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        //
        //     if (Grounded) Gizmos.color = transparentGreen;
        //     else Gizmos.color = transparentRed;
        //
        //     Gizmos.DrawCube(transform.position,
        //         new Vector3(0.15f, 0.1f, 0.15f));
        // }
    }
}
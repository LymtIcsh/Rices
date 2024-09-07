using NBC.ActionEditor;
using UnityEditor;
using UnityEngine;

namespace Suture
{
    [Name("行为树片段")]
    [Description("播放一个行为树剪辑的行为")]
    [Color(0.48f, 0.71f, 0.84f)]
    [Attachable(typeof(SkillGraphTrack))]
    public class PlaySkillGraph:ActionClip
    {
        [SerializeField] [HideInInspector] private float length = 1f;

        [MenuName("模型")]
        public GameObject Model;

        [MenuName("播放行为树")] [SelectObjectPath(typeof(ScriptableObject))]
        public string resPath = "";

        private ScriptableObject _scriptableObject;
        
        
        public ScriptableObject MyScriptableObject
        {
            get
            {
                if (string.IsNullOrEmpty(resPath))
                {
                    _scriptableObject = null;
                    return null;
                }

                if (_scriptableObject == null)
                {
#if UNITY_EDITOR
                    _scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(resPath);
#endif
                }

                return _scriptableObject;
            }
        }
        
        public override float Length
        {
            get => length;
            set => length = value;
        }

        public override bool isValid => MyScriptableObject != null;

        public override string info => isValid ? MyScriptableObject.name : base.info;
        
        public SkillGraphTrack Track => (SkillGraphTrack)parent;
    }
    
}
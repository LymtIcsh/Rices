using Cinemachine;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using Unity.VisualScripting;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class PveGame : GameBase
    {
        /// <summary>
        /// 运行秒数
        /// </summary>
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode => GameMode.Pve;

        public override void Initialize(int typeId, string name, Vector3 InitPos)
        {
            base.Initialize(typeId, name, InitPos);

            GameEntry.Entity.ShowEntity<BossObject>("Boss", Constant.AssetPriority.ArmorAsset,
                BossData.Create(GameEntry.Entity.GenerateSerialId(), 10004, "怪兽", new Vector3(66,0,-20)));
        }


        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;

            if (m_ElapseSeconds >= 1f)
            {
                m_ElapseSeconds = 0f;
                IDataTable<DREntity> drEntities = GameEntry.DataTable.GetDataTable<DREntity>();

                float randomPositionX = (float)Utility.Random.GetRandomDouble();
                float randomPositionZ = (float)Utility.Random.GetRandomDouble();

                //添加怪物
            }
        }
    }
}
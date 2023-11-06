using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace Suture
{
    public class PveGame : GameBase
    {
        /// <summary>
        /// 运行秒数
        /// </summary>
        private float m_ElapseSeconds = 0f;


        public override GameMode GameMode => GameMode.Pve;

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;

            if (m_ElapseSeconds>=1f)
            {
                m_ElapseSeconds = 0f;
                IDataTable<DREntity> drEntities = GameEntry.DataTable.GetDataTable<DREntity>();

                float randomPositionX = (float)Utility.Random.GetRandomDouble();
                float randomPositionZ = (float)Utility.Random.GetRandomDouble();
                
                
            }
        }
    }
}
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 代表一个房间
    /// </summary>
    public class Room : Entity
    {
        /// <summary>
        /// 房主Id
        /// </summary>
        public long RoomHolderPlayerId;

        /// <summary>
        /// 房间人数
        /// </summary>
        public int PlayerCount=1;

        /// <summary>
        /// 房间名
        /// </summary>
        public string RoomName;

        public int PlayerMaxCount = 6;


        protected override void OnRecycle()
        {
            RoomHolderPlayerId = 0;
            PlayerCount = 0;
            RoomName = "";

            base.OnRecycle();
        }
    }
}
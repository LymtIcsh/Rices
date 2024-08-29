//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace Suture
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData { get; private set; }

        /// <summary>
        /// 英雄属性数据存储库组件
        /// </summary>
        public static UnitAttributesDataRepositoryComponent UnitAttributesDataRepository { get; private set; }

        /// <summary>
        /// 房间管理器组件
        /// </summary>
        public static RoomManagerComponent RoomManager { get; private set; }

        /// <summary>
        /// 行为树数据仓库组件
        /// </summary>
        public static NP_TreeDataRepositoryComponent NP_TreeDataRepository { get; private set; }

        /// <summary>
        /// 初始化自定义组件
        /// </summary>
        private static void InitCustomComponents()
        {
            UnitAttributesDataRepository =
                UnityGameFramework.Runtime.GameEntry.GetComponent<UnitAttributesDataRepositoryComponent>();
            RoomManager = UnityGameFramework.Runtime.GameEntry.GetComponent<RoomManagerComponent>();
            NP_TreeDataRepository = UnityGameFramework.Runtime.GameEntry.GetComponent<NP_TreeDataRepositoryComponent>();
        }

        /// <summary>
        /// 初始化自定义调试器
        /// </summary>
        private static void InitCustomDebuggers()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
        }
    }
}
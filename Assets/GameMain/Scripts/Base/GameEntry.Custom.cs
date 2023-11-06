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
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化自定义组件
        /// </summary>
        private static void InitCustomComponents()
        {

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

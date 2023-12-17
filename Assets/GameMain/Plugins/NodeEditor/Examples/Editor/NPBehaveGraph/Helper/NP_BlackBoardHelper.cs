using Unity.VisualScripting;

namespace Suture
{
    /// <summary>
    /// NP黑板助手
    /// </summary>
    public static class NP_BlackBoardHelper
    {
        /// <summary>
        /// 设置当前黑板数据管理器
        /// </summary>
        /// <param name="npBehaveGraph"></param>
        public static void SetCurrentBlackBoardDataManager(NPBehaveGraph npBehaveGraph)
        {
            if (npBehaveGraph==null)
            {
                return;
            }

            NP_BlackBoardDataManager.CurrentEditedNP_BlackBoardDataManager = npBehaveGraph.NpBlackBoardDataManager;
        }
    }
}
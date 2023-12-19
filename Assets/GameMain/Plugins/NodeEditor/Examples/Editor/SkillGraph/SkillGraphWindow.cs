using System;
using GraphProcessor;
using UnityEditor.Experimental.GraphView;

namespace Suture
{
    /// <summary>
    /// 技能 图表窗口
    /// </summary>
    public class SkillGraphWindow:UniversalGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            graphView = new NPBehaveGraphView(this);

            m_MiniMap = new MiniMap() { anchored = true };
            graphView.Add(m_MiniMap);

            m_ToolbarView = new SkillToolbarView(graphView, m_MiniMap, graph);
            graphView.Add(m_ToolbarView);

            SetCurrentBlackBoardDataManager();
        }

        private void OnFocus()
        {
            SetCurrentBlackBoardDataManager();
        }

        /// <summary>
        /// 设置当前黑板数据管理器
        /// </summary>
        private void SetCurrentBlackBoardDataManager()
        {
           
        }
    }
}
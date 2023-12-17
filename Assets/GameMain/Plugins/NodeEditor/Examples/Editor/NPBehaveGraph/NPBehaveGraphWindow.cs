﻿using System;
using GraphProcessor;
using UnityEditor.Experimental.GraphView;

namespace Suture
{
    /// <summary>
    /// NP行为图窗口
    /// </summary>
    public class NPBehaveGraphWindow : UniversalGraphWindow
    {
        protected override void InitializeWindow(BaseGraph graph)
        {
            graphView = new NPBehaveGraphView(this);

            m_MiniMap = new MiniMap() { anchored = true };
            graphView.Add(m_MiniMap);

            m_ToolbarView = new NPBehaveToolbarView(graphView, m_MiniMap, graph);
            graphView.Add(m_ToolbarView);

            NP_BlackBoardHelper.SetCurrentBlackBoardDataManager(this.graph as NPBehaveGraph);
        }

        private void OnFocus()
        {
            NP_BlackBoardHelper.SetCurrentBlackBoardDataManager(this.graph as NPBehaveGraph);
        }
    }
}
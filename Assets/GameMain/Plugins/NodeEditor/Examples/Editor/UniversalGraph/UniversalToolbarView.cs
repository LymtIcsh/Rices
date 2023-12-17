﻿using GraphProcessor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Suture
{
    /// <summary>
    /// 通用工具栏视图
    /// </summary>
    public class UniversalToolbarView : ToolbarView
    {
        protected readonly MiniMap m_MiniMap;
        protected readonly BaseGraph m_BaseGraph;
        protected readonly BaseGraphView m_BaseGraphView;

        private readonly Texture2D m_CreateNewToggleIcon =
            AssetDatabase.LoadAssetAtPath<Texture2D>(
                $"{UniversalGraphWindow.NodeGraphProcessorPathPrefix}//Editor/CreateNew.png");

        private readonly Texture2D m_MiniMapToggleIcon =
            AssetDatabase.LoadAssetAtPath<Texture2D>(
                $"{UniversalGraphWindow.NodeGraphProcessorPathPrefix}/Editor/MiniMap.png");

        private readonly Texture2D m_ConditionalToggleIcon =
            AssetDatabase.LoadAssetAtPath<Texture2D>(
                $"{UniversalGraphWindow.NodeGraphProcessorPathPrefix}/Editor/Run.png");

        private readonly Texture2D m_ExposedParamsToggleIcon =
            AssetDatabase.LoadAssetAtPath<Texture2D>(
                $"{UniversalGraphWindow.NodeGraphProcessorPathPrefix}/Editor/Blackboard.png");

        private readonly Texture2D m_GotoFileButtonIcon =
            AssetDatabase.LoadAssetAtPath<Texture2D>(
                $"{UniversalGraphWindow.NodeGraphProcessorPathPrefix}/Editor/GotoFile.png");

        public UniversalToolbarView(BaseGraphView graphView, MiniMap miniMap, BaseGraph baseGraph) : base(graphView)
        {
            m_MiniMap = miniMap;
            //默认隐藏小地图，防止Graph内容过多而卡顿
            m_MiniMap.visible = false;

            m_BaseGraph = baseGraph;
            m_BaseGraphView = graphView;
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        protected override void AddButtons()
        {
            AddButton(new GUIContent("", m_CreateNewToggleIcon, "新建Graph资产"), () =>
            {
                GenericMenu genericMenu = new GenericMenu();
                foreach (var graphType in TypeCache.GetTypesDerivedFrom<BaseGraph>())
                {
                    genericMenu.AddItem(new GUIContent($"{graphType.Name}"), false,
                        data =>
                        {
                            BaseGraph baseGraph = GraphCreateAndSaveHelper.CreateGraph(data as System.Type);
                            GraphAssetCallbacks.InitializeGraph(baseGraph);
                        }, graphType);
                }
            }, true);

            AddToggle(new GUIContent("", m_ExposedParamsToggleIcon, "开/关参数面板"),
                graphView.GetPinnedElementStatus<ExposedParameterView>() != DropdownMenuAction.Status.Hidden,
                (v) => graphView.ToggleView<ExposedParameterView>());

            AddToggle(new GUIContent("", m_MiniMapToggleIcon, "开/关小地图"), m_MiniMap.visible,
                (v) => m_MiniMap.visible = v);

            AddButton(new GUIContent("", m_GotoFileButtonIcon, "定位至资产文件"),
                () =>
                {
                    EditorGUIUtility.PingObject(graphView.graph);
                    Selection.activeObject = graphView.graph;
                });
            
            AddSeparator(5);
            
            AddCustom(()=>
            {
                GUI.color =  new Color(128 / 255f, 128 / 255f, 128 / 255f);
                GUILayout.Label($"{m_BaseGraph.GetType().Name}-{m_BaseGraph.name}",
                    EditorGUIStyleHelper.GetGUIStyleByName(nameof(EditorStyles.toolbarButton)));
                GUI.color = Color.white;
            });
        }
    }
}
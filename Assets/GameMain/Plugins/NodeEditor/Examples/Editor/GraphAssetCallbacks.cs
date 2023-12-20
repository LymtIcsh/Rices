using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GraphProcessor;
using UnityEditor.Callbacks;
using System.IO;
using Suture;


namespace Suture
{
    /// <summary>
    /// 图形资产回调
    /// </summary>
    public class GraphAssetCallbacks
    {
        /// <summary>
        /// 创建图形处理器
        /// </summary>
        [MenuItem("Assets/Create/GraphProcessor", false, 10)]
        public static void CreateGraphPorcessor()
        {
            var graph = ScriptableObject.CreateInstance<BaseGraph>();
            ProjectWindowUtil.CreateAsset(graph, "GraphProcessor.asset");
        }

        /// <summary>
        /// 创建图形处理器NP
        /// </summary>
        [MenuItem("Assets/Create/GraphProcessor_NP", false, 10)]
        public static void CreateGraphPorcessor_NP()
        {
            var graph = ScriptableObject.CreateInstance<NPBehaveGraph>();
            ProjectWindowUtil.CreateAsset(graph, "NPBehaveGraph.asset");
        }

        /// <summary>
        /// 创建技能图形处理器
        /// </summary>
        [MenuItem("Assets/Create/GraphProcessor_Skill",false,10)]
        public static void CreateGraphPorcessor_Skill()
        {
            var graph = ScriptableObject.CreateInstance<SkillGraph>();
            ProjectWindowUtil.CreateAsset(graph,"SkillGraph.asset");
        }


        /// <summary>
        /// 打开BaseGraph
        /// </summary>
        /// <param name="instanceID"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        [OnOpenAsset(0)]
        public static bool OnBaseGraphOpened(int instanceID, int line)
        {
            // var asset = EditorUtility.InstanceIDToObject(instanceID) as BaseGraph;
            //
            // if (asset != null && AssetDatabase.GetAssetPath(asset).Contains("Examples"))
            // {
            //     EditorWindow.GetWindow<AllGraphWindow>().InitializeGraph(asset as BaseGraph);
            //     return true;
            // }
            //
            // return false;
            
            var baseGraph = EditorUtility.InstanceIDToObject(instanceID) as BaseGraph;
            return InitializeGraph(baseGraph);
        }

        public static bool InitializeGraph(BaseGraph baseGraph)
        {
            if (baseGraph == null) return false;

            switch (baseGraph)
            {
                case SkillGraph skillGraph:
                	NodeGraphWindowHelper.GetAndShowNodeGraphWindow<SkillGraphWindow>(skillGraph)
                		.InitializeGraph(skillGraph);
                	break;
                case NPBehaveGraph npBehaveGraph:
                    NodeGraphWindowHelper.GetAndShowNodeGraphWindow<NPBehaveGraphWindow>(npBehaveGraph)
                        .InitializeGraph(npBehaveGraph);
                    break;
                default:
                	NodeGraphWindowHelper.GetAndShowNodeGraphWindow<FallbackGraphWindow>(baseGraph)
                		.InitializeGraph(baseGraph);
                	break;
            }

            return true;
        }
    }
}
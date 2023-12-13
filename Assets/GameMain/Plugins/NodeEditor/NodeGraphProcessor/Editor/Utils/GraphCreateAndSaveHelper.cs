using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GraphProcessor
{
    /// <summary>
    /// 图形创建和保存助手
    /// </summary>
    public static class GraphCreateAndSaveHelper
    {
        /// <summary>
        /// 创建 Graph
        /// </summary>
        /// <param name="graphType"></param>
        /// <returns></returns>
        public static BaseGraph CreateGraph(Type graphType)
        {
            BaseGraph baseGraph = ScriptableObject.CreateInstance(graphType) as BaseGraph;
            string panelPath = "Assets/Plugins/NodeEditor/Examples/Saves/";
            Directory.CreateDirectory(panelPath);
            string panelFileName = "Graph";
            string path =
                EditorUtility.SaveFilePanelInProject("Save Graph Asset", panelFileName, "asset", "", panelPath);
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("创建graph已取消");
                return null;
            }
            AssetDatabase.CreateAsset(baseGraph,path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return baseGraph;
        }

        /// <summary>
        /// 将图形保存到磁盘
        /// </summary>
        /// <param name="baseGraphToSave"></param>
        public static void SaveGraphToDisk(BaseGraph baseGraphToSave)
        {
            EditorUtility.SetDirty(baseGraphToSave);
            AssetDatabase.SaveAssets();
        }
    }
}
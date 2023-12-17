using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace GraphProcessor
{
    /// <summary>
    /// 节点图窗口助手
    /// </summary>
    public static class NodeGraphWindowHelper
    {
        /// <summary>
        /// 所有节点图窗口
        /// </summary>
        public static Dictionary<string, BaseGraphWindow> AllNodeGraphWindows =
            new Dictionary<string, BaseGraphWindow>();

        /// <summary>
        /// 获取和显示节点图窗口
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetAndShowNodeGraphWindow<T>(string path) where T : BaseGraphWindow
        {
            if (AllNodeGraphWindows.TryGetValue(path,out var universalGraphWindow))
            {
                universalGraphWindow.Focus();
                return universalGraphWindow as T;
            }

            T resultWindow = EditorWindow.CreateWindow<T>(typeof(T));
            AllNodeGraphWindows[path] = resultWindow;
            return resultWindow;
        }
        
        /// <summary>
        /// 获取和显示节点图窗口
        /// </summary>
        /// <param name="owner"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetAndShowNodeGraphWindow<T>(BaseGraph owner) where T:BaseGraphWindow
        {
            return GetAndShowNodeGraphWindow<T>(AssetDatabase.GetAssetPath(owner));
        }

        /// <summary>
        /// 添加节点图窗口
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="universalGraphWindow"></param>
        public static void AddNodeGraphWindow(BaseGraph owner, BaseGraphWindow universalGraphWindow)
        {
            AllNodeGraphWindows[AssetDatabase.GetAssetPath(owner)] = universalGraphWindow;
        }
        
        /// <summary>
        /// 移除节点图窗口
        /// </summary>
        /// <param name="path"></param>
        public static void RemoveNodeGraphWindow(string path)
        {
            AllNodeGraphWindows.Remove(path);
        }
        
        /// <summary>
        /// 移除节点图窗口
        /// </summary>
        /// <param name="owner"></param>
        public static void RemoveNodeGraphWindow(BaseGraph owner)
        {
            AllNodeGraphWindows.Remove(AssetDatabase.GetAssetPath(owner));
        }
    }
}
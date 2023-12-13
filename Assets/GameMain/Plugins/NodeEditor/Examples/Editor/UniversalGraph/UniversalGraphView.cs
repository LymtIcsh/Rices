using GraphProcessor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Suture
{
    /// <summary>
    /// 通用图视图
    /// </summary>
    public class UniversalGraphView:BaseGraphView
    {
        //现在没有什么特别要添加的
        public UniversalGraphView(EditorWindow window) : base(window)
        {
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            BuildStackNodeContextualMenu(evt);
        }

        /// <summary>
        /// 构建堆栈节点上下文菜单
        /// </summary>
        /// <param name="evt"></param>
        protected void BuildStackNodeContextualMenu(ContextualMenuPopulateEvent evt)
        {
            Vector2 position =
                (evt.currentTarget as VisualElement).ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);
            evt.menu.AppendAction("New Stack",e=>AddStackNode(new BaseStackNode(position)),
                DropdownMenuAction.AlwaysEnabled);
        }
    }
}
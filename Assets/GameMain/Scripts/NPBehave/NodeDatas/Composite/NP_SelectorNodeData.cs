﻿using NPBehave;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// NP选择节点数据
    /// </summary>
    public class NP_SelectorNodeData:NP_NodeDataBase
    {
        [HideInEditorMode]
        private Selector m_SelectorNode;

        public override Composite CreateComposite(Node[] nodes)
        {
            this.m_SelectorNode = new Selector(nodes);
            return this.m_SelectorNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_SelectorNode;
        }
    }
}
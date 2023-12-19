using GraphProcessor;
using UnityEngine;

namespace Suture
{
    public class BuffNodeBase:BaseNode
    {
        [Input("InputBuff", allowMultiple = true)]
        [HideInInspector]
        public BuffNodeBase PrevNode;
        
        [Output("OutputBuff", allowMultiple = true)]
        [HideInInspector]
        public BuffNodeBase NextNode;

        public override Color color => Color.green;

        /// <summary>
        /// 自动添加链接buff
        /// </summary>
        public virtual void AutoAddLinkedBuffs()
        {
            
        }
        
        /// <summary>
        /// 获取Buff节点数据
        /// </summary>
        /// <returns></returns>
        public virtual BuffNodeDataBase GetBuffNodeData()
        {
            return null;
        }
    }
}
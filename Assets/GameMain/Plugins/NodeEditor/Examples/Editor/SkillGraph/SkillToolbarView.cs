using GraphProcessor;
using UnityEditor.Experimental.GraphView;

namespace Suture
{
    /// <summary>
    /// 技能 工具栏视图
    /// </summary>
    public class SkillToolbarView : NPBehaveToolbarView
    {
        public SkillToolbarView(BaseGraphView graphView, MiniMap miniMap, BaseGraph baseGraph) : base(graphView,
            miniMap, baseGraph)
        {
        }
    }
}
using NPBehave;

namespace Suture
{
    /// <summary>
    /// NP BB值助手
    /// </summary>
    public static class NP_BBValueHelper
    {
        /// <summary>
        /// 通过ANP_BBValue来设置目标黑板值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="blackboard"></param>
        /// <param name="key"></param>
        public static void SetTargetBlackboardUseANP_BBValue(ANP_BBValue anpBbValue, Blackboard blackboard, string key,
            bool isLocalPlayer = true)
        {
            // 这里只能用这个ToString()来做判断，直接获取Name的话是简略版本的
            switch (anpBbValue.NP_BBValueType.ToString())
            {
                
            }
        }
    }
}
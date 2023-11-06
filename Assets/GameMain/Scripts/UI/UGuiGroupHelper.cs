using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// UGUI 界面组辅助器
    /// </summary>
    public class UGuiGroupHelper : UIGroupHelperBase
    {
        public const int DepthFactor = 1000;

        private int m_Depth = 0;
        private Canvas m_CachedCanvas = null;

        private void Awake()
        {
            m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
            gameObject.GetOrAddComponent<GraphicRaycaster>();
        }

        // Start is called before the first frame update
        void Start()
        {
            m_CachedCanvas.overrideSorting = true;
            m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;

            RectTransform transform = GetComponent<RectTransform>();
            transform.anchorMin = Vector3.zero;
            transform.anchorMax = Vector3.one;
            transform.anchoredPosition = Vector3.zero;
            transform.sizeDelta = Vector3.zero;
        }

        /// <summary>
        /// 设置界面组深度
        /// </summary>
        /// <param name="depth">界面组深度</param>
        public override void SetDepth(int depth)
        {
            m_Depth = depth;
            m_CachedCanvas.overrideSorting = true;
            m_CachedCanvas.sortingOrder = DepthFactor * m_Depth;
        }
    }
}
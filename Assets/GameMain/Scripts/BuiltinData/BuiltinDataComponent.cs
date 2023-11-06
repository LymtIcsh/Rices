using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    public class BuiltinDataComponent : GameFrameworkComponent
    {
        [SerializeField]
        private TextAsset m_BuildInfoTextAsset = null;

        [SerializeField]
        private TextAsset m_DefaultDictionaryTextAsset = null;

        [SerializeField]
        private UpdateResourceForm m_UpdateResourceFormTemplate = null;

        private BuildInfo m_BuildInfo = null;

        public BuildInfo BuildInfo
        {
            get
            {
                return m_BuildInfo;
            }
        }

        public UpdateResourceForm UpdateResourceFormTemplate
        {
            get
            {
                return m_UpdateResourceFormTemplate;
            }
        }

        /// <summary>
        ///  构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取
        /// </summary>
        public void InitBuildInfo()
        {
            if (m_BuildInfoTextAsset == null || string.IsNullOrEmpty(m_BuildInfoTextAsset.text))
            {
                Log.Info("Build info can not be found or empty.");
                return;
            }

            m_BuildInfo = Utility.Json.ToObject<BuildInfo>(m_BuildInfoTextAsset.text);
            if (m_BuildInfo == null)
            {
                Log.Warning("Parse build info failure.");
                return;
            }
        }

        /// <summary>
        ///    默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultDictionary.xml
        /// 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新
        /// </summary>
        public void InitDefaultDictionary()
        {
            if (m_DefaultDictionaryTextAsset.text == null || string.IsNullOrEmpty(m_DefaultDictionaryTextAsset.text))
            {
                Log.Info("Default dictionary can not be found or empty.");
                return;
            }

            if (!GameEntry.Localization.ParseData(m_DefaultDictionaryTextAsset.text))
            {
                Log.Warning("Parse default dictionary failure.");
                return;
            }
        }

    }
}
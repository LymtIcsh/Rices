using GameFramework;
using GameFramework.Localization;
using GameFramework.Procedure;
using System;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Suture
{
    public class ProcedureLaunch : ProcedureBase
    {
        public override bool UseNativeDialog
        {
            get
            {
                return true;
            }
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //GameEntry.BuiltinData.InitBuildInfo();

            //InitLanguageSettings();

            //InitCurrentVariant();

            //GameEntry.BuiltinData.InitDefaultDictionary();
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            //运行一帧即切换到 Splash 展示流程
            ChangeState<ProcedureSplash>(procedureOwner);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }

        /// <summary>
        ///   语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言。
        /// </summary>
        private void InitLanguageSettings()
        {
            if(GameEntry.Base.EditorResourceMode && GameEntry.Base.EditorLanguage != Language.Ukrainian)
            {
                // 编辑器资源模式直接使用 Inspector 上设置的语言
                return;
            }

            Language language = GameEntry.Localization.Language;
            if (GameEntry.Setting.HasSetting(Constant.Setting.Language))
            {
                try
                {
                    string languageString = GameEntry.Setting.GetString(Constant.Setting.Language);
                    language = (Language)Enum.Parse(typeof(Language), languageString);
                }
                catch
                {

                }
            }

            if(language!=Language.English
                &&language!=Language.ChineseSimplified
                &&language!=Language.ChineseTraditional
                && language != Language.Korean)
            {
                // 若是暂不支持的语言，则使用英语
                language = Language.English;

                GameEntry.Setting.SetString(Constant.Setting.Language,language.ToString());
                GameEntry.Setting.Save();
            }
        }

        // 变体配置：根据使用的语言，通知底层加载对应的资源变体
        private void InitCurrentVariant()
        {
            if (GameEntry.Base.EditorResourceMode)
            {
                // 编辑器资源模式不使用 AssetBundle，也就没有变体了
                return;
            }

            string currentVariant = null;
            switch (GameEntry.Localization.Language)
            {
                case Language.English:
                    currentVariant = "en-us";
                    break;

                case Language.ChineseSimplified:
                    currentVariant = "zh-cn";
                    break;

                case Language.ChineseTraditional:
                    currentVariant = "zh-tw";
                    break;

                default:
                    currentVariant = "en-us";
                    break;
            }

            GameEntry.Resource.SetCurrentVariant(currentVariant);
            Log.Info("Init current variant complete.current variant :{0}.", currentVariant);
        }
    }
}

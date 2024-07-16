using System.Collections.Generic;
//using MonKey;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Suture
{
    public class GenerateComponentEditor:OdinEditorWindow
    {
        /// <summary>
        /// 所有需要生成的类
        /// </summary>
        [LabelText("所有需要生成Code配置")]
        [ListDrawerSettings(Expanded = true)]
        public List<AParams_GenerateBase> TargetsForGenerate = new List<AParams_GenerateBase>();

        //[Command("SutureEditor_CodeGenerator","从模板生成代码",Category = "SutureEditor")]
        public static void OpenWindow()
        {
            var window = GetWindow<GenerateComponentEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 600);
            window.titleContent = new GUIContent("代码生成工具");
        }
    }
}
﻿using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// Buff归属技能的Id
    /// </summary>
    [HideReferenceObjectPicker]
    public struct VTD_Id
    {
        [LabelText("此节点ID在数据仓库中的Key")]
        [ValueDropdown("GetIdKey")]
        [OnValueChanged("ApplayId")]
        public string IdKey;

        [LabelText("Id")]
        [InfoBox("无法对其直接赋值，需要在CanvasDataManager中Ids中注册键值对，然后选择NodeIdKey的值")]
        [ReadOnly]
        public long Value;
        
        
// #if UNITY_EDITOR
//         private IEnumerable<string> GetIdKey()
//         {
//             if (NP_BlackBoardDataManager.CurrentEditedNP_BlackBoardDataManager != null)
//             {
//                 return NP_BlackBoardDataManager.CurrentEditedNP_BlackBoardDataManager.Ids.Keys;
//             }
//
//             return null;
//         }
//
//         private void ApplayId()
//         {
//             if (NP_BlackBoardDataManager.CurrentEditedNP_BlackBoardDataManager != null)
//             {
//                 if (NP_BlackBoardDataManager.CurrentEditedNP_BlackBoardDataManager.Ids.TryGetValue(IdKey, out var targetId))
//                 {
//                     Value = targetId;
//                 }
//             }
//         }
// #endif
//
//         
    }
}
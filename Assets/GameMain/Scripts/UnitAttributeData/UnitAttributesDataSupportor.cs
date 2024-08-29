using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Sirenix.OdinInspector;

namespace Suture
{
    /// <summary>
    /// 单位属性数据支持
    /// </summary>
    public class UnitAttributesDataSupportor
    {
        [LabelText("此数据载体ID")]
        public int SupportId;

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<long, UnitAttributesNodeDataBase> UnitAttributesDataSupportorDic =
            new Dictionary<long, UnitAttributesNodeDataBase>();
    }
}
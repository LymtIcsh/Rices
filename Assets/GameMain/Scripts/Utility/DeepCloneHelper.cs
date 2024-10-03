using System;
using System.IO;
using System.Reflection;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 深拷贝辅助
    /// </summary>
    public static class DeepCloneHelper
    {
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T obj) where T : class
        {
            try
            {
                if (obj == null)
                {
                    return null;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    BsonSerializer.Serialize(new BsonBinaryWriter(stream), obj);
                    stream.Position = 0;
                    return BsonSerializer.Deserialize<T>(stream);
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 反射深度复制
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DeepCopyByReflect<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType)
                return obj;

            //反射
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic |
                                                         BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj)));
                }
                catch (Exception e)
                {
                }
            }

            return (T)retval;
        }
    }
}
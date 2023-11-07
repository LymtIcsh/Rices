//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2023-11-07 18:17:31.749
//------------------------------------------------------------

using GameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Suture
{
    /// <summary>
    /// 宠物表。
    /// </summary>
    public class DRPet : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取宠物编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取初始血量。
        /// </summary>
        public int InitHp
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取初始攻击。
        /// </summary>
        public int InitAsk
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取初始防御。
        /// </summary>
        public int InitDefense
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取Q。
        /// </summary>
        public string Skill_Q
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取W。
        /// </summary>
        public string Skill_W
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取E。
        /// </summary>
        public string Skill_E
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取R。
        /// </summary>
        public string Skill_R
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            InitHp = int.Parse(columnStrings[index++]);
            InitAsk = int.Parse(columnStrings[index++]);
            InitDefense = int.Parse(columnStrings[index++]);
            Skill_Q = columnStrings[index++];
            Skill_W = columnStrings[index++];
            Skill_E = columnStrings[index++];
            Skill_R = columnStrings[index++];

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    InitHp = binaryReader.Read7BitEncodedInt32();
                    InitAsk = binaryReader.Read7BitEncodedInt32();
                    InitDefense = binaryReader.Read7BitEncodedInt32();
                    Skill_Q = binaryReader.ReadString();
                    Skill_W = binaryReader.ReadString();
                    Skill_E = binaryReader.ReadString();
                    Skill_R = binaryReader.ReadString();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}

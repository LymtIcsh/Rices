//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2024-09-12 20:46:36.571
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
    /// 装备表。
    /// </summary>
    public class DREquip : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取装甲编号。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取装备名字。
        /// </summary>
        public string EquipName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取提供生命。
        /// </summary>
        public int OfferHP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取提供攻击力。
        /// </summary>
        public int OfferAsk
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取提供防御力。
        /// </summary>
        public int OfferDefense
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取销售价格。
        /// </summary>
        public int SellingPrice
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
            EquipName = columnStrings[index++];
            index++;
            OfferHP = int.Parse(columnStrings[index++]);
            OfferAsk = int.Parse(columnStrings[index++]);
            OfferDefense = int.Parse(columnStrings[index++]);
            SellingPrice = int.Parse(columnStrings[index++]);

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
                    EquipName = binaryReader.ReadString();
                    OfferHP = binaryReader.Read7BitEncodedInt32();
                    OfferAsk = binaryReader.Read7BitEncodedInt32();
                    OfferDefense = binaryReader.Read7BitEncodedInt32();
                    SellingPrice = binaryReader.Read7BitEncodedInt32();
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

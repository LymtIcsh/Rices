namespace Suture
{
    /// <summary>
    /// VTD Buff信息扩展
    /// </summary>
    public static class VTD_BuffInfoExtension
    {
        /// <summary>
        /// 自动添加buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="dataId"></param>
        /// <param name="buffNodeId">Buff节点的Id</param>
        /// <param name="theUnitFrom">Buff来源者</param>
        /// <param name="theUnitBelongTo">Buff寄生者</param>
        /// <param name="theSkillCanvasBelongTo"></param>
        public static void AutoAddBuff(this VTD_BuffInfo self, long dataId, long buffNodeId, Unit theUnitFrom,
            Unit theUnitBelongTo,
            NP_RuntimeTree theSkillCanvasBelongTo)
        {
            int Layers = 0;
            Layers = self.LayersDetermindByBBValue ? theSkillCanvasBelongTo.GetBlackboard().Get<int>(self.LayersThatDetermindByBBValue.BBkey) : self.Layers;

            if (self.LayersIsAbs)
            {
                IBuffSystem nextBuffSystemBase = BuffFactory.AcquireBuff(dataId, buffNodeId, theUnitFrom,
                    theUnitBelongTo, theSkillCanvasBelongTo);

                if (nextBuffSystemBase.CurrentOverlay<nextBuffSystemBase.BuffData.MaxOverlay&&nextBuffSystemBase.CurrentOverlay<Layers)
                {
                    Layers -= nextBuffSystemBase.CurrentOverlay;
                }
                else
                {
                    return;
                }
            }

            for (int i = 0; i < Layers; i++)
            {
                BuffFactory.AcquireBuff(dataId, buffNodeId, theUnitFrom, theUnitBelongTo,
                    theSkillCanvasBelongTo);
            }
        }
        
        /// <param name="npDataSupportor">Buff数据归属的数据块</param>
        /// <param name="buffNodeId">Buff节点的Id</param>
        /// <param name="theUnitFrom">Buff来源者</param>
        /// <param name="theUnitBelongTo">Buff寄生者</param>
        public static void AutoAddBuff(this VTD_BuffInfo self, NP_DataSupportor npDataSupportor, long buffNodeId, Unit theUnitFrom,
            Unit theUnitBelongTo,
            NP_RuntimeTree theSkillCanvasBelongTo)
        {
            int Layers = 0;
            if (self.LayersDetermindByBBValue)
            {
                Layers = theSkillCanvasBelongTo.GetBlackboard().Get<int>(self.LayersThatDetermindByBBValue.BBkey);
            }
            else
            {
                Layers = self.Layers;
            }

            if (self.LayersIsAbs)
            {
                IBuffSystem nextBuffSystemBase = BuffFactory.AcquireBuff(npDataSupportor, buffNodeId, theUnitFrom, theUnitBelongTo,
                    theSkillCanvasBelongTo);
                if (nextBuffSystemBase.CurrentOverlay < nextBuffSystemBase.BuffData.MaxOverlay && nextBuffSystemBase.CurrentOverlay < Layers)
                {
                    Layers -= nextBuffSystemBase.CurrentOverlay;
                }
                else
                {
                    return;
                }
            }

            for (int i = 0; i < Layers; i++)
            {
                BuffFactory.AcquireBuff(npDataSupportor, buffNodeId, theUnitFrom, theUnitBelongTo,
                    theSkillCanvasBelongTo);
            }
        }
        
    }
}
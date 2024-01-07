using GameFramework;
using GameFramework.Event;

namespace Suture
{
    public class UnitEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(UnitEventArgs).GetHashCode();

        public override int Id => EventId;

        public UnitEventArgs()
        {
            NowUnit = null;
        }

        public Unit NowUnit
        {
            get;
            private set;
        }
        
        public object UserData
        {
            get;
            private set;
        }

        public static UnitEventArgs Create(Unit NowUnit, object userData = null)
        {
            UnitEventArgs ne = ReferencePool.Acquire<UnitEventArgs>();
            ne.NowUnit = NowUnit;
            return ne;
        }

        public override void Clear()
        {
            NowUnit = null;
        }
    }
}
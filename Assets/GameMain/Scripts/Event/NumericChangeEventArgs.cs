using GameFramework;
using GameFramework.Event;

namespace Suture
{
    public class NumericChangeEventArgs:GameEventArgs
    {
        public static readonly int EventID = typeof(NumericChangeEventArgs).GetHashCode();

        public NumericComponent NumericComponent { get; private set; }
        public NumericType NumericType{ get; private set; }
        public float Result{ get; private set; }
        
        public override int Id => EventID;
        
        public NumericChangeEventArgs()
        {
            
        }
        
        public override void Clear()
        {
            Result = 0;
        }


        public static NumericChangeEventArgs Create(NumericComponent NumericComponent,NumericType NumericType,float Result)
        {
            NumericChangeEventArgs ne = ReferencePool.Acquire<NumericChangeEventArgs>();
            ne.NumericComponent = NumericComponent;
            ne.NumericType = NumericType;
            ne.Result = Result;
            return ne;
        }
    }
}
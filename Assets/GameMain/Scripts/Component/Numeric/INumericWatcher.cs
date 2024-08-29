namespace Suture
{
    public interface INumericWatcher
    {
        void Run(NumericComponent numericComponent, NumericType numericType, float value);
    }
}
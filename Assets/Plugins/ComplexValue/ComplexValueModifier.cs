namespace Plugins.ComplexValue
{
    public struct ComplexValueModifier
    {
        public int Magnitude;
        public object Payload;

        public ComplexValueModifier(int magnitude, object payload = null)
        {
            Magnitude = magnitude;
            Payload = payload;
        }
    }
}
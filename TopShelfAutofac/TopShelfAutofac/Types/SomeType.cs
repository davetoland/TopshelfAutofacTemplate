using TopShelfAutofac.Interfaces;

namespace TopShelfAutofac.Types
{
    public class SomeType
    {
        public ISomeOtherType MyOtherType { get; set; }

        public SomeType(ISomeOtherType sot)
        {
            MyOtherType = sot;
        }
    }
}

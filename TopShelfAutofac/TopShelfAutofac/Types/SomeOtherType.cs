using TopShelfAutofac.Interfaces;

namespace TopShelfAutofac.Types
{
    public class SomeOtherType : ISomeOtherType
    {
        public IRandomGenerator RandomGenerator { get; set; }

        public SomeOtherType(IRandomGenerator rg)
        {
            // Note: we don't need to pass rg down through the 'SomeType'
            // constructor, the IoC container will automatically generate it
            RandomGenerator = rg;
        }

        public int GetNumber()
        {
            return RandomGenerator.GenerateRandomNumber();
        }
    }
}

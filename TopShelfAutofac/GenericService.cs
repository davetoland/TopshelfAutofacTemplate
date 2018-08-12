using Topshelf;
using TopShelfAutofac.Types;

namespace TopShelfAutofac
{
    // To debug, just F5 and it'll run as a Console app.

    // To install this as a windows service:
    // http://docs.topshelf-project.com/en/latest/overview/commandline.html

    public class GenericService
    {
        public SomeType MyType { get; set; }

        public GenericService(SomeType st)
        {
            // IoC container populates the constructor as standard,
            // and also recursively populates referenced children
            MyType = st;
        }

        public bool Start(HostControl hostControl)
        {
            // Implementation here..
            int number = MyType.MyOtherType.GetNumber();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            // Implementation here..
            return true;
        }        
    }
}

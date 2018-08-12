namespace TopShelfAutofac
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bootstrap = Bootstrap.Create();
            bootstrap.Run();
        }
    }
}

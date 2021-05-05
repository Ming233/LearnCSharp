namespace CSharp_Generics
{
    public interface ILogger
    {

    }

    public class SqlServerLogger : ILogger
    {

    }

    public interface IRepository_ReflectIt<T>
    {

    }

    public class SqlRepository_ReflectIt<T> : IRepository_ReflectIt<T>
    {
        public SqlRepository_ReflectIt(ILogger logger)
        {

        }
    }

    public class Customer
    {

    }

    public class InvoiceService
    {
        public InvoiceService(IRepository<Customer> repository, ILogger logger)
        {

        }
    }
}

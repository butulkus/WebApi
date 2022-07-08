namespace Basket.Domain.Exceptions
{
    public class DomainPriceException : Exception
    {
        public DomainPriceException()
        {

        }

        public DomainPriceException(string message) : base(message)
        {

        }
    }
}

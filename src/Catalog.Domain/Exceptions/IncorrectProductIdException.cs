using Catalog.Domain.Exceptions.Base;

namespace Catalog.Domain.Exceptions
{
    public class IncorrectProductIdException : CatalogException
    {
        public string AdditionalInfo { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }
        public string Title { get; set; }
        public string Instance { get; set; }
        public IncorrectProductIdException(string instance)
        {
            Type = "product-custom-exception";
            Detail = "Chosen product id was incorrect";
            Title = "Custom Product Exception";
            AdditionalInfo = "Maybe you can try again in a bit?";
            Instance = instance;
        }
    }
}

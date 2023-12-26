namespace Library.Web.Areas.Admin.Models
{
    public class BookSearch
    {
        public string Title { get; set; }
        public uint BookPriceFrom { get; set; }
        public uint BookPriceTo { get; set; }
    }
}

namespace ASPNET_Core.ViewModels.ProductViewModels
{
    public class ListProductViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }

    }
}

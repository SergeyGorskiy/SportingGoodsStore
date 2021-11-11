namespace SportingGoodsStore.Models
{
    public class Product
    {
        public string Name { get; set; }

        public string Category { get; set; } = "WaterSports";

        public decimal? Price { get; set; }

        public Product Related { get; set; }

        public bool InStock { get; }

        public Product(bool stock = true)
        {
            InStock = stock;
        }

        public static Product[] GetProducts()
        {
            Product kayak = new Product
            {
                Name = "Kayak",
                Category = "Water craft",
                Price = 275M
            };
            Product lifejacket = new Product(false)
            {
                Name = "Lifejacket",
                Price = 48.95M
            };

            kayak.Related = lifejacket;

            return new Product[] {kayak, lifejacket, null};
        }
    }
}
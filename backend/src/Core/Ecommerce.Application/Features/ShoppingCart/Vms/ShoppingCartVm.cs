namespace Ecommerce.Application.Features.ShoppingCart.Vms
{
    public class ShoppingCartVm
    {
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItemVm>? ShoppingCartItems { get; set; }
        public decimal Total
        {
            get
            {
                return ShoppingCartItems != null ?
                    Math.Round(ShoppingCartItems.Sum(x => x.Price * x.Quantity) +
                               ShoppingCartItems.Sum(x => x.Price * x.Quantity) * Convert.ToDecimal(0.18) +
                               (ShoppingCartItems.Sum(x => x.Price * x.Quantity) < 100 ? 10 : 25)
                               , 2) 
                    : 0;
            }
            set { }
        }
        public int Quantity 
        { 
            get 
            { 
                return ShoppingCartItems != null ?
                    ShoppingCartItems!.Sum(x => x.Quantity) 
                    : 0; 
            } 
            set {} 
        } 
        public decimal SubTotal
        {
            get
            {
                return ShoppingCartItems != null ?
                    Math.Round(ShoppingCartItems.Sum(x => x.Price * x.Quantity), 2)
                    : 0;
            }
            set { }
        }   
        public decimal Tax
        {
            get
            {
                return ShoppingCartItems != null ?
                    Math.Round(ShoppingCartItems.Sum(x => x.Price * x.Quantity) * Convert.ToDecimal(0.18), 2)
                    : 0;
            }
            set {}
        }
        public decimal ShippingCost
        {
            get
            {
                return ShoppingCartItems != null ?
                    Math.Round((ShoppingCartItems.Sum(x => x.Price * x.Quantity) < 100 ? 10m : 25m), 2)
                    : 0;
            }
            set {}
        }
    }
}

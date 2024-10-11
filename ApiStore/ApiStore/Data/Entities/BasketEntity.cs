using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities
{
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty; 

        public virtual ICollection<BasketItem>? BasketItems { get; set; }
    }

    public class BasketItem
    {
        [Key]
        public int BasketItemId { get; set; }

        [Required, ForeignKey("Basket")]
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

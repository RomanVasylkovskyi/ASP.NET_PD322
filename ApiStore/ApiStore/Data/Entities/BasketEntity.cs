using ApiStore.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities
{
    public class Basket
    {
        [Key]
        public int BasketId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public virtual ICollection<BasketItem>? BasketItems { get; set; }
    }

    public class BasketItem
    {
        [Key]
        public int BasketItemId { get; set; }

        [ForeignKey("Basket")]
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

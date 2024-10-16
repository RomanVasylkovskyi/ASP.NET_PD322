using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities
{
    public class Orders
    {
        [Key]
        public int OrdersId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public virtual ICollection<OrdersItem>? OrdersItems { get; set; }
    }

    public class OrdersItem
    {
        [Key]
        public int OrdersItemId { get; set; }

        [Required, ForeignKey("Orders")]
        public int OrdersId { get; set; }
        public virtual Orders Orders { get; set; }

        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStore.Data.Entities.Identity
{
    public class UserRoleEntity : IdentityUserRole<int>
    {
        public virtual UserEntity User { get; set; } = new();
        public virtual RoleEntity Role { get; set; } = new();
    } 
}

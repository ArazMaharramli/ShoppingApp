using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Domain.Models.Base
{
    public class BaseEntitySimple<TStatus> : BaseEntity<TStatus> where TStatus : IConvertible
    {
        [Key]
        public long Id { get; set; }
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}

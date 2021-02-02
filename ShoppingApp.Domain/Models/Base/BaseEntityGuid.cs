using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Domain.Models.Base
{
    // never used
    public class BaseEntityGuid<TStatus> : BaseEntity<TStatus> where TStatus : IConvertible
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}

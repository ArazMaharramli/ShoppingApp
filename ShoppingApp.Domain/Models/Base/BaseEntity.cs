using System;
using System.ComponentModel.DataAnnotations;
using ShoppingApp.Utils.Classes;

namespace ShoppingApp.Domain.Models.Base
{
    public class BaseEntity<TStatus> where TStatus : IConvertible
    {
        public DateTime AddedDate { get; set; } = LocalTime.Now();
        public DateTime? UpdatedDate { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } 
        public TStatus Status { get; set; }
    }
}

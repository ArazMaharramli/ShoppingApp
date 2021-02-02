using System;
namespace ShoppingApp.Utils.Enums
{
    public enum Status : byte
    {
        // 0 -> 7
        Active = 0,
        Deleted = 1,
        Hidden = 2,
        
    }

    public enum StoreStatus : byte
    {
        Active = 0,
        Deleted = 1,
        Hidden = 2,

        // 8 -> 15
        PendingConfirmation = 8,
        NotConfirmed = 9,
        Confirmed = 10,
        Official = 11,
    }

    public enum OrderStatus:byte
    {
        Active = 0,
        Deleted = 1,
        
        Hidden = 2,

        // 16 -> 25
        Addvertized = 16, //????? <- niye ?
        Delivered = 18,
        Completed = 20,
        Refunded = 22,
        Cancelled = 24,

    }

    public enum ProductStatus : byte
    {
        Active = 0,
        Deleted = 1,
        
        Hidden = 2,

        //26 -> 36
        StocFinished = 26,
        HasToBeEdited = 29,
    }
    public enum ShipmentStatus : byte
    {
        Active = 0,
        Deleted = 1,
        
        Hidden = 2,

        // 37 -> 47
    }
    public enum ShoppingCardItemStatus : byte
    {
        Active = 0,
        Deleted = 1,
        
        Hidden = 2,

        // 48 -> 58
        Ordered = 48,
        Removed = 50,
        SavedToLater = 52,

    }
    public enum ShoppingCartStatus
    {
        Active = 0,
        Deleted = 1,
        Hidden = 2,

        // 49 -> 68
        Completed = 50,

    }
    public enum PaymentStatus : byte
    {
        Active = 0,
        Deleted = 1,
        Hidden = 2,

        // 69 -> 76
        Completed = 70,
        Failed = 72,

    }

    public enum RefreshTokenStatus : byte
    {
        Active = 0,
        Deleted = 1,
        //Hidden = 2,

        // 77 -> 88
        Used = 70,
        Invalidated = 71 
    }
}

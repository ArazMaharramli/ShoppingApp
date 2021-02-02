using System;

namespace ShoppingApp.Utils.Classes
{
    public class LocalTime
    {
        public static DateTime Now()
        {
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, tzi());
        }
        public static TimeZoneInfo tzi()
        {
            TimeZoneInfo zi;// = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            //TimeZoneInfo zi = TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku");
            try
            {
                zi = TimeZoneInfo.FindSystemTimeZoneById("Azerbaijan Standard Time");
            }
            catch (Exception)
            {
                zi = TimeZoneInfo.FindSystemTimeZoneById("Asia/Baku");
            }
            return zi;
        }
    }
}

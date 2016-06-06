using System;

namespace Cereris.Helpers
{
    public class ApodHelper
    {
        public static DateTime TodayDate()
        {
            //По скольку NASA принадлежит федеральному правительству США, время указываем тоже Вашингтона
            var date = DateTime.Now.AddHours(-11).Date;
            return date;
        }

        public static string CutVideoUrl(string fullUrl)
        {
            var url = fullUrl;
            if (fullUrl.IndexOf('?') != -1)
                url = fullUrl.Remove(fullUrl.IndexOf('?'));
            return url;
        }
    }
}
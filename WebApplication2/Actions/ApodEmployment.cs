using System;
using System.Collections.Generic;
using System.Linq;
using Cereris.ObjectsModel;

namespace Cereris.Actions
{
    public class ApodEmployment
    {
        /// <summary>
        /// Возвращает url 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetImageUrl(DateTime date)
        {
            string url;
            var apod = MainApodObjectOperations.Get(date);
            if (apod == null)
            {
                url = "images/notFindApod.png";
                return url;
            }
            url = apod.Url;
            var mediaType = apod.MediaType;
            if (mediaType != "image")
            {
                url = "images/notImage.png";
            }
            return url;
        }

        /// <summary>
        /// Возвращaет заголовок
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetTitleApod(DateTime date)
        {
            string title = string.Empty;
            var apod = MainApodObjectOperations.Get(date);
            if (apod == null)
            {
                return title;
            }
            title = apod.Title;
            return title;
        }

        /// <summary>
        /// Самые популярные 2 публикации для главной страницы
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, DateTime> GetMostPopularOnHome()
        {
            var listPopularPost = new Dictionary<string, DateTime>();
            //Сортируем по убыванию
            var apod = MainApodObjectOperations.List().OrderByDescending(o => o.ViewsCount).ToArray();
            if (!apod.Any())
            {
                return null;
            }
            for (int i = 0; i < 2; i++)
            {
                if (apod[i] != null)
                {
                   listPopularPost.Add(apod[i].Title, apod[i].Date());
                }
            }

            return listPopularPost;
        }

        /// <summary>
        /// Список популярных картинок
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<NasaAPOD> GetPopularList(int count)
        {
            var apodsList = MainApodObjectOperations.ListImages().OrderByDescending(o => o.ViewsCount).ToList();
            if (apodsList.Count > count)
            {
                var apodsArray = apodsList.GetRange(0, count);
                return apodsArray;
            }
            return apodsList;
        }

        /// <summary>
        /// Возвращает url случайной картинки
        /// </summary>
        /// <returns></returns>
        public static string GetRandomeImage()
        {
            var url = string.Empty;
            var list = MainApodObjectOperations.ListImages().ToList();
            if (list.Count > 0)
            {
                Random rnd = new Random();
                int r = rnd.Next(list.Count);
                url = list[r].Url;
            }
            return url;
        }

        /// <summary>
        /// Возвращает url случайного видео
        /// </summary>
        /// <returns></returns>
        public static string GetRandomeVideo()
        {
            var url = string.Empty;
            var list = MainApodObjectOperations.ListVideos().ToList();
            if (list.Count > 0)
            {
                Random rnd = new Random();
                int r = rnd.Next(list.Count);
                url = list[r].Url;
            }
            return url;
        }

        /// <summary>
        /// Возвращает за определенный период.
        /// </summary>
        /// <returns></returns>
        public static List<NasaAPOD> GetApodsUnderDate(DateTime minDate, int postCount, DateTime archiveDate)
        {
            var apodsList = new List<NasaAPOD>();
            //Достаем из базы объекты, если их еще не существует создаем
            apodsList = GetListAPODs(minDate, postCount, apodsList, archiveDate);
            //Сортируем список
            apodsList = apodsList.OrderByDescending(o => o.Date()).ToList();
            return apodsList;
        }

        private static List<NasaAPOD> GetListAPODs(DateTime minDate, int postCount, List<NasaAPOD> apodsList,  DateTime archiveDate)
        {
            var date = new DateTime();
            for (int i = 0; i <= postCount; i++)
            {
                date = minDate.AddDays(i);
                if (archiveDate != new DateTime() && minDate < archiveDate)
                {
                    return apodsList;
                }
                var apod = MainApodObjectOperations.Get(date);
                if (apod != null)
                apodsList.Add(apod);
            }
            if (apodsList.Count < postCount)
            {
                GetListAPODs(minDate.AddDays(-(postCount - apodsList.Count())), postCount - apodsList.Count()-1, apodsList, archiveDate);
            }
            return apodsList;
        }

    }
}
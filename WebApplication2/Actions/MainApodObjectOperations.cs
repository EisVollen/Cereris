using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Cereris.ObjectsModel;

namespace Cereris.Actions
{
    public class MainApodObjectOperations
    {
        /// <summary>
        /// Вернуть объект по дате
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static NasaAPOD Get(DateTime date)
        {
            int id = Convert.ToInt32(date.ToString("yyyyMMdd"));
            var apod = Get(id) ?? AddNewNasaApod(date);
            return apod;
        }

        /// <summary>
        /// Возвращает объект из базы по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NasaAPOD Get(int id)
        {
            NasaAPOD apod;
            using (var db = new NasaApodContext.NasaAPODContext())
            {
                apod = db.NasaAPODs.FirstOrDefault(d => d.Id == id);
            }
            return apod;
        }

        /// <summary>
        /// Возращает комментарий по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Comments Get(Guid id)
        {
            Comments comments;
            using (var db = new CommentsContext())
            {
                comments = db.Comments.FirstOrDefault(d => d.Id == id);
            }
            return comments;
        }

        /// <summary>
        /// Отдает комментарии к публикации
        /// </summary>
        /// <param name="apod"></param>
        /// <returns></returns>
        public static List<Comments> GetListComments(NasaAPOD apod, bool isPublic = true)
        {
            var comments = new List<Comments>();
            if (apod == null)
                return comments;
            using (var db = new CommentsContext())
            {
                comments = db.Comments.Where(o => o.Post_Id == apod.Id && o.IsPublic == isPublic).OrderBy(o=>o.PublicateDate).ToList();
            }
            return comments;
        }

        /// <summary>
        /// Отдает комментарии к публикации
        /// </summary>
        /// <param name="apod"></param>
        /// <returns></returns>
        public static int GetCountComments(NasaAPOD apod, bool isPublic = true)
        {
            var count = 0;
            if (apod== null)
                return 0;
            using (var db = new CommentsContext())
            {
                count = db.Comments.Count(o => o.Post_Id == apod.Id && o.IsPublic== isPublic);
            }
            return count;
        }
        /// <summary>
        /// Сохраняет объект 
        /// </summary>
        /// <param name="modifyApod"></param>
        public static void Update(NasaAPOD modifyApod)
        {
            using (var db = new NasaApodContext.NasaAPODContext())
            {
                var original = db.NasaAPODs.Find(modifyApod.Id);

                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(modifyApod);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Возвращает все объекты 
        /// </summary>
        /// <returns></returns>
        public static
            List<NasaAPOD> List()
        {
            var list = new List<NasaAPOD>();
            using (var db = new NasaApodContext.NasaAPODContext())
            {
                list = db.NasaAPODs.Select(d => d).ToList();
            }
            return list;
        }

        /// <summary>
        /// Возвращает только тот контент в котором были изображения
        /// </summary>
        /// <returns></returns>
        public static List<NasaAPOD> ListImages()
        {
            var listImage = new List<NasaAPOD>();
            using (var db = new NasaApodContext.NasaAPODContext())
            {
                listImage = db.NasaAPODs.Where(o=> o.MediaType.Equals("image")).ToList();
            }
            return listImage;
        }

        /// <summary>
        /// Возвращает только тот контент в котором был видео Файл
        /// </summary>
        /// <returns></returns>
        public static List<NasaAPOD> ListVideos()
        {
            var listvideo = new List<NasaAPOD>();
            using (var db = new NasaApodContext.NasaAPODContext())
            {
                listvideo = db.NasaAPODs.Where(o => o.MediaType.Equals("video")).ToList();
            }
            return listvideo;
        }

        /// <summary>
        /// Сохраняет в БД
        /// </summary>
        /// <param name="apod"></param>
        public static void Save(NasaAPOD apod)
        {
            using (var db = new NasaApodContext.NasaAPODContext())
            {
                db.NasaAPODs.Add(apod);
                db.SaveChanges(); 
            }
        }

        /// <summary>
        /// Сохраняет в БД
        /// </summary>
        /// <param name="comments"></param>
        public static void Save(Comments comments)
        {
            using (var db = new CommentsContext())
            {
                db.Comments.Add(comments);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Скачивает и сохраняет новый объект
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static NasaAPOD AddNewNasaApod(DateTime date)
        {
            try
            {
                answerNasaAPI newNasaApod = DownlandNasaApod(date);
                var explanationRu = TranslateExplanation(newNasaApod.explanation);
                if (string.IsNullOrEmpty(newNasaApod.date) || string.IsNullOrEmpty(newNasaApod.url) ||
                    string.IsNullOrEmpty(explanationRu))
                {
                    return null;
                }
                NasaAPOD nasaApod = Create(newNasaApod, explanationRu);
                return nasaApod;
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        /// <summary>
        /// Посылает запрос на API Наса за определенную дату
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static answerNasaAPI DownlandNasaApod(DateTime date)
        {
            string formatDate = date.ToString("yyyy-MM-dd");
            try
            {
                string apiKey = ConfigurationManager.AppSettings["NasaApodApiKey"];
                var uriString = string.Format("https://api.nasa.gov/planetary/apod?api_key={0}&date={1}",
                    apiKey,
                    formatDate);
                Uri uri = new Uri(uriString);
                var jsonstring = new WebClient().DownloadString(uri);
                var apodObject = JsonConvert.DeserializeObject<answerNasaAPI>(jsonstring);
                return apodObject;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        /// <summary>
        /// Создает из Json объекта NASAApod
        /// </summary>
        /// <param name="apod"></param>
        /// <param name="explanationRu"></param>
        /// <returns></returns>
        private static NasaAPOD Create(answerNasaAPI apod, string explanationRu)
        {
            NasaAPOD nasaApod = new NasaAPOD
            {
                Id = Convert.ToInt32(apod.date.Replace("-","")),
                Explanation = apod.explanation,
                ExplanationRu = explanationRu,
                HDUrl = apod.hdur,
                Title = apod.title,
                Url = apod.url,
                MediaType = apod.media_type,
                ViewsCount = 0

            };
            Save(nasaApod);
            return nasaApod;
        }

        /// <summary>
        /// Посылает запрос в яндекс для перевода описания
        /// </summary>
        /// <param name="explanation"></param>
        /// <returns></returns>
        public static string TranslateExplanation(string explanation)
        {
            if (string.IsNullOrEmpty(explanation))
            {
                return string.Empty;
            }
            string demoKey = ConfigurationManager.AppSettings["YandexKey"];
            var uriString =
                string.Format("https://translate.yandex.net/api/v1.5/tr.json/translate?key={0}&text={1}&lang=en-ru",
                    demoKey,
                    explanation);
            Uri uri = new Uri(uriString);
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var jsonstring = wc.DownloadString(uri);
            jsonstring= jsonstring.Replace("[", "").Replace("]", "").Replace("\\\\", "");
            var answerYandex = JsonConvert.DeserializeObject<answerYandex>(jsonstring);
            var explanationRu = answerYandex.text;

            ///Только код 200 соответсвует успешному переводу, во всех других случаях - оповещать и оставлять оригинал
            if (answerYandex.code != 200)
            {
                explanationRu = string.Format("Не удалось перевести.\r\nОригинал:\r\n\"{0}\"",
                    explanation);

            }
            return explanationRu;
        }

        public class answerNasaAPI
        {
            public string date { get; set; }
            public string explanation { get; set; }
            public string hdur { get; set; }
            public string media_type { get; set; }
            public string title { get; set; }
            public string url { get; set; }
        }

        private class answerYandex
        {
            public int code { get; set; }
            public string text { get; set; }

        }


    }
}

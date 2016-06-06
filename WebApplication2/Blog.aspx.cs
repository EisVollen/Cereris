using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Cereris.Actions;
using Cereris.Helpers;

namespace Cereris
{
    public partial class Blog : Page
    {
        protected DateTime _ArchiveDate;
        protected DateTime _MinDate;
        protected DateTime _MaxDate;
        private const int _CountBlogPost = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Установить даты за которые будут показаны посты
            SetDates();
            //Заполняем публикации
            MainPostStore();
            //Заполяем панель популярных постов
            MostPopular();

            //Заполняем панель "Случайного" контента
            RadmomeContent();

            //Заполняем архив
            FillArchiveContent();

        }

        private void FillArchiveContent()
        {
            var todayDate = DateTime.Today.Date;
            for (int i = 1; i < 13; i++)
            {
                var month = todayDate.AddMonths(-i);
                var date = new DateTime(month.Year, month.Month, 1);
                string HTMLPoststring = string.Format("<li><a href=\"Blog.aspx?archiveDate={1}\">{0}</a></li>",
                   date.Date.ToString("MMMM yyyy", CultureInfo.CurrentCulture),
                   date.Date.ToString("yyyyMMdd"));
                if (i < 7)
                {
                    archeveLeftSide.Controls.Add(new LiteralControl(HTMLPoststring));
                }
                else
                {
                    archeveRightSide.Controls.Add(new LiteralControl(HTMLPoststring));
                }

            }
        }

        private void SetDates()
        {
            //Устанавливаем даты 

            var minDateStr = Request.Params["minDate"];
            var maxDateStr = Request.Params["maxDate"];
            var archiveDateSrt = Request.Params["archiveDate"];

            if (!string.IsNullOrEmpty(archiveDateSrt))
            {
                try
                {
                    var archiveDate = ParseDateTime(archiveDateSrt);
                    if (archiveDate != new DateTime())
                    {
                        _ArchiveDate = new DateTime(archiveDate.Year, archiveDate.Month, 1);
                        if (string.IsNullOrEmpty(minDateStr) && string.IsNullOrEmpty(maxDateStr))
                        {
                            SetMaxArchiveDate(archiveDate);
                            _MinDate = _MaxDate.AddDays(-_CountBlogPost);
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                }

            }
            try
            {
                _MaxDate = ParseDateTime(maxDateStr);
            }
            catch (Exception)
            {
                if (_ArchiveDate != new DateTime())
                {
                    SetMaxArchiveDate(_ArchiveDate);
                }
                _MaxDate = ApodHelper.TodayDate();
            }

            _MinDate = _MaxDate.AddDays(-_CountBlogPost);

        }

        private void SetMaxArchiveDate(DateTime archiveDate)
        {
            var lastDayArchiveMonth = DateTime.DaysInMonth(archiveDate.Year, archiveDate.Month);
            _MaxDate = new DateTime(archiveDate.Year, archiveDate.Month, lastDayArchiveMonth);
        }

        private static DateTime ParseDateTime(string dateStr)
        {
            return DateTime.ParseExact(dateStr, "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Основное хранилище публикаций
        /// </summary>
        public void MainPostStore()
        {
            //Посты 
            var listApods = ApodEmployment.GetApodsUnderDate(_MinDate, _CountBlogPost, _ArchiveDate);
            foreach (var apod in listApods)
            {
                var commentsCount = MainApodObjectOperations.GetCountComments(apod);
                string HTMLPoststring = string.Format("<div class=\"clear10\"></div>" +
                                                      " <div class=\"post-box\">" +
                                                      //дата
                                                      " <div class=\"date-box alignleft_block\"> <div> <div></div>" +
                                                      " <span class=\"line1\">{0}</span>" +
                                                      "<span class=\"line2\">{1}</span>" +
                                                      "<span class=\"line3\">{2}</span> </div></div>" +
                                                      //заголовок
                                                      "<h1 class=\"alignleft_block margin-left-20 post-title heading\">{3}</h1>" +
                                                      //контент
                                                      "<div class=\"alignleft_block post-content margin-left-20 margin_top10\">" +
                                                      "{6} <div class=\"clear10\"></div>" +
                                                      //Описание
                                                      "<p>{4}</p></div><div class=\"clear10\"></div>" +

                                                      //ссылка на публикацию
                                                      "<a href=\"ApodDetail.aspx?date={5}\" class=\"alignright_block button\">Открыть публикацию</a>" +
                                                      "<div class=\"clear20\"></div>" +
                                                      "<div class=\"info-bar alignleft_block\"> <span class=\"tags\">Количество просмотров: {7}</span>"+
                                                      "<span class=\"comments\">({8}) Комментариев </span> </div> </div>",
                    apod.Date().ToString("dd"),
                    apod.Date().ToString("MMM", CultureInfo.GetCultureInfo("ru-ru")),
                    apod.Date().ToString("yyyy"),
                    apod.Title,
                    apod.ExplanationRu.Length > 255 ? apod.ExplanationRu.Substring(0, 255) + "..." : apod.ExplanationRu,
                    apod.Date().ToString("yyyy-MM-dd"),
                    apod.MediaType.Equals("video")
                        ? string.Format(
                            "<iframe src=\"{0}\" width=\"570\" height=\"360\" frameborder=\"0\" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe> ",
                            ApodHelper.CutVideoUrl(apod.Url))
                        : string.Format("<img src=\"{0}\" width=\"570\" height=\"270\" alt=\"\" />", apod.Url),
                    apod.ViewsCount,
                    commentsCount);
                postStore.Controls.Add(new LiteralControl(HTMLPoststring));
            }
            _MinDate = listApods.Last() != null? listApods.Last().Date() : _MinDate;
            //навигация
            var nextMaxDate = _MinDate.Date.AddDays(-1);
            var nextMinDate = nextMaxDate.AddDays(-_CountBlogPost);
            var prevMaxDate = _MaxDate.Date.AddDays(_CountBlogPost);
            var prevMinDate = prevMaxDate.AddDays(-_CountBlogPost);
            var maximumAllowableDate = _ArchiveDate == new DateTime()
                ? ApodHelper.TodayDate()
                : new DateTime(_ArchiveDate.Year, _ArchiveDate.Month,
                    DateTime.DaysInMonth(_ArchiveDate.Year, _ArchiveDate.Month));

            var buttonPoststring =
                string.Format("<div class=\"clear10\"></div><div class=\"paging-wrapper blog gray-frame\">{0}{1}</div>{2}",
                    prevMinDate < maximumAllowableDate
                        ? string.Format(
                            "<a href=\"Blog.aspx?minDate={0}&maxDate={1}{2}\" ><span  class=\"next\">&nbsp;</span></a>",
                            prevMinDate.ToString("yyyyMMdd"),
                            prevMaxDate.ToString("yyyyMMdd"),
                            _ArchiveDate != new DateTime() ? "&archiveDate=" + _ArchiveDate.ToString("yyyyMMdd") : "")
                        : "",
                    nextMinDate >= _ArchiveDate
                        ? string.Format(
                            " <a href=\"Blog.aspx?minDate={0}&maxDate={1}{2}\"><span class=\"prev\">&nbsp;</span></a> ",
                            nextMinDate.ToString("yyyyMMdd"),
                            nextMaxDate.ToString("yyyyMMdd"),
                            _ArchiveDate != new DateTime() ? "&archiveDate=" + _ArchiveDate.ToString("yyyyMMdd") : "")
                        : "",
                    _ArchiveDate != new DateTime() ? "<div class=\"clear10\"></div><div class=\"info-bar alignleft_block\"><a href=\"Blog.aspx\"> Сбросить фильтр </a></div>" : "");
            postStore.Controls.Add(new LiteralControl(buttonPoststring));
        }

        /// <summary>
        /// Заполняет панель популярного
        /// </summary>
        public void MostPopular()
        {
            var popularApodList = ApodEmployment.GetPopularList(5);
            foreach (var apod in popularApodList)
            {
                string HTMLPoststring = string.Format("<li><img src=\"{0}\" width=\"52\" height=\"52\" alt=\"\" />" +
                                                      "<p><a href=\"{1}\" class=\"notextdecoration gray-only\">{2}</a> <br />" +
                                                      "<span>{3}</span></p></li>",
                    apod.Url,
                    string.Format("ApodDetail.aspx?date={0}", apod.Date().ToString("yyyy-MM-dd")),
                    apod.Title,
                    apod.Date().ToString("d MMMM yyyy г.", CultureInfo.GetCultureInfo("ru-ru")));
                tabposts.Controls.Add(new LiteralControl(HTMLPoststring));
            }
        }

        public void RadmomeContent()
        {
            randomeImages.Attributes["src"] = ApodEmployment.GetRandomeImage();
            randomeVideo.Attributes["src"] = ApodEmployment.GetRandomeVideo();
        }
        
    }
}
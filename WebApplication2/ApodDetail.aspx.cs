using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Cereris.Actions;
using Cereris.Helpers;
using Cereris.ObjectsModel;

namespace Cereris
{
    public partial class ApodDetail : Page
    {
        protected string commentTemplate = "<div class=\"comment\">" +
                                           "<img src = \"images/blog/avatar/anon-small.png\" width=\"58\" height=\"58\" alt=\"\" class=\"alignleft img-margin-right gray-frame\" />" +
                                           "<h2><a href = \"#\" class=\"gray-only notextdecoration\">{0}</a></h2>" +
                                           "<span class=\"not-active\">{1}</span><div class=\"clear\"></div>" +
                                           "<p>{2}</p>" +
                                           "<a runat=\"server\" id=\"answerButton_{4}\" href=\"<%= parentStr %> {4}\" class=\"alignleft_block button\">Ответить</a>" +
                                           "<div class=\"clear10\"></div>" +
                                           " <div class=\"comment\"><form name=\"comment_form_{4}\" style=\"visibility: hidden\" id=\"comment_form_{4}\" class=\"generic-form alignleft_block\" action=\"blog-post.html\"><p>" +
                                           "<input type=\"text\" runat=\"server\"  name=\"txt_comment_name_{4}\" style=\"width:40%\" id=\"txt_comment_name_{4}\" class=\"medium user\" required=\"true\"></input>" +
                                           "<label for=\"txt_comment_name\">Имя *</label></p>" +
                                           "<p><input type =\"email\" runat=\"server\" name=\"txt_comment_email_{4}\"  style=\"width:40%\" id=\"txt_comment_email_{4}\" class=\"medium email\" required=\"true\"></input>" +
                                           "<label for=\"txt_comment_email\">E-mail *</label></p>" +
                                           "<p><textarea class=\"xxlarge\" runat=\"server\" rows=\"6\" style=\"width: 60%\" cols=\"4\" name=\"txt_comment_message_{4}\" id=\"txt_comment_message{4}\" required=\"true\"></textarea></p>" +
                                           "<div id =\"message_box_place_holder_{4}\"></div><div class=\"clear10\"></div>" +
                                           "<a onserverclick =\"SendClick\" runat=\"server\" class=\"button alignleft_block bold_only\" id=\"btnSubmit_{4}\">Отправить</a>" +
                                           "</form></div><div class=\"clear10\"></div>" +
                                           "{3}</div>";
        protected string referece { get; set; }

        protected string parentStr { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var dateStr = Request.Params["date"];
                var original = Request.Params["original"];
                var isOriginal = false;
                Boolean.TryParse(original, out isOriginal);
                var date = ApodHelper.TodayDate();
                if (!string.IsNullOrEmpty(dateStr))
                {
                    date = DateTime.ParseExact(dateStr, "yyyy-MM-dd",
                        CultureInfo.InvariantCulture);
                }
                //ссылка на оригинал
                this.referece = string.Format("ApodDetail.aspx?date={0}&original=", date.ToString("yyyy-MM-dd"));

                //Основной пост
                OriginalClick(date, isOriginal);

                //Заполяем панель популярных постов
                MostPopular();

                //Заполняем панель "Случайного" контента
                RadmomeContent();

            }
        }

        //Главный пост
        public void OriginalClick(DateTime date, bool isOriginal)
        {
            var apod = MainApodObjectOperations.Get(date);
            if (apod != null)
            {
                Post_Id.Value = apod.Id.ToString();
                day.InnerText = apod.Date().ToString("dd");
                mounth.InnerText = apod.Date().ToString("MMM", CultureInfo.GetCultureInfo("ru-ru"));
                years.InnerText = apod.Date().ToString("yyyy");
                title.InnerText = apod.Title;
                Text.InnerText = isOriginal ? apod.Explanation : apod.ExplanationRu;
                if (apod.MediaType != "image")
                {
                    video.Attributes["src"] = ApodHelper.CutVideoUrl(apod.Url);
                    Picture.Visible = false;
                }
                else
                {
                    Picture.Attributes["src"] = apod.Url;
                    video.Visible = false;
                }
                //увеличиваем количество просмотров
                apod.ViewsCount ++;
                MainApodObjectOperations.Update(apod);

                var comments = MainApodObjectOperations.GetListComments(apod);
                var commentsCount = comments.Count;
                var HTMLPoststring = string.Format("<span class=\"tags\"> Количество просмотров: {0}</span>" +
                                                   "<span class=\"comments\">({1}) Комментариев</span>",
                    apod.ViewsCount,
                    commentsCount);
                infobar.Controls.Add(new LiteralControl(HTMLPoststring));
                if (commentsCount != 0)
                {
                    AddCommets(comments);
                }
            }
        }

        private void AddCommets(List<Comments> comments)
        {
            var mainComments = comments.Where(o => o.ParentId == null);
            var HTMLstring = string.Empty;

            foreach (var mainComment in mainComments)
            {
                HTMLstring +=CreateCommets(mainComment, comments);

            }
            CommentsPosts.Controls.Add(new LiteralControl(HTMLstring));


        }

        private string CreateCommets(Comments mainComment, List<Comments> comments)
        {
            var HTMLstring = string.Empty;
            if (mainComment == null)
                return HTMLstring;
            HTMLstring += string.Format(commentTemplate,
                   mainComment.Author_Name,
                   mainComment.PublicateDate,
                   mainComment.Text,
                   CreateAnswerCommets(mainComment, comments),
                   mainComment.Id);
            return HTMLstring;

        }

        private string CreateAnswerCommets(Comments mainComment, List<Comments> comments)
        {
            var answerComments = comments.Where(o => o.ParentId == mainComment.Id).ToList();
            var HTMLstring = string.Empty;
            foreach (var answerComment in answerComments)
            {
                HTMLstring += string.Format(commentTemplate,
                    answerComment.Author_Name,
                    answerComment.PublicateDate,
                    answerComment.Text,
                    CreateAnswerCommets(answerComment, comments),
                    answerComment.Id);
            }
            return HTMLstring;
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

        protected void SendClick(Object sender, EventArgs e)
        {
            var name = txt_comment_name.Value;
            var email = txt_comment_email.Value;
            var text = txt_comment_message.Value;
            Guid parentId;
            Guid? parent = null;
            if (Guid.TryParse(parentStr, out parentId))
            {
                parent = parentId;
            }


            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(text))
                return;

            Comments comments = new Comments
            {
                Id = Guid.NewGuid(),
                Post_Id = Convert.ToInt32(Post_Id.Value),
                Author_Name = name,
                Author_Email = email,
                Text = text,
                Rating = 0,
                CreatedDate = DateTime.Now,
                PublicateDate = null,
                IsPublic = true,
                ParentId = parent

            };
            MainApodObjectOperations.Save(comments);
            Page_Load(sender, e);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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
                                           "<div class=\"clear10\"></div>" +
                                           "{3}</div>";
        protected string referece { get; set; }

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
                    DateTime.TryParse(dateStr, out date);
                   
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

                AddPostComments(apod);
            }
        }

        private void AddPostComments(NasaAPOD apod)
        {
            var comments = MainApodObjectOperations.GetListComments(apod);
            infobar.Controls.Clear();
            var commentsCount = comments.Count;
            var HTMLPoststring = string.Format("<span class=\"tags\"> Количество просмотров: {0}</span>" +
                                               "<span class=\"comments\">({1}) Комментариев</span>",
                apod.ViewsCount,
                commentsCount);
            infobar.Controls.Add(new LiteralControl(HTMLPoststring));
            if (commentsCount != 0)
            {
                CommentsPosts.Controls.Clear();
                AddCommets(comments);
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
            var name = comment_name.Value;
            var email = comment_email.Value;
            var text = comment_message.Value;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(text))
            {
                throw new Exception("Обязательные поля должныбыть заполнены");
                return;
            }

            SendEmailAthor(email, name);

            Comments comments = new Comments
            {
                Id = Guid.NewGuid(),
                Post_Id = Convert.ToInt32(Post_Id.Value),
                Author_Name = name,
                Author_Email = email,
                Text = text,
                Rating = 0,
                CreatedDate = DateTime.Now,
                PublicateDate = DateTime.Now,
                IsPublic = true,
                ParentId = null

            };

            MainApodObjectOperations.Save(comments);

            var apod = MainApodObjectOperations.Get(comments.Post_Id);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        private void SendEmailAthor(string to, string name)
        {

            string from = ConfigurationManager.AppSettings["email"];
            string password = ConfigurationManager.AppSettings["password"];
            MailMessage message = new MailMessage(from, to)
            {
                Subject = "Ваш комментарий был опубликован",
                Body =  string.Format("Здравствуйте, {0}. Ваш комментарий был опубликован на сайте Cereris."
                , name)
            };
            SmtpClient client = new SmtpClient("smtp.mail.ru", 25);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, password);
            client.EnableSsl = true;
           
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                            ex.ToString());
            }
        }

        private void ClearCommentsForm()
        {
            comment_name.Value = string.Empty;
            comment_email.Value = string.Empty; 
            comment_message.Value = string.Empty; 
        }
    }
}
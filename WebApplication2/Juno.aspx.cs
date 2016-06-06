using System;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Cereris.Actions;
using Cereris.Helpers;

namespace Cereris
{
    public partial class Juno : Page
    {
        protected string ReferenceDetailPage = "ApodDetail.aspx?date=";

        protected void Page_Load(object sender, EventArgs e)
        {
            var today = ApodHelper.TodayDate();

            //массивы разметки
            var arrayImage = new[] {sliderImage1, sliderImage2, sliderImage3};
            var arrayReferenceSlider = new[] {referenceSlider1, referenceSlider2, referenceSlider3};
            var arrayTitleImage = new[] {TitleImage1, TitleImage2, TitleImage3};
            var arrayDesctipt = new[] {DsrImage1, DsrImage2, DsrImage3};

            //Заполняем слайдер 3 изображениями -сегодня-вчера-позавчера
            for (int i = 0; i < 3; i++)
            {
                var date = today.AddDays(-i).Date;
                arrayImage[i].Attributes["src"] = ApodEmployment.GetImageUrl(date);
                arrayReferenceSlider[i].Attributes["href"] = ReferenceDetailPage + date.ToString("yyyy-MM-dd");
                arrayTitleImage[i].InnerText = ApodEmployment.GetTitleApod(date);
                arrayDesctipt[i].InnerText = date.ToString("dd.MM.yyyy");
            }
            
            //Популярные публикации
            var mostPopularList = ApodEmployment.GetMostPopularOnHome();
            var arrayMostPopular = new[] {MostPopular, MostPopular2};
            var arrayMostPopular2Date = new[] {MostPopularDate, MostPopular2Date};

            for (int i = 0; i < 2; i++)
            {
                arrayMostPopular[i].Attributes["href"] = ReferenceDetailPage + mostPopularList.ElementAt(i).Value.ToString("yyyy-MM-dd");
                arrayMostPopular[i].InnerText = mostPopularList.ElementAt(i).Key;
                arrayMostPopular2Date[i].InnerText = mostPopularList.ElementAt(i).Value.ToString("dddd, d MMMM yyyy г.", CultureInfo.GetCultureInfo("ru-ru"));
            }

        }

    }
}
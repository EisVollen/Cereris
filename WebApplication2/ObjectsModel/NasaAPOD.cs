using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Cereris.Actions;

namespace Cereris.ObjectsModel
{
    public class NasaAPOD
    {
        /// <summary>
        /// Индификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        /// <summary>
        /// Описание оргинал
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// Описание на русском языке 
        /// </summary>
        public string ExplanationRu { get; set; }

        /// <summary>
        /// ссылка на картинку в хорошем качестве
        /// </summary>
        public string HDUrl { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на контент
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Тип контента
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Количество просмотров
        /// </summary>
        public int ViewsCount { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        /// <returns></returns>
        public virtual DateTime Date()
        {
            DateTime date = DateTime.ParseExact(Id.ToString(), "yyyyMMdd",
                CultureInfo.InvariantCulture);
            return date;
        }

    }
}
using System;

namespace Cereris.ObjectsModel
{
    public class Comments
    {
        /// <summary>
        ///  Индификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Публикация 
        /// </summary>
        public int Post_Id { get; set; }

        /// <summary>
        /// Имя автора 
        /// </summary>
        public string Author_Name { get; set; }

        /// <summary>
        /// емаил автора
        /// </summary>
        public string Author_Email { get; set; }

        /// <summary>
        /// Текст 
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Рейтинг
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Дата создания 
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime? PublicateDate { get; set; }

        /// <summary>
        /// Опубликован
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Id коментария, на который является ответом этот
        /// </summary>
        public Guid? ParentId { get; set; }
    }
}
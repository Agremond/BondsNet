
namespace QuikSharp.DataStructures
{
    /// <summary>
    /// Состояние заявки или стоп-заявки
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Отправляется или отправлена на сервер
        /// </summary>
        Waiting,
        /// <summary>
        /// Активна
        /// </summary>
        Active,
        /// <summary>
        /// Исполнена
        /// </summary>
        Completed,
        /// <summary>
        /// Снимается
        /// </summary>
        Canceling,
        /// <summary>
        /// Снята
        /// </summary>
        Canceled,
        /// <summary>
        /// Ошибка
        /// </summary>
        Error
    }
}

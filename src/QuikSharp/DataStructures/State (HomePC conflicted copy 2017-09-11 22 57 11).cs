
namespace QuikSharp.DataStructures
{
    /// <summary>
    /// Состояние заявки или стоп-заявки
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Отправлена на сервер
        /// </summary>
        Waiting,
        /// <summary>
        /// Активна
        /// </summary>
        Active,
        /// <summary>
        /// Снимается
        /// </summary>
        Canceling,
        /// <summary>
        /// Отменена
        /// </summary>
        Stopped,
        /// <summary>
        /// Исполнена
        /// </summary>
        Completed,

        /// <summary>
        /// Снята
        /// </summary>
        Canceled
    }
}

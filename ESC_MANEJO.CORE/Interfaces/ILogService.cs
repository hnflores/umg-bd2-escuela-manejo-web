using ESC_MANEJO.CORE.Enumerations;

namespace ESC_MANEJO.CORE.Interfaces
{
    public interface ILogService
    {
        void SaveLogApp(string message, LogType logType);
    }
}

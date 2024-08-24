namespace ThirdPersonView.Harmony
{
    public class Logger : ILogger
    {
        public void Info(string message)
        {
#if DEBUG
            Log.Out("[ThirdPersonView: " + message);
#endif
        }

        public void Warn(string message)
        {
#if DEBUG
            Log.Warning("[ThirdPersonView " + message);
#endif
        }

        public void Warning(string message)
        {
#if DEBUG
            Log.Warning("[ThirdPersonView " + message);
#endif
        }

        public void Error(string message)
        {
#if DEBUG
            Log.Error("[ThirdPersonView" + message);
#endif
        }
    }

    public interface ILogger
    {
        void Info(string message);
        void Warn(string message);
        void Warning(string message);
        void Error(string message);
    }
}
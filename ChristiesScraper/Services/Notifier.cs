namespace ChristiesScraper.Services
{
    public class Notifier
    {
        public static EventHandler<string> OnLog { get; set; }
        public static EventHandler<string> OnError { get; set; }
        public static EventHandler<string> OnDisplay { get; set; }
        public static EventHandler<(int x, int total)> OnProgress { get; set; }


        public static void Display(string s, bool log = true)
        {
            OnDisplay?.Invoke(null, s);
            if (!s.Contains(" / ") && log)
                OnLog?.Invoke(null, s);
        }

        public static void Error(string s)
        {
            OnError?.Invoke(null, s);
        }

        public static void Progress(int x, int total)
        {
            OnProgress?.Invoke(null, (x, total));
        }
        public static void Log(string s)
        {
            OnLog?.Invoke(null, s);
        }
    }
}
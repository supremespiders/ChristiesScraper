using ChristiesScraper.Models;
using ChristiesScraper.Services;

namespace ChristiesScraper.Extensions
{
    public static class MultitaskingExtensions
    {
        private static async Task<List<T2>> P<T, T2>(this IReadOnlyList<T> inputs, int threads, Func<T, Task<T2>> work)
        {
            var outputs = new List<T2>();
            var isString = inputs.First() is string;
            Notifier.Display("Start working");
            var tasks = new List<Task<T2>>();
            var taskUrls = new Dictionary<int, string>();
            var completedUrls = new List<string>();
            if (File.Exists("completed"))
            {
                completedUrls = (await File.ReadAllLinesAsync("completed")).ToList();
                inputs = inputs.Where(x => !completedUrls.Contains(x as string)).ToList();
            }

            if (File.Exists("output"))
            {
                "output".Load<T2>();
            }
            
            var i = 0;
            do
            {
                if (i < inputs.Count)
                {
                    var item = inputs[i];
                    Notifier.Display($"Working on {i + 1} / {inputs.Count} , Total collected : {outputs.Count}");
                   Console.WriteLine($"Working on {i + 1} / {inputs.Count} , Total collected : {outputs.Count}");
                    var t = work(item);
                    tasks.Add(t);
                    if (item is string url)
                        taskUrls.Add(t.Id, url);
                    i++;
                }

                if (tasks.Count != threads && i < inputs.Count) continue;
                try
                {
                    var t = await Task.WhenAny(tasks).ConfigureAwait(false);
                    completedUrls.Add(taskUrls[t.Id]);
                    if (completedUrls.Count % 100 == 0)
                    {
                        await File.WriteAllLinesAsync("completed",completedUrls);
                        outputs.Save("output");
                    }
                    tasks.Remove(t);
                    outputs.Add(await t);
                }
                catch (TaskCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Notifier.Error($"{(e is KnownException ? e.Message : e.ToString())}");
                  Console.WriteLine($"{(e is KnownException ? e.Message : e.ToString())}");
                    var t = tasks.FirstOrDefault(x => x.IsFaulted);
                    tasks.Remove(t);
                }

                if (tasks.Count == 0 && i == inputs.Count) break;
            } while (true);

            await File.WriteAllLinesAsync("completed",completedUrls);
            outputs.Save("output");
            Notifier.Display($"Work completed, collected : {outputs.Count}");
            return outputs;
        }

        public static async Task<List<T2>> Parallel<T, T2>(this IReadOnlyList<T> inputs, Func<T, Task<T2>> work, int threads, bool resume = true)
        {
            var outputs = await P(inputs, threads, work);
            return outputs;
        }

        public static async Task<List<T2>> Parallel<T, T2>(this IReadOnlyList<T> inputs, Func<T, Task<List<T2>>> work, int threads, bool resume = true)
        {
            var outputs = await P(inputs, threads, work).Reduce();
            return outputs;
        }

        public static async Task<List<T>> Reduce<T>(this Task<List<List<T>>> l)
        {
            return (await l).SelectMany(x => x).ToList();
        }
    }
}
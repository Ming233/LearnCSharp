using System;
using System.Net;

namespace CSharpFunddamental2
{
    public static class DownloadContectWithFunc
    {
        private static void Downloads()
        {
            var client = new WebClient();
            Func<string, string> download = url => client.DownloadString(url);
            Func<string, Func<string>> downloadCurry = download.Curry();

            var data = download.Partial("http://microsoft.com").WithRetry();
            var data2 = downloadCurry("http://microsoft.com").WithRetry();
        }

        public static Func<TResult> Partial<TParam1, TResult>(
            this Func<TParam1, TResult> func, TParam1 parameter)
        {
            return () => func(parameter);
        }

        public static Func<TParam1, Func<TResult>> Curry<TParam1, TResult>
            (this Func<TParam1, TResult> func)
        {
            return parameter => () => func(parameter);
        }

        public static T WithRetry<T>(this Func<T> action)
        {
            var result = default(T);
            int retryCount = 0;

            bool succesful = false;
            do
            {
                try
                {
                    result = action();
                    succesful = true;
                }
                catch (WebException ex)
                {
                    retryCount++;
                }
            } while (retryCount < 3 && !succesful);

            return result;
        }


    }
}

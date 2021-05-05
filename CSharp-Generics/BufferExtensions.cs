using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp_Generics
{
    public static class BufferExtensions
    {
        public static void Dump<T>(this IBuffer<T> buffer, Action<T> print)
        {
            foreach (var item in buffer)
            {
                print(item);
            }
        }


        //This is showing how to convert from As Enumerable to Map.
        //public static IEnumerable<TOutput> AsEnumerableOf<t, TOutput>(
        //    this IBuffer<T> buffer)
        //{
        //    var converter = TypeDescriptor.GetConverter(typeof(T));
        //    foreach (var item in _queue)
        //    {
        //        var result = converter.ConvertTo(item, typeof(TOutput));
        //        yield return (TOutput)result;
        //    }
        //}

        public static IEnumerable<TOutput> Map<T, TOutput>(
            this IBuffer<T> buffer, Converter<T, TOutput> converter)
        {
            return buffer.Select(i => converter(i));
        }
    }
}

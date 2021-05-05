using System;

namespace CSharp_Generics
{
    class Part3_Datastructures
    {
        public static void TryDataStructureMain()
        {
            var buffer = new Buffer<double>();


            ProcessInput(buffer);

            foreach (var item in buffer)
            {
                Console.WriteLine(item);
            }

            ProcessBuffer(buffer);
        }

        private static void ProcessBuffer(IBuffer<double> buffer)
        {
            var sum = 0.0;
            Console.WriteLine("Buffer: ");
            while (!buffer.IsEmpty)
            {
                sum += buffer.Read();
            }
            Console.WriteLine(sum);
        }

        private static void ProcessInput(IBuffer<double> buffer)
        {
            while (true)
            {
                var value = 0.0;
                var input = Console.ReadLine();

                if (double.TryParse(input, out value))
                {
                    buffer.Write(value);
                    continue;
                }
                break;
            }
        }
    }



    /// <summary>
    /// This is file that hold interface and its methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //public interface IBuffer<T> : IEnumerable<T>
    //{
    //    bool IsEmpty { get; }
    //    void Write(T value);
    //    T Read();
    //}

    //public class Buffer<T> : IBuffer<T>
    //{
    //    protected Queue<T> _queue = new Queue<T>();

    //    public virtual bool IsEmpty
    //    {
    //        get { return _queue.Count == 0; }
    //    }

    //    public virtual void Write(T value)
    //    {
    //        _queue.Enqueue(value);
    //    }

    //    public virtual T Read()
    //    {
    //        return _queue.Dequeue();
    //    }

    //    public IEnumerator<T> GetEnumerator()
    //    {
    //        foreach (var item in _queue)
    //        {
    //            // ...
    //            yield return item;
    //        }
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }
    //}

    public class Part3_CircularBuffer<T> : Buffer<T>
    {
        int _capacity;
        public Part3_CircularBuffer(int capacity = 10)
        {
            _capacity = capacity;
        }

        public override void Write(T value)
        {
            base.Write(value);
            if (_queue.Count > _capacity)
            {
                _queue.Dequeue();
            }
        }

        public bool IsFull { get { return _queue.Count == _capacity; } }

    }

}

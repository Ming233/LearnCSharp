namespace TestNewFeature
{

    public delegate void Printer(object message);
    public static class ParticeDelegate
    {
        public static void tryDelegate(string messageinput, Printer print)
        {
            print(messageinput);
        }
    }
}

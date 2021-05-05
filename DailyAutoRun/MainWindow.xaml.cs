using System.Runtime.InteropServices;
using System.Windows;

namespace DailyAutoRun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //show console app
        [DllImport("Kernel32")]
        public static extern void AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();
        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            //var helloWorld = new HelloWorld.Program();
            //AllocConsole();
            //helloWorld.SayHello();
            //FreeConsole();
        }

        private void HelloWorld_Click(object sender, RoutedEventArgs e)
        {
            //This one need NameSpace and add reference
            var helloWorld = new HelloWorld.Program();
            AllocConsole();
            helloWorld.SayHello();
            FreeConsole();

            //THis is not working due to static
            //var helloWorld = new Program();
            //helloWorld.Main(null);

            //This load assemably cannot work with NameSpace
            //Type helloWorldType = Assembly.Load("HelloWorld").GetType("Program");
            //dynamic sayHello = Activator.CreateInstance(helloWorldType);
            //AllocConsole();
            //sayHello.SayHello();
            //FreeConsole();
        }

        private void GradeBook_Click(object sender, RoutedEventArgs e)
        {
            var grades = new Grades.Program();
            AllocConsole();
            //grades.Main();
            FreeConsole();
        }



    }
}

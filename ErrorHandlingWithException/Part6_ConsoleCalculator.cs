using System;
using static System.Console;

namespace ErrorHandlingWithException
{
    class Part6_ConsoleCalculator
    {
        public static void Part6_ConsoleCalculatorMain()
        {
            AppDomain currentAppDomain = AppDomain.CurrentDomain;
            currentAppDomain.UnhandledException
                += new UnhandledExceptionEventHandler(HandleException);

            WriteLine("Enter first number");
            int number1 = int.Parse(ReadLine());

            WriteLine("Enter second number");
            int number2 = int.Parse(ReadLine());

            WriteLine("Enter operation");
            string operation = ReadLine().ToUpperInvariant();


            var calculator = new Part6_Calculator();

            try
            {
                int result = calculator.Calculate(number1, number2, operation);
                DisplayResult(result);
            }
            catch (Part6_CalculationException ex)
            {
                // Log.Error(ex);
                WriteLine(ex);
            }
            catch (Exception ex)
            {
                WriteLine($"Sorry, something went wrong. {ex}");
            }
            finally
            {
                WriteLine("...finally...");
            }




            WriteLine("\nPress enter to exit.");
            ReadLine();
        }

        private static void HandleException(object sender, UnhandledExceptionEventArgs e)
        {
            WriteLine($"Sorry there was a problem and we need to close. Details: {e.ExceptionObject}");
        }

        private static void DisplayResult(int result)
        {
            WriteLine($"Result is: {result}");
        }
    }

    class Part6_CalculationException : Exception
    {
        private static readonly string DefaultMessage = "An error occurred during calculation. Ensure that the operator is supported and that the values are within the valid ranges for the requested operation.";

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationException"/> with a predefined message.
        /// </summary>
        public Part6_CalculationException() : base(DefaultMessage)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationException"/> with a user-supplied message.
        /// </summary>
        public Part6_CalculationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationException"/> with a predefined message and a wrapped inner exception.
        /// </summary>
        public Part6_CalculationException(Exception innerException)
            : base(DefaultMessage, innerException)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationException"/> with a user-supplied message and a wrapped inner exception.
        /// </summary>
        public Part6_CalculationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

    class Part6_CalculationOperationNotSupportedException : Part6_CalculationException
    {
        public string Operation { get; }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationOperationNotSupportedException"/> with a predefined message.
        /// </summary>
        public Part6_CalculationOperationNotSupportedException()
            : base("Specified operation was out of the range of valid values.")
        {
        }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationOperationNotSupportedException"/> with the operation that is not supported.
        /// </summary>
        public Part6_CalculationOperationNotSupportedException(string operation)
            : this()
        {
            Operation = operation;
        }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationOperationNotSupportedException"/> with a user-supplied message and a wrapped inner exception.
        /// </summary>
        public Part6_CalculationOperationNotSupportedException(string message,
            Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Creates a new <see cref="Part6_CalculationOperationNotSupportedException"/> with the operation that is not supported and a user-supplied message.
        /// </summary>
        public Part6_CalculationOperationNotSupportedException(string operation, string message)
            : base(message)
        {
            Operation = operation;
        }

        public override string Message
        {
            get
            {
                string message = base.Message;

                if (Operation != null)
                {
                    return message + Environment.NewLine +
                        $"Unsupported operation: {Operation}";
                }

                return message;
            }
        }
    }

    public class Part6_Calculator
    {
        public int Calculate(int number1, int number2, string operation)
        {
            string nonNullOperation =
                operation ?? throw new ArgumentNullException(nameof(operation));

            if (nonNullOperation == "/")
            {
                try
                {
                    return Divide(number1, number2);
                }
                catch (ArithmeticException ex)
                {
                    throw new Part6_CalculationException("An error occurred during division", ex);
                }
            }
            else
            {
                throw new Part6_CalculationOperationNotSupportedException(operation);
            }
        }

        private int Divide(int number, int divisor)
        {
            return number / divisor;
        }
    }
}

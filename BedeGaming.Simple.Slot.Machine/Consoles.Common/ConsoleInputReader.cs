using Consoles.Common.Interfaces;
using System.ComponentModel;

namespace Consoles.Common
{
    public class ConsoleInputReader : IConsoleInputReader
    {
        private readonly string _invalidInput = "Invalid input. Please enter a valid value of the specified type.";

        public T ReadValidInput<T>(string message) where T : struct
        {
            T result;
            bool isValidInput;

            do
            {
                Console.Write(message);
                string input = Console.ReadLine();
                isValidInput = TryParseInput(input, out result);

                if (!isValidInput)
                {
                    Console.WriteLine(_invalidInput);
                }

            } while (!isValidInput);

            return result;
        }

        private bool TryParseInput<T>(string input, out T result) where T : struct
        {
            if (typeof(T).IsEnum)
            {
                return Enum.TryParse(input, out result);
            }
            else
            {
                return TryParseNumericInput(input, out result);
            }
        }

        private bool TryParseNumericInput<T>(string input, out T result) where T : struct
        {
            if (TypeDescriptor.GetConverter(typeof(T)).IsValid(input) && TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(input) is T parsedValue)
            {
                result = parsedValue;
                return true;
            }

            result = default;
            return false;
        }
    }


}

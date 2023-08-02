using System;

namespace Consoles.Common.Interfaces
{
    public interface IConsoleInputReader
    {
        /// <summary>
        /// <para>How to use:</para>
        /// <para>int number = inputReader.ReadValidInput<int>("Please enter your correct number: ")</para>;
        /// <para>decimal foaltingNumber = inputReader.ReadValidInput<decimal>("Please enter your correct floating point number: ")</para>;
        /// <para>DateTime dateOfBirth = inputReader.ReadValidInput<DateTime>("Please enter your date of birth (YYYY-MM-DD): ")</para>;
        /// <para>MyEnum customEnum = inputReader.ReadValidInput<MyEnum>("Please enter a value for the custom enum: ")</para>;
        /// </summary>
        /// <typeparam name="T">Can be int, decimal, DateTime, enum</typeparam>
        /// <param name="message">Prompt message<param>
        /// <returns>Struct type</returns>
        public T ReadValidInput<T>(string message) where T : struct;
    }
}

﻿using System;
using System.Collections.Generic;

namespace Calculator
{
    class Program
    {
        static List<string> history = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в консольный калькулятор!");

            while (true)
            {
                Console.Write("Введите выражение (или 'exit' для выхода): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit") break;

                try
                {
                    double result = EvaluateExpression(input);
                    Console.WriteLine($"Результат: {result}");
                    history.Add($"{input} = {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            Console.WriteLine("История вычислений:");
            foreach (string item in history)
            {
                Console.WriteLine(item);
            }

            static void ExportHistoryToTxt(string filePath)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string item in history)
                    {
                        writer.WriteLine(item);
                    }
                }
                Console.WriteLine($"История вычислений экспортирована в файл: {filePath}");

            }
            Console.Write("Экспортировать историю в TXT? (y/n): ");
            string exportChoice = Console.ReadLine();
            if (exportChoice.ToLower() == "y")
            {
                Console.Write("Введите имя файла: ");
                string fileName = Console.ReadLine();
                ExportHistoryToTxt(fileName + ".txt");
            }
        }


        // Базовая функция вычисления (будет расширена в ветке 'operations')
        static double EvaluateExpression(string expression)
        {
            // Добавляем поддержку -, *, /
            expression = expression.Replace(" ", ""); // Убираем пробелы

            // Простая реализация без приоритета операций (для упрощения)
            string[] parts = expression.Split('+', '-', '*', '/');
            double result = double.Parse(parts[0]);
            for (int i = 1; i < parts.Length; i++)
            {
                char op = expression[expression.IndexOf(parts[i - 1]) + parts[i - 1].Length];
                switch (op)
                {
                    case '+': result += double.Parse(parts[i]); break;
                    case '-': result -= double.Parse(parts[i]); break;
                    case '*': result *= double.Parse(parts[i]); break;
                    case '/': result /= double.Parse(parts[i]); break;
                }
            }
            return result;
        }
    }
}


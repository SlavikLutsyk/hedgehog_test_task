using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hedgehog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] input = new int[3];
            int wishedColour;
            EnterData(input, out wishedColour);
            int res = CalcMinCountMeetings(input, wishedColour);
            Console.WriteLine($"Minimum number of meetings: {(res == -1 ? "unreal" : res.ToString())}.");
        }
        static void EnterData(int[] input, out int wishedColour)
        {
            int[] parsedArr = EnterArray();
            Array.Copy(parsedArr, input, 3);
            wishedColour = EnterDesiredColour();
        }
        static int[] EnterArray()
        {
            int[] parsedInput = new int[3];
            bool isValidInput = true;
            do
            {
                Console.WriteLine("Enter 3 values separated by commas (e.g., 0,3,7), values must be in range [0, 2^31-1]: ");
                string arrayHedgehogs = Console.ReadLine();
                string[] parts = arrayHedgehogs.Split(',');
                if (isValidInput = (parts.Length == 3))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!int.TryParse(parts[i], out parsedInput[i]) || parsedInput[i] < 0)
                        {
                            isValidInput = false;
                            break;
                        }
                    }
                }
                if (!isValidInput)
                    Console.WriteLine("Error.");
            } while (!isValidInput);
            return parsedInput;
        }
        static int EnterDesiredColour()
        {
            int wishedColour = -1;
            bool isValidInput = false;
            do
            {
                Console.WriteLine("Enter desired colour in range [0, 2]: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out wishedColour) && wishedColour >= 0 && wishedColour <= 2)
                    isValidInput = true;
                else
                    Console.WriteLine("Error.");
            } while (!isValidInput);
            return wishedColour;
        }
        static int CalcMinCountMeetings(int[] input, int wishedColour)
        {
            int sum = input[0] + input[1] + input[2], minCountMeetings = 0;
            if (input[wishedColour] == sum) return 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == sum) return -1;
            }

            
            (int firstGroupIndex, int secondGroupIndex) = DefineGroups(input, wishedColour);

            if(input[firstGroupIndex] == input[secondGroupIndex]) return input[firstGroupIndex];
            //Якщо кількість їжачків що лишилася після занулення кількості їжачків певного небажаного кольору ділиться на 3
            //то досягненя мети є можливим, в іншому випадку ні
            if ((input[firstGroupIndex] - input[secondGroupIndex]) % 3 != 0)
                return -1;
            //одна зустріч це зустріч їжачка бажаного кольору і того що лишилося, вони перефарбовуються в дефіцитний колір
            //потім ці ж два їжаки зустрічаються з тими їжаками що лишилися небажаного кольору
            return Math.Max(input[firstGroupIndex], input[secondGroupIndex]);
        } 
        static (int, int) DefineGroups(int[] input, int wishedColour)
        {
            int firstGroupIndex = -1, secondGroupIndex = -1;
            for (int i = 0; i < input.Length; i++)
            {
                if (i != wishedColour)
                {
                    if (firstGroupIndex == -1)
                    {
                        firstGroupIndex = i;
                    }
                    else
                    {
                        secondGroupIndex = i; break;
                    }
                }
            }
            return (firstGroupIndex, secondGroupIndex);
        }
    }
}
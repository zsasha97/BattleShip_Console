using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_Console
{
    class Paint
    {
        public static readonly string[] str1 = { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "к" };
        public static readonly string[] str2 = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public void Draw(int[,] BotField, int[,] UserField, int size)
        {
            Console.Clear();
            for (int i = 0; i < size; i++)
            {
                Console.SetCursorPosition(2 * i + 3, 0);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < size; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 1);
                Console.Write("|");
                for (int j = 0; j < size; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 1);
                    DrawUserField(UserField[i, j]);                                 //отдаем поле в метод для отрисовки
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(2 * i + 3, 13);
                Console.Write(str1[i]);
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(0, i + 14);
                Console.Write(str2[i]);
                Console.SetCursorPosition(2, i + 14);
                Console.Write("| ");
                for (int j = 0; j < 10; j++)
                {
                    Console.SetCursorPosition(2 * j + 3, i + 14);
                    DrawBotField(BotField[i, j]);                                   //отдаем поле в метод для отрисовки     
                }
            } 
        }
        public void DrawUserField(int a)
        {
            switch (a)
            {
                case 0:                                                             //пустая клетка
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(".");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 1:                                                             //клетка с кораблем
                    Console.Write("■");
                    break;
                case 2:                                                             //клетка, в которую уже стреляли
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("O"); 
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 3:                                                             //сбитая палуба
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        public void DrawBotField(int a)
        {
            switch (a)
            {
                case 0:
                case 1:                                                                           // чит мод выкл.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(".");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                /*case 1:                                                                         // чит мод вкл.
                    Console.Write("■");
                    break;*/
                case 2:                                                                         
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("O");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_Console
{
    class Program
    {
        const int size = 10;                                             //размер полей, ну просто потому, что могу, почему бы и нет?
        public static int[,] BotField = new int[size, size];             //поле бота 0 - пустая клетка, 1 - палуба, 2 - клетка по которой стреляли, 3 - сбитая палуба
        public static int[,] UserField = new int[size, size];            //поле пользователя

        static void Main(string[] args)
        {
            bool Victory = false;                                                                           
            int win_counter_Bot = 0;                                     //считает количество палуб, которые сбил бот
            int win_counter_User = 0;

            Player bot = new Player();
            Player user = new Player();
            bot.Set_Position(BotField, size);                            //рандомно заполняем поле бота кораблями
            user.Set_Position(UserField, size);                          //рандомно заполняем поле пользователя кораблями

            Paint field = new Paint();
            field.Draw(BotField, UserField, size);                       //отрисовываем в консоль

            Battle shoot = new Battle();
            while (Victory == false)
            {  //зачем передавать два поля если для выстрела нужно одно? чтобы потом это было удобно отрисовать
                win_counter_User = shoot.User_Shoot(BotField, UserField, size, win_counter_User);           //здесь могла быть Ваша реклама
                win_counter_Bot = shoot.Bot_Shoot(BotField, UserField, size, win_counter_Bot);              //это вообще легально?
                field.Draw(BotField, UserField, size);
                if (win_counter_User == 20)                                                                
                {
                    Console.Clear();
                    Console.SetCursorPosition(15,15);
                    Console.Write("Какая вопиющая неожиданность! Вы победили! Сегодня удача явно на Вашей стороне.");
                    Console.ReadKey();
                    break;
                }
                else if (win_counter_Bot == 20)
                {
                    Console.Clear();
                    Console.SetCursorPosition(40, 13);
                    Console.Write("Вы проиграли! Повезет в другой раз!");
                    Console.ReadKey();
                    break;
                }
            }                               
        }
    }
}

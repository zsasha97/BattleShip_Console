using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_Console
{
    class Battle
    {
        static bool finish = true;                                  //если корабль не нужно добивать
        static bool some_trouble = false;                           //чтобы обработать тот случай когда бот точно должен знать где находится оставшаяся часть корабля, но не знает. если бот начал добивать корабль в одну сторону, но в конечном итоге ннаткнулся на 0, а не на 2 или конец массива
        static bool Bot_shot = false;
        static int shoot_i;                                         //координаты клетки, по котрой должен стрельнуть бот
        static int shoot_j;
        static Random rand = new Random();                          //АААААААААААААААА, ЧТО ЭТО? ОНО МЕНЯ ПРЕСЛЕДУЕТ! КАК ЭТО ПОНИМАТЬ? ААААААААААААААААА 

        public int User_Shoot(int[,] BotField, int[,] UserField, int size, int win_counter_User)
        {
            Paint field = new Paint();
            int i = 0;
            int j = 0;
            int step = 0;                                                             //отслеживает номер строки в которую нужно выводить сообщения пользователю
            bool miss = false;                                                        //отслеживает промах                                     

            Bot_shot = false;
            while (miss == false)                                                    //до тех пор пока не промахнулся
            {
                Console.SetCursorPosition(30, step);
                Console.WriteLine("Введите номер ячейки:");
                step++;
                Console.SetCursorPosition(30, step);
                step++;
                string position = Console.ReadLine();                                  //читаем то, что ввел пользователь(не самая лучшая идея, полагаю, т.к. пользователь может оказаться обезьяной и вводить откровенный бред, но что уж тут поделать)
                position.ToLower();                                                    //сделаем нижний регистр, чтобы было проще обработать

                    miss = true;                                                       //допустим пользователь промахнулся
                    try
                    {
                        switch (position[0])                                           //рассматриваем первый введенный символ
                        {
                            case 'а':
                                j = 0;
                                break;
                            case 'б':
                                j = 1;
                                break;
                            case 'в':
                                j = 2;
                                break;
                            case 'г':
                                j = 3;
                                break;
                            case 'д':
                                j = 4;
                                break;
                            case 'е':
                                j = 5;
                                break;
                            case 'ж':
                                j = 6;
                                break;
                            case 'з':
                                j = 7;
                                break;
                            case 'и':
                                j = 8;
                                break;
                            case 'к':
                                j = 9;
                                break;
                            default:
                                Console.SetCursorPosition(30, step);
                                Console.WriteLine("Введите верные координаты");
                                step++;
                                miss = false;                                           //обезьяна не может промазать если она не может выстрелить
                                continue;
                        }
                        if (position.Length == 2)
                        {
                            i = Convert.ToInt32(Convert.ToString(position[1])) - 1;     //честно говоря я не понимаю зачем и почему Convert.ToInt32(Char) выдает вместо числа код символа(из таблицы юникод, полагаю), но из-за этого пришлось вот так вот извращаться(на месте процессора я подумал бы, что тот кто это писал дебил, но был ли у меня выбор?)
                        }
                        else if (position.Length == 3 && Convert.ToInt32(Convert.ToString(position[1])) == 1 && Convert.ToInt32(Convert.ToString(position[2])) == 0)    //нет, ну серьезно, кто здесь еще дебил? я или тот кто придумал эту чудную схему возвращать из char код?
                        {
                            i = 9;
                        }
                        else
                        {
                            Console.SetCursorPosition(30, step);
                            Console.WriteLine("Введите верные координаты");
                            step++;
                            miss = false;                                             
                        continue;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.SetCursorPosition(30, step);
                        Console.WriteLine("Введите верные координаты");
                        step++;
                        miss = false;
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.SetCursorPosition(30, step);
                        Console.WriteLine("Введите верные координаты");
                        step++;
                        miss = false;
                        continue;
                    }
                    if (i < 0 || i > 9)
                    {
                        Console.SetCursorPosition(30, step);
                        Console.WriteLine("Введите верные координаты");
                        step++;
                        miss = false;
                        continue;
                    }
                    switch (BotField[i, j])
                    {
                        case 2:
                        case 3:
                            Console.SetCursorPosition(30, step);
                            Console.WriteLine("Сюда стрелять нельзя");
                            step++;
                            miss = false;
                            continue;
                        case 0:
                            BotField[i, j] = 2;
                            Console.SetCursorPosition(30, 0);
                            Console.WriteLine("Промах!");
                            step++;
                            break;
                        case 1:
                            BotField[i, j] = 3;
                            win_counter_User++;
                            if (win_counter_User == 20)
                            {
                                return win_counter_User;
                            }
                            Crush(BotField, i, j);
                            field.Draw(BotField, UserField, size);
                            Console.SetCursorPosition(30, 0);
                            Console.WriteLine("Попадание!");
                            Console.Beep();
                            step = 1;
                            miss = false;
                            break;
                    }
            }
            return win_counter_User;
        }

        public int Bot_Shoot(int[,] BotField, int[,] UserField, int size, int win_counter_Bot)
        {
            int side;
            bool miss = false;
            Paint field = new Paint();

            Bot_shot = true;
            while (miss != true)                                                                                                   //пока не промазал
            {

                if (finish == false)                                                  //если есть корабль который мы недобили(процедура определния значения этой переменной находится в методе Crush)
                {
                    if (some_trouble == true)                                         //если бот должен точно знать где находятся недобитые палубы
                    {
                        if (shoot_j - 1 >= 0 && UserField[shoot_i, shoot_j - 1] == 1)                       //если есть палуба западнее точки в которую стреляли
                        { miss = Kill_West(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                            continue;
                        }      
                        if (shoot_i - 1 >= 0 && UserField[shoot_i - 1, shoot_j] == 1)                       //севернее(с той стороны где растет мох)
                        { miss = Kill_North(UserField, shoot_i, shoot_j, ref win_counter_Bot);               
                            continue;
                        }    
                        if (shoot_j + 1 < 10 && UserField[shoot_i, shoot_j + 1] == 1)                       //восточнее
                        { miss = Kill_East(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                            continue;
                        }     
                        if (shoot_i + 1 < 10 && UserField[shoot_i + 1, shoot_j] == 1)                       //южнее
                        { miss = Kill_South(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                            continue;
                        }     
                    }
                    else
                    {
                        side = rand.Next(4);                                          //если бот не должен знать в какой стороне находятся недобитые палубы
                        switch (side)
                        {
                            case 0:
                                if (shoot_j - 1 >= 0 && UserField[shoot_i, shoot_j - 1] != 2 && UserField[shoot_i, shoot_j - 1] != 3)            //если в эту клетку стрелять нельзя
                                {
                                    miss = Kill_West(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                                }
                                continue;
                            case 1:
                                if (shoot_i - 1 >= 0 && UserField[shoot_i - 1, shoot_j] != 2 && UserField[shoot_i - 1, shoot_j] != 3)
                                {
                                    miss = Kill_North(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                                }
                                continue;
                            case 2:
                                if (shoot_j + 1 < 10 && UserField[shoot_i, shoot_j + 1] != 2 && UserField[shoot_i, shoot_j + 1] != 3)
                                {
                                    miss = Kill_East(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                                }
                                continue;
                            case 3:
                                if (shoot_i + 1 < 10 && UserField[shoot_i + 1, shoot_j] != 2 && UserField[shoot_i + 1, shoot_j] != 3)
                                {
                                    miss = Kill_South(UserField, shoot_i, shoot_j, ref win_counter_Bot);
                                }
                                continue;
                        }
                    }
                }
                else                                                                //если добивать никого не нужно
                {
                    if (win_counter_Bot == 20)
                    {
                        Console.Clear();
                        Console.SetCursorPosition(40, 13);
                        Console.Write("Вы проиграли! Повезет в другой раз!");
                        Console.ReadKey();
                        return win_counter_Bot;
                    }
                    shoot_i = rand.Next(size);
                    shoot_j = rand.Next(size);

                    switch (UserField[shoot_i, shoot_j])    
                    {
                        case 0:                                                     //попал в пустую клетку
                            UserField[shoot_i, shoot_j] = 2;
                            miss = true;
                            break;
                        case 1:                                                     //попал в корабль
                            UserField[shoot_i, shoot_j] = 3;
                            win_counter_Bot++;                                      //засчитаем палубу
                            if (win_counter_Bot == 20)                              //победа бота
                            {
                                return win_counter_Bot;
                            }
                            finish = false;                                         //корабль нужно добить
                            Crush(UserField, shoot_i, shoot_j);                     //проверим не убит ли этот корабль(возможно однопалубный)
                            field.Draw(BotField, UserField, size);                  //отрисуем поле
                            Console.Beep();                                         //бип
                            break;
                    }
                }
            }
            return win_counter_Bot;
        }


        private void Crush(int[,] Field, int curr_i, int curr_j)
        {
            int length = 1;                                        //длина корабля
            int y = curr_i;
            int x = curr_j;

            for (int i = curr_i - 1; i >= curr_i - 3; i--)          //идем на 3 клетки вверх(максимум даже когда корабль 4-х палубный)
            {
                try
                {
                    if (Field[i, curr_j] == 3)                      //считаем длину
                    {
                        length++;
                        y--;                                        //найдем начало корабля
                    }
                    else if (Field[i, curr_j] == 1)                 //если находим не сбитую палубу
                    {
                        return;                                     //до свидания
                    }
                    else if (Field[i, curr_j] == 0 || Field[i, curr_j] == 2)            //если уткнулись в пустую клетку
                    {
                        break;                                                         
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            for (int i = curr_i + 1; i <= curr_i + 3; i++)             //идем на 3 клетки вниз
            {
                try
                {
                    if (Field[i, curr_j] == 3)
                    {
                        length++;                                       //продолжаем считать длину
                    }
                    else if (Field[i, curr_j] == 1)
                    {
                        return;
                    }
                    else if (Field[i, curr_j] == 0 || Field[i, curr_j] == 2)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            if (length > 1)                                               //если корабль длинный(очень, от 2 до 4)
            {
                if (Bot_shot == true)
                {
                    finish = true;                                        //добивать не нужно, можно расслабиться
                    some_trouble = false;                                 //проблем тоже не возникнет
                }                                  
                for (int i = y - 1; i <= y + length && i < 10; i++)       //от начала корабля - 1 и до клетки за кораблем
                {
                    if (i < 0)                              
                    {
                        continue;
                    }
                    for (int j = curr_j - 1; j <= curr_j + 1 && j < 10; j++)
                    {
                        if (j < 0)
                        {
                            continue;
                        }
                        if (Field[i, j] != 3 && Field[i, j] != 2)
                        {
                            Field[i, j] = 2;                                //зарисовываем поле куда стрелять уже не нужно
                        }
                    }
                }
                return;
            }
            for (int j = curr_j - 1; j >= curr_j - 3; j--)                   //аналогично предыдущему алгоритму только для другой ориентации корабля
            {
                try
                {
                    if (Field[curr_i, j] == 3)
                    {
                        length++;
                        x--;
                    }
                    else if (Field[curr_i, j] == 1)
                    {
                        return;
                    }
                    else if (Field[curr_i, j] == 0 || Field[curr_i, j] == 2)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            for (int j = curr_j + 1; j <= curr_j + 3; j++)
            {
                try
                {
                    if (Field[curr_i, j] == 3)
                    {
                        length++;
                    }
                    else if (Field[curr_i, j] == 1)
                    {
                        return;
                    }
                    else if (Field[curr_i, j] == 0 || Field[curr_i, j] == 2)
                    {
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    break;
                }
            }
            if (length > 1)
            {
                if (Bot_shot == true)
                {
                    finish = true;
                    some_trouble = false;
                }
                for (int i = curr_i - 1; i <= curr_i + 1 && i < 10; i++)
                {
                    if (i < 0)
                    {
                        continue;
                    }
                    for (int j = x - 1; j <= x + length && j < 10; j++)
                    {
                        if (j < 0)
                        {
                            continue;
                        }
                        if (Field[i, j] != 3 && Field[i, j] != 2)
                        {
                            Field[i, j] = 2;
                        }
                    }
                }
            }
            if (length == 1)                                    //если по итогу корабль однопалубный
            {
                if (Bot_shot == true)
                {
                    finish = true;
                }
                for (int i = curr_i - 1; i <= curr_i + 1 && i < 10; i++)
                {
                    if (i < 0)
                    {
                        continue;
                    }
                    for (int j = curr_j - 1; j <= curr_j + 1 && j < 10; j++)
                    {
                        if (j < 0)
                        {
                            continue;
                        }
                        if (Field[i, j] != 3 && Field[i, j] != 2)
                        {
                            Field[i, j] = 2;
                        }
                    }
                }
            }
        }
        private bool Kill_West(int[,] UserField, int curr_i, int curr_j, ref int win_counter_Bot)
        {
            bool miss = false;
            switch (UserField[curr_i, curr_j - 1])                          //стреляем на запад
            {
                case 0:                                                     //обидно
                    miss = true;                    
                    UserField[curr_i, curr_j - 1] = 2;
                    break;
                case 1:
                    curr_j--;
                    UserField[curr_i, curr_j] = 3;
                    win_counter_Bot++;
                    Console.Beep();
                    Crush(UserField, curr_i, curr_j);                       //корабль убит?
                    if (finish == false && curr_j - 1 >= 0)                 //если не убит и за границы массива не выйдем
                    {
                        if (UserField[curr_i, curr_j - 1] == 0) some_trouble = true;              //если бот не знает что находится в следующей клетке, то у него проблемы
                        miss = Kill_West(UserField, curr_i, curr_j, ref win_counter_Bot);                    //рекурсия ееееееее
                    }
                    else if ((finish == false) && (curr_j - 1 < 0 || UserField[curr_i, curr_j - 1] == 2))              //если не убит, но в заданом направлении двигаться больше не можем
                    {
                        while ((curr_j + 1 < 10) && (UserField[curr_i, curr_j + 1] != 0 || UserField[curr_i, curr_j + 1] != 2))       //пока не дошли до другого конца корабля
                        {
                            curr_j++;                                                //можно было вызвать тут соответствующий метод, но я подумал, что так будет проще для понимания сторонним людям
                            if (UserField[curr_i, curr_j] == 1)
                            {
                                UserField[curr_i, curr_j] = 3;
                                win_counter_Bot++;
                                Console.Beep();
                            }
                        }

                    }
                    Crush(UserField, curr_i, curr_j);                       //проверим не убили ли мы бедный кораблик
                    break;
            }
            return miss;                                                    //вдруг промахнулись пока добивали(именно в этом случае у бота проблемы)
        }
        private bool Kill_North(int[,] UserField, int curr_i, int curr_j, ref int win_counter_Bot)       //аналогичный алгоритм для северной стороны
        {
            bool miss = false;
            switch (UserField[curr_i - 1, curr_j])
            {
                case 0:
                    miss = true;
                    UserField[curr_i - 1, curr_j] = 2;
                    break;
                case 1:
                    curr_i--;
                    UserField[curr_i, curr_j] = 3;
                    win_counter_Bot++;
                    Console.Beep();
                    Crush(UserField, curr_i, curr_j);
                    if (finish == false && curr_i - 1 >= 0)
                    {
                        if (UserField[curr_i - 1, curr_j] == 0) some_trouble = true;
                        miss = Kill_North(UserField, curr_i, curr_j, ref win_counter_Bot);
                    }
                    else if ((finish == false) && (curr_i - 1 < 0 || UserField[curr_i - 1, curr_j] == 2))
                    {
                        while ((curr_i + 1 < 10) && (UserField[curr_i + 1, curr_j] != 0 || UserField[curr_i + 1, curr_j] != 2))
                        {
                            curr_i++;
                            if (UserField[curr_i, curr_j] == 1)
                            {
                                UserField[curr_i, curr_j] = 3;
                                win_counter_Bot++;
                                Console.Beep();
                            }
                        }

                    }
                    Crush(UserField, curr_i, curr_j);
                    break;
            }
            return miss;
        }
        private bool Kill_East(int[,] UserField, int curr_i, int curr_j, ref int win_counter_Bot)                        //аналогичный алгоритм для восточной стороны
        {
            bool miss = false;
            switch (UserField[curr_i, curr_j + 1])
            {
                case 0:
                    miss = true;
                    UserField[curr_i, curr_j + 1] = 2;
                    break;
                case 1:
                    curr_j++;
                    UserField[curr_i, curr_j] = 3;
                    win_counter_Bot++;
                    Console.Beep();
                    Crush(UserField, curr_i, curr_j);
                    if (finish == false && curr_j + 1 < 10)
                    {
                        if (UserField[curr_i, curr_j + 1] == 0) some_trouble = true;
                        miss = Kill_East(UserField, curr_i, curr_j, ref win_counter_Bot);
                    }
                    else if ((finish == false) && (curr_j + 1 > 9 || UserField[curr_i, curr_j + 1] == 2))
                    {
                        while ((curr_j - 1 >= 0) && (UserField[curr_i, curr_j - 1] != 0 || UserField[curr_i, curr_j - 1] != 2))
                        {
                            curr_j--;
                            if (UserField[curr_i, curr_j] == 1)
                            {
                                UserField[curr_i, curr_j] = 3;
                                win_counter_Bot++;
                                Console.Beep();
                            }
                        }

                    }
                    Crush(UserField, curr_i, curr_j);
                    break;
            }
            return miss;
        }
        private bool Kill_South(int[,] UserField, int curr_i, int curr_j, ref int win_counter_Bot)                                //аналогичный алгоритм для южной стороны
        {
            bool miss = false;
            switch (UserField[curr_i + 1, curr_j])
            {
                case 0:
                    miss = true;
                    UserField[curr_i + 1, curr_j] = 2;
                    break;
                case 1:
                    curr_i++;
                    UserField[curr_i, curr_j] = 3;
                    win_counter_Bot++;
                    Console.Beep();
                    Crush(UserField, curr_i, curr_j);
                    if (finish == false && curr_i + 1 < 10)
                    {   if (UserField[curr_i + 1, curr_j] == 0) some_trouble = true; 
                        miss = Kill_South(UserField, curr_i, curr_j, ref win_counter_Bot);
                    }
                    else if ((finish == false) && (curr_i + 1 > 9 || UserField[curr_i + 1, curr_j] == 2))
                    {
                        while ((curr_i - 1 >= 0) && (UserField[curr_i - 1, curr_j] != 0 || UserField[curr_i - 1, curr_j] != 2))
                        {
                            curr_i--;
                            if (UserField[curr_i, curr_j] == 1)
                            {
                                UserField[curr_i, curr_j] = 3;
                                win_counter_Bot++;
                                Console.Beep();
                            }
                        }

                    }
                    Crush(UserField, curr_i, curr_j);
                    break;
            }
            return miss;
        }
    }
}

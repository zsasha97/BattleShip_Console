using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip_Console
{
  class Player          // этот класс просто ужасен, можно было описать его гораздо проще на подобие того как сделано в методе Crush класса Battle. 
                        // я был молодой, зеленый и не знал, что это легально, уже не стану переделывать, и так работает ведь
    {
        //(так нигде это и не использовал, но оставлю в память о своих первоначальных планах)  
        // enum direction {horisontal,vertical};                                           //перечисление, хранит направление корабля, по умолчанию первый индекс = 0, последующие на 1 больше, тип int
        static Random rand = new Random();                                                 //АААААААААААААААААА, КАК ЭТО РАБОТАЕТ? НИЧЁ НЕ ПОНЯЛ, ААААААААААААААААААААА.
        public void Set_Position(int[,] Field, int size)
        {
            int direction;                                                                          //ориентация корабля в пространстве
            int posi;                                                                               //координата у первой клетки корабля
            int posj;                                                                               //координата х первой клетки корабля
            bool ship_set;
            int CountOfCell;                                                                        //количество свободных клеток необходимых для расстановки корабля
            int deck = 4;                                                                           //количество палуб корабля

            while (deck != 0)                                                                     //пока есть корабли с палубами
            {
                for (int k = 1; k <= 5 - deck; k++)                                               //разместить 1 четырехпалубный, 2 трехпалубных и т.д.
                {
                    ship_set = false;                                                             //флаг размещения корабля
                    direction = rand.Next(2);                                                     //генерируем ориентацию корабля 0 - горизонтальная, 1 - вертикальная
                    while (ship_set != true)
                    {
                        CountOfCell = 0;                                                                //для счета пустых клеток в области куда хотим поставить кораблик
                        if (direction == 0)                                                             // если горизонтальная
                        {
                            posi = rand.Next(size);                                                                                   //строка в которой будет размещаться корабль
                            posj = rand.Next(size - deck + 1);                                                                        //номер столбца в котором будет размещена первая палуба корабля
                            if ((posi - 1 >= 0) && (posj - 1 >= 0) && (posi + 1 < size) && (posj + deck < size))                      //если не выходим за границы массива
                            {
                                for (int i = posi - 1; i <= posi + 1; i++)
                                {                                                               //проверяем область вокруг места где должен встать новый корабль
                                    for (int j = posj - 1; j <= posj + deck; j++)
                                    {
                                        if (Field[i, j] != 1)                                   //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 2) * 3)                              //количество пустых клеток для корабля, который стоит не у края поля
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;                                            
                                }
                            }
                            else if ((posi - 1 < 0) && (posj - 1 < 0))                                              //если корабль решил встать в левый верхний угол
                            {
                                for (int i = posi; i <= posi + 1; i++)
                                {
                                    for (int j = posj; j <= posj + deck; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi - 1 < 0) && (posj + deck >= size))                                           //если корабль решил встать в правый верхний угол
                            {
                                for (int i = posi; i <= posi + 1; i++)
                                {
                                    for (int j = posj - 1; j <= posj + deck - 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                           //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi + 1 >= size) && (posj + deck >= size))                                       //если корабль решил встать в правый нижний угол
                            {
                                for (int i = posi - 1; i <= posi; i++)
                                {
                                    for (int j = posj - 1; j <= posj + deck - 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                           //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi + 1 >= size) && (posj - 1 < 0))                                       //если корабль решил встать в левый нижний угол
                            {
                                for (int i = posi - 1; i <= posi; i++)
                                {
                                    for (int j = posj; j <= posj + deck; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posi - 1 < 0)                                                                //если коорабль решил встать в первую строку поля, но не в угол
                            {
                                for (int i = posi; i <= posi + 1; i++)
                                {
                                    for (int j = posj - 1; j <= posj + deck; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 2) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posi + 1 >= size)                                                                //если коорабль решил встать в последнюю строку поля, но не в угол
                            {
                                for (int i = posi - 1; i <= posi; i++)
                                {
                                    for (int j = posj - 1; j <= posj + deck; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 2) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posj + deck >= size)                                                                //если последняя палуба корабля находится в последнем столбце, но не в углу
                            {
                                for (int i = posi - 1; i <= posi + 1; i++)
                                {
                                    for (int j = posj - 1; j <= posj + deck - 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 3)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posj - 1 < 0)                                                                //если первая палуба корабля находится в первом столбце, но не в углу
                            {
                                for (int i = posi - 1; i <= posi + 1; i++)
                                {
                                    for (int j = posj; j <= posj + deck; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 3)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posj++;
                                    }
                                    ship_set = true;
                                }
                            }
                        }
                        else if (direction == 1)                                                                           //если ориентация корабля вертикальная
                        {
                            posi = rand.Next(size - deck + 1);                                                            
                            posj = rand.Next(size);
                            if ((posi - 1 >= 0) && (posj - 1 >= 0) && (posi + deck < size) && (posj + 1 < size))           //если не выходим за границы массива
                            {
                                for (int i = posi - 1; i <= posi + deck; i++)
                                {
                                    for (int j = posj - 1; j <= posj + 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                           //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 2) * 3)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi - 1 < 0) && (posj - 1 < 0))                                                     //когда корабль должен попасть в левый верхний угол
                            {
                                for (int i = posi; i <= posi + deck; i++)
                                {
                                    for (int j = posj; j <= posj + 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi - 1 < 0) && (posj + 1 >= size))                                           //если корабль решил встать в правый верхний угол
                            {
                                for (int i = posi; i <= posi + deck; i++)
                                {
                                    for (int j = posj - 1; j <= posj; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi + deck >= size) && (posj + 1 >= size))                                      //если корабль решил встать в правый нижний угол
                            {
                                for (int i = posi - 1; i <= posi + deck - 1; i++)
                                {
                                    for (int j = posj - 1; j <= posj; j++)
                                    {
                                        if (Field[i, j] != 1)                                                         //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if ((posi + deck >= size) && (posj - 1 < 0))                                       //если корабль решил встать в левый нижний угол
                            {
                                for (int i = posi - 1; i <= posi + deck - 1; i++)
                                {
                                    for (int j = posj; j <= posj + 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posi - 1 < 0)                                                                //если коорабль решил встать первой палубой в первую строку поля, но не в угол
                            {
                                for (int i = posi; i <= posi + deck; i++)
                                {
                                    for (int j = posj - 1; j <= posj + 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 1) * 3)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posi + deck >= size)                                                        //если последняя палуба коорабля решила встать в последнюю строку поля, но не в угол
                            {
                                    for (int i = posi - 1; i <= posi + deck - 1; i++)
                                    {
                                        for (int j = posj - 1; j <= posj + 1; j++)
                                        {
                                            if (Field[i, j] != 1)                                                        //если клетка пустая
                                            {
                                                CountOfCell++;
                                            }
                                        }
                                    }
                                    if (CountOfCell == (deck + 1) * 3)
                                    {
                                        for (int i = 0; i < deck; i++)
                                        {
                                            Field[posi, posj] = 1;
                                            posi++;
                                        }
                                        ship_set = true;
                                    }
                            }
                            else if (posj + deck >= size)                                                                //если корабль находится в последнем столбце, но не в углу
                            {
                                for (int i = posi - 1; i <= posi + deck; i++)
                                {
                                    for (int j = posj - 1; j <= posj; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 2) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                            else if (posj - 1 < 0)                                                                //если корабль находится в первом столбце, но не в углу
                            {
                                for (int i = posi - 1; i <= posi + deck; i++)
                                {
                                    for (int j = posj; j <= posj + 1; j++)
                                    {
                                        if (Field[i, j] != 1)                                                        //если клетка пустая
                                        {
                                            CountOfCell++;
                                        }
                                    }
                                }
                                if (CountOfCell == (deck + 2) * 2)
                                {
                                    for (int i = 0; i < deck; i++)
                                    {
                                        Field[posi, posj] = 1;
                                        posi++;
                                    }
                                    ship_set = true;
                                }
                            }
                        }
                    }
                }
                    deck--;
            }
        }
    }
}

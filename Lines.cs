using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LINES
{
    class Program
    {
        static ConsoleColor[] colors = new ConsoleColor[] {ConsoleColor.Black,
                                                           ConsoleColor.Red,
                                                           ConsoleColor.DarkGray,
                                                           ConsoleColor.White,
                                                           ConsoleColor.Yellow,
                                                           ConsoleColor.Green,
                                                           ConsoleColor.Magenta,
                                                           ConsoleColor.Cyan };
        static int size;
        static int line = 0;
        static int StartX = 33;
        static int StartY = 5;
        static int CurrentX = 0;
        static int CurrentY = 0;
        static int MarkedBallX = -1;
        static int MarkedBallY = -1;
        static int TotalAccount = 0;
        static int TotalAccountX = 7;
        static int TotalAccountY = 9;
        static int NoMarked = 0;
        static int IsMarked = 1;
        static int IsDelete = 0;
        static bool IsMarkedBall = false;
        static bool end = true;


        static void Main()
        {
            int[,,] Field = start();
            CheckAndDel(Field);
            ShowBalls(Field);
            Console.SetCursorPosition(StartX + 1, StartY + 12);
            while (end)
            {
                GetKey(Field);
                ShowBalls(Field);
                ShowScore();
                Console.SetCursorPosition(StartX + 1 + CurrentX * 2, StartY + 1 + CurrentY * 2);
            }
        }

        static void CheckAndDel(int[,,] Field)
        {
            int LastColor;
            int u;
            //Проверка горизонталей

            for (int i = 0; i < size; i++)
            {
                u = 1;
                LastColor = -1;
                for (int q = 0; q < size; q++)
                {
                    if (Field[q, i, NoMarked] == LastColor & Field[q, i, NoMarked] != 0)
                    {
                        u++;
                        if (u > 2)
                        {
                            IsDelete = 1;
                            Field[q - 2, i, IsMarked] = IsMarked;
                            Field[q - 1, i, IsMarked] = IsMarked;
                            Field[q, i, IsMarked] = IsMarked;
                        }
                    }
                    else
                    {
                        LastColor = Field[q, i, NoMarked];
                        u = 1;
                    }
                }
            }

            //Проверка вертикалей  

            for (int x = 0; x < size; x++)
            {
                u = 1;
                LastColor = -1;
                for (int y = 0; y < size; y++)
                {
                    if (Field[x, y, NoMarked] == LastColor & Field[x, y, NoMarked] != 0)
                    {
                        u++;
                        if (u > 2)
                        {
                            IsDelete = 1;
                            Field[x, y - 2, IsMarked] = IsMarked;
                            Field[x, y - 1, IsMarked] = IsMarked;
                            Field[x, y, IsMarked] = IsMarked;
                        }
                    }
                    else
                    {
                        LastColor = Field[x, y, NoMarked];
                        u = 1;
                    }
                }
            }

            //Проверка диагонали влево верхняя

            for (int x = 2; x < size; x++)
            {
                u = 1;
                LastColor = -1;
                for (int y = 0; y < x + 1; y++)
                {
                    if (Field[x - y, y, NoMarked] == LastColor & Field[x - y, y, NoMarked] != 0)
                    {
                        u++;
                        if (u > 2)
                        {
                            IsDelete = 1;
                            Field[x - y + 2, y - 2, IsMarked] = IsMarked;
                            Field[x - y + 1, y - 1, IsMarked] = IsMarked;
                            Field[x - y, y, IsMarked] = IsMarked;
                        }
                    }
                    else
                    {
                        LastColor = Field[x - y, y, NoMarked];
                        u = 1;
                    }
                }
            }

            //нижняя
            for (int y = 0; y < size - 2; y++)
            {
                u = 1;
                LastColor = -1;
                for (int x = size - 1; x > y - 1; x--)
                {
                    if (Field[x, size - 1 - x + y, NoMarked] == LastColor & Field[x, size - 1 - x + y, NoMarked] != 0)
                    {
                        u++;
                        if (u > 2)
                        {
                            IsDelete = 1;
                            Field[x + 2, size - 1 - x + y - 2, IsMarked] = IsMarked;
                            Field[x + 1, size - 1 - x + y - 1, IsMarked] = IsMarked;
                            Field[x, size - 1 - x + y, IsMarked] = IsMarked;
                        }
                    }
                    else
                    {
                        LastColor = Field[x, size - 1 - x + y, NoMarked];
                        u = 1;
                    }
                }
            }

            //Диагонали вправо нижняя

            for (int y = size - 3; y > -1; y--)
            {
                u = 1;
                LastColor = -1;
                for (int x = 0; x < size - y; x++)
                {
                    if (Field[x, x + y, NoMarked] == LastColor & Field[x, x + y, NoMarked] != 0)
                    {
                        u++;
                        if (u > 2)
                        {
                            IsDelete = 1;
                            Field[x - 2, x + y - 2, IsMarked] = IsMarked;
                            Field[x - 1, x + y - 1, IsMarked] = IsMarked;
                            Field[x, x + y, IsMarked] = IsMarked;
                        }
                    }
                    else
                    {
                        LastColor = Field[x, x + y, NoMarked];
                        u = 1;
                    }
                }
            }

            //верхняя
            for (int x = 0; x < size - 2; x++)
            {
                u = 1;
                LastColor = -1;
                for (int y = 0; y < size - x; y++)
                {
                    if (Field[x + y, y, NoMarked] == LastColor & Field[x + y, y, NoMarked] != 0)
                    {
                        u++;
                        if (u > 2)
                        {
                            IsDelete = 1;
                            Field[x + y - 2, y - 2, IsMarked] = IsMarked;
                            Field[x + y - 1, y - 1, IsMarked] = IsMarked;
                            Field[x + y, y, IsMarked] = IsMarked;
                        }
                    }
                    else
                    {
                        LastColor = Field[x + y, y, NoMarked];
                        u = 1;
                    }
                }
            }

            //Удаление
            for (int i = 0; i < size; i++)
            {
                for (int q = 0; q < size; q++)
                {
                    if (Field[q, i, IsMarked] == IsMarked)
                    {
                        TotalAccount++;
                        Field[q, i, NoMarked] = 0;
                        Field[q, i, IsMarked] = NoMarked;
                    }
                }
            }
        }

        static void GetKey(int[,,] Field)
        {
            ConsoleKeyInfo cki;
            cki = Console.ReadKey(true);
            if (cki.Key == ConsoleKey.UpArrow)
            {
                if (CurrentY > 0)
                    CurrentY--;
            }

            if (cki.Key == ConsoleKey.DownArrow)
            {
                if (CurrentY < size - 1)
                    CurrentY++;
            }

            if (cki.Key == ConsoleKey.RightArrow)
            {
                if (CurrentX < size - 1)
                    CurrentX++;
            }

            if (cki.Key == ConsoleKey.LeftArrow)
            {
                if (CurrentX > 0)
                    CurrentX--;
            }

            if (cki.Key == ConsoleKey.Spacebar)
            {
                if (IsMarkedBall == true)
                {
                    if (MoveBallNewPosition(Field))
                    {
                        CheckAndDel(Field);
                        if (IsDelete == 0)
                        {
                            if (AddBallsInField(3, Field))
                            {
                                CheckAndDel(Field);
                            }
                            else
                            {
                                GameOver(Field);
                            }
                        }
                        else
                            IsDelete = 0;
                    }
                }
                else
                {
                    if (Field[CurrentX, CurrentY, NoMarked] != 0)
                    {
                        IsMarkedBall = true;
                        MarkedBallX = CurrentX;
                        MarkedBallY = CurrentY;
                    }
                }
            }
            if (cki.Key == ConsoleKey.Escape)
                end = false;
        }

        static int[,,] start()
        {

            Console.WriteLine("****************************************************");
            Console.WriteLine("* Game LINES for terminal (by Artem Dryagin), 2017 *");
            Console.WriteLine("****************************************************\n\n\n\n");
            Console.Write("Enter size of Game Field: ");
            size = int.Parse(Console.ReadLine());
            int[,,] Field = new int[size, size, 2];                       //Двумерный массив
            Console.Write("Enter number of Startballs: ");
            int StartBalls = int.Parse(Console.ReadLine());
            Console.WriteLine("\n\"Arrows\" - movement\n\"Spacebar\" - Choose a ball or position\n\"Esc\" - Exit\n");
            Console.Write("Press any button to start");
            Console.ReadKey();
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Size: {0}x{0}                            ", size);
            Console.WriteLine("Startballs: {0}                          ", StartBalls);
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.WriteLine("\t\t\t\t\t\t");
            Console.SetCursorPosition(0, 9);
            Console.Write("Total: {0}", TotalAccount);
            ShowFrame();
            AddBallsInField(StartBalls, Field);
            ShowBalls(Field);
            Console.SetCursorPosition(StartX + 1 + CurrentX * 2, StartY + 1 + CurrentY * 2);
            return Field;
        }

        static void GameOver(int[,,] Field)
        {
            ShowBalls(Field);
            ShowScore();
            Console.SetCursorPosition(StartX, StartY - 1);
            Console.WriteLine("Game Over");
        }

        static bool AddBallsInField(int n, int[,,] Field)
        {
            Random random = new Random();
            int i = 0;
            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                    if (Field[x, y, 0] == 0)
                        i++;

            do
            {
                if (i > 0)
                {
                    int x = random.Next(0, size);
                    int y = random.Next(0, size);
                    if (Field[x, y, NoMarked] == 0)
                    {
                        Field[x, y, NoMarked] = random.Next(1, 8);
                        n--;
                        i--;
                    }
                }
                else
                    return false;
            }
            while (n > 0);
            return true;
        }

        static bool MoveBallNewPosition(int[,,] Field)
        {
            if (CurrentX == MarkedBallX & CurrentY == MarkedBallY)
            {
                IsMarkedBall = false;
                MarkedBallX = -1;
                MarkedBallY = -1;
                return false;
            }
            else
            {
                if (Field[CurrentX, CurrentY, NoMarked] == 0)
                {
                    //FakeFiled
                    int[,] Field2 = new int[size, size];
                    for (int x = 0; x < size; x++)
                        for (int y = 0; y < size; y++)
                            if (Field[x, y, 0] != 0)
                                Field2[x, y] = 1;
                    //Конец заполнения FakeField
                    if (Moving(CurrentX, CurrentY, CurrentX, CurrentY, Field2))
                    {
                        Field[CurrentX, CurrentY, NoMarked] = Field[MarkedBallX, MarkedBallY, NoMarked];
                        Field[MarkedBallX, MarkedBallY, NoMarked] = 0;
                        IsMarkedBall = false;
                        MarkedBallX = -1;
                        MarkedBallY = -1;
                        return true;
                    }
                    else
                    {
                        IsMarkedBall = false;
                        MarkedBallX = -1;
                        MarkedBallY = -1;
                        Console.Beep();
                        return false;
                    }
                }
                else
                {
                    Console.Beep();
                    return false;
                }
            }
        }

        static void vivod(int[,] Field2)
        {
            Console.SetCursorPosition(0, 25);
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (Field2[x, y] == 1)
                        Console.Write("*");
                    else Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.Beep();
        }

        static bool Moving(int X, int Y, int newX, int newY, int[,] Field2)
        {
            int[,] FieldNext = Field2;
            FieldNext[X, Y] = 1;
            X = newX;
            Y = newY;

            if (Y > 0)
            {
                if (X == MarkedBallX && Y - 1 == MarkedBallY)
                {
                    return true;
                }

                if (FieldNext[X, Y - 1] == 0)
                {
                    if (Moving(X, Y, newX, newY - 1, FieldNext))
                        return true;
                }
            }

            if (X > 0)
            {
                if (X - 1 == MarkedBallX && Y == MarkedBallY)
                {
                    return true;
                }
                if (FieldNext[X - 1, Y] == 0)
                {
                    if (Moving(X, Y, newX - 1, newY, FieldNext))
                        return true;
                }
            }

            if (Y < size - 1)
            {
                if (X == MarkedBallX && Y + 1 == MarkedBallY)
                {
                    return true;
                }
                if (FieldNext[X, Y + 1] == 0)
                {
                    if (Moving(X, Y, newX, newY + 1, FieldNext))
                        return true;
                }
            }

            if (X < size - 1)
            {
                if (X + 1 == MarkedBallX && Y == MarkedBallY)
                {
                    return true;
                }
                if (FieldNext[X + 1, Y] == 0)
                {
                    if (Moving(X, Y, newX + 1, newY, FieldNext))
                        return true;
                }
            }

            return false;
        }

        static void ShowScore()
        {
            Console.SetCursorPosition(TotalAccountX, TotalAccountY);
            Console.Write(TotalAccount);
        }

        static void ShowBalls(int[,,] Field)
        {
            int x = StartX + 1;
            int y = StartY + 1;

            for (int i = 0; i < size; i++)
            {
                for (int q = 0; q < size; q++)
                {
                    Console.SetCursorPosition(x, y);
                    x = x + 2;
                    if (Field[q, i, NoMarked] != 0)
                    {
                        Console.ForegroundColor = colors[Field[q, i, NoMarked]];
                        if (i == MarkedBallY & q == MarkedBallX)
                            Console.Write("X");
                        else
                            Console.Write(Char.ConvertFromUtf32(9608));
                        Console.ResetColor();
                    }
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
                x = StartX + 1;
                y = y + 2;
            }
        }

        static void ShowFrame()
        {
            Console.WriteLine("\n\n\n");
            Line1(size);
            for (int i = 0; i < size - 1; i++)
            {
                Line2(1, size);
            }
            Line2(0, size);
            Line1(size);
        }

        static void Line1(int t)
        {
            Console.SetCursorPosition(StartX, StartY + line);
            Console.Write("+");
            for (int q = 0; q < t * 2 - 1; q++)
                Console.Write("-");
            Console.WriteLine("+");
            line++;
        }

        static void Line2(int r, int t)
        {
            Console.SetCursorPosition(StartX, StartY + line);
            for (int q = 0; q < t; q++)
                Console.Write("| ");
            Console.WriteLine("|");
            line++;
            if (r == 1)
            {
                Console.SetCursorPosition(StartX, StartY + line);
                for (int q = 0; q < t; q++)
                    Console.Write("|-");
                Console.WriteLine("|");
                line++;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeeslag
{
    class Program
    {

     

        static void Main(string[] args)
        {

            
            
            bool running = true;
            bool runningGame = true;
            string[,] grid = new string[10, 10];

            grid = ClearGrid(grid);

            while (running) {

                switch (SelectMenu(grid,"Speel Spel", "Stop"))
                {
                    case 1:
                  
                        int[] availableBoats = { 5, 4, 3, 3, 2 };
                        
                        grid = ClearGrid(grid);

                        while (runningGame)
                        {
                            if (CheckAvailableBoats(availableBoats))
                            {
                                DrawGrid(grid);
                                int length = 0;
                                switch (SelectMenu(grid, "Voeg boot horizontaal toe", "Voeg boot verticaal toe","Terug"))
                                {
                                    case 1:

                                        length = SetLenght(grid, availableBoats);
                                        grid = AddBoatToGridHorizontal(grid, length, SetCordsHorizontal(length, grid));
                                        DrawGrid(grid);
                                        Console.ReadLine();

                                        break;
                                    case 2:
                                        length = SetLenght(grid, availableBoats);
                                        grid = AddBoatToGridVertical(grid, length, SetCordsVertical(length, grid));
                                        DrawGrid(grid);
                                        Console.ReadLine();
                                        break;
                                    case 3:
                                        runningGame = false;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                // boten bord maken voor de andere speler/computer
                                Console.WriteLine("Hier moet nog stuff komen voor de 2de speler/ computer ");
                            }
                            
                        }                        
                        Console.ReadLine();
                        break;
                    case 2:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("How Did You Do This");
                        break;
                }

            }
          

            


        }

        static void DrawGrid(string[,] grid)
        {
            string row = string.Empty;

            //Schrijft 1-10 voor coordinaten 
            //setcursorposition is voor midden van scherm te nemen 
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop);
            Console.WriteLine(" 1 2 3 4 5 6 7 8 9 10");
            
            for (int i = 0; i < grid.Length/10; i++)
            {
                for (int j = 0; j < grid.Length/10; j++)
                {
                    //als het aan een nieuwe rij begint wordt er het 65 char (A) + i voor gezet
                    if (string.IsNullOrEmpty(row))
                    {
                        
                        row = Convert.ToChar(65 + i) + " " + grid[i, j] + " ";

                    }
                    else
                    {
                        row += grid[i,j]+" ";
                        
                    }
                }

                Console.SetCursorPosition((Console.WindowWidth - row.Length) / 2, Console.CursorTop);

                Console.WriteLine(row);
                
                row = string.Empty;
            }
            Console.WriteLine();
            Console.WriteLine();
            
        }
        static string[,] AddBoatToGridHorizontal(string[,] grid, int length, int[] cords)
        {
            string[,] tempgrid = new string[10, 10];

            for (int i = 0; i <= length-1; i++)
            {
                 grid[cords[0], cords[1]+i] = "B";
            }
            
            tempgrid = grid;
            return tempgrid;
        }
        static int[] SetCordsHorizontal(int lenght,string[,] grid)
        {
            int[] cords = new int[2];
            cords[0] = SelectMenuHorizontal(grid,"A", "B", "C", "D", "E", "F", "G", "H", "I", "J")-1;
            cords[1] = SelectMenuHorizontal(grid,"1", "2", "3", "4", "5", "6", "7", "8", "9", "10")-1;

            if (cords[1] + lenght > 10)
            {
                Console.WriteLine("Hier Mag je geen schip horizontaal zetten Geef andere coördinaten in ");
                Console.ReadLine();
                SetCordsHorizontal(lenght, grid);
            }

            if (!CheckAvailableSpotHorizontal(grid,cords))
            {
                Console.WriteLine("Hier Staat al een schip kijk op je grid waar je nog plaats hebt ");
                Console.ReadLine();
                SetCordsHorizontal(lenght, grid);
            }

            return cords;
        }
        static string[,] AddBoatToGridVertical(string[,] grid, int length, int[] cords)
        {
            string[,] tempgrid = new string[10, 10];

            for (int i = 0; i < length; i++)
            {
                grid[cords[0] + i, cords[1]] = "B";
            }

            tempgrid = grid;
            return tempgrid;
        }
        static int[] SetCordsVertical(int lenght, string[,] grid)
        {
            int[] cords = new int[2];
            cords[0] = SelectMenuHorizontal(grid, "A", "B", "C", "D", "E", "F", "G", "H", "I", "J") - 1;
            cords[1] = SelectMenuHorizontal(grid, "1", "2", "3", "4", "5", "6", "7", "8", "9", "10") - 1;

            if (cords[0] + lenght > 10)
            {
                Console.WriteLine("Hier Mag je geen schip verticaal zetten Geef andere coördinaten in ");
                Console.ReadLine();
                SetCordsHorizontal(lenght, grid);
            }

            if (!CheckAvailableSpotVertical(grid, cords))
            {
                Console.WriteLine("Hier Staat al een schip kijk op je grid waar je nog plaats hebt ");
                Console.ReadLine();
                SetCordsHorizontal(lenght, grid);
            }

            return cords;
        }
        static int SetLenght(string[,] grid,int[] availableBoats)
        {


            int lenght = SelectMenu(grid,availableBoats);

            for (int i = 0; i < availableBoats.Length; i++)
            {
                if (availableBoats[i] == lenght)
                {

                    availableBoats[i] = -1;

                    break;
                }
            }
            return lenght;

        }
        static string[,] ClearGrid(string[,] grid)
        {
            string[,] temp = new string[10, 10];

            for (int i = 0; i < grid.Length/10; i++)
            {
                for (int j = 0; j < grid.Length/10; j++)
                {
                    temp[i, j] = "0";
                }
            }
            return temp;
        }
        static int SelectMenu(string[,] grid, params string[] menu)
        {
            Console.SetCursorPosition(0,0);
            Console.CursorVisible = false;
            Console.Clear();

            int selection = 1;
            bool selected = false;
            ConsoleColor selectionForeground = Console.BackgroundColor;
            ConsoleColor selectionBackground = Console.ForegroundColor;

            while (!selected)
            {
                DrawGrid(grid);
                for (int i = 0; i < menu.Length; i++)
                {
                    if (selection == i + 1)
                    {
                        Console.ForegroundColor = selectionForeground;
                        Console.BackgroundColor = selectionBackground;
                    }
                    Console.SetCursorPosition((Console.WindowWidth - menu[i].Length) / 2, Console.CursorTop);
                    Console.WriteLine((i + 1) + ": " + menu[i]);
                    Console.ResetColor();
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        selection--;
                        break;
                    case ConsoleKey.DownArrow:
                        selection++;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                }

                selection = Math.Min(Math.Max(selection, 1), menu.Length);
                Console.SetCursorPosition(0,0);
            }

            Console.Clear();
            Console.CursorVisible = true;

            return selection;
        }
        static int SelectMenuHorizontal(string[,] grid, params string[] menu)
        {
            Console.SetCursorPosition(0, 15);
            Console.CursorVisible = false;
            Console.Clear();
            
            int selection = 1;
            bool selected = false;
            ConsoleColor selectionForeground = Console.BackgroundColor;
            ConsoleColor selectionBackground = Console.ForegroundColor;

            while (!selected)
            {
                DrawGrid(grid);
                for (int i = 0; i < menu.Length; i++)
                {

                    if (selection == i + 1)
                    {
                        Console.ForegroundColor = selectionForeground;
                        Console.BackgroundColor = selectionBackground;
                    }

                    
                    Console.Write(menu[i] + " ");

                    Console.ResetColor();

                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        selection--;
                        break;
                    case ConsoleKey.RightArrow:
                        selection++;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                }

                selection = Math.Min(Math.Max(selection, 1), menu.Length);
                Console.SetCursorPosition(0, 0);
            }

            Console.Clear();
            Console.CursorVisible = true;

            return selection;
        }
        static int SelectMenu(string[,] grid, int[] availableBoats)
        {
            Console.SetCursorPosition(0, 15);
            Console.CursorVisible = false;
            Console.Clear();

            int selection = 1;
            bool selected = false;
            ConsoleColor selectionForeground = Console.BackgroundColor;
            ConsoleColor selectionBackground = Console.ForegroundColor;

            while (!selected)
            {
                DrawGrid(grid);
                for (int i = 0; i < availableBoats.Length; i++)
                {
                    
                    if (selection == i + 1)
                    {
                        Console.ForegroundColor = selectionForeground;
                        Console.BackgroundColor = selectionBackground;
                    }


                    switch (availableBoats[i])
                    {
                        case 5:
                            Console.WriteLine($"Carrier: {availableBoats[i]} lang");
                            break;
                        case 4:
                            Console.WriteLine($"Battleship: {availableBoats[i]} lang");
                            break;
                        case 3:
                            Console.WriteLine($"Submarine: {availableBoats[i]} lang");
                            break;
                        case 2:
                            Console.WriteLine($"Destroyer: {availableBoats[i]} lang" );
                            break;
                        default:
                            break;
                    }                    

                    Console.ResetColor();

                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        selection--;
                        break;
                    case ConsoleKey.DownArrow:
                        selection++;
                        break;
                    case ConsoleKey.Enter:
                        selected = true;
                        break;
                }

                selection = Math.Min(Math.Max(selection, 1), availableBoats.Length);
                Console.SetCursorPosition(0, 0);
            }

            Console.Clear();
            Console.CursorVisible = true;

            return availableBoats[selection -1];
        }
        static bool CheckAvailableBoats(int[] availableBoats)
        {
            int counter = 0;
            for (int i = 0; i < availableBoats.Length; i++)
            {
                if (availableBoats[i] == -1)
                {
                    counter++;
                }
            }
            if (counter == 5)
            {
                return false;
            }
            return true;
        }
        static bool CheckAvailableSpotHorizontal(string[,] grid, int[] cords)
        {
            for (int i = 0; i < (grid.Length/10)-cords[1]; i++)
            {
                if (grid[cords[0], cords[1]+i] == "B")
                {
                    return false;
                }
            }
            
            return true;
        }

        static bool CheckAvailableSpotVertical(string[,] grid, int[] cords)
        {
            for (int i = 0; i < (grid.Length / 10) - cords[1]; i++)
            {
                if (grid[cords[0] + i, cords[1]] == "B")
                {
                    return false;
                }
            }

            return true;
        }
    }
}

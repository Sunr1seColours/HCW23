using System.Runtime.InteropServices;
using PolyclinicsInfo;

namespace HCW23;

/// <summary>
/// Implements some operations in console.
/// </summary>
public static class ConsoleWork
{
    /// <summary>
    /// Makes long string shorter. Take some first symbols from original string and adds three dots in the end.
    /// </summary>
    /// <param name="str">String to cut.</param>
    /// <returns>Shorter variant of original string.</returns>
    private static string StringCut(string str)
    {
        // 40 is just a number for amount of string symbols. It makes string not very long, but not too small.
        string cuttedString;
        if (str.Length >= 40)
        {
            cuttedString = new string(str[..37] + "...");
        }
        else
        {
            cuttedString = str;
        }

        return cuttedString;
    }
    
    /// <summary>
    /// Let user see another columns of data, if all columns don't feet.
    /// </summary>
    /// <param name="startColumn">Number of the first showed column.</param>
    /// <param name="showedColumns">Amount of columns, which are displayed.</param>
    /// <returns>Does user want to see another columns or not.</returns>
    private static bool MoveColumns(ref int startColumn, int showedColumns)
    {
        Console.Write("To see another columns, tap right and left arrows\nAnother buttons will do next steps: ");
        ConsoleKey arrow = Console.ReadKey().Key;
        Console.WriteLine();
        bool moving = false;
        if (arrow == ConsoleKey.RightArrow)
        {
            if (startColumn + showedColumns < 27)
            {
                startColumn++;
            }

            moving = true;
        }
        else if (arrow == ConsoleKey.LeftArrow)
        {
            if (startColumn > 0)
            {
                startColumn--;
            }

            moving = true;
        }

        return moving;
    }
    
    /// <summary>
    /// Lets user jump between points of the menu.
    /// </summary>
    /// <param name="point">Choosen point.</param>
    /// <param name="key">Key, that user tapped.</param>
    /// <param name="generalMenu">If it's 'true', this method is for general menu.
    /// If it's 'false' - for choosing saving format menu.</param>
    private static void ChooseAnotherMenuPoint(ref int point, ConsoleKey key, bool generalMenu)
    {
        if (generalMenu)
        {
            if (key == ConsoleKey.UpArrow & point > 1)
            {
                point--;
            }
            else if (key == ConsoleKey.DownArrow & point < 5)
            {
                point++;
            }
        }
        else
        {
            if (key == ConsoleKey.UpArrow & point > 1)
            {
                point--;
            }
            else if (key == ConsoleKey.DownArrow & point < 3)
            {
                point++;
            }
        }
    }
    
    /// <summary>
    /// Asks path to file from user.
    /// </summary>
    /// <param name="toRead">If it's 'true', the path is for reading.
    /// If 'false', the path is for writing.</param>
    /// <returns>Path to file.</returns>
    public static string GetPath(bool toRead = true)
    {
        Console.Write($"Enter a path to file to {(toRead ? "read" : "write data into")}: ");
        return Console.ReadLine();    
    }

    /// <summary>
    /// Keeps sides of table: top and bottom.
    /// </summary>
    public enum Sides
    {
        top,
        bottom
    }
    
    /// <summary>
    /// Let user choose side of dataset, that will be showed in console.
    /// </summary>
    /// <returns>'top' or 'bottom' - element of enum 'Sides'.</returns>
    public static Sides ChooseSide()
    {
        Console.Write("Do you want to see some data from top of the dataset? \n(y/n): ");
        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            Console.WriteLine();
            if (key == ConsoleKey.Y)
            {
                return Sides.top;
            }
            else if (key == ConsoleKey.N)
            {
                return Sides.bottom;
            }
            else
            {
                Console.Write("Answer can be only 'y' or 'n'. Try again: ");
            }
        }
    }

    /// <summary>
    /// Let user choose amount of records. This number of them will be showed in console.
    /// </summary>
    /// <param name="data">Array of Polyclinics objects.</param>
    /// <returns>Amount of records, that will be showed.</returns>
    public static int ItemsAmount(Polyclinics[] data)
    {
        int height = Console.WindowHeight;
        Console.Write($"How many items do you want to see? \nEnter a number between 1 and " +
                      $"{Math.Min(height - 3, data.Length)}:");
        int lines;
        while (!int.TryParse(Console.ReadLine(), out lines) || lines < 1 || lines > Math.Min(height - 3, data.Length))
        {
            Console.Write("Seems like you entered wrong amount of lines.\nPlease, try again:");
        }

        return lines;
    }

    /// <summary>
    /// Overload with the List of Polyclinics objects instead of array.
    /// Let user choose amount of records. This number of them will be showed in console.
    /// The number of records defines between 1 and minimum of console height and amount of records.
    /// </summary>
    /// <param name="data">List of Polyclinics objects.</param>
    /// <returns>Amount of records, that will be showed.</returns>
    public static int ItemsAmount(List<Polyclinics> data)
    {
        int height = Console.WindowHeight;
        Console.Write($"How many items do you want to see? \nEnter a number between 1 and " +
                      $"{Math.Min(height - 3, data.Count)}:");
        int lines;
        while (!int.TryParse(Console.ReadLine(), out lines) || lines < 1 || lines > Math.Min(height - 3, data.Count))
        {
            Console.Write("Seems like you entered wrong amount of lines.\nPlease, try again:");
        }

        return lines;
    }
    
    /// <summary>
    /// Shows part of dataset in console.
    /// </summary>
    /// <param name="side">'top' or 'bottom'.</param>
    /// <param name="heading">String array of column headers which data has.</param>
    /// <param name="dataToShow">Array of Polyclinics object. Some records of it will be shown.</param>
    /// <param name="amountOfRecords">Amount of records, which will be shown.</param>
    public static void ShowData(Sides side, string[] heading, Polyclinics[] dataToShow, int amountOfRecords)
    {
        int showedColumns = Console.WindowWidth / 45;
        bool fromTop = side == Sides.top ? true : false;
        int startColumn = 0;
        do
        {
            Console.Clear();
            for (int i = startColumn; i < showedColumns + startColumn; i++)
            {
                Console.Write($"{heading[1..][i], -45}");
            }
            Console.WriteLine();

            int recordNumber = fromTop ? 0 : dataToShow.Length - amountOfRecords;
            for (int i = 0; i < amountOfRecords; i++)
            {
                List<string> toShow = dataToShow[recordNumber].ToStringList();
                for (int j = startColumn; j < showedColumns + startColumn; j++)
                {
                    Console.Write($"{StringCut(toShow[j]), -45}");
                }
                Console.WriteLine();
                recordNumber++;
            }
            
            if (showedColumns == dataToShow.Length)
            {
                Console.Write("To go to next steps, tap something: ");
                Console.ReadKey();
                Console.WriteLine();
                break;
            }
        } while (MoveColumns(ref startColumn, showedColumns));
    }

    /// <summary>
    /// Shows part of dataset in console.
    /// </summary>
    /// <param name="side">'top' or 'bottom'.</param>
    /// <param name="heading">String array of column headers which data has.</param>
    /// <param name="dataToShow">List of Polyclinics object. Some records of it will be shown.</param>
    /// <param name="amountOfRecords">Amount of records which, will be shown.</param>
    public static void ShowData(Sides side, string[] heading, List<Polyclinics> dataToShow, int amountOfRecords)
    {
        int showedColumns = Console.WindowWidth / 45;
        bool fromTop = side == Sides.top ? true : false;
        int startColumn = 0;
        do
        {
            Console.Clear();
            for (int i = startColumn; i < showedColumns + startColumn; i++)
            {
                Console.Write($"{heading[1..][i], -45}");
            }
            Console.WriteLine();

            int recordNumber = fromTop ? 0 : dataToShow.Count - amountOfRecords;
            for (int i = 0; i < amountOfRecords; i++)
            {
                for (int j = startColumn; j < showedColumns + startColumn; j++)
                {
                    List<string> toShow = dataToShow[recordNumber].ToStringList();
                    Console.Write($"{StringCut(toShow[j]), -45}");
                }
                Console.WriteLine();
                recordNumber++;
            }
            
            if (showedColumns == dataToShow.Count)
            {
                Console.Write("To go to next steps, tap something: ");
                Console.ReadKey();
                Console.WriteLine();
                break;
            }
        } while (MoveColumns(ref startColumn, showedColumns));
    }
    
    /// <summary>
    /// Changes foreground and background color of console.
    /// </summary>
    /// <param name="highlight">If it's 'true', the text will be highlighted.</param>
    public static void SetConsoleColor(bool highlight)
    {
        if (highlight)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
        }
        else
        {
            try
            {
                // This works only in MacOS. Sets default console colors.
                Console.ForegroundColor = (ConsoleColor)(-1);
                Console.BackgroundColor = (ConsoleColor)(-1);
            }
            catch (Exception)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
    
    /// <summary>
    /// Shows menu with available actions with data.
    /// </summary>
    /// <returns>Number of </returns>
    public static int DynamicMenu()
    {
        Console.Clear();
        int point = 1;
        do
        {
            Console.WriteLine("Choose mode of saving (press 'Enter' when decide)");
            Console.WriteLine("To swap between points, use up and down arrows.");
            switch (point)
            {
                case 1:
                {
                    SetConsoleColor(true);
                    Console.WriteLine("1. Select some data from file by administration area parameter.");
                    SetConsoleColor(false);
                    Console.WriteLine("2. Select some data from file by information about paid services.");
                    Console.WriteLine("3. Sort data by district parameter in alphabetical order.");
                    Console.WriteLine("4. Sort data by district parameter in reversed alphabetical order.");
                    Console.WriteLine("5. Exit / Restart menu.");
                    break;
                }
                case 2:
                {
                    Console.WriteLine("1. Select some data from file by administration area parameter.");
                    SetConsoleColor(true);
                    Console.WriteLine("2. Select some data from file by information about paid services.");
                    SetConsoleColor(false);
                    Console.WriteLine("3. Sort data by district parameter in alphabetical order.");
                    Console.WriteLine("4. Sort data by district parameter in reversed alphabetical order.");
                    Console.WriteLine("5. Exit / Restart menu.");
                    break;
                }
                case 3:
                {
                    Console.WriteLine("1. Select some data from file by administration area parameter.");
                    Console.WriteLine("2. Select some data from file by information about paid services.");
                    SetConsoleColor(true);
                    Console.WriteLine("3. Sort data by district parameter in alphabetical order.");
                    SetConsoleColor(false);
                    Console.WriteLine("4. Sort data by district parameter in reversed alphabetical order.");
                    Console.WriteLine("5. Exit / Restart menu.");
                    break;
                }
                case 4:
                {
                    Console.WriteLine("1. Select some data from file by administration area parameter.");
                    Console.WriteLine("2. Select some data from file by information about paid services.");
                    Console.WriteLine("3. Sort data by district parameter in alphabetical order.");
                    SetConsoleColor(true);
                    Console.WriteLine("4. Sort data by district parameter in reversed alphabetical order.");
                    SetConsoleColor(false);
                    Console.WriteLine("5. Exit / Restart menu.");
                    break;
                }
                case 5:
                {
                    Console.WriteLine("1. Select some data from file by administration area parameter.");
                    Console.WriteLine("2. Select some data from file by information about paid services.");
                    Console.WriteLine("3. Sort data by district parameter in alphabetical order.");
                    Console.WriteLine("4. Sort data by district parameter in reversed alphabetical order.");
                    SetConsoleColor(true);
                    Console.WriteLine("5. Exit / Restart menu.");
                    SetConsoleColor(false);
                    break;
                }
            }
            
            ConsoleKey choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.Enter)
            {
                return point;
            }
            ChooseAnotherMenuPoint(ref point, choice, true);
            Console.Clear();
        } while (true);
    }

    /// <summary>
    /// Takes parameters for selecting some data from user.
    /// </summary>
    /// <returns>String array of parameters.</returns>
    /// <exception cref="ArgumentException">User didn't enter something.</exception>
    public static string[] GetParameters()
    {
        Console.Write("Enter a parameters value, separated by ',' to find in dataset: ");
        string[] parameters = Console.ReadLine().Split(',');
        if (parameters == null || parameters.Length == 0)
        {
            // Wrong argument of selecting parameters.
            throw new ArgumentException("You must enter something.");
        }
        else
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = parameters[i].Trim();
            }
        }

        return parameters;
    }
    
    /// <summary>
    /// Asks user about mode of saving data.
    /// </summary>
    /// <returns>Number of mode.
    /// 1 - Saving data into a new file.
    /// 2 - Rewriting existed file.
    /// 3 - Adding data into existed file.</returns>
    public static int GetSavingMode()
    {
        Console.Clear();
        int point = 1;
        do
        {
            Console.WriteLine("Choose mode of saving (press 'Enter' when decide)");
            Console.WriteLine("To swap between points, use up and down arrows.");
            switch (point)
            {
                case 1:
                {
                    SetConsoleColor(true);
                    Console.WriteLine("1. Create file and save into it.");
                    SetConsoleColor(false);
                    Console.WriteLine("2. Save data in existed file with rewriting.");
                    Console.WriteLine("3. Append data in existed file.");
                    break;
                }
                case 2:
                {
                    Console.WriteLine("1. Create file and save into it.");
                    SetConsoleColor(true);
                    Console.WriteLine("2. Save data in existed file with rewriting.");
                    SetConsoleColor(false);
                    Console.WriteLine("3. Append data in existed file.");
                    break;
                }
                case 3:
                {
                    Console.WriteLine("1. Create file and save into it.");
                    Console.WriteLine("2. Save data in existed file with rewriting.");
                    SetConsoleColor(true);
                    Console.WriteLine("3. Append data in existed file.");
                    SetConsoleColor(false);
                    break;
                }
            }

            ConsoleKey choice = Console.ReadKey().Key;
            if (choice == ConsoleKey.Enter)
            {
                return point;
            }
            ChooseAnotherMenuPoint(ref point, choice, false);
            Console.Clear();
        } while (true);
        
    }
    
    /// <summary>
    /// Asks user if he/she wants to rerun program or to exit.
    /// </summary>
    /// <param name="repeat">'true' if user wants to rerun. 'false' - if doesn't.</param>
    public static void Exit(ref bool repeat)
    {
        Console.WriteLine("Tap 'q' to exit or something else to rerun: ");
        ConsoleKey exitButton = Console.ReadKey().Key;
        Console.WriteLine();
        if (exitButton == ConsoleKey.Q)
        {
            repeat = false;
        }
        Console.Clear();
    }
}
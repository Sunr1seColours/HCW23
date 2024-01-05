using Csv;
using Data;
using PolyclinicsInfo;
using static HCW23.ConsoleWork;

namespace HCW23;

/// <summary>
/// Class which implements all program logic and catches exceptions.
/// </summary>
public static class Catcher
{
    /// <summary>
    /// Asks a path to file from user.
    /// Then reads this file and creates array of Polyclinics objects with data from file.
    /// </summary>
    /// <param name="headers">String array with headers of columns.</param>
    /// <param name="polyclinics">Array of Polyclinics objects. Contains all info about polyclinics in data.</param>
    /// <returns>'True' if everything is okay and program can go further. 'False' if some data is incorrect.</returns>
    /// <exception cref="NullReferenceException">File doesn't contain any records about polyclinics.</exception>
    public static bool GetData(out string[] headers, out Polyclinics[] polyclinics)
    {
        try
        {
            Reading.RPath = GetPath();
            string[] polyclinicsStr = Reading.Read();
            if (polyclinicsStr.Length <= 1)
            {
                // There aren't any records about polyclinics in file.
                throw new NullReferenceException("There aren't any records in file.");
            }
            polyclinics = new Polyclinics[polyclinicsStr.Length - 1];
            headers = Reading.CsvSplit(polyclinicsStr[0]);
            for (int i = 1; i < polyclinicsStr.Length; i++)
            {
                polyclinics[i - 1] = new Polyclinics(Reading.CsvSplit(polyclinicsStr[i]));
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            polyclinics = null;
            headers = null;
            return false;
        }
        catch (DirectoryNotFoundException e)
        {
            Console.WriteLine(e.Message);
            polyclinics = null;
            headers = null;
            return false;
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
            polyclinics = null;
            headers = null;
            return false;
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine(e.Message);
            polyclinics = null;
            headers = null;
            return false;
        }
        catch (NullReferenceException)
        {
            Console.WriteLine("Seems like file has got invalid data.");
            polyclinics = null;
            headers = null;
            return false;
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine("Seems like file is hollow.");
            polyclinics = null;
            headers = null;
            return false;
        }

        return true;
    }
    

    /// <summary>
    /// Implements actions of each menu point.
    /// </summary>
    /// <param name="choice">Number of point which was chosen.</param>
    /// <param name="data">Array with Polyclinics objects to make some actions under it.</param>
    /// <returns>Changed data about polyclinics as a List of Polyclinics objects.</returns>
    /// <exception cref="NullReferenceException">File doesn't contain any records about polyclinics.</exception>
    public static List<Polyclinics> MenuRealization(int choice, Polyclinics[] data)
    {
        List<Polyclinics> chosen = null;
        try
        {
            if (choice == 1)
            {
                string[] parameters = GetParameters();
                chosen = Filtration.Filter(true, data, parameters);
                if (chosen.Count == 0)
                {
                    // Nothing was found.
                    throw new NullReferenceException("No needed data found.");
                }
            }
            else if (choice == 2)
            {
                string[] parameters = GetParameters();
                chosen = Filtration.Filter(false, data, parameters);
                if (chosen.Count == 0)
                {
                    // Nothing was found.
                    throw new NullReferenceException("No needed data found.");
                }
            }
            else if (choice == 3)
            {
                chosen = Sorting.Sort(data);
            }
            else if (choice == 4)
            {
                chosen = Sorting.Sort(data, alphabetical: false);
            }
        }
        catch (ArgumentException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
            chosen = null;
        }
        catch (NullReferenceException e)
        {
            Console.Clear();
            Console.WriteLine(e.Message);
            chosen = null;
        }
        
        return chosen;
    }

    /// <summary>
    /// Saves data to file.
    /// </summary>
    /// <param name="headers">String array of column headers.</param>
    /// <param name="dataToSave">List of Polyclinics objects. Info about them will be saved.</param>
    public static void SaveData(string[] headers, List<Polyclinics> dataToSave)
    {

        if (dataToSave != null)
        {
            Sides side2 = ChooseSide();
            ShowData(side2, headers, dataToSave, ItemsAmount(dataToSave));
            Console.Clear();
        }
        else
        {
            return;
        }
        
        while (true)
        {
            try
            {
                Saving.SPath = GetPath(toRead: false);
                string savingStatus = Saving.Save(GetSavingMode(), headers, dataToSave);
                Console.WriteLine(savingStatus);
                break;
            }
            catch (ArgumentException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            catch (NullReferenceException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
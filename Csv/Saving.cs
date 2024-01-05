using System.Text;
using PolyclinicsInfo;

namespace Csv;

/// <summary>
/// Save some data about polyclinics into file with .csv extension.
/// </summary>
public static class Saving
{
    /// <summary>
    /// Pole which keeps path to file for saving info into.
    /// </summary>
    private static string _sPath;

    private static bool CorrectPath(string path)
    {
        char[] badChars = Path.GetInvalidPathChars();
        for (int i = 0; i < badChars.Length; i++)
        {
            if (path.Contains(badChars[i]))
            {
                return false;
            }
        }

        return true;
    }
    
    /// <summary>
    /// Converts data to strings.
    /// </summary>
    /// <param name="headers">String array with headers of dataset.</param>
    /// <param name="polyclinics">List of Polyclinics objects. It's items contains information about some polyclinics.</param>
    /// <param name="recordNumber">First record number of saving data. Default value is 1.</param>
    /// <returns></returns>
    private static string[] MakeDataGoodForSaving(string[] headers, List<Polyclinics> polyclinics, int recordNumber = 1)
    {
        string[] toSave = new string[polyclinics.Count + 1];
        StringBuilder head = new StringBuilder();
        foreach (string columnHead in headers)
        {
            head.AppendFormat($"{columnHead};");
        }

        toSave[0] = head.ToString();

        for (int i = 1; i <= polyclinics.Count; i++)
        {
            toSave[i] = string.Concat(Convert.ToString(recordNumber++), ";", polyclinics[i - 1].ToString());
        }

        return toSave;
    }
    
    /// <summary>
    /// Property to get and set value to path to file.
    /// Also checks path for containing invalid symbols and .csv extension.
    /// </summary>
    /// <exception cref="ArgumentException">File hasn't got a .csv extension or path contains invalid chars.</exception>
    public static string SPath
    {
        get => _sPath;

        set
        {
            if (value.Length > 4 && string.Equals(value[^4..], ".csv"))
            {
                if (CorrectPath(value))
                {
                    _sPath = value;   
                }
                else
                {
                    // Path has got invalid symbols -> path is wrong.
                    throw new ArgumentException("Seems like there are banned symbols in your path.");
                }
            }
            else
            {
                // File extension isn't a .csv -> it's wrong.
                throw new ArgumentException("I can save data only in '.csv' files.");
            }
        }
    }
    
    /// <summary>
    /// Saves data into file.
    /// </summary>
    /// <param name="mode">Mode of saving.
    /// 1 - Write info into new file.
    /// 2 - Rewrite existed file.
    /// 3 - Append new info into existed file.</param>
    /// <param name="headers">Heading of data columns.</param>
    /// <param name="data">List of Polyclinics objects. It's items contains information about some polyclinics.</param>
    /// <returns>String with status of saving.</returns>
    /// <exception cref="ArgumentException">Writing data into existed file when mode is '1'.
    /// While rewriting file, when file doesn't exist and mode is '2'.
    /// While appending data, when file doesn't exist and mode is '3'.</exception>
    public static string Save(int mode, string[] headers, List<Polyclinics> data)
    {
        string status = "";
        switch (mode)
        {
            case 1:
            {
                if (File.Exists(_sPath))
                {
                    // Path to file is wrong -> wrong argument.
                    throw new ArgumentException("File already exists.");
                }
                else
                {
                    File.WriteAllLines(_sPath, MakeDataGoodForSaving(headers, data));
                    status = "Data saved to a new file.";
                }

                break;
            }
            case 2:
            {
                if (!File.Exists(_sPath))
                {
                    // Path to file is wrong -> wrong argument.
                    throw new ArgumentException("There is no file you're looking for.");
                }
                else
                {
                    File.WriteAllLines(_sPath, MakeDataGoodForSaving(headers, data));
                    status = "File with some data has been rewritten.";
                }

                break;
            }
            case 3:
            {
                if (!File.Exists(_sPath))
                {
                    // Path to file is wrong -> wrong argument.
                    throw new ArgumentException("There is no file you're looking for.");
                }
                else
                {
                    int lastRecordNumber = 1;
                    string[] existedData = Reading.Read(_sPath);
                    if (existedData != null)
                    {
                        if (existedData.Length > 1)
                        {
                            lastRecordNumber = Convert.ToInt32(Reading.CsvSplit(existedData[^1])[0]);
                            File.AppendAllLines(_sPath, MakeDataGoodForSaving(headers, data, lastRecordNumber + 1)[1..]);
                        }
                        else
                        {
                            File.AppendAllLines(_sPath, MakeDataGoodForSaving(headers, data));
                        }
                    }
                    else
                    {
                        MakeDataGoodForSaving(headers, data);
                    }
                    
                    status = "Data has been added to the file.";
                }

                break;
            }
        }

        return status;
    }
}
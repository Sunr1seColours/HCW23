using System.Text;

namespace Csv;

/// <summary>
/// Class that does some operations with reading files.
/// These files must be in .csv extension and satisfy some format. 
/// </summary>
public static class Reading
{
    /// <summary>
    /// Pole which keeps path to file for reading.
    /// </summary>
    private static string _rPath;

    /// <summary>
    /// Checks path to file for invalid symbols (Invalid symbols defined by OS).
    /// </summary>
    /// <param name="path">Path to check.</param>
    /// <returns>'True' if path doesn't contain any invalid symbols.
    /// 'False' if path contains invalid symbols.</returns>
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
    /// Checks head format.
    /// </summary>
    /// <param name="heading">String array which contains headers for each column.</param>
    /// <returns>'True' if every header is correct.
    /// 'False' if it isn't.</returns>
    private static bool HeadingValidation(string[] heading)
    {
        bool rightHeading = string.Equals(heading[0], "ROWNUM");
        rightHeading = string.Equals(heading[1], "ShortName");
        rightHeading = string.Equals(heading[2], "AdmArea");
        rightHeading = string.Equals(heading[3], "District");
        rightHeading = string.Equals(heading[4], "PostalCode");
        rightHeading = string.Equals(heading[5], "Address");
        rightHeading = string.Equals(heading[6], "ChiefName");
        rightHeading = string.Equals(heading[7], "ChiefPosition");
        rightHeading = string.Equals(heading[8], "ChiefGender");
        rightHeading = string.Equals(heading[9], "ChiefPhone");
        rightHeading = string.Equals(heading[10], "PublicPhone");
        rightHeading = string.Equals(heading[11], "Fax");
        rightHeading = string.Equals(heading[12], "Email");
        rightHeading = string.Equals(heading[13], "CloseFlag");
        rightHeading = string.Equals(heading[14], "CloseReason");
        rightHeading = string.Equals(heading[15], "CloseDate");
        rightHeading = string.Equals(heading[16], "ReopenDate");
        rightHeading = string.Equals(heading[17], "PaidServicesInfo");
        rightHeading = string.Equals(heading[18], "FreeServicesInfo");
        rightHeading = string.Equals(heading[19], "WorkingHours");
        rightHeading = string.Equals(heading[20], "ClarificationOfWorkingHours");
        rightHeading = string.Equals(heading[21], "Specialization");
        rightHeading = string.Equals(heading[22], "BeneficialDrugPrescriptions");
        rightHeading = string.Equals(heading[23], "ExtraInfo");
        rightHeading = string.Equals(heading[24], "AddressUNOM");
        rightHeading = string.Equals(heading[25], "Point X");
        rightHeading = string.Equals(heading[26], "Point Y");
        rightHeading = string.Equals(heading[27], "GLOBALID");

        return rightHeading;
    }
    
    /// <summary>
    /// Property to get and set value to path to file.
    /// Also checks path for containing invalid symbols and .csv extension.
    /// </summary>
    /// <exception cref="ArgumentException">File hasn't got a .csv extension or path contains invalid chars.</exception>
    public static string RPath
    {
        get => _rPath;

        set
        {
            if (value.Length > 4 && string.Equals(value[^4..], ".csv"))
            {
                if (CorrectPath(value))
                {
                    _rPath = value;   
                }
                else
                {
                    // Path has got invalid symbols -> path is wrong.
                    throw new ArgumentException("Seems like there are banned symbols in your path.");
                }
            }
            else
            {
                // File hasn't got a .csv extension -> path is wrong.
                throw new ArgumentException("I can open only '.csv' files.");
            }
        }
    }
    
    /// <summary>
    /// Reads file. Path to it is in private pole '_rPath'.
    /// Also checks headers of file columns.
    /// </summary>
    /// <returns>String array which contains each line of file.</returns>
    /// <exception cref="ArgumentException">Some column headers are incorrect.</exception>
    public static string[] Read()
    {
        string[] readed = File.ReadAllLines(_rPath);
        if (HeadingValidation(CsvSplit(readed[0])))
        {
            return readed;
        }

        // Column headers are wrong -> we throw ArgumentException.
        throw new ArgumentException("Wrong data format.");
    }
    
    /// <summary>
    /// Overload of Read() method with 'path' parameter. Reads file. Also checks headers of file columns.
    /// </summary>
    /// <param name="path">Path to file.</param>
    /// <returns>String array which contains each line of file.</returns>
    /// <exception cref="ArgumentException">Some column headers are incorrect.</exception>
    public static string[] Read(string path)
    {
        string[] readed = File.ReadAllLines(path);
        if (HeadingValidation(CsvSplit(readed[0])))
        {
            return readed;
        }

        // Column headers are wrong -> we throw ArgumentException.
        throw new ArgumentException("Wrong data format.");
    }
    
    /// <summary>
    /// Converts string to array of strings by csv-separators. Quotes is also taken into account.
    /// </summary>
    /// <param name="row">String to convert.</param>
    /// <returns>Array of strings splitted by classical csv separators.</returns>
    /// <exception cref="ArgumentException">Quote isn't ended.</exception>
    public static string[] CsvSplit(string row)
    {
        // This we need to not split if we are in quote.
        bool inQuote = false;
        // Classical separators for csv-files.
        char[] seps = {';', ',', '\t', '|'};
        // In our data must be 28 columns.
        string[] splitedRow = new string[28];
        // StringBuilder object to collect symbols.
        StringBuilder item = new StringBuilder();
        int n = 0;
        foreach (char symbol in row)
        {
            if (symbol == '"')
            {
                inQuote = !inQuote;
            }
            else
            {
                if (!inQuote & seps.Contains(symbol))
                {
                    splitedRow[n] = item.ToString();
                    n++;
                    // Make StringBuilder collector hollow.
                    item.Remove(0, item.Length);
                }
                else
                {
                    item.Append(symbol);
                }
            }
        }
        // It means that there are odd amount of quotation marks in line => something wrong with quotes => wrong data.
        if (inQuote)
        {
            throw new ArgumentException("Wrong data.");
        }
        
        return splitedRow;
    }
}
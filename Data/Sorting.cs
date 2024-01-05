using PolyclinicsInfo;

namespace Data;

/// <summary>
/// Class for sorting data of Polyclinics objects.
/// </summary>
public static class Sorting
{
    /// <summary>
    /// Sort array of Polyclinics objects by administration area parameter.
    /// </summary>
    /// <param name="data">Array of Polyclinics objects to sort.</param>
    /// <param name="alphabetical">If this is 'true', sorting will be in alphabetical order.
    /// If this isn't - in reversed alphabetical order.
    /// Default value is 'true'.</param>
    /// <returns>List of sorted Polyclinics objects.</returns>
    public static List<Polyclinics> Sort(Polyclinics[] data, bool alphabetical = true)
    {
        Polyclinics[] sorted = new Polyclinics[data.Length];
        Array.Copy(data, sorted, data.Length);

        for (int i = 0; i < sorted.Length; i++)
        {
            for (int j = 0; j < sorted.Length - i - 1; j++)
            {
                if (alphabetical)
                {
                    if (String.CompareOrdinal(sorted[j].District.ToLower(), 
                            sorted[j + 1].District.ToLower()) > 0)
                    {
                        (sorted[j], sorted[j + 1]) = (sorted[j + 1], sorted[j]);
                    }    
                }
                else
                {
                    if (String.CompareOrdinal(sorted[j].District.ToLower(), 
                            sorted[j + 1].District.ToLower()) < 0)
                    {
                        (sorted[j], sorted[j + 1]) = (sorted[j + 1], sorted[j]);
                    }    
                }
            }
        }
        
        return sorted.ToList();
    }
}
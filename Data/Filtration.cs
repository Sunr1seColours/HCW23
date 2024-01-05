using PolyclinicsInfo;

namespace Data;

/// <summary>
/// Class for filtration data of Polyclinics objects. 
/// </summary>
public static class Filtration
{
    /// <summary>
    /// Make filtration of array of Polyclinics objects.
    /// </summary>
    /// <param name="admArea">If this is 'true', filtration will be with values of administration areas.
    /// If this is 'false', selection will be with values of info about paid services.</param>
    /// <param name="data">Array of Polyclinics objects to select some part of it.</param>
    /// <param name="parameters">Values of parameters to make filtration of.</param>
    /// <returns>List of Polyclinics objects selected by 'parameters'.</returns>
    public static List<Polyclinics> Filter(bool admArea, Polyclinics[] data, params string[] parameters)
    {
        List<Polyclinics> chosen = new List<Polyclinics>();
        if (!admArea)
        { 
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < parameters.Length; j++)
                {
                    if (string.Equals(data[i].PaidServicesInfo, parameters[j]))
                    {
                        chosen.Add(data[i]);
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < parameters.Length; j++)
                {
                    if (string.Equals(data[i].AdmArea, parameters[j]))
                    {
                        chosen.Add(data[i]);
                        break;
                    }
                }
            }
        }
        
        return chosen;
    }    
}
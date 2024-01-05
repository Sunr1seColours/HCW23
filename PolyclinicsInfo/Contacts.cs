namespace PolyclinicsInfo;

/// <summary>
/// Structure with information about polyclinics contacts. 
/// </summary>
public readonly struct Contacts
{
    /// <summary>
    /// Private pole which contains info about polyclinic's postal code.
    /// </summary>
    private readonly string _postalCode;
    /// <summary>
    /// Private pole which contains info about city where polyclinic is.
    /// </summary>
    private readonly string _city;
    /// <summary>
    /// Private pole which contains info about street where polyclinic is.
    /// </summary>
    private readonly string _street;
    /// <summary>
    /// Private pole which contains info about polyclinic's number of building.
    /// </summary>
    private readonly string _building;
    /// <summary>
    /// Private pole which contains info about polyclinic's fax.
    /// </summary>
    private readonly string _fax;
    /// <summary>
    /// Private pole which contains info about polyclinic's chief's phone.
    /// </summary>
    private readonly string _chiefPhone;
    /// <summary>
    /// Private pole which contains info about polyclinic's contact phone.
    /// </summary>
    private readonly string _publicPhone;
    /// <summary>
    /// Private pole which contains info about polyclinic's contact email.
    /// </summary>
    private readonly string _email;

    /// <summary>
    /// Get-only property to get value of polyclinic's postal code.
    /// </summary>
    public string PostalCode => _postalCode;

    /// <summary>
    /// Get-only property to get value of polyclinic's city.
    /// </summary>
    public string City => _city;

    /// <summary>
    /// Get-only property to get value of polyclinic's street.
    /// </summary>
    public string Street => _street;
    
    /// <summary>
    /// Get-only property to get value of polyclinic's building.
    /// </summary>
    public string Building => _building;

    /// <summary>
    /// Get-only property to get value of polyclinic's fax.
    /// </summary>
    public string Fax => _fax;

    /// <summary>
    /// Get-only property to get value of polyclinic's chief's phone.
    /// </summary>
    public string ChiefPhone => _chiefPhone;

    /// <summary>
    /// Get-only property to get value of polyclinic's contact phone.
    /// </summary>
    public string PublicPhone => _publicPhone;

    /// <summary>
    /// Get-only property to get value of polyclinic's contact email.
    /// </summary>
    public string Email => _email;

    /// <summary>
    /// Base structure constructor.
    /// </summary>
    public Contacts() : this(new string[] { "" }) { } 

    /// <summary>
    /// Constructor of Contacts structure.
    /// </summary>
    /// <param name="elements">String array with all information about polyclinic.</param>
    /// <exception cref="ArgumentException">String array with polyclinic info hasn't got 28 elements or
    /// info about address is wrong.</exception>
    public Contacts(string[] elements)
    {
        if (elements.Length != 28)
        {
            _postalCode = "";
            _city = "";
            _street = "";
            _building = "";
            _fax = "";
            _chiefPhone = "";
            _publicPhone = "";
            _email = "";
            // File has got wrong amount of columns -> data is wrong. So we throw an ArgumentException.
            throw new ArgumentException("Wrong data.");
        }
        else
        {
            _postalCode = elements[4];
            _chiefPhone = elements[9];
            _publicPhone = elements[10];
            _fax = elements[11];
            _email = elements[12];

            string[] address = elements[5].Split(',');
            switch (address.Length)
            {
                case < 1:
                case > 4:
                    _city = "Unknown";
                    _street = "Unknown";
                    _building = "Unknown";
                    break;
                case 1:
                    if (elements[5].Length > 0)
                    {
                        _city = "Unknown";
                        _street = address[0];
                        _building = "Unknown";
                        break;
                    }
                    else
                    {
                        _city = "Unknown";
                        _street = "Unknown";
                        _building = "Unknown";
                        break;
                    }
                case 2:
                    _city = "Unknown";
                    _street = address[0];
                    _building = address[1];
                    break;
                case 3:
                    _city = "Unknown";
                    _street = address[0];
                    _building = string.Concat(address[1..]);
                    break;
                case 4:
                    _city = address[0].Trim();
                    _street = address[1].Trim();
                    _building = string.Concat(address[2..]).Trim();
                    break;
            }
        }
    }

    /// <summary>
    /// Property to get address information about polyclinic in one string element. 
    /// </summary>
    /// <returns>Address info in one string.</returns>
    public string ConcatAddress => $"{_city}, {_street}, {_building}";
}
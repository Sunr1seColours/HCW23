using System.Text;

namespace PolyclinicsInfo;

public class Polyclinics
{
    /// <summary>
    /// Private pole which contains info about polyclinic's short name.
    /// </summary>
    private readonly string _shortName;
    /// <summary>
    /// Private pole which contains info about administration area where polyclinic is.
    /// </summary>
    private readonly string _admArea;
    /// <summary>
    /// Private pole which contains info about district where polyclinic is.
    /// </summary>
    private readonly string _district;
    /// <summary>
    /// Private pole which contains info about polyclinic's chief's name.
    /// </summary>
    private readonly string _chiefName;
    /// <summary>
    /// Private pole which contains info about polyclinic's chief's position.
    /// </summary>
    private readonly string _chiefPosition;
    /// <summary>
    /// Private pole which contains info about polyclinic's chief's gender.
    /// </summary>
    private readonly string _chiefGender;
    /// <summary>
    /// Private pole which shows if polyclinic is closed.
    /// </summary>
    private readonly string _closeFlag;
    /// <summary>
    /// Private pole which contains close reason if it exists.
    /// </summary>
    private readonly string _closeReason;
    /// <summary>
    /// Private pole which contains close data if it exists.
    /// </summary>
    private readonly string _closeDate;
    /// <summary>
    /// Private pole which contains reopen data if it exists.
    /// </summary>
    private readonly string _reopenDate;
    /// <summary>
    /// Private pole which contains info about paid services which polyclinic provide.
    /// </summary>
    private readonly string _paidServicesInfo;
    /// <summary>
    /// Private pole which contains info about free services which polyclinic provide.
    /// </summary>
    private readonly string _freeServicesInfo;
    /// <summary>
    /// Private pole which contains info about hours when polyclinic works.
    /// </summary>
    private readonly string _workingHours;
    /// <summary>
    /// Private pole which contains info about clarification of working hours.
    /// </summary>
    private readonly string _clarificationOfWorkingHours;
    /// <summary>
    /// Private pole which contains info polyclinic's specialization.
    /// </summary>
    private readonly string _specialization;
    /// <summary>
    /// Private pole which contains info about beneficial drug prescriptions.
    /// </summary>
    private readonly string _beneficialDrugPrescriptions;
    /// <summary>
    /// Private pole which contains any extra info about polyclinis.
    /// </summary>
    private readonly string _extraInfo;
    /// <summary>
    /// Private pole which contains info about clarification of working hours.
    /// </summary>
    private readonly string _addressUnom;
    /// <summary>
    /// Private pole which contains polyclinic's contacts info.
    /// </summary>
    private readonly Contacts _contactInfo;
    /// <summary>
    /// Private pole with horizontal coordinate.
    /// </summary>
    private readonly string _pointX;
    /// <summary>
    /// Private pole with vertical coordinate.
    /// </summary>
    private readonly string _pointY;
    /// <summary>
    /// Private pole which contains global ID of polyclinic.
    /// </summary>
    private readonly string _globalId;
    
    /// <summary>
    /// Get-only property to get value of administration area where polyclinic is.
    /// </summary>
    public string AdmArea => _admArea;

    /// <summary>
    /// Get-only property to get value of district where polyclinic is.
    /// </summary>
    public string District => _district;
    
    /// <summary>
    /// Get-only property to get info about paid services which polyclinic provides.
    /// </summary>
    public string PaidServicesInfo => _paidServicesInfo;
    
    /// <summary>
    /// Base constructor for Polyclinics object.
    /// </summary>
    public Polyclinics() : this(new string[] { "" }){}
    
    /// <summary>
    /// Creates new Polyclinics object.
    /// </summary>
    /// <param name="elements">String array of data about polyclinic.</param>
    /// <exception cref="ArgumentException">Amount of information poles about polyclinic is different from 28.</exception>
    public Polyclinics(string[] elements)
    {
        if (elements.Length != 28)
        {
            _shortName = "";
            _admArea = "";
            _district = "";
            _chiefName = "";
            _chiefPosition = "";
            _chiefGender = "";
            _closeFlag = "";
            _closeReason = "";
            _closeDate = "";
            _reopenDate = "";
            _paidServicesInfo = "";
            _freeServicesInfo = "";
            _workingHours = "";
            _clarificationOfWorkingHours = "";
            _specialization = "";
            _beneficialDrugPrescriptions = "";
            _extraInfo = "";
            _addressUnom = "";
            _pointX = "";
            _pointY = "";
            _globalId = "";
            _contactInfo = new Contacts();
            // File has got wrong amount of columns -> data is wrong. So we throw an ArgumentException.
            throw new ArgumentException("Wrong data.");
        }
        else
        {
            _shortName = elements[1];
            _admArea = elements[2];
            _district = elements[3];
            _chiefName = elements[6];
            _chiefPosition = elements[7];
            _chiefGender = elements[8];
            _closeFlag = elements[13];
            _closeReason = elements[14];
            _closeDate = elements[15];
            _reopenDate = elements[16];
            _paidServicesInfo = elements[17];
            _freeServicesInfo = elements[18];
            _workingHours = elements[19];
            _clarificationOfWorkingHours = elements[20];
            _specialization = elements[21];
            _beneficialDrugPrescriptions = elements[22];
            _extraInfo = elements[23];
            _addressUnom = elements[24];
            _pointX = elements[25];
            _pointY = elements[26];
            _globalId = elements[27];
            _contactInfo = new Contacts(elements);
        }
    }

    /// <summary>
    /// Converts all info about polyclinic to string. All poles are in quotes,and they're separeted by ';'.  
    /// </summary>
    /// <returns>String with info about polyclinic.</returns>
    public override string ToString()
    {
        StringBuilder allInOne = new StringBuilder();
        allInOne.Append($"\"{_shortName}\";");
        allInOne.Append($"\"{_admArea}\";");
        allInOne.Append($"\"{_district}\";");
        allInOne.Append($"\"{_contactInfo.PostalCode}\";");
        allInOne.Append($"\"{_contactInfo.City}, {_contactInfo.Street}, {_contactInfo.Building}\";");
        allInOne.Append($"\"{_chiefName}\";");
        allInOne.Append($"\"{_chiefPosition}\";");
        allInOne.Append($"\"{_chiefGender}\";");
        allInOne.Append($"\"{_contactInfo.ChiefPhone}\";");
        allInOne.Append($"\"{_contactInfo.PublicPhone}\";");
        allInOne.Append($"\"{_contactInfo.Fax}\";");
        allInOne.Append($"\"{_contactInfo.Email}\";");
        allInOne.Append($"\"{_closeFlag}\";");
        allInOne.Append($"\"{_closeReason}\";");
        allInOne.Append($"\"{_closeDate}\";");
        allInOne.Append($"\"{_reopenDate}\";");
        allInOne.Append($"\"{_paidServicesInfo}\";");
        allInOne.Append($"\"{_freeServicesInfo}\";");
        allInOne.Append($"\"{_workingHours}\";");
        allInOne.Append($"\"{_clarificationOfWorkingHours}\";");
        allInOne.Append($"\"{_specialization}\";");
        allInOne.Append($"\"{_beneficialDrugPrescriptions}\";");
        allInOne.Append($"\"{_extraInfo}\";");
        allInOne.Append($"\"{_addressUnom}\";");
        allInOne.Append($"\"{_pointX}\";");
        allInOne.Append($"\"{_pointY}\";");
        allInOne.Append($"\"{_globalId}\";");

        return allInOne.ToString();
    }

    /// <summary>
    /// Converts all info about polyclinic to List of strings.
    /// </summary>
    /// <returns>List of strings with info about polyclinic.</returns>
    public List<string> ToStringList()
    {
        List<string> strInfo = new List<string>
        {
            _shortName,
            _admArea,
            _district,
            _contactInfo.PostalCode,
            _contactInfo.ConcatAddress,
            _chiefName,
            _chiefPosition,
            _chiefGender,
            _contactInfo.ChiefPhone,
            _contactInfo.PublicPhone,
            _contactInfo.Fax,
            _contactInfo.Email,
            _closeFlag,
            _closeReason,
            _closeDate,
            _reopenDate,
            _paidServicesInfo,
            _freeServicesInfo,
            _workingHours,
            _clarificationOfWorkingHours,
            _specialization,
            _beneficialDrugPrescriptions,
            _extraInfo,
            _addressUnom,
            _pointX,
            _pointY,
            _globalId
        };

        return strInfo;
    }
}
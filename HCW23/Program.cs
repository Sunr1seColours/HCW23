using static HCW23.ConsoleWork;
using static HCW23.Catcher;
using PolyclinicsInfo;


namespace HCW23;

class Program
{
    static void Main(string[] args)
    {
        // var 10.
        SetConsoleColor(false);
        bool repeat = true;
        do
        {
            if (GetData(out string[] headers, out Polyclinics[] polyclinics))
            {
                Sides side1 = ChooseSide();
                ShowData(side1, headers, polyclinics, ItemsAmount(polyclinics));

                int point = DynamicMenu();

                if (point != 5)
                {
                    List<Polyclinics> newData = MenuRealization(point, polyclinics);
                    SaveData(headers, newData);
                }
            }
            Exit(ref repeat);
        } while (repeat);    
    }
    
}
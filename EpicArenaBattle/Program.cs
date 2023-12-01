// See https://aka.ms/new-console-template for more information
using EpicArenaBattle;

General.LogEvents("Epic Arena Fight!");
General.LogEvents("Enter number of heroes:");
string line =  Console.ReadLine();

try
{
    int i = Int32.Parse(line);
    Arena arena = new Arena(i);
    arena.DoAllFights();
}
catch(Exception ex)
{
    General.LogEvents(ex.Message);
}
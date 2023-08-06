namespace SimulacrumSharp.SimulationAPI.Helpers.Interfaces
{
    public interface ICommonHelper
    {
        string FormatListOfNamesString(List<string> names);
        T GetRandomElement<T>(IEnumerable<T> list);
    }
}

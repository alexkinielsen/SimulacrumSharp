namespace SimulacrumSharp.Backend.Helpers.Interfaces
{
    public interface ICommonHelper
    {
        string FormatListOfNamesString(List<string> names);
        T GetRandomElement<T>(IEnumerable<T> list);
    }
}

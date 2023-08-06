using SimulacrumSharp.SimulationAPI.Helpers.Interfaces;

namespace SimulacrumSharp.SimulationAPI.Helpers
{
    public class CommonHelper : ICommonHelper
    {
        public T GetRandomElement<T>(IEnumerable<T> collection)
        {
            var random = new Random();
            var collectionSize = collection.Count();
            var index = random.Next(collection.Count());

            return collection.ToList()[index];
        }

        public string FormatListOfNamesString(List<string> names)
        {
            var nameString = string.Empty;
            if (names.Count >= 3)
            {

                for (var i = 0; i < names.Count; i++)
                {
                    nameString += names[i];
                    if (i < names.Count - 2)
                    {
                        nameString += ", ";
                        continue;
                    }
                    if (i < names.Count - 1)
                    {
                        nameString += ", and ";
                        continue;
                    }
                    break;
                }
            }
            else if (names.Count.Equals(2))
            {
                nameString = string.Join(" and ", names);
            }
            else
            {
                nameString = names[0];
            }

            return nameString;
        }
    }
}

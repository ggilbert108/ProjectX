using System.Collections.Generic;

namespace ProjectX.Finite
{
    public static class Util
    {
        public static int AddRangeToSet<T>(HashSet<T> set, IEnumerable<T> collection, bool tag = false)
        {
            int total = 0;
            foreach (T value in collection)
            {
                if (set.Add(value))
                {
                    total++;
                }
                else if(tag)
                {
                    int n = 0;
                }
            }
            return total;
        }

        public static bool PathSetContainsState(HashSet<Path> paths, State state)
        {
            foreach (Path path in paths)
            {
                if (path.State.Equals(state))
                    return true;
            }
            return false;
        }

        public static string GetPathSetString(HashSet<Path> paths)
        {
            string result = "";
            foreach (Path path in paths)
            {
                result += path.State.Label == "" ? "---" : path.State.Label;
                result += "   ";
            }
            return result;
        }
    }
}
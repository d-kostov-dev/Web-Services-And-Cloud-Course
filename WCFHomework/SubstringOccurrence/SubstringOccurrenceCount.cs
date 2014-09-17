namespace SubstringOccurrence
{
    using System;

    public class SubstringOccurrenceCount : ISubstingOccurrenceCount
    {
        public int CountSubstringOccurrence(string mainString, string searchString)
        {
            int count = 0;
            int n = 0;

            if (!string.IsNullOrEmpty(searchString))
            {
                while ((n = mainString.IndexOf(searchString, n, StringComparison.InvariantCulture)) != -1)
                {
                    n += searchString.Length;
                    count++;
                }
            }

            return count;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MentorSpeedDatingApp.ExtraFunctions
{
    public static class ExtensionMethods
    {
        public static bool CompareCollectionsOnEqualContent<T, T1>(this IEnumerable<T> source, IEnumerable<T1> target, Func<T, T1, bool> predicate)
        {
            if (source.Count() != target.Count())
            {
                return false;
            }

            var sourceEnamerable = source.ToList();
            var targetEnamerable = target.ToList();

            for (int i = 0; i < sourceEnamerable.Count(); i++)
            {
                if (!predicate(sourceEnamerable[i], targetEnamerable[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

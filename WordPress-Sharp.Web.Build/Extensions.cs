using System.Collections.Generic;
using System.Text;

namespace WordPress_Sharp.Web.Build
{
    public static class Extensions
    {
        public static string Concat(this List<string> list, string delim, string surround)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.AppendFormat("{0}{1}{0}", surround, list[i]);
                if (i < list.Count - 1)
                    builder.Append(delim);
            }
            return builder.ToString();
        }
    }
}

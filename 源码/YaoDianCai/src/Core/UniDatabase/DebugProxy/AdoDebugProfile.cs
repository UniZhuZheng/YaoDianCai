using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using log4net;

namespace Uni.Core.Database.DebugProxy
{
    class AdoDebugProfile
    {
        private readonly static ILog logger = LogManager.GetLogger("Uni.ADOSQL.DEBUG");

        public static void DebugOut(IDbCommand cmd, string method, TimeSpan duration)
        {
            AdoDebugEventArgs args = new AdoDebugEventArgs("Command." + method, duration, cmd);

            ThreadContext.Properties["duration"] = args.Duration.TotalMilliseconds;
            ThreadContext.Properties["sql"] = RemoveWhiteSpaces(args.Sql);
            ThreadContext.Properties["sqlParams"] = RemoveWhiteSpaces(args.SqlParams);

            logger.DebugFormat("[{0} {1}] {2} | {3}", ThreadContext.Properties["duration"], args.SqlMethod,
                string.IsNullOrEmpty((string)ThreadContext.Properties["sql"]) ? "" : ThreadContext.Properties["sql"],
                string.IsNullOrEmpty((string)ThreadContext.Properties["sqlParams"]) ? "" : ThreadContext.Properties["sqlParams"]);
        }

        public static void DebugOut(IDbConnection conn, string method, TimeSpan duration)
        {
            AdoDebugEventArgs args = new AdoDebugEventArgs("Command." + method, duration);

            ThreadContext.Properties["duration"] = args.Duration.TotalMilliseconds;
            ThreadContext.Properties["sql"] = RemoveWhiteSpaces(args.Sql);
            ThreadContext.Properties["sqlParams"] = RemoveWhiteSpaces(args.SqlParams);

            logger.DebugFormat("[{0} {1}] {2} | {3}", ThreadContext.Properties["duration"], args.SqlMethod,
                string.IsNullOrEmpty((string)ThreadContext.Properties["sql"]) ? "" : ThreadContext.Properties["sql"],
                string.IsNullOrEmpty((string)ThreadContext.Properties["sqlParams"]) ? "" : ThreadContext.Properties["sqlParams"]);
        }

        public static void DebugOut(IDbTransaction tx, string method, TimeSpan duration)
        {
            AdoDebugEventArgs args = new AdoDebugEventArgs("Command." + method, duration);

            ThreadContext.Properties["duration"] = args.Duration.TotalMilliseconds;
            ThreadContext.Properties["sql"] = RemoveWhiteSpaces(args.Sql);
            ThreadContext.Properties["sqlParams"] = RemoveWhiteSpaces(args.SqlParams);

            logger.DebugFormat("[{0} {1}] {2} | {3}", ThreadContext.Properties["duration"], args.SqlMethod,
                string.IsNullOrEmpty((string)ThreadContext.Properties["sql"]) ? "" : ThreadContext.Properties["sql"],
                string.IsNullOrEmpty((string)ThreadContext.Properties["sqlParams"]) ? "" : ThreadContext.Properties["sqlParams"]);
        }

        private static string RemoveWhiteSpaces(string str)
        {
            return !string.IsNullOrEmpty(str)
                ? str.Replace(Environment.NewLine, " ").Replace("\n", "").Replace("\r", "").Replace("\t", " ")
                : string.Empty;
        }
    }
}

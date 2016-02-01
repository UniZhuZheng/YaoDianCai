using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Uni.Core.Database.DebugProxy
{
    class AdoDebugEventArgs : EventArgs
    {
        public TimeSpan Duration  { get; private set; }
        public string   Sql       { get; private set; }
        public string   SqlMethod { get; private set; }
        public string   SqlParams { get; private set; }


        public AdoDebugEventArgs(string method, TimeSpan duration)
            : this(method, duration, null)
        {
        }

        public AdoDebugEventArgs(string method, TimeSpan duration, IDbCommand command)
        {
            SqlMethod = method;
            Duration = duration;

            if (command != null)
            {
                Sql = command.CommandText;

                if (0 < command.Parameters.Count)
                {
                    var stringBuilder = new StringBuilder();
                    foreach (IDbDataParameter p in command.Parameters)
                    {
                        if (!string.IsNullOrEmpty(p.ParameterName))
                        {
                            stringBuilder.AppendFormat("{0}=", p.ParameterName);
                        }

                        stringBuilder.AppendFormat("{0}, ", p.Value == null ? "NULL" : p.Value.ToString());
                    }
                    SqlParams = stringBuilder.ToString(0, stringBuilder.Length - 2);
                }
            }
        }
    }
}

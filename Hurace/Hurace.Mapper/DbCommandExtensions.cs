using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Hurace.Core.Mapper
{
    public static class DbCommandExtensions
    {
        public static void AddValue(this DbCommand command, string name, object value)
        {
            DbParameter dbParam = command.CreateParameter();
            dbParam.ParameterName = name;
            dbParam.Value = value;
            command.Parameters.Add(dbParam);
        }
    }
}

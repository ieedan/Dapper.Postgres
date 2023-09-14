using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Postgres
{
    /// <summary>
    /// Extends Dapper's DynamicParameters but modifies the behavior slightly so that every attribute is added to the list.
    /// <code>
    /// // Use [NotMapped] to prevent mapping
    /// [NotMapped]
    /// public int Age { get; set; } 
    /// </code>
    /// </summary>
    public class DynamicFunctionParameters : DynamicParameters
    {
        public DynamicFunctionParameters() : base() { }

        public DynamicFunctionParameters(object obj)
        {
            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetCustomAttribute(typeof(NotMappedAttribute)) == null)
                {
                    this.Add(property.Name, property.GetValue(obj));
                }
            }
        }

        /// <summary>
        /// Returns a string with the parameterized function parameters.
        /// 
        /// <code>
        /// string function = "add_user";
        /// 
        /// string query = $"SELECT * FROM {function}({parameters.GetParameterList()});";
        /// 
        /// Console.WriteLine(query); // SELECT * FROM add_user(username := @username, email := @email);
        /// </code>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetParameterList()
        {
            string param = "";

            if (this != null)
            {
                if (this.ParameterNames.Count() == 0) return "";

                var paramNames = this.ParameterNames.ToList();

                for (int i = 0; i < paramNames.Count; i++)
                {
                    if (i > 0)
                    {
                        param += $", {paramNames[i]} := @{paramNames[i]}";
                    }
                    else
                    {
                        param += $"{paramNames[i]} := @{paramNames[i]}";
                    }
                }
            }

            return param;
        }
    }
}

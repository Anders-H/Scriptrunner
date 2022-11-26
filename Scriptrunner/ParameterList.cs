using System;
using System.Collections.Generic;
using System.Linq;

namespace Scriptrunner
{
    public class ParameterList : List<Parameter>
    {
        public string GetValue(string name)
        {
            foreach (var p in this.Where(p => string.Compare(p.Name, name, StringComparison.CurrentCultureIgnoreCase) == 0 && !string.IsNullOrWhiteSpace(p.Value)))
                return p.Value;

            return "";
        }

        public string GetDatatype(string name)
        {
            foreach (var p in this.Where(p => string.Compare(p.Name, name, StringComparison.CurrentCultureIgnoreCase) == 0 && !string.IsNullOrWhiteSpace(p.Datatype)))
                return p.Value;

            return "";
        }

        public void AddIfNotExits(Parameter parameter)
        {
            foreach (var p in this)
            {
                if (string.Compare(p.Name, parameter.Name, StringComparison.CurrentCultureIgnoreCase) != 0)
                    continue;

                if (string.IsNullOrWhiteSpace(p.Value))
                    p.Value = parameter.Value;
                else if (string.IsNullOrWhiteSpace(parameter.Value))
                    parameter.Value = p.Value;

                if (string.IsNullOrEmpty(p.Datatype))
                    p.Datatype = parameter.Datatype;
                else if (string.IsNullOrWhiteSpace(parameter.Datatype))
                    parameter.Datatype = p.Datatype;

                return;
            }

            Add(parameter);
        }
    }
}
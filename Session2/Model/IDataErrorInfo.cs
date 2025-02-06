using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session2.Model
{
    internal interface IDataErrorInfo
    {
        string Error { get; }
        string this[string columnName] { get; }
    }
}

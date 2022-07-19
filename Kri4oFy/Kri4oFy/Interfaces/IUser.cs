using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    internal interface IUser
    {
        string Username { get; set; }

        string Password { get; set; }

        UserTypeEnum Type { get; set; }

        bool CheckLogInInfo(string userame, string password);
    }
}

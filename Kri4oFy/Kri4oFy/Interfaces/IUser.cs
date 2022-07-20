using Kri4oFy.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    public interface IUser
    {
        string Username { get; set; }

        string Password { get; set; }

        UserTypeEnum Type { get; set; }

        string GetFileString { get; }

        bool CheckLogInInfo(string userame, string password);

        
    }
}

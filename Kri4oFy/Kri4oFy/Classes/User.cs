using Kri4oFy.Constants;
using Kri4oFy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Classes
{
    public class User : IUser
    {
        //fields
        private string username;
        private string password;
        private UserTypeEnum type;

        //constructors
        public User(string username, string password, UserTypeEnum type)
        {
            this.Username = username;
            this.Password = password;
            this.Type = type;
        }

        //Properties
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public UserTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual string GetFileString
        {
            get { return $"<user><{Username}>({Password}){{{Type}}}</user>"; }
        }

        public string GetUserFileString
        {
            get { return $"<user><{Username}>({Password}){{{Type}}}</user>"; }
        }

        //methods
        public bool CheckLogInInfo(string userame, string password)
        {
            return this.Username == userame && this.Password == password;
        }

        virtual public string getUserInformation()
        {
            return $"{type} Username:{username} Password:{password}";
        }
    }
}

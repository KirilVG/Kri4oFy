﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kri4oFy.Interfaces
{
    internal interface IDataHelper
    {
        ISpData TakeData();

        void SaveData(ISpData Helper, List<string> changes);
    }
}

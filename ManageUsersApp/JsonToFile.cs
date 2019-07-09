﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageUsersApp
{
    public class JsonToFile
    {
        public static void WriteJson(string _json)
        {

            var name = DateTime.Now;
            StreamWriter SW = new StreamWriter(new FileStream("json\\" + name.ToFileTime().ToString() + ".json", FileMode.Create, FileAccess.Write));
            SW.Write(_json);
            SW.Close();
        }
    }
}

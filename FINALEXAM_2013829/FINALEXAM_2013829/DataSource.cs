using Firebase.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FINALEXAM_2013829
{
    public class DataSource
    {
        public static FirebaseClient db;

        public static void Init()
        {
            if (db != null)
                return;

            db = new FirebaseClient(Constant.firebaseDatabaseUri);
        }
    }
}
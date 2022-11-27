using System;
using System.Collections.Generic;

namespace DriveStructure
{
    public class Directory
    {
        public string Name {get; set;}
        public Directory[] Directories {get; set;}
        public string[] Files {get; set;}


        public static Directory Map(string root_directory)
        {
            string[] dirs = System.IO.Directory.GetDirectories(root_directory);
            string[] files = System.IO.Directory.GetFiles(root_directory);

            Directory ToReturn = new Directory();

            //Set the name
            string? dirname = System.IO.Path.GetFileName(root_directory);
            if (dirname != null)
            {
                ToReturn.Name = dirname;
            }
            

            //Get all of the file names
            List<string> file_names = new List<string>();
            foreach (string s in files)
            {
                string fn = System.IO.Path.GetFileName(s);
                file_names.Add(System.IO.Path.GetFileName(s));
            }
            ToReturn.Files = file_names.ToArray();

            //Get directories
            List<Directory> tadirs = new List<Directory>();
            foreach (string dirroot_directory in dirs)
            {
                Directory? d = null;
                try
                {
                    d = Map(dirroot_directory);
                }
                catch
                {

                }
                if (d != null)
                {
                    tadirs.Add(d);
                }
            }
            ToReturn.Directories = tadirs.ToArray();

            return ToReturn;
        }



        public Directory()
        {
            Name = String.Empty;
            Directories = new Directory[]{};
            Files = new string[]{};
        }

        public string[] Flatten(string root_directory = "")
        {
            List<string> ToReturn = new List<string>();

            //Get my files
            foreach (string s in Files)
            {
                ToReturn.Add(System.IO.Path.Combine(root_directory, s));
            }

            //Get child directory files
            foreach (Directory cd in Directories)
            {
                string[] cdpaths = cd.Flatten(Path.Combine(root_directory, cd.Name));
                ToReturn.AddRange(cdpaths);
            }

            return ToReturn.ToArray();
        }
    
        public string[] Search(string query)
        {
            string[] AllFlattened = Flatten();
            List<string> ToReturn = new List<string>();
            foreach (string s in AllFlattened)
            {
                if (s.ToLower().Contains(query.ToLower()))
                {
                    ToReturn.Add(s);
                }
            }
            return ToReturn.ToArray();
        }
    }
}
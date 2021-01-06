using System;
using System.Collections.Generic;

namespace AutoUpgrade.Model
{
    public class Version
    {
        public int total_file_count { get; set; }

        public int total_size { get; set; }

        public int version { get; set; }

        public string version_no { get; set; }

        public List<VersionFiles> files { get; set; }
    }

    public class VersionFiles
    {
        public int size { get; set; }

        public int version { get; set; }

        public string time { get; set; }

        public string name { get; set; }
    }
}

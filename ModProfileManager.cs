using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaleworldsProductSerialKeyVerifier
{
    public class ModProfileManager
    {
        private List<ModProfile> profiles;
        private static ModProfileManager instance;
        public static ModProfileManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModProfileManager();
                }
                return instance;
            }
        }

        public void LoadAll()
        {
            string profileDirFullPath = Path.Combine(Environment.CurrentDirectory, "Profiles/");
            if (!Directory.Exists(profileDirFullPath))
            {
                Directory.CreateDirectory(profileDirFullPath);
            }
            DirectoryInfo di = new DirectoryInfo(profileDirFullPath);
            foreach (var file in di.EnumerateFiles())
            {
                if (file.Extension == ".profile")
                {
                    profiles.Add(new ModProfile(file.FullName, null));
                }
            }
        }

        public List<ModProfile> Profiles
        {
            get { return profiles; }
        }

        public ModProfileManager()
        {
            profiles = new List<ModProfile>();
        }
    }
}

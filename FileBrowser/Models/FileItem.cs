using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Models
{
    public class FileItem
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public DateTime CreationDate { get; set; }

        public FileItem()
        {
            Size = 0;
            CreationDate = DateTime.MinValue;
        }
    }
}

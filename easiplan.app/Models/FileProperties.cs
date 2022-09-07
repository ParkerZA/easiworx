using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFileImporter.CsvFile.Entities
{
    public class FileProperties
    {
        public virtual int ID { get; set; }
        public virtual string Filesource { get; set; }
        public virtual string Filename { get; set; }
        public virtual DateTime FileDate { get; set; }
        public virtual long FileSize { get; set; }
        public virtual string Format { get; set; }
    }
}

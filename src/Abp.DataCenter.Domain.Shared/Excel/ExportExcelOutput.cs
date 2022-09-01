using System;
using System.Collections.Generic;
using System.Text;

namespace Abp.DataCenter.Excel
{
    public class ExportExcelOutput
    {
        public byte[] Content { get; set; }

        public string MimeType { get; set; }

        public string FileName { get; set; }
    }
}

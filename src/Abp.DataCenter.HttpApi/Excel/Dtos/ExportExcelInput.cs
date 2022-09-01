using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DataCenter.Excel.Dtos
{
    public class ExportExcelInput
    {
        public Guid ConfigId { get; set; }

        public string Data { get; set; }
    }
}

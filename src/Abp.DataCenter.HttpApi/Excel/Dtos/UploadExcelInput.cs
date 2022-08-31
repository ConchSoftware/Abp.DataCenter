using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.DataCenter.Excel.Dtos
{
    public class UploadExcelInput
    {
        /// <summary>
        /// 文件
        /// </summary>
        public IFormFile File { get; set; }

        /// <summary>
        /// 配置ID
        /// </summary>
        public Guid ConfigId { get; set; }
    }
}

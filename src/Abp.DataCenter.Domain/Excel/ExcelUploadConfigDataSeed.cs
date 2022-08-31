using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Guids;

namespace Abp.DataCenter.Excel
{
    public class ExcelUploadConfigDataSeed: IDataSeeder
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IExcelUploadConfigRepository _excelUploadConfigRepository;

        public ExcelUploadConfigDataSeed(IExcelUploadConfigRepository excelUploadConfigRepository, IGuidGenerator guidGenerator)
        {
            this._excelUploadConfigRepository = excelUploadConfigRepository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SendDemoData();
        }

        private async Task SendDemoData()
        {
            var data = new ExcelUploadConfigMaster(_guidGenerator.Create(), "员工信息导入配置","员工信息");
            data.AddItems("姓名", true, 1);
            data.AddItems("性别", true, 2, "无");
            data.AddItems("爱好", false, 3);

            await _excelUploadConfigRepository.InsertAsync(data);
        }
    }
}

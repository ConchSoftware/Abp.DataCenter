using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Abp.DataCenter.Excel
{
    public interface IExcelManager : IDomainService
    {
        List<dynamic> ReadStream(byte[] memoryStream);

        Task<List<dynamic>> ReadStreamAsync(byte[] memoryStream, Guid configId);
    }
}

using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.DataCenter.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}

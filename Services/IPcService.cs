using Apbd5.DTOs;

namespace Apbd5.Services;

public interface IPcService
{
    Task<IEnumerable<GetPcResponse>> GetAllPcsAsync();
    Task<IEnumerable<GetPcComponentResponse>?> GetPcComponentsAsync(int pcId);
    Task<GetPcResponse> CreatePcAsync(CreatePcRequest request);
    Task<bool> UpdatePcAsync(int pcId, PutPcRequest request);
    Task<bool> DeletePcAsync(int pcId);
}
using Apbd5.Data;
using Apbd5.DTOs;
using Apbd5.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd5.Services;

public class PcService(AppDbContext context) : IPcService
{
    public async Task<IEnumerable<GetPcResponse>> GetAllPcsAsync()
    {
        return await context.PCs
            .Select(pc => new GetPcResponse(
                pc.Id,
                pc.Name,
                pc.Weight,
                pc.Warranty,
                pc.CreatedAt,
                pc.Stock))
            .ToListAsync();
    }

    public async Task<IEnumerable<GetPcComponentResponse>?> GetPcComponentsAsync(int pcId)
    {
        var pcExists = await context.PCs.AnyAsync(p => p.Id == pcId);
        if (!pcExists) return null;

        return await context.PCComponents
            .Where(pcc => pcc.PCId == pcId)
            .Select(pcc => new GetPcComponentResponse(
                pcc.ComponentCode,
                pcc.Component.Name,
                pcc.Component.Description,
                pcc.Amount))
            .ToListAsync();
    }

    public async Task<GetPcResponse> CreatePcAsync(CreatePcRequest request)
    {
        var pc = new PC
        {
            Name = request.name,
            Weight = request.weight,
            Warranty = request.warranty,
            CreatedAt = request.createdAt,
            Stock = request.stock
        };

        context.PCs.Add(pc);
        await context.SaveChangesAsync();

        return new GetPcResponse(
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock);
    }

    public async Task<bool> UpdatePcAsync(int pcId, PutPcRequest request)
    {
        var rows = await context.PCs
            .Where(p => p.Id == pcId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, request.name)
                .SetProperty(p => p.Weight, request.weight)
                .SetProperty(p => p.Warranty, request.warranty)
                .SetProperty(p => p.CreatedAt, request.createdAt)
                .SetProperty(p => p.Stock, request.stock)
            );

        return rows > 0;
    }

    public async Task<bool> DeletePcAsync(int pcId)
    {
        var rows = await context.PCs
            .Where(p => p.Id == pcId)
            .ExecuteDeleteAsync();

        return rows > 0;
    }
}
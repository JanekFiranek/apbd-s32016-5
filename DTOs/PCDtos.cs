namespace Apbd5.DTOs;

public record GetPcResponse(
    int id,
    string name,
    float weight,
    int warranty,
    DateTime createdAt,
    int stock
);

public record GetPcComponentResponse(
    string componentCode,
    string name,
    string description,
    int amount
);

public record CreatePcRequest(
    string name,
    float weight,
    int warranty,
    DateTime createdAt,
    int stock
);

public record PutPcRequest(
    string name,
    float weight,
    int warranty,
    DateTime createdAt,
    int stock
);
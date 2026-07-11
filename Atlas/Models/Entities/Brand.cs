namespace Atlas.Models.Entities;

// NOTA:
//
// Actualmente Brand es un catálogo independiente.
//
// La relación con Product (BrandId)
// será implementada en una fase posterior.
//
// No modificar esta entidad para agregar BrandId
// durante este sprint.

public class Brand
{
    public int BrandId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;
}

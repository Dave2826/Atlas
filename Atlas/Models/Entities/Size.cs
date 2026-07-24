namespace Atlas.Models.Entities;

// NOTA:
//
// Actualmente Size es un catálogo independiente.
//
// La relación con Product (SizeId)
// será implementada en una fase posterior.
//
// No modificar esta entidad para agregar SizeId
// durante este sprint.

public class Size
{
    public int SizeId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;
}

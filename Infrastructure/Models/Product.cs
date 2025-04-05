using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [StringLength(255)]
    public string? PictureUrl { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    [StringLength(50)]
    public string? Brand { get; set; }

    public int QuantityInStock { get; set; }
}

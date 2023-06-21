using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Models;

[Table("t_Task")]
public partial class TTask
{
    [Key]
    public int Id { get; set; }

    [StringLength(120)]
    public string Name { get; set; } = null!;

    [StringLength(120)]
    public string? Description { get; set; }

    [Column("status")]
    public bool Status { get; set; }

    [Column("email")]
    [StringLength(120)]
    public string Email { get; set; } = null!;
}

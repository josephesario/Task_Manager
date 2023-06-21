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

    [Column("status_Id")]
    public int StatusId { get; set; }

    [Column("email")]
    [StringLength(120)]
    public string Email { get; set; } = null!;

    [ForeignKey("Email")]
    [InverseProperty("TTasks")]
    public virtual TUserDetail EmailNavigation { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("TTasks")]
    public virtual TTaskStatus Status { get; set; } = null!;
}

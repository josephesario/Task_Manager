using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Models;

[Table("t_Task_status")]
public partial class TTaskStatus
{
    [Key]
    [Column("status")]
    public bool Status { get; set; }

    [StringLength(120)]
    public string? CreatedOn { get; set; }

    [Column("email")]
    [StringLength(120)]
    public string Email { get; set; } = null!;

    [ForeignKey("Email")]
    [InverseProperty("TTaskStatuses")]
    public virtual TUserDetail EmailNavigation { get; set; } = null!;
}

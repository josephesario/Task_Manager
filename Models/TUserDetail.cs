using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Task_Manager.Models;

[Table("t_UserDetails")]
public partial class TUserDetail
{
    [Key]
    [Column("email")]
    [StringLength(120)]
    public string Email { get; set; } = null!;

    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [InverseProperty("EmailNavigation")]
    public virtual ICollection<TTaskStatus> TTaskStatuses { get; set; } = new List<TTaskStatus>();
}

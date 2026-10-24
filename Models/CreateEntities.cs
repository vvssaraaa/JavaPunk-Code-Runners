using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Javapunk.Models;

public class Users
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string User_name { get; set; } = string.Empty;
    [DefaultValue(0)]
    public int User_score { get; set; }

    [Required]
    public string User_password { get; set; } = string.Empty;
}

public class Modules
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Module_name { get; set; } = string.Empty;
}

public class Questions
{
    [Key]
    public int Id { get; set; }
    public Modules? Modules { get; set; }
    [Required]
    public string Question_text { get; set; } = string.Empty;

    public List<Answers> Answers { get; set; } = new List<Answers>();
}

public class Answers
{
    [Key]
    public int Id { get; set; }
    public Questions Questions { get; set; } = null!;
    [Required]
    public bool Is_correct { get; set; }
    [Required]
    public string Answer_text { get; set; } = string.Empty;
}

public class Completed_modules
{
    [Key]
    public int Id { get; set; }
    public Users Users { get; set; } = null!;
    public Modules? Modules { get; set; }
}
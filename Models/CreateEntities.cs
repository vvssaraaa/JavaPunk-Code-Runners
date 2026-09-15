using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Javapunk.Models;

    public class Users
{
    [Key]
    public int Id {get; set;}
    [Required]
    public string User_name{get; set;}
    [DefaultValue(0)]
    public int User_score{get; set;}
}

public class Modules
{
    [Key]
    public int Id {get; set;}
    [Required]
    public string Module_name {get; set;}
}

public class Questions
{
    [Key]
    public int Id {get; set;}
    public Modules Modules {get; set;}
    [Required]
    public string Question_text {get; set;}
}

public class Answers
{
    [Key]
    public int Id {get; set;}
    public Questions Questions {get; set;}
    [Required]
    public bool Is_correct {get; set;}
    [Required]
    public string Answer_text {get; set;}
}

public class Completed_modules
{
    [Key]
    public int Id {get; set;}
    public Users Users {get; set;}
    public Modules Modules {get; set;}
}
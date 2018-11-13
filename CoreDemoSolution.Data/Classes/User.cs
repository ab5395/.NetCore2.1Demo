namespace CoreDemoSolution.Data.Classes
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// User Class For IdentityUser
    /// Add Fields in IdentityUser or AspNetUser Table into Database
    /// </summary>
    public class User : IdentityUser
    {
        public User() { }

        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using TestNetProsegur.Application.Enums;

namespace TestNetProsegur.Application.Dtos.Auth
{
    public class AddRolesDto
    {

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public RolesEnum[]? Roles { get; set; }


        internal List<string>? GetRoles()
        {
            if (Roles == null) return null;
            return Roles.Select(e => e.ToString()).Distinct().ToList();
        }
    }
}

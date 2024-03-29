﻿namespace NerdStoreEnterprise.WebApp.Mvc.Models.Users
{
    public record UserRegisterViewModel
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
﻿using Domain.Enumerates;

namespace Application.CQRS.Users.ResponseDtos
{
    public class GetByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string FatherName { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string CardNumber { get; set; }
        public string TableNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime DateOfDismissal { get; set; }
        public string Note { get; set; }
        public Gender Gender { get; set; }
        public UserType UserType { get; set; }
    }
}

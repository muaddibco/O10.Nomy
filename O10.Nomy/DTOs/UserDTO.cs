﻿namespace O10.Nomy.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsEmptyOnly { get; set; }
    }
}

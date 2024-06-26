﻿namespace Lab3.Exceptions.PersonExceptions
{
    public class FutureBirthDateException : Exception
    {
        public FutureBirthDateException(string message) : base(message)
        {
        }
    }

    public class DistantPastBirthDateException : Exception
    {
        public DistantPastBirthDateException(string message) : base(message)
        {
        }
    }

    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
        }
    }
}

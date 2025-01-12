﻿using Devpack.Extensions.Types;
using Devpack.ObjectValues.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Devpack.ObjectValues
{
    public readonly struct Email : IContact
    {
        public string Address { get; }
        public string Domain { get; }

        public bool IsValid => Validate();

        public Email(string email)
        {
            Address = email;
            Domain = email?.Split('@').LastOrDefault()!;
        }

        public static bool operator ==(Email a, string b)
        {
            var result = TryParse(b, out var email);
            return result && email!.Value.Address == a.Address;
        }

        public static bool operator ==(string a, Email b)
        {
            return b == a;
        }

        public static bool operator !=(Email a, string b)
        {
            return !(a == b);
        }

        public static bool operator !=(string a, Email b)
        {
            return b != a;
        }

        public static bool TryParse(string address, out Email? email)
        {
            email = null;

            var emailAux = new Email(address);

            if (emailAux.IsValid)
                email = emailAux;

            return emailAux.IsValid;
        }

        public override string ToString()
        {
            return Address;
        }

        [ExcludeFromCodeCoverage]
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        [ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private bool Validate()
        {
            if (Address.IsNullOrEmpty())
                return false;

            var regex = new Regex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])");
            return regex.IsMatch(Address);
        }
    }
}
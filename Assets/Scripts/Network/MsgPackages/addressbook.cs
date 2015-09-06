﻿// Classes and structures being serialized

// Generated by ProtocolBuffer
// - a pure c# code generation implementation of protocol buffers
// Report bugs to: https://silentorbit.com/protobuf/

// DO NOT EDIT
// This file will be overwritten when CodeGenerator is run.
// To make custom modifications, edit the .proto file and add //:external before the message line
// then write the code and the changes in a separate file.
using System;
using System.Collections.Generic;

namespace Tutorial
{
    public partial class Person
    {
        public enum PhoneType
        {
            MOBILE = 0,
            HOME = 1,
            WORK = 2,
        }

        public string Name { get; set; }

        public int Id { get; set; }

        public string Email { get; set; }

        public List<Tutorial.Person.PhoneNumber> Phone { get; set; }

        public partial class PhoneNumber
        {
            public string Number { get; set; }

            public Tutorial.Person.PhoneType Type { get; set; }

        }

    }

    public partial class AddressBook
    {
        public List<Tutorial.Person> Person { get; set; }

    }

}

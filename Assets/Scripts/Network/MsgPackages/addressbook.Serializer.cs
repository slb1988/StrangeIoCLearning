﻿// This is the backend code for reading and writing

// Generated by ProtocolBuffer
// - a pure c# code generation implementation of protocol buffers
// Report bugs to: https://silentorbit.com/protobuf/

// DO NOT EDIT
// This file will be overwritten when CodeGenerator is run.
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Tutorial
{
    public partial class Person : IProtoBuf
    {
        /// <summary>Helper: create a new instance to deserializing into</summary>
        public void Deserialize(Stream stream)
        {
            Deserialize(stream, this);
        }

        /// <summary>Helper: create a new instance to deserializing into</summary>
        public static Person DeserializeLengthDelimited(Stream stream)
        {
            var instance = new Person();
            DeserializeLengthDelimited(stream, instance);
            return instance;
        }

        /// <summary>Helper: create a new instance to deserializing into</summary>
        public static Person DeserializeLength(Stream stream, int length)
        {
            var instance = new Person();
            DeserializeLength(stream, length, instance);
            return instance;
        }

        /// <summary>Helper: put the buffer into a MemoryStream and create a new instance to deserializing into</summary>
        public static Person Deserialize(byte[] buffer)
        {
            var instance = new Person();
            using (var ms = new MemoryStream(buffer))
                Deserialize(ms, instance);
            return instance;
        }

        /// <summary>Helper: put the buffer into a MemoryStream before deserializing</summary>
        public static Tutorial.Person Deserialize(byte[] buffer, Tutorial.Person instance)
        {
            using (var ms = new MemoryStream(buffer))
                Deserialize(ms, instance);
            return instance;
        }

        /// <summary>Takes the remaining content of the stream and deserialze it into the instance.</summary>
        public static Tutorial.Person Deserialize(Stream stream, Tutorial.Person instance)
        {
            if (instance.Phone == null)
                instance.Phone = new List<Tutorial.Person.PhoneNumber>();
            while (true)
            {
                int keyByte = stream.ReadByte();
                if (keyByte == -1)
                    break;
                // Optimized reading of known fields with field ID < 16
                switch (keyByte)
                {
                    // Field 1 LengthDelimited
                    case 10:
                        instance.Name = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                        continue;
                    // Field 2 Varint
                    case 16:
                        instance.Id = (int)global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt64(stream);
                        continue;
                    // Field 3 LengthDelimited
                    case 26:
                        instance.Email = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                        continue;
                    // Field 4 LengthDelimited
                    case 34:
                        // repeated
                        instance.Phone.Add(Tutorial.Person.PhoneNumber.DeserializeLengthDelimited(stream));
                        continue;
                }

                var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                // Reading field ID > 16 and unknown field ID/wire type combinations
                switch (key.Field)
                {
                    case 0:
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                    default:
                        global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                        break;
                }
            }

            return instance;
        }

        /// <summary>Read the VarInt length prefix and the given number of bytes from the stream and deserialze it into the instance.</summary>
        public static Tutorial.Person DeserializeLengthDelimited(Stream stream, Tutorial.Person instance)
        {
            if (instance.Phone == null)
                instance.Phone = new List<Tutorial.Person.PhoneNumber>();
            long limit = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt32(stream);
            limit += stream.Position;
            while (true)
            {
                if (stream.Position >= limit)
                {
                    if (stream.Position == limit)
                        break;
                    else
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Read past max limit");
                }
                int keyByte = stream.ReadByte();
                if (keyByte == -1)
                    throw new System.IO.EndOfStreamException();
                // Optimized reading of known fields with field ID < 16
                switch (keyByte)
                {
                    // Field 1 LengthDelimited
                    case 10:
                        instance.Name = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                        continue;
                    // Field 2 Varint
                    case 16:
                        instance.Id = (int)global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt64(stream);
                        continue;
                    // Field 3 LengthDelimited
                    case 26:
                        instance.Email = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                        continue;
                    // Field 4 LengthDelimited
                    case 34:
                        // repeated
                        instance.Phone.Add(Tutorial.Person.PhoneNumber.DeserializeLengthDelimited(stream));
                        continue;
                }

                var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                // Reading field ID > 16 and unknown field ID/wire type combinations
                switch (key.Field)
                {
                    case 0:
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                    default:
                        global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                        break;
                }
            }

            return instance;
        }

        /// <summary>Read the given number of bytes from the stream and deserialze it into the instance.</summary>
        public static Tutorial.Person DeserializeLength(Stream stream, int length, Tutorial.Person instance)
        {
            if (instance.Phone == null)
                instance.Phone = new List<Tutorial.Person.PhoneNumber>();
            long limit = stream.Position + length;
            while (true)
            {
                if (stream.Position >= limit)
                {
                    if (stream.Position == limit)
                        break;
                    else
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Read past max limit");
                }
                int keyByte = stream.ReadByte();
                if (keyByte == -1)
                    throw new System.IO.EndOfStreamException();
                // Optimized reading of known fields with field ID < 16
                switch (keyByte)
                {
                    // Field 1 LengthDelimited
                    case 10:
                        instance.Name = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                        continue;
                    // Field 2 Varint
                    case 16:
                        instance.Id = (int)global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt64(stream);
                        continue;
                    // Field 3 LengthDelimited
                    case 26:
                        instance.Email = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                        continue;
                    // Field 4 LengthDelimited
                    case 34:
                        // repeated
                        instance.Phone.Add(Tutorial.Person.PhoneNumber.DeserializeLengthDelimited(stream));
                        continue;
                }

                var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                // Reading field ID > 16 and unknown field ID/wire type combinations
                switch (key.Field)
                {
                    case 0:
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                    default:
                        global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                        break;
                }
            }

            return instance;
        }

        /// <summary>Get SerializeSize</summary>
        public uint GetSerializedSize()
        {
            uint num = 0u;

            if (this.Name == null)
                throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Name is required by the proto specification.");

            {
                num += 1u;
                uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Name);
                num += global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
            }


            {
                num += 1u;
                num += global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt64((ulong)this.Id);
            }

            if (this.Email != null)
            {
                {
                    num += 1u;
                    uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Email);
                    num += global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
                }
            }

            if (this.Phone != null)
            {
                foreach (var i4 in this.Phone)
                {
                    {
                        num += 1u;
                        uint serializedSize = i4.GetSerializedSize();
                        num += serializedSize + global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt32(serializedSize);
                    }
                }
            }

            return num;
        }
        /// <summary>Serialize this into the stream</summary>
        public void Serialize(Stream stream)
        {
            Serialize(stream, this);
        }

        /// <summary>Serialize the instance into the stream</summary>
        public static void Serialize(Stream stream, Person instance)
        {
            var msField = global::SilentOrbit.ProtocolBuffers.ProtocolParser.Stack.Pop();
            if (instance.Name == null)
                throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Name is required by the proto specification.");
            // Key for field: 1, LengthDelimited
            stream.WriteByte(10);
            global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Name));
            // Key for field: 2, Varint
            stream.WriteByte(16);
            global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt64(stream,(ulong)instance.Id);
            if (instance.Email != null)
            {
                // Key for field: 3, LengthDelimited
                stream.WriteByte(26);
                global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Email));
            }
            if (instance.Phone != null)
            {
                foreach (var i4 in instance.Phone)
                {
                    // Key for field: 4, LengthDelimited
                    stream.WriteByte(34);
                    ﻿msField.SetLength(0);
                    Tutorial.Person.PhoneNumber.Serialize(msField, i4);
                    // Length delimited byte array
                    uint length4 = (uint)msField.Length;
                    global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt32(stream, length4);
                    msField.WriteTo(stream);

                }
            }
            global::SilentOrbit.ProtocolBuffers.ProtocolParser.Stack.Push(msField);
        }

        /// <summary>Helper: Serialize into a MemoryStream and return its byte array</summary>
        public static byte[] SerializeToBytes(Person instance)
        {
            using (var ms = new MemoryStream())
            {
                Serialize(ms, instance);
                return ms.ToArray();
            }
        }
        /// <summary>Helper: Serialize with a varint length prefix</summary>
        public static void SerializeLengthDelimited(Stream stream, Person instance)
        {
            var data = SerializeToBytes(instance);
            global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt32(stream, (uint)data.Length);
            stream.Write(data, 0, data.Length);
        }

        public partial class PhoneNumber : IProtoBuf
        {
            /// <summary>Helper: create a new instance to deserializing into</summary>
            public void Deserialize(Stream stream)
            {
                Deserialize(stream, this);
            }

            /// <summary>Helper: create a new instance to deserializing into</summary>
            public static PhoneNumber DeserializeLengthDelimited(Stream stream)
            {
                var instance = new PhoneNumber();
                DeserializeLengthDelimited(stream, instance);
                return instance;
            }

            /// <summary>Helper: create a new instance to deserializing into</summary>
            public static PhoneNumber DeserializeLength(Stream stream, int length)
            {
                var instance = new PhoneNumber();
                DeserializeLength(stream, length, instance);
                return instance;
            }

            /// <summary>Helper: put the buffer into a MemoryStream and create a new instance to deserializing into</summary>
            public static PhoneNumber Deserialize(byte[] buffer)
            {
                var instance = new PhoneNumber();
                using (var ms = new MemoryStream(buffer))
                    Deserialize(ms, instance);
                return instance;
            }

            /// <summary>Helper: put the buffer into a MemoryStream before deserializing</summary>
            public static Tutorial.Person.PhoneNumber Deserialize(byte[] buffer, Tutorial.Person.PhoneNumber instance)
            {
                using (var ms = new MemoryStream(buffer))
                    Deserialize(ms, instance);
                return instance;
            }

            /// <summary>Takes the remaining content of the stream and deserialze it into the instance.</summary>
            public static Tutorial.Person.PhoneNumber Deserialize(Stream stream, Tutorial.Person.PhoneNumber instance)
            {
                instance.Type = Tutorial.Person.PhoneType.HOME;
                while (true)
                {
                    int keyByte = stream.ReadByte();
                    if (keyByte == -1)
                        break;
                    // Optimized reading of known fields with field ID < 16
                    switch (keyByte)
                    {
                        // Field 1 LengthDelimited
                        case 10:
                            instance.Number = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                            continue;
                        // Field 2 Varint
                        case 16:
                            instance.Type = (Tutorial.Person.PhoneType)global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt64(stream);
                            continue;
                    }

                    var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                    // Reading field ID > 16 and unknown field ID/wire type combinations
                    switch (key.Field)
                    {
                        case 0:
                            throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                        default:
                            global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                            break;
                    }
                }

                return instance;
            }

            /// <summary>Read the VarInt length prefix and the given number of bytes from the stream and deserialze it into the instance.</summary>
            public static Tutorial.Person.PhoneNumber DeserializeLengthDelimited(Stream stream, Tutorial.Person.PhoneNumber instance)
            {
                instance.Type = Tutorial.Person.PhoneType.HOME;
                long limit = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt32(stream);
                limit += stream.Position;
                while (true)
                {
                    if (stream.Position >= limit)
                    {
                        if (stream.Position == limit)
                            break;
                        else
                            throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Read past max limit");
                    }
                    int keyByte = stream.ReadByte();
                    if (keyByte == -1)
                        throw new System.IO.EndOfStreamException();
                    // Optimized reading of known fields with field ID < 16
                    switch (keyByte)
                    {
                        // Field 1 LengthDelimited
                        case 10:
                            instance.Number = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                            continue;
                        // Field 2 Varint
                        case 16:
                            instance.Type = (Tutorial.Person.PhoneType)global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt64(stream);
                            continue;
                    }

                    var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                    // Reading field ID > 16 and unknown field ID/wire type combinations
                    switch (key.Field)
                    {
                        case 0:
                            throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                        default:
                            global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                            break;
                    }
                }

                return instance;
            }

            /// <summary>Read the given number of bytes from the stream and deserialze it into the instance.</summary>
            public static Tutorial.Person.PhoneNumber DeserializeLength(Stream stream, int length, Tutorial.Person.PhoneNumber instance)
            {
                instance.Type = Tutorial.Person.PhoneType.HOME;
                long limit = stream.Position + length;
                while (true)
                {
                    if (stream.Position >= limit)
                    {
                        if (stream.Position == limit)
                            break;
                        else
                            throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Read past max limit");
                    }
                    int keyByte = stream.ReadByte();
                    if (keyByte == -1)
                        throw new System.IO.EndOfStreamException();
                    // Optimized reading of known fields with field ID < 16
                    switch (keyByte)
                    {
                        // Field 1 LengthDelimited
                        case 10:
                            instance.Number = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadString(stream);
                            continue;
                        // Field 2 Varint
                        case 16:
                            instance.Type = (Tutorial.Person.PhoneType)global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt64(stream);
                            continue;
                    }

                    var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                    // Reading field ID > 16 and unknown field ID/wire type combinations
                    switch (key.Field)
                    {
                        case 0:
                            throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                        default:
                            global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                            break;
                    }
                }

                return instance;
            }

            /// <summary>Get SerializeSize</summary>
            public uint GetSerializedSize()
            {
                uint num = 0u;

                if (this.Number == null)
                    throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Number is required by the proto specification.");

                {
                    num += 1u;
                    uint byteCount = (uint)Encoding.UTF8.GetByteCount(this.Number);
                    num += global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt32(byteCount) + byteCount;
                }

                if (this.Type != PhoneType.HOME)
                {
                    {
                        num += 1u;
                        num += global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt64((ulong)this.Type);
                    }
                }

                return num;
            }
            /// <summary>Serialize this into the stream</summary>
            public void Serialize(Stream stream)
            {
                Serialize(stream, this);
            }

            /// <summary>Serialize the instance into the stream</summary>
            public static void Serialize(Stream stream, PhoneNumber instance)
            {
                var msField = global::SilentOrbit.ProtocolBuffers.ProtocolParser.Stack.Pop();
                if (instance.Number == null)
                    throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Number is required by the proto specification.");
                // Key for field: 1, LengthDelimited
                stream.WriteByte(10);
                global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteBytes(stream, Encoding.UTF8.GetBytes(instance.Number));
                if (instance.Type != PhoneType.HOME)
                {
                    // Key for field: 2, Varint
                    stream.WriteByte(16);
                    global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt64(stream,(ulong)instance.Type);
                }
                global::SilentOrbit.ProtocolBuffers.ProtocolParser.Stack.Push(msField);
            }

            /// <summary>Helper: Serialize into a MemoryStream and return its byte array</summary>
            public static byte[] SerializeToBytes(PhoneNumber instance)
            {
                using (var ms = new MemoryStream())
                {
                    Serialize(ms, instance);
                    return ms.ToArray();
                }
            }
            /// <summary>Helper: Serialize with a varint length prefix</summary>
            public static void SerializeLengthDelimited(Stream stream, PhoneNumber instance)
            {
                var data = SerializeToBytes(instance);
                global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt32(stream, (uint)data.Length);
                stream.Write(data, 0, data.Length);
            }
        }

    }

    public partial class AddressBook : IProtoBuf
    {
        /// <summary>Helper: create a new instance to deserializing into</summary>
        public void Deserialize(Stream stream)
        {
            Deserialize(stream, this);
        }

        /// <summary>Helper: create a new instance to deserializing into</summary>
        public static AddressBook DeserializeLengthDelimited(Stream stream)
        {
            var instance = new AddressBook();
            DeserializeLengthDelimited(stream, instance);
            return instance;
        }

        /// <summary>Helper: create a new instance to deserializing into</summary>
        public static AddressBook DeserializeLength(Stream stream, int length)
        {
            var instance = new AddressBook();
            DeserializeLength(stream, length, instance);
            return instance;
        }

        /// <summary>Helper: put the buffer into a MemoryStream and create a new instance to deserializing into</summary>
        public static AddressBook Deserialize(byte[] buffer)
        {
            var instance = new AddressBook();
            using (var ms = new MemoryStream(buffer))
                Deserialize(ms, instance);
            return instance;
        }

        /// <summary>Helper: put the buffer into a MemoryStream before deserializing</summary>
        public static Tutorial.AddressBook Deserialize(byte[] buffer, Tutorial.AddressBook instance)
        {
            using (var ms = new MemoryStream(buffer))
                Deserialize(ms, instance);
            return instance;
        }

        /// <summary>Takes the remaining content of the stream and deserialze it into the instance.</summary>
        public static Tutorial.AddressBook Deserialize(Stream stream, Tutorial.AddressBook instance)
        {
            if (instance.Person == null)
                instance.Person = new List<Tutorial.Person>();
            while (true)
            {
                int keyByte = stream.ReadByte();
                if (keyByte == -1)
                    break;
                // Optimized reading of known fields with field ID < 16
                switch (keyByte)
                {
                    // Field 1 LengthDelimited
                    case 10:
                        // repeated
                        instance.Person.Add(Tutorial.Person.DeserializeLengthDelimited(stream));
                        continue;
                }

                var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                // Reading field ID > 16 and unknown field ID/wire type combinations
                switch (key.Field)
                {
                    case 0:
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                    default:
                        global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                        break;
                }
            }

            return instance;
        }

        /// <summary>Read the VarInt length prefix and the given number of bytes from the stream and deserialze it into the instance.</summary>
        public static Tutorial.AddressBook DeserializeLengthDelimited(Stream stream, Tutorial.AddressBook instance)
        {
            if (instance.Person == null)
                instance.Person = new List<Tutorial.Person>();
            long limit = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadUInt32(stream);
            limit += stream.Position;
            while (true)
            {
                if (stream.Position >= limit)
                {
                    if (stream.Position == limit)
                        break;
                    else
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Read past max limit");
                }
                int keyByte = stream.ReadByte();
                if (keyByte == -1)
                    throw new System.IO.EndOfStreamException();
                // Optimized reading of known fields with field ID < 16
                switch (keyByte)
                {
                    // Field 1 LengthDelimited
                    case 10:
                        // repeated
                        instance.Person.Add(Tutorial.Person.DeserializeLengthDelimited(stream));
                        continue;
                }

                var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                // Reading field ID > 16 and unknown field ID/wire type combinations
                switch (key.Field)
                {
                    case 0:
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                    default:
                        global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                        break;
                }
            }

            return instance;
        }

        /// <summary>Read the given number of bytes from the stream and deserialze it into the instance.</summary>
        public static Tutorial.AddressBook DeserializeLength(Stream stream, int length, Tutorial.AddressBook instance)
        {
            if (instance.Person == null)
                instance.Person = new List<Tutorial.Person>();
            long limit = stream.Position + length;
            while (true)
            {
                if (stream.Position >= limit)
                {
                    if (stream.Position == limit)
                        break;
                    else
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Read past max limit");
                }
                int keyByte = stream.ReadByte();
                if (keyByte == -1)
                    throw new System.IO.EndOfStreamException();
                // Optimized reading of known fields with field ID < 16
                switch (keyByte)
                {
                    // Field 1 LengthDelimited
                    case 10:
                        // repeated
                        instance.Person.Add(Tutorial.Person.DeserializeLengthDelimited(stream));
                        continue;
                }

                var key = global::SilentOrbit.ProtocolBuffers.ProtocolParser.ReadKey((byte)keyByte, stream);

                // Reading field ID > 16 and unknown field ID/wire type combinations
                switch (key.Field)
                {
                    case 0:
                        throw new global::SilentOrbit.ProtocolBuffers.ProtocolBufferException("Invalid field id: 0, something went wrong in the stream");
                    default:
                        global::SilentOrbit.ProtocolBuffers.ProtocolParser.SkipKey(stream, key);
                        break;
                }
            }

            return instance;
        }

        /// <summary>Get SerializeSize</summary>
        public uint GetSerializedSize()
        {
            uint num = 0u;

            if (this.Person != null)
            {
                foreach (var i1 in this.Person)
                {
                    {
                        num += 1u;
                        uint serializedSize = i1.GetSerializedSize();
                        num += serializedSize + global::SilentOrbit.ProtocolBuffers.ProtocolParser.SizeOfUInt32(serializedSize);
                    }
                }
            }

            return num;
        }
        /// <summary>Serialize this into the stream</summary>
        public void Serialize(Stream stream)
        {
            Serialize(stream, this);
        }

        /// <summary>Serialize the instance into the stream</summary>
        public static void Serialize(Stream stream, AddressBook instance)
        {
            var msField = global::SilentOrbit.ProtocolBuffers.ProtocolParser.Stack.Pop();
            if (instance.Person != null)
            {
                foreach (var i1 in instance.Person)
                {
                    // Key for field: 1, LengthDelimited
                    stream.WriteByte(10);
                    ﻿msField.SetLength(0);
                    Tutorial.Person.Serialize(msField, i1);
                    // Length delimited byte array
                    uint length1 = (uint)msField.Length;
                    global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt32(stream, length1);
                    msField.WriteTo(stream);

                }
            }
            global::SilentOrbit.ProtocolBuffers.ProtocolParser.Stack.Push(msField);
        }

        /// <summary>Helper: Serialize into a MemoryStream and return its byte array</summary>
        public static byte[] SerializeToBytes(AddressBook instance)
        {
            using (var ms = new MemoryStream())
            {
                Serialize(ms, instance);
                return ms.ToArray();
            }
        }
        /// <summary>Helper: Serialize with a varint length prefix</summary>
        public static void SerializeLengthDelimited(Stream stream, AddressBook instance)
        {
            var data = SerializeToBytes(instance);
            global::SilentOrbit.ProtocolBuffers.ProtocolParser.WriteUInt32(stream, (uint)data.Length);
            stream.Write(data, 0, data.Length);
        }
    }

}

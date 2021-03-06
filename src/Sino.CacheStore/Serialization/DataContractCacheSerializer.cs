﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Sino.CacheStore.Serialization
{
    public class DataContractCacheSerializer : CacheSerializer
    {
        public DataContractSerializerSettings SerializerSettings { get; private set; }

        public DataContractCacheSerializer()
        {

        }

        public override T Deserialize<T>(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var serializer = GetSerializer(typeof(T));
            using (var ms = new MemoryStream(data))
            {
                return serializer.ReadObject(ms) as T;
            }
        }

        public override byte[] Serialize<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializer = GetSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, value);
                return ms.ToArray();
            }
        }

        private XmlObjectSerializer GetSerializer(Type target)
        {
            if (SerializerSettings == null)
            {
                return new DataContractSerializer(target);
            }
            else
            {
                return new DataContractSerializer(target, SerializerSettings);
            }
        }
    }
}

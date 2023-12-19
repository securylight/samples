using System;

namespace DeserializationTests
{
    internal sealed class  Product
    {
        internal string[] Sizes;

        public string Name { get; internal set; }
        public DateTime Expiry { get; internal set; }
    }
}
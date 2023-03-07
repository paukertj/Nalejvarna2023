using System;

namespace SourceGeneratorDemo.Generator.Mapping.Exceptions
{
    internal class InvalidMappingException : Exception
    {
        public InvalidMappingException(string message) : base(message) 
        {

        }
    }
}

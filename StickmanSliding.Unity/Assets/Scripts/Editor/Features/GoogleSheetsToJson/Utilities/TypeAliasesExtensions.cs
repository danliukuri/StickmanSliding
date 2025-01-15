using System;
using System.Collections.Generic;

namespace StickmanSliding.Editor.Features.GoogleSheetsToJson.Utilities
{
    public static class TypeAliasesExtensions
    {
        private static readonly Dictionary<Type, string> _typeAliasesNames = new()
        {
            [typeof(byte)]     = "byte",
            [typeof(sbyte)]    = "sbyte",
            [typeof(short)]    = "short",
            [typeof(ushort)]   = "ushort",
            [typeof(int)]      = "int",
            [typeof(uint)]     = "uint",
            [typeof(long)]     = "long",
            [typeof(ulong)]    = "ulong",
            [typeof(float)]    = "float",
            [typeof(double)]   = "double",
            [typeof(decimal)]  = "decimal",
            [typeof(object)]   = "object",
            [typeof(bool)]     = "bool",
            [typeof(char)]     = "char",
            [typeof(string)]   = "string",
            [typeof(void)]     = "void",
            [typeof(byte?)]    = "byte?",
            [typeof(sbyte?)]   = "sbyte?",
            [typeof(short?)]   = "short?",
            [typeof(ushort?)]  = "ushort?",
            [typeof(int?)]     = "int?",
            [typeof(uint?)]    = "uint?",
            [typeof(long?)]    = "long?",
            [typeof(ulong?)]   = "ulong?",
            [typeof(float?)]   = "float?",
            [typeof(double?)]  = "double?",
            [typeof(decimal?)] = "decimal?",
            [typeof(bool?)]    = "bool?",
            [typeof(char?)]    = "char?"
        };

        public static string Alias(this Type type) =>
            _typeAliasesNames.ContainsKey(type) ? _typeAliasesNames[type] : null;

        public static string AliasOrName(this Type type) =>
            _typeAliasesNames.ContainsKey(type) ? _typeAliasesNames[type] : type.Name;
    }
}
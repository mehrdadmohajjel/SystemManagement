using System;

namespace SystemManagement.Shared.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property)]
    public class IgnoreInStoredProcedureParametersAttribute : Attribute
    {
    }
}
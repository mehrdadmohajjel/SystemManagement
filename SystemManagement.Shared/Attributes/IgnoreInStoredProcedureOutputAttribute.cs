using System;

namespace SystemManagement.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInStoredProcedureOutputAttribute : Attribute
    {
    }
}
using System;
using System.ComponentModel;
using System.Linq;

namespace JanuszMarcinik.Mvc
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum e)
        {
            DescriptionAttribute attribute = e.GetType()
                .GetField(e.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? e.ToString() : attribute.Description;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Linq;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace marking_api.Global.Extensions
{
    public static class EnumExtensions
    {
        //public static string GetEnumDisplayName(this Enum enumType)
        //{
        //    return enumType.GetType().GetMember(enumType.ToString()).FirstOrDefault()?.GetCustomAttributes<DisplayAttribute>()?.Name ?? enumType.ToString();
        //}

        public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(x => new SelectListItem() { Text = Enum.GetName(typeof(T), x), Value = x.ToString() })).ToList();
        }

        //public static IEnumerable<SelectListItem> GetEnumSelectListUsingDisplayName<T>()
        //{
        //    return (Enum.GetValues(typeof(T)).Cast<Enum>().Select(x => new SelectListItem() { Text = x.GetEnumDisplayName(), Value = x.ToString() })).ToList();
        //}

        public static IEnumerable<SelectListItem> GetEnumSelectList<T>(T value)
        {
            var toString = Enum.GetName(typeof(T), value);
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(x => new SelectListItem() { Text = Enum.GetName(typeof(T), x), Value = x.ToString(), Selected = (toString != null && toString.Equals(Enum.GetName(typeof(T), x))) ? true : false })).ToList();
        }
    }
}

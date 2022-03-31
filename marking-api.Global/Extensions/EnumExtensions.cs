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
    /// <summary>
    /// Enum extension methods
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the displayname of an enum
        /// </summary>
        /// <param name="enumType">Enum - Enum extended from</param>
        /// <returns>Enum displayname</returns>
        public static string GetEnumDisplayName(this Enum enumType)
        {
            return enumType.GetType().GetMember(enumType.ToString()).FirstOrDefault()?.GetCustomAttribute<DisplayAttribute>()?.Name ?? enumType.ToString();
        }

        /// <summary>
        /// Generically get select list of all items in an extended enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Select list of all items of an enum</returns>
        public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(x => new SelectListItem() { Text = Enum.GetName(typeof(T), x), Value = x.ToString() })).ToList();
        }

        /// <summary>
        /// Generically get select list of items in an enum using the display name of the enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Select list of enum items</returns>
        public static IEnumerable<SelectListItem> GetEnumSelectListUsingDisplayName<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<Enum>().Select(x => new SelectListItem() { Text = x.GetEnumDisplayName(), Value = x.ToString() })).ToList();
        }

        /// <summary>
        /// Generically get select list of items in an enum with selected value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">T - Value of selected item in the list</param>
        /// <returns>Select list of enum items</returns>
        public static IEnumerable<SelectListItem> GetEnumSelectList<T>(T value)
        {
            var toString = Enum.GetName(typeof(T), value);
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(x => new SelectListItem() { Text = Enum.GetName(typeof(T), x), Value = x.ToString(), Selected = (toString != null && toString.Equals(Enum.GetName(typeof(T), x))) ? true : false })).ToList();
        }
    }
}

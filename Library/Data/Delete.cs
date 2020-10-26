using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Net.WebRequestMethods;

namespace Library.Data
{

    public abstract class BaseClass
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public StatusId Status { get; set; }
    }

    public class DerivedClass : BaseClass
    {
        public DerivedClass(DateTime dateOfBirth)
        {
            this.DateOfBirth = dateOfBirth;
            this.Age = DateTime.Now.Year - dateOfBirth.Year;
        }
        [Range(byte.MinValue, byte.MaxValue)]
        public int Age { get; private set; }
        [DataType(DataType.Date)]
        
        public DateTime DateOfBirth { get; set; }
    }


    public struct StatusStruct //: Models.Status
    {
        public const byte Available = (byte)StatusId.Available;
        public const byte CheckedOut = (byte)StatusId.CheckedOut;
        public const byte Lost = (byte)StatusId.Lost;
        public const byte OnHold = (byte)StatusId.OnHold;
    }
}
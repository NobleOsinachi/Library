using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Models
{
    public struct Discriminator
    {
        public const string Book = "Book";
        public const string Video = "Video";
}
}

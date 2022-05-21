using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

#nullable disable

namespace BookShop.Models
{
    public partial class BooksImage
    {
        public BooksImage()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public byte[] Img { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public async void CopyImageAsync()
        {
            using (MemoryStream stream = new MemoryStream((int)ImageFile.Length))
            {
                await ImageFile.CopyToAsync(stream);
                Img = stream.ToArray();
            }
        }
    }
}

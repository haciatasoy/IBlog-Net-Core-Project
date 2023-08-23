using EntityLayer.Concreate;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.Kategori
{
    public class CategoryAddDto
    {
        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public IFormFile KategoriResim { get; set; }
        public bool Status { get; set; }
        public Category CategoryModel { get; set; }
        public List<Category> Category { get; set; }
        
    }
}

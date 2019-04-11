using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cet37Market.Web.Data.Entities
{
    public class Product:IEntity
    {
        public int Id { get; set; }  

        [Required]
        [MaxLength(50, ErrorMessage ="The field {0} can only comtain {1} characters")]
        public string Name { get; set; }
        
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Price { get; set; }

        [Display(Name="Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime? LastSale{ get; set; }

        [Display(Name = "Is Available?")]
        public bool IsAvailable { get; set; }

       [DisplayFormat(DataFormatString ="{0:N2}", ApplyFormatInEditMode =false)]
        public double Stock { get; set; }

        //para inserir um producto na tabela dos produtos ficar com id do utilizador
        //Ajuda remover as propiedades Virtuais
        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImageURL))
                {
                    return null;
                }

                return $"http://ratim47-001-site1.gtempurl.com{this.ImageURL.Substring(1)}";
            }
        }
    }
}

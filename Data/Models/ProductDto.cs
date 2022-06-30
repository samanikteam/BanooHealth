using Entities.Articles;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string Text { get; set; }

        public List<int> CategoryId { get; set; }
        public List<int> ListArticleId { get; set; }

        public List<int> ProductArticles { get; set; }

        public byte[] Avatar1 { get; set; }

        public string AvatarTitle1 { get; set; }

        public string AvatarAlt1 { get; set; }

        public string Slug { get; set; }

        public string KeyWords { get; set; }

        public string Country { get; set; }

        public DateTime RegisterDate { get; set; }
        public String RegisterDateFa { get; set; }

        public string RegisterUserId { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateUserId { get; set; }


        //لیست بلاگ های مرتبط
        public List<Article> ListArticleLinked { get; set; }
        //لیست محصولات مرتبط
        public List<Product> ListProductLinked { get; set; }
        public string Price { get; set; }
        //حداقل قیمت محصول
        public string MinimumPrice { get; set; }
        //تعداد داروخانه هایی که محول رو موجود دارند
        public int CountPharmacyExistProduct { get; set; }

        public bool IsDelete { get; set; }
        public bool popular { get; set; }

        [MaxLength(100)]
        [Display(Name = "نام برند")]
        public string BrandName { get; set; }

    }

    public class EditProductDto : ProductDto
    {

    }

    public class ListProductDto
    {
        public List<ProductDto> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }


    public class ProductWithPharmacyDto
    {

        #region ProductDto

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public List<int> ProductArticles { get; set; }

        public byte[] Avatar1 { get; set; }

        public string AvatarTitle1 { get; set; }

        public string AvatarAlt1 { get; set; }

        public string Slug { get; set; }

        public string KeyWords { get; set; }

        public string Country { get; set; }

        public DateTime RegisterDate { get; set; }

        public string RegisterUserId { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string LastUpdateUserId { get; set; }

        public bool IsDelete { get; set; }
        //لیست بلاگ های مرتبط
        public List<Article> ListArticleLinked { get; set; }
        //لیست محصولات مرتبط
        public List<Product> ListProductLinked { get; set; }
        //حداقل قیمت محصول
        public string MinimumPrice { get; set; }
        //تعداد داروخانه هایی که محول رو موجود دارند
        public int CountPharmacyExistProduct { get; set; }

        #region Sabad Kharid
        public string Picture { get; set; }
        public string Name { get; set; }
        public double DoublePrice { get; set; }
        public string Price { get; set; }
        #endregion

        #endregion

        #region Pharmacy Dto

        public int PharmacyId { get; set; }


        public string PharmacyName { get; set; }


        public string Username { get; set; }



        public string Password { get; set; }


        public string DrName { get; set; }

        public string Phone { get; set; }


        public string Mobile { get; set; }


        public string PharmacyCode { get; set; }

        public string Province { get; set; }


        public string City { get; set; }


        public string Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime Registerdate { get; set; }


        #endregion
    }
    public class ListProductDetailWithPharmacyDto
    {
        public List<ProductWithPharmacyDto> ProductsWithPharmacy { get; set; }
    }

}

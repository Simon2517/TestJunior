using DataLayer.DetailedEntities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TestJunior.Controllers
{
    public class ValidationController : Controller
    {
        public bool BrandValidation(BrandViewModel brandModel)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(brandModel.brand.BrandName) || brandModel.brand.BrandName.Length > 255)
            {
                isValid = false;
                ModelState.AddModelError("brand.brandName", "brand name format is incorrect");
            }
            if (!string.IsNullOrWhiteSpace(brandModel.brand.Description) && brandModel.brand.Description.Length > 255)
            {
                isValid=false;
                ModelState.AddModelError("brand.description", "brand description format is incorrect");
            }
            if (string.IsNullOrWhiteSpace(brandModel.brand.Description))
            {
                isValid = false;
                ModelState.AddModelError("brand.description", "brand description is required");
            }
            foreach(var product in brandModel.prodCategories)
            {
                ProductValidation(product);
            }
            if(brandModel.prodCategories.Select(x=>x.Product.Name).Distinct().Count()!= brandModel.prodCategories.Count)
            {
                isValid = false;
                ModelState.AddModelError("brand.products", "you are trying to insert the same product");
            }
            return isValid;
        }

        public bool ProductValidation(APIProductWithCategories productModel)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(productModel.Product.Name) || productModel.Product.Name.Length > 255)
            {
                isValid = false;
                ModelState.AddModelError("product.name", "product name is either null or more than 255 characters");
            }
            return isValid;
        }
    }
}

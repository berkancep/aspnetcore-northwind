using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz.");
            RuleFor(p => p.ProductName).Length(2, 30).WithMessage("Ürün 2-30 karakter olmalıdır.");
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün adı A karakteri ile başlamalıdır.");


            RuleFor(p => p.UnitPrice).NotEmpty().WithMessage("Ürün fiyatı  boş olamaz.");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).WithMessage("Ürün fiyatı en az 1 lira olmalıdır.");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryID == 1).WithMessage("Bu kategorideki ürün fiyatı en az 10 lira olmalıdır.");

        }

        // arg parametresine productname bilgisi geliyor, delegate
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}

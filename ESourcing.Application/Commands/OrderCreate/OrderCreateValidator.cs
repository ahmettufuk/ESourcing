﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ESourcing.Application.Commands.OrderCreate
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
    {
        public OrderCreateValidator()
        {
            RuleFor(v => v.SellerUserName).EmailAddress().NotEmpty();
            RuleFor(v => v.ProductId).NotEmpty();
        }
    }
}

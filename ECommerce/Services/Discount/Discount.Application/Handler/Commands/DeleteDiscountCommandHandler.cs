using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handler.Commands
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepo _discountRepo;

        public DeleteDiscountCommandHandler(IDiscountRepo discountRepo) 
        {
            _discountRepo = discountRepo;
        }
        public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _discountRepo.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}

using AutoMapper;
using Basket.Application.Commands;
using Basket.Core.Repositaries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers.Commands
{
    public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public DeleteBasketByUserNameCommandHandler(IBasketRepo basketRepo, IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await _basketRepo.DeleteBasket(request.UserName);
            return Unit.Value;
        }
    }
}

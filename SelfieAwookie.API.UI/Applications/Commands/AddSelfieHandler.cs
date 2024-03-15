using MediatR;
using SelfieAwookie.API.UI.Applications.DTOs;
using SelfieAwookie.API.UI.Applications.Queries;
using SelfieAWookies.Core.Selfies.Domain;

namespace SelfieAwookie.API.UI.Applications.Commands
{
    public class AddSelfieHandler : IRequestHandler<AddSelfieCommand, SelfieDto>
    {
        #region Fields
        private readonly ISelfieRepository _repository = null;
        #endregion

        #region Constructors
        public AddSelfieHandler(ISelfieRepository repository)
        {
            _repository = repository;
        }
        #endregion
        public Task<SelfieDto> Handle(AddSelfieCommand request, CancellationToken cancellationToken)
        {
            SelfieDto result = null;

            Selfie addSelfie = _repository.AddOne(new Selfie()
            {
                ImagePath = request.Item.ImagePath,
                Title = request.Item.Title
            });

            _repository.UnitOfWork.SaveChanges();

            if (addSelfie != null)
            {
                request.Item.Id = addSelfie.Id;
                result = request.Item;
            }

            return Task.FromResult(result);
        }
    }
}

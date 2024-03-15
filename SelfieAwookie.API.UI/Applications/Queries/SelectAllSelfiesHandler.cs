using MediatR;
using SelfieAwookie.API.UI.Applications.DTOs;
using SelfieAWookies.Core.Selfies.Domain;

namespace SelfieAwookie.API.UI.Applications.Queries
{
    public class SelectAllSelfiesHandler : IRequestHandler<SelectAllSefliesQuery, List<SelfieResumeDto>>
    {
        #region Fields
        private readonly ISelfieRepository _repository = null;
        #endregion

        #region Constructors
        public SelectAllSelfiesHandler(ISelfieRepository repository)
        {
                _repository = repository;
        }
        #endregion

        #region Public methods
        public Task<List<SelfieResumeDto>> Handle(SelectAllSefliesQuery request, CancellationToken cancellationToken)
        {
            var selfiesList = _repository.GetAll(request.WookieId);
            var model = selfiesList.Select(item => new SelfieResumeDto { Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();

            return Task.FromResult(model);
        }
        #endregion
    }
}

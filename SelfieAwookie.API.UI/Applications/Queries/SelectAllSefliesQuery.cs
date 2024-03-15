using MediatR;
using SelfieAwookie.API.UI.Applications.DTOs;

namespace SelfieAwookie.API.UI.Applications.Queries
{
    /// <summary>
    /// Query to select all selfie (with dto class)
    /// </summary>
    public class SelectAllSefliesQuery : IRequest<List<SelfieResumeDto>>
    {
        #region Properties
        public int WookieId { get; set; }
        #endregion
    }
}

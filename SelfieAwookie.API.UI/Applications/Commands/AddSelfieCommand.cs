using MediatR;
using SelfieAwookie.API.UI.Applications.DTOs;

namespace SelfieAwookie.API.UI.Applications.Commands
{
    /// <summary>
    /// Command to add one selfie in database
    /// </summary>
    public class AddSelfieCommand : IRequest<SelfieDto>
    {
        #region Properties
        public SelfieDto Item { get; set; }
        #endregion
        #region Constructors

        #endregion
    }
}

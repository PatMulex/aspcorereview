namespace SelfieAWookies.Core.Selfies.Domain
{
    /// <summary>
    /// Représente un selfie avec un wookie lé
    /// </summary>
    public class Selfie
    {
        #region public methods
        public int Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int WookieId { get; set; }
        public Wookie Wookie { get; set; }

        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        #endregion
    }
}

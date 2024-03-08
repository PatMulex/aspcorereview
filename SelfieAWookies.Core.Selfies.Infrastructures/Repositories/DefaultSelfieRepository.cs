using Microsoft.EntityFrameworkCore;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Data;
using SelfiesAWookies.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Repositories
{
    public class DefaultSelfieRepository : ISelfieRepository
    {
        #region Fiels
        private readonly SelfiesContext _context = null;
        #endregion
        #region Constructors
        public DefaultSelfieRepository(SelfiesContext context)
        {
            _context = context;
        }
        #endregion
        #region Public method

        public ICollection<Selfie> GetAll(int wookieId)
        {
            var query = _context.Selfies.Include(item => item.Wookie).AsQueryable();

            if (wookieId > 0)
                query = query.Where(i => i.WookieId == wookieId);

            return [.. query];
        }

        public Selfie AddOne(Selfie item)
        {
            return _context.Selfies.Add(item).Entity;
        }

        public Picture AddOnePicture(string url)
        {
            return _context.Pictures.Add(new Picture
            {
                Url = url
            }).Entity;
        }
        #endregion

        #region Properties
        public IUnitOfWork UnitOfWork => this._context;

        #endregion
    }
}

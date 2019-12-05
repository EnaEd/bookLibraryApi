using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryApi.DataAccess.Interfaces
{
    public interface ITokenServie
    {
        Task<string> GenerateToken(string userName, string password);
    }
}

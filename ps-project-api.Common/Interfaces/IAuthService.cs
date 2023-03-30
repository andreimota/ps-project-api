using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_project_api.Common.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string id, int accountTypeId);
        string Encrypt(string password);
        string GetSalt(int length);
    }
}

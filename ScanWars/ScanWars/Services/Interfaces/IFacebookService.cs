using ScanWars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanWars.Services.Interfaces
{
    public interface IFacebookService
    {
        void Login(Action<User, Exception> OnLoginCompleted);
        void Logout();
    }
}

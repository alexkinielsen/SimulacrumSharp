using SimulacrumSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacrumSharp.Services.Interfaces
{
    public interface ISimulacrumApiService
    {
        Task<ApiResponse> Post(string controller, string endpoint, object requestObject);
    }
}

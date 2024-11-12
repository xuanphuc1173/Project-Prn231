using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountServicer
    {
        private readonly HttpClient _httpClient;
    public AccountServicer(HttpClient httpClient)
    {  
            _httpClient = httpClient; 
    }

    
    }
}

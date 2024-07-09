using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using BW_U_1.Models;

namespace BW_U_1.Service
{
    public class ServiceBase : sqlservice
    {
        public ServiceBase(IConfiguration config) : base(config) { }

        // Non è necessario implementare nuovamente i metodi CRUD qui, perché sono già definiti in sqlservice
        // Puoi utilizzare direttamente i metodi ereditati per l'interazione con il database
    }
}

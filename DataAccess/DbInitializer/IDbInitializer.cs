using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.DbInitializer
{
    public interface IDbInitializer
    {
        public void Initialize();
    }
}
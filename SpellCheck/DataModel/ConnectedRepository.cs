using SpellCheck.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellCheck
{

    class ConnectedRepository
    {
        private readonly SpellCheckContext _context = new SpellCheckContext();


        public List<SpellTest> Tests
        {
            get
            {
                return _context.SpellTest.ToList();
        }
        }
    }
}

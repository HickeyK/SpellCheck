using System;
using System.Collections.Generic;

namespace SpellCheck.Entities
{
    public class SpellTest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Spelling> Spellings { get; set; }

        public List<TestOccurance>  TestOccurances { get; set; }
    }
}

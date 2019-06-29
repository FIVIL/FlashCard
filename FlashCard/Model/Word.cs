using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Model
{
    public class Word
    {
        public Word()
        {
            Id = Guid.NewGuid();
        }
        public Word(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public string TheWord { get; set; }
        public string Definitions { get; set; }
        public string Persian { get; set; }
        public string Pron { get; set; }
        public int Meaning { get; set; }
        public int Spelling { get; set; }
        public int PronScore { get; set; }
        public bool IsMeaning { get; set; }
        public bool IsSpelling { get; set; }
        public bool IsPron { get; set; }

        public string CategoryMeaning { get; set; }
        public string CategorySpelling { get; set; }

    }
}

using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Model
{
    public class Dictionary : IDisposable
    {
        private readonly LiteDatabase db;
        private readonly LiteCollection<Word> words;
        public Dictionary()
        {
            db = new LiteDatabase(@"MyData.db");
            words = db.GetCollection<Word>("Word");
            words.EnsureIndex(x => x.TheWord);
        }
        public IEnumerable<Word> GetWord(System.Linq.Expressions.Expression<Func<Word, bool>> expression)
            => words.Find(expression);
        public IEnumerable<Word> GetAll() => words.FindAll();
        public void Insert(Word w) => words.Insert(w);
        public void Inser(IEnumerable<Word> w) => words.Insert(w);
        public void Update(Word w) => words.Update(w);
        public void Dispose()
        {
            db.Dispose();
        }
    }
}

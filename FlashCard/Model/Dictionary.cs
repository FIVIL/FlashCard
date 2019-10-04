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
            if (words.Count() == 0)
            {
                words.Insert(new Word()
                {
                    TheWord = "Test",
                    Definitions = "To Test",
                    Meaning = 0,
                    Persian = "تست",
                    Pron = "تست",
                    Spelling = 0
                });
            }
        }
        public IEnumerable<Word> GetWord(System.Linq.Expressions.Expression<Func<Word, bool>> expression)
            => words.Find(expression);
        public IEnumerable<Word> GetAll() => words.FindAll();
        public void Insert(Word w) => words.Insert(w);
        public void Insert(IEnumerable<Word> w) => words.Insert(w);
        public void Update(Word w) => words.Update(w);
        public void Remove(Guid id) => words.Delete(x => x.Id == id);
        public void Update(IEnumerable<Word> ws) => words.Update(ws);
        public void Dispose()
        {
            db.Dispose();
        }
    }
}

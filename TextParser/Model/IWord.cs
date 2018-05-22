using System.Collections.Generic;

namespace TextParser.Model
{
    public interface IWord: ISentenceItem, IEnumerable<Symbol>
    {
        Symbol this[int index] { get; }
        //int Length { get; }
    }
}

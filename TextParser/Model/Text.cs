using System.Collections.Generic;

namespace TextParser.Model
{
    public class Text
    {
        public ICollection<ILine> Lines { get; set; }

        public Text()
        {
            Lines = new List<ILine>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextParser.Model;

namespace TextParser
{
    public class Parser
    {
        private SentenceItemFactory _sentenceItemFactory;

        private SeparatorContainer SeparatorContainer { get; }

        private ISentenceItemFactory WordFactory { get; }

        private ISentenceItemFactory PunctuationFactory { get; }

        private int currentLine = 1;
        private int currentSentence = 1;

        public Parser()
        {
            SeparatorContainer = new SeparatorContainer();
            WordFactory = new WordFactory();
            PunctuationFactory = new PunctuationFactory(SeparatorContainer);
        }

        public Text Parse(TextReader reader)
        {
            Text text = new Text();
            _sentenceItemFactory = new SentenceItemFactory(PunctuationFactory, WordFactory);
            string buffer = reader.ReadLine();

            while (buffer != null)
            {
                text.Lines.Add(ParseLine(buffer));
                buffer = reader.ReadLine();
            }

            Console.WriteLine("Text is parsed!");

            return text;
        }

        private Line ParseLine(string source)
        {
            Line line = new Line();

            while (source.Length > 0)
            {
                int sentenceSeparatorOccurence = -1;
                string sentenceSeparator = SeparatorContainer.SentenceSeparators().FirstOrDefault(
                    x =>
                    {
                        sentenceSeparatorOccurence = source.IndexOf(x);
                        return sentenceSeparatorOccurence >= 0;
                    });
                
                line.AddSentence(ParseSentence(source.Substring(0, sentenceSeparatorOccurence)));
                line.AddSentenceSeparator(_sentenceItemFactory.Create(
                        source.Substring(sentenceSeparatorOccurence, sentenceSeparator.Length)));

                source = source.Remove(0, sentenceSeparatorOccurence + sentenceSeparator.Length);
            }

            currentLine++;

            return line;
        }

        private Sentence ParseSentence(string source)
        {
            Console.WriteLine("Parsing {0} sentence in {1} paragraph", currentSentence, currentLine);

            Sentence sentence = new Sentence();

            while (source.Length > 0)
            {
                int separatorOccurence = -1;
                string sentenceSeparator = SeparatorContainer.WordSeparators().FirstOrDefault(
                    x =>
                    {
                        separatorOccurence = source.IndexOf(x);
                        return separatorOccurence >= 0;
                    });

                if (separatorOccurence == -1)
                {//there is no separators in sentence
                    sentence.AddSentenceItemsRange(ParseSubSentence(source));
                    source = source.Remove(0);
                }
                else if (separatorOccurence == 0)
                {//separator is first in sentence
                    sentence.AddSentenceItem(_sentenceItemFactory.Create(source.Substring(0, sentenceSeparator.Length)));
                    source = source.Remove(0, sentenceSeparator.Length);
                }
                else
                {//separator inside somewhere between words
                    string subsentence = source.Substring(0, separatorOccurence);
                    sentence.AddSentenceItemsRange(ParseSubSentence(subsentence));
                    sentence.AddSentenceItem(_sentenceItemFactory.Create(source.Substring(separatorOccurence, sentenceSeparator.Length)));
                    source = source.Remove(0, separatorOccurence + sentenceSeparator.Length);
                }
            }

            currentSentence++;

            return sentence;
        }

        private IEnumerable<ISentenceItem> ParseSubSentence(string source)
        {
            List<ISentenceItem> wordsAndSpaces = new List<ISentenceItem>();
            int startIndex = 0;
            int spaceOccurence = source.IndexOf(SeparatorContainer.Space(), startIndex);

            while (spaceOccurence != -1)
            {
                if (spaceOccurence != startIndex)
                {//beginning with word
                    wordsAndSpaces.Add(_sentenceItemFactory.Create(source.Substring(startIndex, spaceOccurence - startIndex)));
                    wordsAndSpaces.Add(_sentenceItemFactory.Create(SeparatorContainer.Space().ToString()));
                }
                else
                {//beginning with space
                    wordsAndSpaces.Add(_sentenceItemFactory.Create(SeparatorContainer.Space().ToString()));
                }

                startIndex = spaceOccurence + 1;
                spaceOccurence = source.IndexOf(SeparatorContainer.Space(), startIndex);
            }
            wordsAndSpaces.Add(_sentenceItemFactory.Create(source.Substring(startIndex)));

            return wordsAndSpaces.AsEnumerable();
        }
    }
}

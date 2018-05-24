using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using TextParser.Model;

namespace TextParser
{
    class Program
    {
        static void Main()
        {
            Text text;

            //parsing
            String inputFilePath = ConfigurationManager.AppSettings["InputFile"];

            using (StreamReader reader = File.OpenText(inputFilePath))
            {
                Parser parser = new Parser();
                text = parser.Parse(reader);
            }

            //from model to file
            String outputFilePath = ConfigurationManager.AppSettings["OutputFile"];

            using (StreamWriter writer = File.CreateText(outputFilePath))
            {
                int index = 1;
                foreach (Line line in text.Lines)
                {
                    Console.WriteLine("Writing {0} paragraph", index);

                    if (index == text.Lines.Count)
                    {
                        writer.Write(line.ToString());
                    }
                    else
                    {
                        writer.WriteLine(line.ToString());
                    }

                    index++;
                }
            }

            //Вывести все предложения заданного текста в порядке возрастания количества слов в каждом из них.
            IEnumerable allSentencesAsEnumerable = text.Lines.SelectMany(l => l.AllSentencesAsEnumerable()).
                OrderBy(s => s.AllSentenceItemsAsEnumerable().Count());
            foreach (Sentence s in allSentencesAsEnumerable)
            {
                Console.WriteLine(s.ToString());
            }

            //Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
            Console.WriteLine("Enter length of words:");
            string input = Console.ReadLine();
            int length;
            try
            {
                length = Int32.Parse(input);

                allSentencesAsEnumerable =
                    text.Lines.SelectMany(l => l.AllSentencesAsEnumerable()).Where(s => s.ToString().EndsWith("?"));
                foreach (Sentence s in allSentencesAsEnumerable)
                {
                    IEnumerable filteredWords = s.AllSentenceItemsAsEnumerable().Where(i => i.GetType() == typeof(Word))
                        .Where(w => w.GetLength() == length);

                    foreach (Word word in filteredWords)
                    {
                        Console.WriteLine(word.Chars);
                    }
                }
            }
            catch (SystemException exception)
            {
                Console.WriteLine("Exception occured: {0}", exception.Message);
            }

            //Из текста удалить все слова заданной длины, начинающиеся на согласную букву.
            Console.WriteLine("Enter length of words:");
            input = Console.ReadLine();

            try
            {
                Text text2 = new Text();

                length = Int32.Parse(input);

                foreach (Line line in text.Lines)
                {
                    Line lineToOutput = new Line();
                    char[] vowels = { 'e', 'y', 'u', 'i', 'o', 'a' };

                    IEnumerable sentences = line.AllSentencesAsEnumerable();
                    foreach (Sentence sentence in sentences)
                    {
                        Sentence sentenceToOutput = new Sentence();
                        sentenceToOutput.AddSentenceItemsRange(sentence.AllSentenceItemsAsEnumerable().
                            Where(i => i.GetLength() != length || (i.GetLength() == length && i.Chars.IndexOfAny(vowels) == 0)));
                        lineToOutput.AddSentence(sentenceToOutput);
                    }

                    text2.Lines.Add(lineToOutput);
                }

                Console.WriteLine("Words with length {0} that begins with consonant were deleted", length);
            }
            catch (SystemException exception)
            {
                Console.WriteLine("Exception occured: {0}", exception.Message);
            }

            //В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой может не совпадать с длиной слова.
            Console.WriteLine("Enter number of sentence:");
            string inputNumberOfSentence = Console.ReadLine();
            Console.WriteLine("Enter length of words:");
            string inputLength = Console.ReadLine();
            Console.WriteLine("Input replacement string:");
            string replacementString = Console.ReadLine();

            try
            {
                Sentence resultingSentence = new Sentence();
                Parser parser = new Parser();
                Sentence replacement = parser.Parse(replacementString);

                int numberOfSentence = Int32.Parse(inputNumberOfSentence);
                length = Int32.Parse(inputLength);

                IEnumerable sentences = text.Lines.SelectMany(l => l.AllSentencesAsEnumerable());

                int i = 0;
                foreach (Sentence sentence in sentences)
                {
                    if (i == numberOfSentence)
                    {
                        IEnumerable<ISentenceItem> sentenceItems = sentence.AllSentenceItemsAsEnumerable();

                        foreach (ISentenceItem item in sentenceItems)
                        {
                            if (item.GetLength() == length && item.GetType() == typeof(Word))
                            {
                                resultingSentence.AddSentenceItemsRange(replacement.AllSentenceItemsAsEnumerable());
                            }
                            else
                            {
                                resultingSentence.AddSentenceItem(item);
                            }
                        }

                        break;
                    }

                    i++;
                }
;
                Console.WriteLine("Words with length {0} were replaced with given string", length);
            }
            catch (SystemException exception)
            {
                Console.WriteLine("Exception occured: {0}", exception.Message);
            }

            Console.ReadLine();
        }
    }
}

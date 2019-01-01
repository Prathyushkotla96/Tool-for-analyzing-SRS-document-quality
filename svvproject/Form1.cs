using System.Text.RegularExpressions;

using java.io;
using java.util;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;

using System.Windows.Forms;
using System;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace svvproject
{
    
    public partial class Form1 : Form
    {
        public object Filepath { get; private set; }
        string path;
        int imp = 0;
        int opt = 0;
        int vague = 0;
        int weak = 0;
        int direct = 0;
        int cont = 0;
        int impta = 0;

        public Form1()
        {
            InitializeComponent();
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
           path= openFileDialog1.FileName;

        }




        private void helloWorldLabel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //-----------------RSQ-------Requirement Sentence Quality----------------------------------------
            string location = "C:/Users/PRATHYUSH/Desktop/text.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(location);

            file.WriteLine("------------------Requirement Sentence Quality---------------------");
            var jarRoot = @"C:\Users\PRATHYUSH\Desktop\project files\stanford-postagger-full-2018-10-16";
            var modelsDirectory = jarRoot + @"\models";

            // Loading POS Tagger
            var tagger = new MaxentTagger(modelsDirectory + @"\wsj-0-18-bidirectional-nodistsim.tagger");

            //----------------------------------------------------------------------------------------------------
            string FilePath = path;
            string FileText = new System.IO.StreamReader(FilePath).ReadToEnd();

            string[] SentenceArray = FileText.Split('.', '!', '?');

            //string arrays containing the necessary words
            string[] ImplicitWords = { "this", "This", "these", "These", "that", "those", "it", "they", "they", "above", "below", "previous", "next", "following", "last", "and first" };
            string[] OptionalWords = { "can", "eventually", "if appropriate", "if needed", "may", "optionally", "possibly" };
            string[] VagueWords = { "adequate", "back", "bad", "clear", "close", "easy", "efficient", "far", "fast", "future", "good", "in front", "low", "near", "new", "old", "past", "recent", "significant", "slow", "strong", "today’s", "useful", "weak", "and well" };
            string[] WeakWords = { "as a minimum", "as applicable", "as appropriate", "be able to", "be capable", "not limited to", "the capability of", "the capability to", "easy", "effective", "if practical", "normal", "provide for", "to be determined", "and timely" };
            string[] DirectiveWords = { "figures", "for example", "table", "note", "i.e.", "e.g." };
            string[] Continuances = { "below","as follows","following","listed","in particular","support","and" };
            string[] imperatives = { "Shall","shall","Must","must","is required to","Are Applicable","Are to","are to","Responsible for","will","should"};

            for (int i = 0; i < SentenceArray.Length; i++)
            {
                file.WriteLine(" ");
                file.WriteLine("Sentence {0}", i + 1);
                file.WriteLine(SentenceArray[i]);


                file.WriteLine("------ Multiple Sentence ------ ");

                object[] sentences = MaxentTagger.tokenizeText(new StringReader(SentenceArray[i])).toArray();
                foreach (ArrayList sentence in sentences)
                {
                    var taggedSentence = tagger.tagSentence(sentence);
                    file.WriteLine(SentenceUtils.listToString(taggedSentence, false));

                    var myRegX = new Regex("/V");
                    MatchCollection m = myRegX.Matches(SentenceUtils.listToString(taggedSentence, false));
                    if (m.Count > 0)
                        file.WriteLine("Verb Count - " + m.Count);

                }


                file.WriteLine("------ Implicit Words ------ ");
                for (int j = 0; j < ImplicitWords.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + ImplicitWords[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int impword = matches.Count;
                    imp = impword + imp;
                    if (matches.Count > 0)
                        file.WriteLine(ImplicitWords[j] + " - " + matches.Count + " times");
                }


                file.WriteLine("------ Optional Words ------ ");
                for (int j = 0; j < OptionalWords.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + OptionalWords[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int optword = matches.Count;
                    opt = optword + opt;
                    if (matches.Count > 0)
                        file.WriteLine(OptionalWords[j] + " - " + matches.Count + " times");
                }


                file.WriteLine("------ Vague Words ------ ");
                for (int j = 0; j < VagueWords.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + VagueWords[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int vagword = matches.Count;
                    vague = vagword + vague;
                    if (matches.Count > 0)
                        file.WriteLine(VagueWords[j] + " - " + matches.Count + " times");
                }


                file.WriteLine("------ Weak Words ------ ");
                for (int j = 0; j < WeakWords.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + WeakWords[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int Weword = matches.Count;
                    weak = Weword + weak;
                    if (matches.Count > 0)
                        file.WriteLine(WeakWords[j] + " - " + matches.Count + " times");
                }


                file.WriteLine("------ Directive Words ------ ");
                for (int j = 0; j < DirectiveWords.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + DirectiveWords[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int diwords = matches.Count;
                    direct = diwords + direct;
                    if (matches.Count > 0)
                        file.WriteLine(DirectiveWords[j] + " - " + matches.Count + " times");
                }

                file.WriteLine("------ Continuance Words ------ ");
                for (int j = 0; j < Continuances.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + Continuances[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int contwords = matches.Count;
                    cont = contwords + cont;
                    if (matches.Count > 0)
                        file.WriteLine(Continuances[j] + " - " + matches.Count + " times");
                }

                file.WriteLine("------ imperatives ------ ");
                for (int j = 0; j < imperatives.Length; j++)
                {
                    var myRegEx = new Regex("\\b" + imperatives[j] + "\\b");
                    MatchCollection matches = myRegEx.Matches(SentenceArray[i]);
                    int imptawords = matches.Count;
                    impta = imptawords + impta;
                    if (matches.Count > 0)
                        file.WriteLine(imperatives[j] + " - " + matches.Count + " times");
                }
            }
            file.WriteLine("------------ ");
            file.WriteLine("------ Total tally ------ ");
            file.WriteLine("DirectiveWords" + " - " + direct + " times");
            file.WriteLine("weak words" + " - " + weak + " times");
            file.WriteLine("vague words" + " - " + vague + " times");
            file.WriteLine("optional words" + " - " + opt + " times");
            file.WriteLine("implicit words" + " - " + imp + " times");
            file.WriteLine("continuance words" + " - " + cont + " times");
            file.WriteLine("imperative words" + " - " + impta + " times");




            //-------------------------RDQ------Requirement Document Quality-----------------------------------------
            file.WriteLine("\r\n\r\n------------Requirement Document Quality-------------\r\n \r\n");
            double LinesCount;
            int CharCount;
            double WordsCount;
            string Path = path;
            string Text = new System.IO.StreamReader(Path).ReadToEnd();



            CharCount = Text.Trim().Length;                     //total char 

            LinesCount = Text.Split('.', '!', '?').Length;       //total lines


            WordsCount = Text.Split(' ').Length;               // total Words


            file.WriteLine("Character count is {0} \r\n", CharCount);
            file.WriteLine("Word count is {0} \r\n", WordsCount);
            file.WriteLine("Line count is {0} \r\n", LinesCount);


            //finding the number of syllables
            string contents = System.IO.File.ReadAllText(path);
            int SCount = SyllableCount(contents);
            file.WriteLine("Syllable count is {0} \r\n", SCount);

            //finding the Flesch index
            file.WriteLine("finding the Flesch index: \r\n");
            double ASL = WordsCount / LinesCount;
            file.WriteLine("Average Sentence Length");
            file.WriteLine("{0}\r\n", ASL);
            double ASW = SCount / WordsCount;
            file.WriteLine("Average number of syllables per word");
            file.WriteLine("{0}\r\n", ASW);

            double FRI = 206.835 - (1.015 * ASL) - (84.6 * ASW);
            file.WriteLine("The Flesch index is");
            file.WriteLine("{0}\r\n", FRI);
            //printinh the ranges for flesch index value
            file.WriteLine("Flesch index ranges:\r\n \r\n100 - 90 \t very easy to read (5th grade) \r\n90 – 80 \t easy to read (6th grade) \r\n80 – 70 \t fairly easy to read (7th grade) \r\n70 – 60 \t plain english (8th & 9th grade) \r\n60 – 50 \t fairly difficult to read (10th - 12th grade) \r\n50 – 30 \t difficult to read (College) \r\n30 – 0  \t very difficult to read (College graduate) \r\n\r\n");


            //finding the Automatic Readabilty index
            file.WriteLine("finding the Automatic readabilty index:\r\n");
            double AVL = CharCount / WordsCount;
            file.WriteLine("the average number of letters per word");
            file.WriteLine("{0}\r\n", AVL);
            double AVW = WordsCount / LinesCount;
            file.WriteLine("the average number of words in sentences");
            file.WriteLine("{0}\r\n", AVW);
            double ARI = (AVL * 4.71) + (AVW * 0.5) - 21.43;
            file.WriteLine("The Automatic readability index is");
            file.WriteLine("{0}\r\n", ARI);
            file.WriteLine("Automatic redability index ranges:\r\n \r\n10 - 11	\tFifth Grade. \r\n11 - 12 \tSixth Grade. \r\n12 - 13 \tSeventh Grade. \r\n13 - 14 \tEighth Grade. \r\n14 - 15 \tNinth Grade. \r\n15 - 16 \tTenth Grade. \r\n16 - 17 \tEleventh grade. \r\n17 - 20 \tTwelfth grade. \r\n20 - 24 \tCollege. \r\n24 - 27 \tCollege graduate.");



            file.Flush();
            file.Close();
            System.Console.WriteLine("The output is stored in the file {0}", location);
            
        }


        private static int SyllableCount(string word)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            string currentWord = word;
            int numVowels = 0;
            bool lastWasVowel = false;
            foreach (char wc in currentWord)
            {
                bool foundVowel = false;
                foreach (char v in vowels)
                {
                    //don't count diphthongs
                    if (v == wc && lastWasVowel)
                    {
                        foundVowel = true;
                        lastWasVowel = true;
                        break;
                    }
                    else if (v == wc && !lastWasVowel)
                    {
                        numVowels++;
                        foundVowel = true;
                        lastWasVowel = true;
                        break;
                    }
                }

                //if full cycle and no vowel found, set lastWasVowel to false;
                if (!foundVowel)
                    lastWasVowel = false;
            }
            //remove es, it's _usually? silent
            if (currentWord.Length > 2 &&
                currentWord.Substring(currentWord.Length - 2) == "es")
                numVowels--;
            // remove silent e
            else if (currentWord.Length > 1 &&
                currentWord.Substring(currentWord.Length - 1) == "e")
                numVowels--;

            return numVowels;
        }

    }
}
  


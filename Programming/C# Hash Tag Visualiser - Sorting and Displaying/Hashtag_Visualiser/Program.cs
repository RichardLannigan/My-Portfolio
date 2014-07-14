#define TEST //Defines "TEST" to allow testing. Commenting out this will disable any methods associated with this.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //use an absolute or relative path to the source file
            //this one shows it is in the same directory as the exe
            //TextReader tr = new StreamReader("HashTags.txt");

            //create the TextReader object - but uninitialised
            //this means the object is visible throughout Main
            TextReader tr = null;

            while (true)
            {

                try
                {
                    //initialise the textReader object inside the try block
                    //this will generate a file not found exception
                    using (tr = new StreamReader("HashTags.txt"))
                    {
                        tr.ReadToEnd();
                    }

                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Specified file could not be found.");
                    Console.WriteLine("Please place file in debug folder, then press Enter to retry.");
                    Console.ReadLine();
                }
                break;
            }
            tr = new StreamReader("HashTags.txt"); //Creates a new StreamReader and calls it "tr" which reads in data from the "HashTags.txt" file, so the data from it can be used.

            TextWriter tw = new StreamWriter("tagsOrdered.html"); //Creates a new StreamWriter and calls it "tw", this is where the data will be outputted.
            Dictionary<string, int> list = new Dictionary<string, int>(); //Create a new dictionary object called "list".
            string count = ""; //Creates the string "count" and sets it to equal 'null'.
            //Reads the data into the dictionary "list".
            while (true) //While loop ends if when the file ends.
            {
                count = tr.ReadLine(); //Sets count to equal the number of lines (items).
                if (count == null) //Breaks the while loop if count equals zero i.e. end of file.
                    break;
                if (list.ContainsKey(count)) 
                {
                    list[count]++; //Increments the integer asscociated with "count".
                }
                else
                {
                    list.Add(count, 1); //Adds count to the dictionary "list".
                }
            }//end of while
#if TEST
            testMethod(); //Calls the specified method to execute at this location.
#endif
            outputDictionaryContents(list, tw);
            //Shows the user that the code has executed correctly.
            tr.Close(); //Ends the text reader.
            tw.Close(); //Ends the text writer.
        }
        private static void outputDictionaryContents(Dictionary<string, int> list, TextWriter t)
        {
            //Declares variables used for calculations.
            int counter = 0;
            double total = 0.0;
            double lengthAverage = 0.0;
            double frequencyAverage = 0.0;
            double quantity = 0.0;
            int rowNum = 1;

            
            t.WriteLine("<html><head>Top 20 Hash Tags</head><body><table border>"); //Creates top of table, gives it a border and a header.
            t.WriteLine("<tr><th>Popularity</th> <th>HashTag</th> <th>String Length</th> <th>Frequency</th></tr>"); //Labels each column with an appropriate heading. 
            foreach (KeyValuePair<string, int> pair in list.OrderByDescending(key => key.Value)) //A loop to output the name, length and frequency of the top 20 hash tags.
            {
                t.WriteLine("<tr><td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td></tr>", rowNum, pair.Key, pair.Key.Length - 1, pair.Value); //Outputs each item into the appropriate column.
                counter++; //Increments the integer associated with "count", which allows the loop to know when to break and continue.
                quantity += pair.Value; //Adds the "frequency" of each hash tag to the double "quantity", to calculate the frequency of the top 20 hash tags.
                total += pair.Key.Length - 1; //Adds the "length" of each unique hash tag to the double "total", to calculate the total length of all top 20 unique hash tags.
                rowNum++; //Increments the integer associated with "rowNum", which allows the popularity of each hash tag, with 1 being the most popular.
                if (counter == 20) //If the integer "count" equals 19, then the foreach loop will break, signifying that the top 20 unique hash tags have been found. 
                    break; //Breaks the loop.
            }
            lengthAverage = total / counter; //Divides the double "total" by the double "counter", to calculate the average length of the top 20 unique hash tags. 
            frequencyAverage = quantity / counter; //Divides the double "quantity" by the double "counter", to calculate the average frequency of the top 20 unique hash tags.
            t.WriteLine("<td>Average</td> <td></td> <td>{0}</td> <td>{1}</td>", lengthAverage, frequencyAverage); //Outputs the average length along with the average frequency of the top 20 hash tags, into the table, leaving a free cell at the start so that it lines up correctly.
            t.WriteLine("</table border></body></html></td></tr>"); //Ends the table border and body. 
            t.Close(); //Ends the text writer.
            Console.WriteLine("Code has executed correctly with zero errors... Please press Enter."); //This tells the user that the program has finished successfully.
            Console.ReadLine(); //This temporally pauses the program so that the user can read the text from the write line.
        }
        public static void testMethod()
        {
            TextReader tr = new StreamReader("debug.txt"); //Creates a new StreamReader and calls it "tr" which reads in data from the "debug.txt" file which has a few hashtags in it, making it easy to see whether the program works correctly.
            TextWriter tw = new StreamWriter("debug.html"); //Creates a new StreamWriter and calls it "tw", this is where the data will be outputted.
            Dictionary<string, int> testD = new Dictionary<string, int>(); //Create a new dictionary object called "testD".
            string count = ""; //Creates the string "count" and sets it to equal 'null'.

            //Reads the data into the dictionary "list".
            while (true) //While loop ends when the file ends.
            {
                count = tr.ReadLine(); //Sets count to equal the number of lines (items).
                //if end of file, stop loop
                if (count == null) //Breaks the while loop if count equals zero i.e. end of file.
                    break;
                if (testD.ContainsKey(count))
                {
                    testD[count]++; //Increments the integer asscociated with "count".
                }
                else
                {
                    testD.Add(count, 1); //Adds count to the dictionary "list".
                }
            }
            outputDictionaryContents(testD, tw); //Outputs the contents of "testD" Dictionary to the "tw" text writer.
        }

    }
}
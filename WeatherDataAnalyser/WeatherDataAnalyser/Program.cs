using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

//This is my program for the Computer & Network Technology 1, Assignment 1, for analysing chosen weather data and providing the user with certain results
//This is coded by Jamie Fletcher (11033350) 

namespace WeatherDataAnalyser
{
    class Program
    {
        public static string filePath;     //this is declaring "filePath" which i will use to determine what file is used when the user chooses one

        static public void dataMethod()     //this is my method for creating the array to store the data in       
        {

            string input2;
            int counter = 0;
            double aveVal = 0, sumMin = 0, sumMax = 0;
            double sumRain = 0, varRain = 0, aveRain = 0, rainStdDev = 0;     //these variables will be used throughout this method to return data results to the user


            //---COUNT THE AMOUNT OF LINES WITHIN THE FILE---

            StreamReader countLines = new StreamReader(filePath);     //opening a streamreader, reading the file
            while (countLines.ReadLine() != null)      //while the end of the file has not been reached
            {
                counter++;     //add 1 to counter
            }

            countLines.Close();     //close the streamreader


            //---CREATE AN ARRAY TO STORE THE DATA FROM FILE IN---


            string[,] weather = new string[counter, 6];     //array created called "weather" with the dimensions of the value of "counter" and 6                  
            using (StreamReader array = new StreamReader(filePath))
            {
                int i = 1;     //setting integer "i" to 1

                array.ReadLine();     //title of document, read this so it isnt put into the array

                while (array.Peek() >= 0)     //while the end of the file has not been reached
                {
                    var split = array.ReadLine().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);     //splitting each seperate piece of read data with a ",".
                                                                                                                         //this means there are several strings seperated by commas.
                    for (int j = 0; j < split.Length; j++) //j=0, when j is less than the length of array, 1 is added to the value j
                    {
                        weather[i, j] = split[j];     //this for loop is assigning each string to the row i within the array, each string after this is assigned to collumn j in order
                    }

                    i++; // add 1 to i                    
                }
                array.Close();     //closes the streamreader 
            }

            //---ASK THE USER WHAT THEY WISH TO DO---

            Console.WriteLine("---------------WHAT WOULD YOU LIKE TO KNOW?----------------");
            Console.WriteLine("1. All weather data for a certain year.");
            Console.WriteLine("2. Maximum temperature within a certain year.");
            Console.WriteLine("3. Minimum temperature within a certain year.");
            Console.WriteLine("4. Average temperatue within a certain year.");
            Console.WriteLine("5. Average and Standard Deviation of rainfall within a certain year.");
            Console.WriteLine("6. Close the program.");
            Console.WriteLine();

            input2 = Console.ReadLine();

            if (input2 == "1")     //starting an if statement, stating what to do when the user inputs a certain value
            {
                Console.WriteLine("Please enter a year:");
                string year = Console.ReadLine();     //the string "year" = whatever the user input                

                try     //trying to perform everyting inside these bracket, if it can't then the catch will be performed
                {
                    int tempYear = int.Parse(year);     //converting the string "year" from above, into an integer called tempYear

                    for (int i = 1; i < counter; i++)     //i=1, when i is less than the value of "counter" (determined above with the count), add 1 to i
                    {
                        int yearArr = int.Parse(weather[i, 0]);     //the integer yearArr = the value in collumn [i,0] of the array
                        if (yearArr == tempYear)     //if the year the user input is equal to the year in the collumn [i,0] then perform the following...
                        {
                            Console.WriteLine("In the year: " + weather[i, 0]);
                            Console.WriteLine("In month number: " + weather[i, 1]);
                            Console.WriteLine("The maximum temperature was: " + weather[i, 2] + "°C");
                            Console.WriteLine("The Minimum Temperature was: " + weather[i, 3] + "°C");     //this is going through each collumn and displaying the determined results
                            Console.WriteLine("There were: " + weather[i, 4] + " days of air frost");
                            Console.WriteLine("There were: " + weather[i, 5] + "mm's of rain");
                            Console.WriteLine();
                        }
                    }                   
                }
                catch     //this is what the program will do if the user inputs an invalid value
                {
                    Console.WriteLine("---------------INCORRECT YEAR INPUT, PLEASE TRY AGAIN---------------");
                    dataMethod();
                }

                
                dataMethod();     //goes back to the start of the method and allows the user to choose another option
            }

            else if (input2 == "2")
            {
                Console.WriteLine("Please enter a year:");
                string year = Console.ReadLine();

                try
                {
                    int userYear = int.Parse(year);
                    double maxVal = 0;     //setting maxVal to 0, makes it easier as i know the highest temp will be above this

                    for (int i = 1; i < counter; i++)      //looping through each element of the array
                    {
                        int yearArray = int.Parse(weather[i, 0]);
                        double yearTemp = double.Parse(weather[i, 2]);

                        if (userYear == yearArray)
                        {
                            if (yearTemp > maxVal)     //if yearTemp([i, 2] of the integer) is larger than maxVal(declared above as 0 to start with)
                            {
                                maxVal = yearTemp;     //then maxVal = yearTemp, this runs through the array and replaces maxVal with the current value if it is larger
                            }
                        }
                    }
                    Console.WriteLine("In the year " + userYear + " the maximum temperature was " + maxVal + "°C");     //displaying the results to the user
                    Console.WriteLine();
                }

                catch
                {
                    Console.WriteLine("---------------INCORRECT YEAR INPUT, PLEASE TRY AGAIN---------------");
                    dataMethod();                   
                }
                
                dataMethod();     //goes back to the start of the method and allows the user to choose another option 
            }

            else if (input2 == "3")
            {
                Console.WriteLine("Please enter a year:");
                string year = Console.ReadLine();

                try
                {
                    int userYear = int.Parse(year);
                    double minVal = 10000;     //set to 10,000 as there are no temperature readings higher than this, so allows to read the minimum temperature

                    //looping through each element of the array
                    for (int i = 1; i < counter; i++)
                    {
                        int yearArray = int.Parse(weather[i, 0]);
                        double yearTemp = double.Parse(weather[i, 3]);

                        if (userYear == yearArray)
                        {
                            if (yearTemp < minVal)     //if yearTemp(the value in [i, 3] of my array (weather) is less than minVal(set as 10,000 earlier)
                            {

                                minVal = yearTemp;     //then replace minVal with this value. This runs throuh each element of part of the array specified,
                                                       //and replaces minVal when the value is less than it currently is
                            }
                        }
                    }
                    Console.WriteLine("In the year " + userYear + " the minimum temperature was " + minVal + "°C");
                    Console.WriteLine();
                }

                catch
                {
                    Console.WriteLine("---------------INCORRECT YEAR INPUT, PLEASE TRY AGAIN---------------");
                    dataMethod();
                }

                dataMethod();     //goes back to the start of the method and allows the user to choose another option
            }

            else if (input2 == "4")
            {
                Console.WriteLine("Please enter a year:");
                string year = Console.ReadLine();

                try
                {
                    int userYear = int.Parse(year);

                    for (int i = 1; i < counter; i++)
                    {
                        int yearArray = int.Parse(weather[i, 0]);
                        double maxTemp = double.Parse(weather[i, 2]);     //to determine the average i need to assign 2 different parts of the array to variables in order to add all the
                        double minTemp = double.Parse(weather[i, 3]);     //temperatures together as they come in 2 different collumns

                        if (userYear == yearArray)
                        {
                            sumMax = sumMax + maxTemp;     //sumMax is declared as 0 at the start of the method. Running through the array and adding all values in [i, 2] to sumMax

                            sumMin = sumMin + minTemp;     //sumMin is declared as 0 at the start of the method. Running through the array and adding all values in [i, 3] to sumMin

                            aveVal = (sumMax + sumMin) / 24;     //adding sumMax and sumMin together, and then dividing by 24(number of temperature data) to determine the mean

                        }
                    }

                    Console.WriteLine("The average temperature in the year " + userYear + " was " + Math.Round(aveVal, 2) + "°C");     //Math.Round is rounding data to 2 decimal places
                    Console.WriteLine();
                }

                catch
                {
                    Console.WriteLine("---------------INCORRECT YEAR INPUT, PLEASE TRY AGAIN---------------");
                    dataMethod();
                }

                dataMethod();
            }

            else if (input2 == "5")
            {
                Console.WriteLine("Please enter a year:");
                string year = Console.ReadLine();

                try
                {
                    int userYear = int.Parse(year);

                    for (int i = 1; i < counter; i++)     //this is my for loop for calculating the average rainfall
                    {
                        int yearArray = int.Parse(weather[i, 0]);
                        double arrayRain = double.Parse(weather[i, 5]);

                        if (userYear == yearArray)
                        {
                            sumRain = sumRain + arrayRain;     //adding all of the data in the rain collumn of the array together
                            aveRain = sumRain / 12;     //dividing the sum by 12(total pieces of data) to discover the mean
                        }
                    }

                    Console.WriteLine("The average rainfall in the year " + userYear + " was " + Math.Round(aveRain, 2) + "mm's");     //displaying results to 2 decimal places
                    Console.WriteLine();

                    for (int i = 1; i < counter; i++)     //this is my for loop for calculating the standard deviation of rainfall within a given year
                    {
                        int yearArray = int.Parse(weather[i, 0]);
                        double arrayRain = double.Parse(weather[i, 5]);

                        if (userYear == yearArray)
                        {
                            varRain = varRain + ((arrayRain - aveRain) * (arrayRain - aveRain));     //subtracting the average value from each piece of data and squaring the result,
                            //then adding them together for the variance
                            rainStdDev = Math.Sqrt(varRain / 11);     //dividing the variance by 11(amount of pieces of data(12) - 1) to discover the standard deviation
                        }
                    }

                    Console.WriteLine("The Standard Deviation of rainfall in " + userYear + " was " + Math.Round(rainStdDev, 2));     //displaying results to 2 decimal places
                    Console.WriteLine();
                }

                catch
                {
                    Console.WriteLine("---------------INCORRECT YEAR INPUT, PLEASE TRY AGAIN---------------");
                    dataMethod();
                }

                dataMethod();     //return to start of method
            }

            else if (input2 == "6")
            {
                Environment.Exit(0);     //closes the environment (console)
            }


            else     //handles an incorrect input, if the user does not input a specified number (1-5), then this will tell them to try again
            {
                Console.WriteLine("---------------ERROR, INVALID INPUT, PLEASE TRY AGAIN---------------");
                Console.WriteLine();

                dataMethod();     //restarts the method, and asks the user what they would like to do again
            }


        }


        static void Main()                 //this is my main method for interacting with the user
        {
            string Input1;


            Console.WriteLine("---------------WELCOME TO THE MET OFFICE WEATHER DATA ANALYSER!---------------");
            Console.WriteLine();
            Console.WriteLine("Please follow the options and select what you wish to do.");
            Console.WriteLine("Please press the appropiate key. What would you like to know?");
            Console.WriteLine("1. Weather data for Heathrow.");                 //allows the user to choose which file they wish to use
            Console.WriteLine("2. Weather data for Sheffield.");

            Input1 = Console.ReadLine();

            if (Input1 == "1")
            {
                filePath = "heathrowdata.csv";     //assigning "filePath" so the array and counter will use this data file
                dataMethod();               //runs my method to run the heathrow data file
            }

            else if (Input1 == "2")
            {
                filePath = "sheffielddata1.csv";     //assigning "filePath" so the array and counter will use this data file
                dataMethod();              //runs my method to run the sheffield data file
            }

            else     //if the user does not input "1" or "2" then this allows them to try again
            {
                Console.WriteLine("---------------ERROR, INVALID INPUT, PLEASE TRY AGAIN---------------");
                Console.WriteLine();
                Console.WriteLine();
                    Main();     //restarts the Main() method
            }












            
            Console.ReadLine(); //so the program doesn't close


        }
    }
}


    




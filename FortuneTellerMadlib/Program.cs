using System;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Title: Fortune Teller MadLib Program: 
/// 
/// About: Users get their fortune by entering words that turn
/// into Wacky Madlibs. They can choose a number that
/// specifies a particular Madlib or let the program
/// chose (Randomize) for them.
/// 
/// Author: Kathleen West
/// Website:http://www.katiegirl.net
/// Notes: I programmed this as an example program for
/// my C# portfolio on my website. 
/// </summary>


namespace FortuneTellerMadlib
{
    class Program
    {
        // Comments: Global Fields for Program Class
        // Static and Private for Program Class
        // Did not use properties per design choice

        private static string choice;       // User Menu Choice
        private static int number;          // Fortune Number (Randomly set if not chosen)
        private static bool play;           // Sentinal for Program Loop
        private static StringBuilder menu;  // Holds Menu String Object

        static void Main(string[] args)
        {
            Task runGame = Task.Run(() => RunGame());
            Task.WaitAll(runGame);
        }

        #region RunGame
        private static void RunGame()
        {
            // Generate Main Menu StringBuilder Object
            menu = CreateMenu();

            // Set the entry for the program flow
            play = true;

            #region ConsoleAttributes
            Console.Title = "Fortune Teller MadLib Program";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            #endregion

            while (play)
            {
                // Reset Choices for Next Game Play
                number = -1;
                choice = String.Empty;

                // Clear the Screen
                Console.Clear();

                // Write Menu
                Console.WriteLine(menu);

                // User Input
                #region UserInput
                while (choice == String.Empty)
                {
                    // Read User Input
                    choice = Console.ReadLine();

                    switch (choice.ToUpper())
                    {
                        // Choose a Number
                        case "A":
                            while (!(number > 0 && number <= 8))
                            {
                                Console.Write("Enter a Number 1-8: ");
                                string temp = Console.ReadLine();
                                if (int.TryParse(temp, out number))
                                {
                                    Console.WriteLine();
                                }
                                else
                                {
                                    Console.WriteLine("Enter a valid number");
                                }
                            }
                            break;
                        // Randomize Fortune
                        case "B":
                            number = -1;
                            break;
                        // Exit the Program
                        case "X":
                            play = false;
                            return;
                        default:
                            choice = String.Empty;
                            break;
                    }
                }
                #endregion

                // Calls the Main Method
                PlayGame();
                Console.ReadLine();
            }
        }
        #endregion

        #region PlayGame
        private static void PlayGame()
        {
            //Generate a Custom Fortune Game
            using (Game game = new Game(number))
            {
                // Clear the Screen
                Console.Clear();

                game.SetFortune();

                // Clear the Screen
                Console.Clear();

                // Output the Fortune
                Console.WriteLine(game);
            }
        }
        #endregion

        #region CreateMainMenu
        private static StringBuilder CreateMenu()
        {
            // Creates the Main Menu
            menu = new StringBuilder();
            menu.AppendLine("|==================================|");
            menu.AppendLine("|....Fortune Teller MadLib Game....|");
            menu.AppendLine("|==================================|");
            menu.AppendLine("| How would you like your fortune? |");
            menu.AppendLine("|__________________________________|");
            menu.AppendLine("| A.)...Choose a Number............|");
            menu.AppendLine("| B.)...Choose for Me..............|");
            menu.AppendLine("| X.)...Exit.......................|");
            menu.AppendLine("|__________________________________|");
            return menu;
        }
        #endregion
    }

    class Game : IDisposable
    {
        
        // Comments: Using fields instead of properties
        // I chose private fields instead of properties
        // because these properties will not be accessed
        // outside the class

        #region PrivateFields

        private string adjective = String.Empty;       // Adjective
        private string pluralnoun = String.Empty;      // Plural Nound
        private string bodypart = String.Empty;        // Body Part
        private string singlenoun = String.Empty;      // Single Noun
        private string personname = String.Empty;      // Name of Person
        private string clothing = String.Empty;        // Article of Clothing
        private int numbergreaterone = -1;             // Number Greater Than One
        private Random random;                         // Random Number
        private int choicenumber;                      // Choice Number
        #endregion

        //Comments: Constructors initialize some private fields
        // Depends if the use chose a fortune number  

        #region Constructors
        public Game(int choice)
        {
            if(choice > 0 && choice <=8)
                choicenumber = choice;
            else
            {
                random = new Random();
                choicenumber = random.Next(1, 9);
            }
        }
       
        #endregion

        // Comments: Asks User to Enter and Set Madlib Words
        // Depends on choice number, they will get a specific
        // Madlib. If they did not make a choice, it will be
        // randomly set from the Constructor

        #region SetFortune
        public void SetFortune()
        {
            switch (choicenumber)
            {
                case 1:
                case 2:
                case 5:
                    while (String.IsNullOrEmpty(adjective))
                    {
                        Console.WriteLine("Adjective Definition: any member of a class of words that modify nouns and pronouns, primarily by describing a particular quality of the word they are modifying, as wise in a wise grandmother, or perfect in a perfect score, or handsome in He is extremely handsome. ");
                        Console.Write("Enter an Adjective: ");
                        adjective = Console.ReadLine();
                    }
                    break;
                case 3:
                    while (numbergreaterone <= 1)
                    {
                        Console.Write("Enter a Number Greater Than 1: ");
                        string temp = Console.ReadLine();
                        if (int.TryParse(temp, out numbergreaterone))
                        {
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Enter a valid number");
                        }                       
                    }
                    while (String.IsNullOrEmpty(pluralnoun))
                    {
                        Console.WriteLine("Plural Definition: consisting of, containing, or pertaining to more than one.");
                        Console.WriteLine("Noun Definition: any member of a class of words that can function as the main or only elements of subjects of verbs (A dog just barked), or of objects of verbs or prepositions (to send money from home), and that in English can take plural forms and possessive endings (Three of his buddies want to borrow John's laptop). Nouns are often described as referring to persons, places, things, states, or qualities, and the word noun is itself often used as an attributive modifier, as in noun compound; noun group.");
                        Console.Write("Enter a Plural Noun: ");
                        pluralnoun = Console.ReadLine();
                    }
                    break;
                case 4:
                    while (String.IsNullOrEmpty(bodypart))
                    {
                        Console.Write("Enter a Body Part: ");
                        bodypart = Console.ReadLine();
                    }
                    break;
                case 6:
                    while (String.IsNullOrEmpty(adjective))
                    {
                        Console.WriteLine("Adjective Definition: any member of a class of words that modify nouns and pronouns, primarily by describing a particular quality of the word they are modifying, as wise in a wise grandmother, or perfect in a perfect score, or handsome in He is extremely handsome. ");
                        Console.Write("Enter an Adjective: ");
                        adjective = Console.ReadLine();
                    }

                    while (String.IsNullOrEmpty(singlenoun))
                    {
                        Console.WriteLine("Noun Definition: any member of a class of words that can function as the main or only elements of subjects of verbs (A dog just barked), or of objects of verbs or prepositions (to send money from home), and that in English can take plural forms and possessive endings (Three of his buddies want to borrow John's laptop). Nouns are often described as referring to persons, places, things, states, or qualities, and the word noun is itself often used as an attributive modifier, as in noun compound; noun group.");
                        Console.Write("Enter a Single Noun: ");
                        singlenoun = Console.ReadLine();
                    }
                    break;
                case 7:
                    while (String.IsNullOrEmpty(clothing))
                    {
                        Console.Write("Enter an Article of Clothing: ");
                        clothing = Console.ReadLine();
                    }
                    break;
                case 8:
                    while (String.IsNullOrEmpty(personname))
                    {
                        Console.Write("Enter a Person's Name: ");
                        personname = Console.ReadLine();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        // Print The Fortune Results
        // Note the override of the string object

        #region PrintFortune
        public override string ToString()
        {
            switch(choicenumber)
            {
                case 1:
                     return String.Format("Signs point to a very {0} yes.",adjective);
                case 2:
                     return String.Format("The skies are {0}. The future is uncertain.", adjective);
                case 3:
                    return String.Format("I see {0} big {1} in your future.", numbergreaterone, pluralnoun);
                case 4:
                    return String.Format("What does your {0} tell you?", bodypart);
                case 5:
                    return String.Format("Signs point to a very {0} no.", adjective);
                case 6:
                    return String.Format("Picture a/an {0} {1}. That is your answer.", adjective, singlenoun);
                case 7:
                    return String.Format("You will find the answer in your {0}.", clothing);
                case 8:
                    return String.Format("Don't believe anything {0} says.", personname);
                default:
                    return "You do not get a fortune.";
            }
        }
        #endregion

        // Garbage Disposal of Game Object
        // Helps with memory resources if user runs like 1000+ games objects during their session
        // Should not be a problem in this application

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Game() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
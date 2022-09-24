using System.Linq;

namespace StackQueue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Stack Using [LIFO]
            //Stack<Commands> undo = new Stack<Commands>();
            //Stack<Commands> redo = new Stack<Commands>();
            //Commands.Create(undo, redo);
            #endregion


            #region Queue Using [FIFO]
            Queue<PrintingJob> printingJobs = new Queue<PrintingJob>();
            printingJobs.Enqueue(new PrintingJob("documentation.docx", 2));
            printingJobs.Enqueue(new PrintingJob("user-stories.pdf", 6));
            printingJobs.Enqueue(new PrintingJob("report.xlsx", 6));
            printingJobs.Enqueue(new PrintingJob("payroll.report", 5));
            printingJobs.Enqueue(new PrintingJob("budget.xlsx", 4));
            printingJobs.Enqueue(new PrintingJob("algorithms.pdf", 1));

            Console.WriteLine("Remember that 'First In First Out' [FIFO]");
            Console.WriteLine($"Current Before while Queue Count: {printingJobs.Count}");
            Console.WriteLine("--------Dequeuing--------");
            Console.ForegroundColor = ConsoleColor.Green;

            Random rnd = new Random();
            while (printingJobs.Count >= 0)
            {
                if (printingJobs.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                }
                var job = printingJobs.Dequeue();
                Console.WriteLine($"{job}");
                Thread.Sleep(rnd.Next(1, 5) * 500);
            }

            Console.WriteLine("-------------------------");
            Console.WriteLine($"Current After while Queue Count: {printingJobs.Count}");
            Console.ReadKey();
            #endregion
        }
    }



    #region Queue Using [FIFO]
    class PrintingJob
    {
        private readonly string File;
        private readonly int Copies;

        public PrintingJob(string file, int copies)
        {
            File = file;
            Copies = copies;
        }

        public override string ToString()
        {
            return $"[{File}] --- [{Copies}] Copies";
        }
    }
    #endregion


    #region Stack Using [LIFO]
    class Commands
    {
        private readonly DateTime CreatedAt;
        private readonly string Url;

        public Commands(string url)
        {
            CreatedAt = DateTime.Now;
            Url = url;
        }
        public static void Create(Stack<Commands> undo, Stack<Commands> redo)
        {
            string line;
            Commands item;
            while (true)
            {
                Console.WriteLine("\t[Back to undo -- Forward to redo]");
                Console.Write("Write down an Url or ('exit' to quit): ");
                line = Console.ReadLine().ToLower();
                /////////////////////////////////////////
                if (line == "exit")
                {
                    break;
                }
                else if (line == "back")
                {
                    if (undo.Count > 0)
                    {
                        item = undo.Pop();
                        redo.Push(item);
                    }
                    else { continue; }
                }
                else if (line == "forward")
                {
                    if (redo.Count > 0)
                    {
                        item = redo.Pop();
                        undo.Push(item);
                    }
                    else { continue; }
                }
                else
                {
                    if (!undo.Contains(new Commands(line)))
                    {
                        undo.Push(new Commands(line)); // add url to undo list
                    }
                    else { continue; }
                }
                /////////////////////////////////////////

                Console.Clear();

                Print("Back", undo);
                Print("Forward", redo);

            } // end of While
        }
        public static void Print(string name, Stack<Commands> commands)
        {
            Console.WriteLine($"{name} history");
            Console.BackgroundColor = name.ToLower() == "back" ? ConsoleColor.DarkGreen : ConsoleColor.DarkBlue;
            foreach (var u in commands)
            {
                Console.WriteLine($"\t{u}");
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        #region Overrided Methods
        public override bool Equals(object obj)
        {
            Commands command = obj as Commands;

            if (command == null)
            {
                return false;
            }

            return command.Url == this.Url;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + this.Url.GetHashCode();
                return hash;
            }
        }
        public override string ToString()
        {
            return $"[{this.CreatedAt.ToString("yyyy-MM-dd hh:mm")}] {this.Url}";
        }
        #endregion
    }
    #endregion
}
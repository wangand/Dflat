using System;
using System.Text.RegularExpressions;

namespace Dflat
{
    public enum StatementType { 
        STATEMENT_INSERT,
        STATEMENT_SELECT
    }
    public enum MetaCommandResult
    {
        META_COMMAND_EXIT,
        META_COMMAND_SUCCESS,
        META_COMMAND_UNRECOGNIZED_COMMAND
    }
    public enum PrepareResult
    {
        PREPARE_SUCCESS,
        PREPARE_UNRECOGNIZED_STATEMENT,
        PREPARE_SYNTAX_ERROR
    }

    public class Row
    {
        public int id;
        public string username;
        public string email;
    }

    public class Statement
    {
        public StatementType type;
        public Row rowToInsert; // only used by insert statement

        public Statement()
        {
            this.rowToInsert = new Row();
        }

        public void ExecuteStatement() 
        {
            switch (this.type)
            {
                case StatementType.STATEMENT_INSERT:
                    Console.WriteLine("This is where we would do an insert.");
                    break;
                case StatementType.STATEMENT_SELECT:
                    Console.WriteLine("This is where we would do a select.");
                    break;
            }
        }
    }
    public class Program
    {
        static void PrintPrompt()
        {
            Console.Write("db > ");
        }
        
        
        public static MetaCommandResult DoMetaCommand(string line)
        {
            if (line == ".exit")
            {
                return MetaCommandResult.META_COMMAND_EXIT;
            }
            else
            {
                return MetaCommandResult.META_COMMAND_UNRECOGNIZED_COMMAND;
            }
        }
        public static PrepareResult PrepareStatement(string line, Statement statement)
        {
            if (Regex.IsMatch(line, @"insert.*"))
            {
                statement.type = StatementType.STATEMENT_INSERT;

                string insertPattern = @"insert\s(\d+)\s(\w+)\s([\w.@]+)";
                Regex rx = new Regex(insertPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(line);

                // Syntax error if we couldn't insert correctly
                if (matches.Count != 1)
                {
                    return PrepareResult.PREPARE_SYNTAX_ERROR;
                }
                else if (matches[0].Groups.Count != 4)
                {
                    return PrepareResult.PREPARE_SYNTAX_ERROR;
                }

                statement.rowToInsert.id = Int32.Parse(matches[0].Groups[1].Value);
                statement.rowToInsert.username = matches[0].Groups[2].Value;
                statement.rowToInsert.email = matches[0].Groups[3].Value;

                return PrepareResult.PREPARE_SUCCESS;
            }
            if (Regex.IsMatch(line, @"select.*"))
            {
                statement.type = StatementType.STATEMENT_SELECT;
                return PrepareResult.PREPARE_SUCCESS;
            }

            return PrepareResult.PREPARE_UNRECOGNIZED_STATEMENT;
        }
        static void Main(string[] args)
        {
            while (true) {
                
                PrintPrompt();
                string line = Console.ReadLine();

                if (line[0] == '.')
                {
                    switch (DoMetaCommand(line))
                    {
                        case MetaCommandResult.META_COMMAND_EXIT:
                            Console.WriteLine("Goodbye");
                            return;
                        case MetaCommandResult.META_COMMAND_SUCCESS:
                            continue;
                        case MetaCommandResult.META_COMMAND_UNRECOGNIZED_COMMAND:
                            Console.WriteLine("Unrecognized command: {0}.", line);
                            continue;
                    }
                }

                Statement statement = new Statement();

                switch (PrepareStatement(line, statement))
                {
                    case PrepareResult.PREPARE_SUCCESS:
                        break;
                    case PrepareResult.PREPARE_UNRECOGNIZED_STATEMENT:
                        Console.WriteLine("Unrecognized keyword at start of: '{0}'.", line);
                        continue;
                    case PrepareResult.PREPARE_SYNTAX_ERROR:
                        Console.WriteLine("Syntax error at start of: '{0}'.", line);
                        continue;
                }

                statement.ExecuteStatement();
                Console.WriteLine("Executed.");
            }
        }
    }
}

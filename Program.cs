using System;

namespace Dflat
{
    enum StatementType { 
        STATEMENT_INSERT,
        STATEMENT_SELECT
    }
        enum MetaCommandResult
    {
        META_COMMAND_EXIT,
        META_COMMAND_SUCCESS,
        META_COMMAND_UNRECOGNIZED_COMMAND
    }
    enum PrepareResult
    {
        PREPARE_SUCCESS,
        PREPARE_UNRECOGNIZED_STATEMENT
    }
    class Statement
    {
        public StatementType type;
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
    class Program
    {
        static void PrintPrompt()
        {
            Console.Write("db > ");
        }
        
        
        static MetaCommandResult DoMetaCommand(string line)
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
        static PrepareResult PrepareStatement(string line, Statement statement)
        {
            if (line.Substring(0,6) == "insert")
            {
                //statement->type = STATEMENT_INSERT;
                return PrepareResult.PREPARE_SUCCESS;
            }
            if (line.Substring(0, 6) == "select")
            {
                //statement->type = STATEMENT_SELECT;
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
                }

                statement.ExecuteStatement();
                Console.WriteLine("Executed.");
            }
        }
    }
}

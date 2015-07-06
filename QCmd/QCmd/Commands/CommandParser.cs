using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jint.Parser.Ast;

namespace QCmd.Commands
{
    class CommandParser
    {
        public IEnumerable<QCommandDefinition> Parse(string command)
        {
            bool readingCommand = true;
            bool inQuotes = false;
            int index = 0;

            var commands = new List<QCommandDefinition>();
            var currentCommand = new QCommandDefinition();

            for (int i = 0; i < command.Length; i++)
            {
                switch (command[i])
                {
                    case ' ':
                        if (readingCommand)
                        {
                            var cmd = command.Substring(index, i - index).Trim();
                            if (cmd == ";" || cmd == " ")
                            {
                                index = i;
                                break;
                            }
                            readingCommand = false;
                            currentCommand.Command = command.Substring(index, i - index).Trim();
                            index = i + 1;
                            break;
                        }

                        if (inQuotes)
                            break;

                        var p1 = command.Substring(index, i - index).Trim();
                        index = i + 1;
                        if (p1 != " ")
                            currentCommand.Parameters.Add(p1);
                        break;
                    case ';':
                        if (readingCommand)
                        {
                            currentCommand.Command = command.Substring(index, i - index).Trim();
                            commands.Add(currentCommand);
                            currentCommand = new QCommandDefinition();
                            index = i;
                            break;
                        }

                        if (!inQuotes)
                        {
                            if (index != i)
                            {
                                var p2 = command.Substring(index, i - index).Trim();
                                if (p2 != " " || p2 != ";")
                                currentCommand.Parameters.Add(p2);
                            }
                        }

                        commands.Add(currentCommand);
                        currentCommand = new QCommandDefinition();
                        index = i;
                        readingCommand = true;
                        break;
                    case '"':
                        inQuotes = !inQuotes;
                        break;
                }
            }

            if (readingCommand)
            {
                currentCommand.Command = command.Substring(index, command.Length - index).Trim();
                if (currentCommand.Command != " " || currentCommand.Command != ";")
                commands.Add(currentCommand);
            }
            else if (index != command.Length)
            {
                currentCommand.Parameters.Add(command.Substring(index, command.Length - index).Trim());
                commands.Add(currentCommand);
            }

            return commands;
        }
    }
}

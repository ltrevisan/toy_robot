using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ToyRobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Declaring Variables
            string[] Positions = new string[2];
            int x = 0, y = 0, x_critical = 0, y_critical = 0, outErr = -1, count = 0;
            bool FirstCommandInsert = false;
            string orientation = "";
            bool ResultReported = false;
            string Place = "";


            //Application Name
            Console.WriteLine("Toy Robot Simulator:\n");


            //Information (List of commands to use)
            Console.WriteLine("List of Commands:\n");

            Console.WriteLine( "- PLACE X,Y,F: Put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.\n\n" 
                                                         + "- MOVE: move the toy robot one unit forward in the direction it is currently facing.\n\n"
                                                         + "- LEFT / RIGHT: rotate the robot 90 degrees in the specified direction without changing the position of the robot\n\n"
                                                         + "- REPORT: announce the X,Y and F of the robot\n");
    

            while (true)
            {
                Console.WriteLine("\nEnter Commands to move robot ('End' to close): ");

                Place = Console.ReadLine();

                if(Regex.IsMatch(Place, "END", RegexOptions.IgnoreCase))
                {
                    Environment.Exit(0);
                }

                if (Regex.IsMatch(Place, "Place", RegexOptions.IgnoreCase))                   
                {
                    count = 1;
                    Place = Regex.Replace(Place, "Place", "", RegexOptions.IgnoreCase);                  
                    Place = Place.Replace(" ", "");
                    Positions = Place.Split(',');
                    
                    if(Positions.Length == 3)
                    {
                        //Check if x starting position inserted is correct.
                        if (int.TryParse(Positions[0], out outErr))
                        {
                            x = Convert.ToInt32(Positions[0]);
                        }
                        else
                        {
                            Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, missing 'X' information");
                            continue;
                        }

                        //Check if y starting position inserted is correct.
                        if (int.TryParse(Positions[1], out outErr))
                        {
                            y = Convert.ToInt32(Positions[1]);
                        }
                        else
                        {
                            Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, missing 'Y' information");
                            continue;
                        }

                        //Check if starting orientation inserted is correct.          
                        if (Positions[2] == "NORTH" || Positions[2] == "SOUTH" || Positions[2] == "EAST" || Positions[2] == "WEST")
                        {
                            orientation = Positions[2];
                        }
                        else if (Positions[2] == "")
                        {
                            Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, missing 'F' information");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, 'F' information not correct (correct informations: NORTH, SOUTH, EAST, WEST)");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct. Please insert a valid starting position...");
                        continue;
                    }


                    //Check if robot has been positioned outside the table
                    if (x < 0 || x > 4 || y < 0 || y > 4)
                    {
                        Console.WriteLine("Incorrect robot position: the robot is outside the table. Please insert a valid starting position...");
                        continue;
                    }

                    FirstCommandInsert = true;
                }

                string instruction = string.Empty;

                if(FirstCommandInsert)
                {
                    while(!Regex.IsMatch(instruction, "END", RegexOptions.IgnoreCase))                  
                    {
                        if(ResultReported)
                        {                           
                            ResultReported = false;
                        }
                        else
                        {
                            instruction = Console.ReadLine();
                        }
                       
                        if (Regex.IsMatch(instruction, "Place", RegexOptions.IgnoreCase))
                        {
                            instruction = Regex.Replace(instruction, "Place", "", RegexOptions.IgnoreCase);
                            instruction = instruction.Replace(" ", "");
                            Positions = instruction.Split(',');

                            if (Positions.Length == 3)
                            {
                                //Check if x starting position inserted is correct.
                                if (int.TryParse(Positions[0], out outErr))
                                {
                                    x = Convert.ToInt32(Positions[0]);
                                }
                                else
                                {
                                    Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, missing 'X' information");
                                    continue;
                                }

                                //Check if y starting position inserted is correct.
                                if (int.TryParse(Positions[1], out outErr))
                                {
                                    y = Convert.ToInt32(Positions[1]);
                                }
                                else
                                {
                                    Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, missing 'Y' information");
                                    continue;
                                }

                                //Check if starting orientation inserted is correct.          
                                if (Positions[2] == "NORTH" || Positions[2] == "SOUTH" || Positions[2] == "EAST" || Positions[2] == "WEST")
                                {
                                    orientation = Positions[2];
                                }
                                else if (Positions[2] == "")
                                {
                                    Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, missing 'F' information");
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct, 'F' information not correct (correct informations: NORTH, SOUTH, EAST, WEST)");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Warning! Inserted command (PLACE X,Y,F) not correct. Please insert a valid starting position...");
                                continue;
                            }


                            //Check if robot has been positioned outside the table
                            if (x < 0 || x > 4 || y < 0 || y > 4)
                            {
                                Console.WriteLine("Incorrect robot position: the robot is outside the table. Please insert a valid starting position...");
                                continue;
                            }
                        }
                        else
                        {
                            //if inbstruction written is not correct cancels the inseirment
                            if (instruction != "MOVE" && instruction != "RIGHT" && instruction != "LEFT" && !Regex.IsMatch(instruction, "REPORT", RegexOptions.IgnoreCase))
                            {
                                Console.WriteLine("Warning! Inserted command not correct (correct commands: \"MOVE\", \"RIGHT\", \"LEFT\", \"REPORT\").");
                                continue;
                            }

                            if (instruction == "MOVE")
                            {
                                x_critical = x;
                                y_critical = y;

                                switch (orientation)
                                {
                                    case "NORTH":
                                        y = y + 1;
                                        break;
                                    case "SOUTH":
                                        y = y - 1;
                                        break;
                                    case "EAST":
                                        x = x + 1;
                                        break;
                                    case "WEST":
                                        x = x - 1;
                                        break;
                                }

                                //Check if limit of the table is reached. If true robot cannot move, but can be rotate
                                if (y < 0)
                                {
                                    y = y_critical;
                                    Console.WriteLine("Cannot move robot in this direction. The hell limit of the table has been reached. Please insert a command for a valid movement...");
                                    continue;
                                }
                                else if (y > 4)
                                {
                                    y = y_critical;
                                    Console.WriteLine("Cannot move robot in this direction. The upper limit of the table has been reached. Please insert a command for a valid movement...");
                                    continue;
                                }
                                else if (x < 0)
                                {
                                    x = x_critical;
                                    Console.WriteLine("Cannot move robot in this direction. The left limit of the table has been reached. Please insert a command for a valid movement...");
                                    continue;
                                }
                                else if (x > 4)
                                {
                                    x = x_critical;
                                    Console.WriteLine("Cannot move robot in this direction. The right limit of the table has been reached. Please insert a command for a valid movement...");
                                    continue;
                                }

                            }
                            else if (instruction == "RIGHT")
                            {
                                string OrientationSwitched = "";

                                switch (orientation)
                                {
                                    case "NORTH":
                                        OrientationSwitched = "EAST";
                                        break;
                                    case "SOUTH":
                                        OrientationSwitched = "WEST";
                                        break;
                                    case "EAST":
                                        OrientationSwitched = "SOUTH";
                                        break;
                                    case "WEST":
                                        OrientationSwitched = "NORTH";
                                        break;
                                }

                                orientation = OrientationSwitched;
                            }
                            else if (instruction == "LEFT")
                            {
                                string OrientationSwitched = "";

                                switch (orientation)
                                {
                                    case "NORTH":
                                        OrientationSwitched = "WEST";
                                        break;
                                    case "SOUTH":
                                        OrientationSwitched = "EAST";
                                        break;
                                    case "EAST":
                                        OrientationSwitched = "NORTH";
                                        break;
                                    case "WEST":
                                        OrientationSwitched = "SOUTH";
                                        break;
                                }

                                orientation = OrientationSwitched;
                            }
                            else if(Regex.IsMatch(instruction, "REPORT", RegexOptions.IgnoreCase))
                            {
                                //If Inserted command is 'REPORT' print Output
                                Console.WriteLine(x.ToString() + "," + y.ToString() + "," + orientation);
                                Console.WriteLine("\nPress 'End' to Exit or Insert Command to Continue");
                                instruction = Console.ReadLine();
                                ResultReported = true;               
                                continue;                               
                            }
                        }
                    }

                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Warning! The first command to insert is PLACE X,Y,F");
                    continue;
                }                          
            }            
        }
    }
}

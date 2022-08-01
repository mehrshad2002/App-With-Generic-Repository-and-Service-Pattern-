using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Calsses;
using Repositories;

namespace End
{
    public class App
    {
        public static IO io = new IO();
        public static void Main()
        {
            io.Print("Welcome");
            Start();
        }

        private static void Start()
        {
            int commandVal;
            while (true)
            {
                try
                {
                    io.Print("1-Create New User\n2-Read All Users\n3-Update User\n4-Delete User\n5-Read by ID \n0-Exit");
                    io.PrintAt("Enter Your Command Number : ");
                    commandVal = io.GetInt();
                    if (commandVal == 0)
                    {
                        break;
                    }
                    break;
                }
                catch (Exception e)
                {
                    io.Print("Somthings Wrong");
                }
            }
            CallCommand(commandVal);
        }

        private static void CallCommand(int commandVal)
        {
            IService<Users> service = new Service<Users>(new Repository<Users>());

            switch (commandVal)
            {
                case 1:
                    Users user = new Users();
                    io.PrintAt("User Name : ");
                    user.Name = io.GetStr();
                    bool Result = service.Add(user);
                    break;
                case 2:
                    List<Users> users = new List<Users>();
                    users = service.ReadAll();
                    foreach( Users u in users)
                    {
                        io.Print($"Name : {u.Name} -- ID : {u.Id}");
                        io.Print("- - - - - - - - - - - - - - - - - -");
                    }
                    break;
                case 3:
                    // show all users first 
                    users = service.ReadAll();
                    foreach (Users u in users)
                    {
                        io.Print($"Name : {u.Name} -- ID : {u.Id}");
                        io.Print("- - - - - - - - - - - - - - - - - -");
                    }

                    Users newUser = new Users();
                    io.PrintAt("Enter old ID : ");
                    newUser.Id = io.GetInt();

                    io.PrintAt("Enter New Name : ");
                    newUser.Name = io.GetStr();

                    Result = service.Update(newUser);
                    if (Result)
                    {
                        io.Print("User Was Updated");
                    }
                    else
                    {
                        io.Print("Somthings Wrong");
                    }

                    // show all users first 
                    users = service.ReadAll();
                    foreach (Users u in users)
                    {
                        io.Print($"Name : {u.Name} -- ID : {u.Id}");
                        io.Print("- - - - - - - - - - - - - - - - - -");
                    }

                    break;
                case 4:
                    // show all users first 
                    users = service.ReadAll();
                    foreach (Users u in users)
                    {
                        io.Print($"Name : {u.Name} -- ID : {u.Id}");
                        io.Print("- - - - - - - - - - - - - - - - - -");
                    }

                    io.PrintAt("Enter User ID : ");
                    int ID = io.GetInt();
                    Result = service.Delete(ID);
                    if (Result)
                    {
                        io.Print("User Was Deleted");
                    }
                    else
                    {
                        io.Print("Somthings Wrong");
                    }

                    // show all users  
                    users = service.ReadAll();
                    foreach (Users u in users)
                    {
                        io.Print($"Name : {u.Name} -- ID : {u.Id}");
                        io.Print("- - - - - - - - - - - - - - - - - -");
                    }
                    break;
                case 5:
                    Users users1 = new Users();
                    io.PrintAt("Enter User ID : ");
                    ID = io.GetInt();
                    users1 = service.ReadByID(ID);

                    io.Print($"ID : {users1.Id} - Name : {users1.Name}");
                    break;
                default:
                    io.Print("Invalid Command");
                    break;
            }
        }
    }
}

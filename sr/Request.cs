using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace sr
{
    public class Request
    {
        public static string Server;
        public static uint Port;
        public static string UserID;
        public static string Password;

        public Request(string Data)
        {
            try
            {
                var stringR = new StringReader(Data.ToLower());
                string command = stringR.ReadNextWord();
                if (command == "list")
                {
                    using (MySqlConnection mc = new MySqlConnection(
                        (new MySqlConnectionStringBuilder() { Server = Request.Server, Port = Request.Port, UserID = Request.UserID, Password = Request.Password }).ToString()))
                    {                        
                        using (MySqlCommand msc = new MySqlCommand() { Connection = mc })
                        {
                            StringBuilder builder = new StringBuilder();

                            builder.Append("SELECT * ");
                            
                            string source = stringR.ReadNextWord();
                            if (source.Length == 0)
                                throw new Exception("no data");

                            builder.Append("FROM " + source + " ");

                            string nextClause = stringR.ReadNextWord();
                            if(nextClause == "where")
                            {
                                builder.Append("where ");

                                while ((nextClause = stringR.ReadNextWord()) != string.Empty)
                                {
                                    builder.Append(nextClause);
                                }
                            }

                            mc.Open();
                            if(mc.State == System.Data.ConnectionState.Open)
                            {
                                msc.CommandText = builder.ToString();

                                using (MySqlDataReader mdr = msc.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                                {                                    
                                    MysqlFastLoader oo = new MysqlFastLoader(mdr);
                                    //while (mdr.Read())
                                    //{
                                    //    object[] data = new object[mdr.FieldCount];
                                    //    mdr.GetValues(data);
                                    //    foreach (var item in data)
                                    //    {
                                    //        Console.Write(" {0} ", Convert.ToString(item));
                                    //    }
                                    //    Console.WriteLine();
                                    //}
                                }
                            }
                            mc.Close();
                        }
                    }
                }else if (command == "server")
                {
                    Server = stringR.ReadNextWord();
                }
                else if (command == "port")
                {
                    Port = Convert.ToUInt32(stringR.ReadNextWord());
                }
                else if (command == "userid")
                {
                    UserID = stringR.ReadNextWord();
                }
                else if (command == "password")
                {
                    Password = stringR.ReadNextWord();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }            
        }
    }
}

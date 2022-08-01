using System.Data;
using System.Data.SqlClient;
using System.Reflection;
namespace Repositories
{
    public class Repository<T> : IRepository<T>
    {
        private readonly string cs = "Data Source=DESKTOP-6E77HUQ;Initial Catalog=RegisterDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool Add(T obj)
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = ModelMapping<T>.AddQuery(obj);
            SqlCommand cmd = new SqlCommand(query , conn );
            conn.Open();

            try
            {
                int Result = cmd.ExecuteNonQuery();
                if(Result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            SqlConnection conn = new SqlConnection(cs);
            string Query = ModelMapping<T>.DeleteQuery(id);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query , conn );
            try
            {
                int Result = cmd.ExecuteNonQuery();
                if (Result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<T> ReadAll()
        {
            SqlConnection conn = new SqlConnection(cs);
            string Query = ModelMapping<T>.ReadAllQuery();
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);//for read table from db 
            DataSet tables = new DataSet(); //can read data from row and column 
            adapter.Fill(tables);
            var AllRows = tables.Tables[0].Rows;
            List<T> list = new List<T>();

            foreach( DataRow row in AllRows)
            {
                T obj = default(T);
                obj = Activator.CreateInstance<T>();
                Type type = obj.GetType();
                foreach(PropertyInfo prop in type.GetProperties())
                {
                    prop.SetValue(obj, row[$"{prop.Name}"], null);
                }
                list.Add(obj);
            }
            return list;
        }

        public T ReadByID(int id)
        {
            SqlConnection conn = new SqlConnection(cs);
            string Query = ModelMapping<T>.ReadByID(id);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);//for read table from db 
            DataSet tables = new DataSet(); //can read data from row and column 
            adapter.Fill(tables);
            var AllRows = tables.Tables[0].Rows;

            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();

            foreach (DataRow row in AllRows)
            {
                foreach(PropertyInfo prop in type.GetProperties())
                {
                    prop.SetValue(obj, row[$"{prop.Name}"] , null );
                }
            }
            return obj;
        }

        public bool Update(T obj)
        {
            SqlConnection conn = new SqlConnection(cs);
            string Query = ModelMapping<T>.UpdateQuery(obj);
            SqlCommand cmd = new SqlCommand(Query, conn);
            conn.Open();

            try
            {
                int Result = cmd.ExecuteNonQuery();
                if (Result != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public static class ModelMapping<T>
    {
        public static string AddQuery(T obj )
        {
            Type type = obj.GetType();
            string values = "";
            string columns = "";
            foreach(PropertyInfo prop in type.GetProperties())
            {
                //columns = "'"+ prop.Name + "',";
                columns = "" + prop.Name + ",";
                values = "'" + prop.GetValue(obj) + "',";
            }
            columns = columns.Remove(columns.Length - 1, 1);
            values = values.Remove(values.Length-1,1);

            string query = $"insert into {type.Name}({columns}) Values({values})";
            return query;
        }

        public static string ReadAllQuery()
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            string TableName = type.Name;
            string query = $"select * from {TableName} ";
            return query ;
        }

        public static string DeleteQuery(int id)
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            string TableName = type.Name;
            string query = $"delete from {TableName} where ID = {id}";

            return query;
        }

        internal static string ReadByID(int id)
        {
            //T obj = default(T);
            var obj = Activator.CreateInstance<T>();
            Type type = obj.GetType();
            string TableName = type.Name;
            string query = $"select * from {TableName} where ID = {id}";
            return query;
        }

        internal static string UpdateQuery<T>(T? obj)
        {
            Type type = obj.GetType();
            string TableName = type.Name;
            int ID =  -1 ;
            string Set = "";
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if( prop.Name != "Id")
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data.Add(prop.Name, (string)prop.GetValue(obj));
                    foreach(KeyValuePair<string , string> k in data)
                    {
                        Set += "" + k.Key + " = '" + k.Value + "' ,";
                    }
                }
                else
                {
                    ID = (int)prop.GetValue(obj);
                }
            }
            Set = Set.Remove(Set.Length - 1);
            string query = $"update {TableName} set {Set}  where ID = {ID}";
            return query;
        }
    }
}
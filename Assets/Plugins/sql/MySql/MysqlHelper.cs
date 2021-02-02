using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
public class MysqlHelper
{
    public static MySqlConnection cono;//连接类对象
    private static string host;     //IP地址。如果只是在本地的话，写localhost就可以。
    private static string user;       //用户名。
    private static string password;      //密码。
    private static string dbName; //数据库名称。
    public static int port = 3306;
    public MysqlHelper(string _url, int _port, string _user, string _password, string _dbName)
    {
        //
        //UnityEngine.Debug.Log("???"+_url);
        host = _url;
        port = _port;
        user = _user;
        password = _password;
        dbName = _dbName;
        OpenSql();
    }

    public void OpenSql()
    {
        try
        {
            //Prot=33333;
            //string str = string.Format("Database={0};Data Source=49.233.80.251;User Id={2};Password={3};SslMode = none;", dataBase, host, id, pwd, port);
            string str = string.Format("server={0};port={1};user id={2};password={3};database={4};charset=utf8", host, port, user, password, dbName);
            //UnityEngine.Debug.Log(str);
            cono = new MySqlConnection(str);
            cono.Open();
        }
        catch (Exception e)
        {
            //UnityEngine.Debug.Log("???" + e.Message.ToString());
            throw new Exception("服务器连接失败，请重新检查是否打开MySql服务。" + e.Message.ToString());
        }
    }



    public DataTable SelectContain(string tableName, string judeColum, string containStr)
    {
        string quer = @"select* from " + tableName + " where " + judeColum + " like '%" + containStr + "%'";
        return QuerySet(quer).Tables[0];
    }


    public DataSet CreateTable(string name, string[] colName, string[] colType)
    {
        if (colName.Length != colType.Length)
        {
            throw new Exception("输入不正确：" + "columns.Length != colType.Length");
        }
        string query = "CREATE TABLE  " + name + "(" + colName[0] + " " + colType[0];
        for (int i = 1; i < colName.Length; i++)
        {
            query += "," + colName[i] + " " + colType[i];
        }
        query += ")";
        return QuerySet(query);
    }


    public DataSet CreateTableAutoID(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new Exception("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE  " + name + " (" + col[0] + " " + colType[0] + " NOT NULL AUTO_INCREMENT";
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }
        query += ", PRIMARY KEY (" + col[0] + ")" + ")";
        return QuerySet(query);
    }

    public DataSet Insert(string tableName, string[] col, string[] values)
    {
        if (col.Length != values.Length)
        {
            throw new Exception("columns.Length != colType.Length");
        }
        string query = "INSERT INTO " + tableName + " (" + col[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i];
        }
        query += ") VALUES (" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + "'" + values[i] + "'";
        }
        query += ")";
        return QuerySet(query);
    }


    public DataTable GetTable(string tableName)
    {
        string query = "SELECT* from " + tableName;
        DataSet ds = QuerySet(query);
        return ds.Tables[0];
    }

    public bool IsExist(string tableName, string colName, string value)
    {
        string query = "SELECT id FROM " + tableName + " WHERE " + colName + "='" + value + "' LIMIT 1";
        DataTable dt = QuerySet(query).Tables[0];
        if (dt.Rows.Count == 0)
        {
            return false;
        }
        else
            return true;
    }
    public DataRow GetExist(string tableName, string colName, string value)
    {
        string query = "SELECT * FROM " + tableName + " WHERE " + colName + "='" + value + "' LIMIT 1";
        DataTable dt = QuerySet(query).Tables[0];
        if (dt.Rows.Count == 0)
            return null;
        else
            return dt.Rows[0];
    }

    public string GetFromDT(DataRow dr, string colName)
    {
        int colIndex = dr.Table.Columns.IndexOf(colName);
        return dr[colIndex].ToString(); ;
    }
    public string GetFromDT(DataTable dt, int rowIndex, string colName)
    {
        int colIndex = dt.Columns.IndexOf(colName);
        return dt.Rows[rowIndex][colIndex].ToString();
    }

    public string GetFromDT(DataSet ds, int rowIndex, string colName)
    {
        DataTable dt = ds.Tables[0];
        int colIndex = dt.Columns.IndexOf(colName);
        return dt.Rows[rowIndex][colIndex].ToString();
    }

    public void Update(string tableName, string tarCol, string tarValue, string judeCol, string judeValue)
    {
        string query = "update " + tableName + " set " + tarCol + " = " + tarValue + " where " + judeCol + " like '" + judeValue + "'";
        QuerySet(query);
    }
    //字符串的话需要加上单引号，有时候不需要
    public void Update(string tableName, string[] tarColS, string[] tarValueS, string[] judeColS, string[] judeOperation, string[] judeValueS)
    {
        //update Equip set isStatic=1,isHoverOutline=1,isClickFlashing=1 where id > 2 and goId < 30000;
        if (tarColS.Length != tarValueS.Length || judeColS.Length != judeValueS.Length)
        {
            throw new Exception("输入不正确：" + "col.Length != operation.Length != values.Length");
        }
        string query = "update " + tableName + " set ";
        query += tarColS[0] + "='" + tarValueS[0] + "'";
        for (int i = 1; i < tarColS.Length; i++)
        {
            query += "," + tarColS[i] + "='" + tarValueS[i] + "'";
        }
        query += " where ";
        query += judeColS[0] + judeOperation[0] + "'" + judeValueS[0] + "'";
        for (int i = 1; i < judeColS.Length; i++)
        {
            query += " and " + judeColS[i] + judeOperation[i] + judeValueS[i];
        }
        query += ";";
        QuerySet(query);
    }
    public void Delete(string tableName, string col, string value)
    {
        string query = "delete from " + tableName + " where " + col + " like '" + value + "'";
        QuerySet(query);
    }
    public void DELETE_ALL(string tableName)
    {
        //string query = "truncate " + tableName;
    }


    /// 返回某一行的字符串，默认最后一行
    public string GetTableRowStr(string tableName, int hang = -1, string colMarkStr = "\t")
    {
        string result = "";
        DataTable dt = GetTable(tableName);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (hang != -1 && i == hang)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    result += dt.Rows[i][col];
                    result += colMarkStr;
                }
            }
            else if (hang == -1 && i == dt.Rows.Count - 1)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    result += dt.Rows[i][col];
                    result += colMarkStr;
                }
            }
        }
        return result;
    }
    public string GetTableStr(string tableName, string colMarkStr = "\t", string rowMarkStr = "\n")
    {
        string result = "";
        DataTable dt = GetTable(tableName);
        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn col in dt.Columns)
            {
                result += row[col];
                result += colMarkStr;
            }
            result += rowMarkStr;
        }
        return result;
    }

    public DataTable SelectMinId(string tableName, string colName)
    {
        string query = "select min(" + colName + ") from " + tableName;

        return QuerySet(query).Tables[0];
    }
    public DataTable SelectTopS(string tableName, int rowCounts)
    {
        string query = "select * from " + tableName + " limit " + rowCounts;
        return QuerySet(query).Tables[0];
    }
    public DataTable SelectRow(string tableName, int rowNumber)
    {
        string query = "select * from " + tableName + " limit " + rowNumber + "," + 1;
        return QuerySet(query).Tables[0];
    }
    public DataTable SelectLimit(string tableName, int rowStartNumber, int rowCount)
    {
        string query = "select * from " + tableName + " limit " + rowStartNumber + "," + rowCount;
        return QuerySet(query).Tables[0];
    }
    public DataTable SelectTop1(string tableName)
    {
        return SelectRow(tableName, 0);
    }
    public DataTable SelectAllTable(string tableName)
    {
        string query = "select * from " + tableName;
        return QuerySet(query).Tables[0];
    }


    public DataTable SelectGetTable0(string tableName, string[] selectColS, string[] judeColS, string[] operationS, string[] valueS)
    {
        return Select(tableName, selectColS, judeColS, operationS, valueS).Tables[0];
    }
    public DataTable Setlect根据某列值返回某列(string tableName, string selectCol, string judeCol, string operation, string value)
    {
        string query = "SELECT " + selectCol;
        query += "  FROM  " + tableName + "  WHERE " + " " + judeCol + " " + operation + " '" + value + "'";
        return QuerySet(query).Tables[0];
    }
    public DataSet Select(string tableName, string[] selectColS, string[] judeColS, string[] operationS, string[] valueS)
    {
        if (judeColS.Length != operationS.Length || operationS.Length != valueS.Length)
        {
            throw new Exception("输入不正确：" + "col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + selectColS[0];
        for (int i = 1; i < selectColS.Length; i++)
        {
            query += "," + selectColS[i];
        }
        query += "  FROM  " + tableName + "  WHERE " + " " + judeColS[0] + " " + operationS[0] + " '" + valueS[0] + "'";
        for (int i = 1; i < judeColS.Length; i++)
        {
            query += " AND " + judeColS[i] + " " + operationS[i] + " " + "'" + valueS[i] + "'";
        }

        //UnityEngine.Debug.Log(query);
        return QuerySet(query);
    }


    public static string RowToString(DataRow row, string markStr = "   ", int i = -1)
    {
        string resule = "";

        if (i == -1)
        {
            foreach (DataColumn col in row.Table.Columns)
            {
                resule += row[col];
                resule += markStr;
            }
        }

        return resule;
    }


    public DataTable Select(string tableName, string whereColName, string value, string tarColName = "*", string operation = "=")
    {
        string query = "SELECT " + tarColName + "  FROM  " + tableName + "  WHERE " + whereColName + " like '" + value + "'";

        return QuerySet(query).Tables[0];
        //非空和没值，不对，这个返回原值，尽量不该他的，无论查到查不到都这个dt都非空，就直接返回。外边反正也得判断table一行二行,索性外边不再判断非空，外边判断值
    }



    public Dictionary<int, List<string>> QueryInfo(string sql, MySqlConnection con)
    {
        int indexDic = 0;
        int indexList = 0;
        Dictionary<int, List<string>> dic = new Dictionary<int, List<string>>();
        MySqlCommand com = new MySqlCommand(sql, con);
        MySqlDataReader reader = com.ExecuteReader();
        while (true)
        {
            if (reader.Read())
            {
                List<string> list = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    list.Add(reader[indexList].ToString());
                    indexList++;
                }
                dic.Add(indexDic, list);
                indexDic++;
                indexList = 0;
            }
            else
            {
                break;
            }
        }
        return dic;
    }


    public DataSet Update(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " =" + colsvalues[i];
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return QuerySet(query);
    }



    public void Delete(string sql, MySqlConnection con)
    {
        //MySqlCommand com = new MySqlCommand(sql, con);
        //int res = com.ExecuteNonQuery();
    }


    public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];
        }
        return QuerySet(query);
    }

    public void Close()
    {
        if (cono != null)
        {
            cono.Close();
            cono.Dispose();
            cono = null;
        }
    }
    public static DataSet QuerySet(string sqlString)
    {
        if (cono.State == ConnectionState.Open)
        {
            //U_Debug.LogStr(sqlString);
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString, cono);

                mySqlDataAdapter.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("SQL:" + sqlString + "/n" + e.Message.ToString());
            }
            finally
            {
            }
            return ds;
        }
        return null;
    }






    public static T ToModel<T>(DataRow dr) where T : new()
    {
        if (dr == null)
        {
            return new T();
        }
        T t = new T();
        PropertyInfo[] propertys = t.GetType().GetProperties();
        //Type type = typeof(T);
        string tempName = "";
        foreach (PropertyInfo pi in propertys)
        {
            tempName = pi.Name;
            if (dr.Table.Columns.Contains(tempName))
            {
                if (!pi.CanWrite)
                    continue;
                object value = dr[tempName];
                if (value != DBNull.Value)
                {
                    pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType, CultureInfo.CurrentCulture), null);
                }
            }
        }
        return t;

    }

    public static List<T> ToModelS<T>(DataTable dt) where T : new()
    {
        List<T> ts = new List<T>();
        string tempName = "";
        foreach (DataRow dr in dt.Rows)
        {
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;
                if (dt.Columns.Contains(tempName))
                {
                    if (!pi.CanWrite)
                        continue;
                    object value = dr[tempName];
                    if (value != DBNull.Value)
                    {
                        pi.SetValue(t, Convert.ChangeType(value, pi.PropertyType, CultureInfo.CurrentCulture), null);
                    }
                }
            }
            ts.Add(t);
        }
        return ts;
    }
    public List<T> ToModelS<T>(string tableName) where T : new()
    {
        DataTable dt = GetTable(tableName);
        return ToModelS<T>(dt);
    }
}
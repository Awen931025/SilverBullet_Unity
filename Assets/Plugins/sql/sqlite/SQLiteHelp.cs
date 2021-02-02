using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;

///<summary>
///数据库辅助类
///</summary>
public class SQLiteHelp
{
    public SqliteConnection con;
    private SqliteCommand cmd;
    private SqliteDataReader dbReader;
    private SqliteDataAdapter da;
    //4.x
    public SQLiteHelp(string conStr)
    {
        try
        {
            con = new SqliteConnection(conStr);
            con.Open();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }




    public DataTable ExecuteDataTable(string sql, SqliteParameter[] parameters)
    {
        if (parameters != null)
        {
            cmd.Parameters.AddRange(parameters);
        }
        SqliteDataAdapter adapter = new SqliteDataAdapter(cmd);
        DataTable data = new DataTable();
        adapter.Fill(data);
        return data;
    }



    /// <summary>
    /// 执行查询语句(DataSet)
    /// </summary>
    /// <param name="sql">查询语句
    /// <returns>DataSet</returns>
    public DataSet QuerySqlReturnDataSet(string sql)
    {
        DataSet ds = new DataSet();

        try
        {
            cmd = con.CreateCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;

            da = new SqliteDataAdapter(cmd);

            //cmd.Connection.Open();

            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Connection.Close();
        }
    }











    ////3.5
    //public SQLiteHelp()
    //{
    //    //string path = "";
    //    string path = @"Data Source = E:/LS/Assets/StreamingAssets\Sqlite\jsjls.sqlite";
    //    dbConnection = new SqliteConnection(path);    // 创建SQLite对象的同时，创建SqliteConnection对象
    //    dbConnection.Open();                          // 打开数据库链接
    //}



    //创建表
    public SqliteDataReader CreateTable(string tabName, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new SqliteException("columns.Length != colType.Length");
        }

        string query = "CREATE TABLE " + tabName + " (" + col[0] + " " + colType[0];

        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col[i] + " " + colType[i];
        }

        query += ")";

        return ExecuteQuery(query);
    }

    //连接数据库
    public void CloseSqlConnection()

    {
        if (cmd != null)
        {
            cmd.Dispose();
        }
        cmd = null;

        if (dbReader != null)
        {
            dbReader.Dispose();
        }
        dbReader = null;

        if (con != null)
        {
            con.Close();
        }
        con = null;

        //Debug.Log("断开连接");
    }

    //执行sqlQuery操作 
    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        cmd = con.CreateCommand();
        cmd.CommandText = sqlQuery;
        dbReader = cmd.ExecuteReader();
        //Debug.Log(sqlQuery);
        return dbReader;
    }

    //插入数据
    public SqliteDataReader InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES (" + values[0];

        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + values[i];
        }

        query += ")";

        return ExecuteQuery(query);
    }

    public DataTable SelectAllTable(string tableName)
    {
        DataTable dt = this.QuerySqlReturnDataSet("SELECT * FROM " + tableName).Tables[0];
        return dt;
    }
    public SqliteDataReader ExecuteQueryString(string query)
    {
        return ExecuteQuery(query);
    }

    //查找表中所有数据
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string query = "SELECT * FROM " + tableName;

        return ExecuteQuery(query);
    }

    //查找表中指定数据
    public SqliteDataReader ReadSpecificData(string tableName, string selectkey, string selectvalue)
    {
        string query = "SELECT * FROM " + tableName + " where " + selectkey + " = " + selectvalue + " ";

        return ExecuteQuery(query);
    }
    public SqliteDataReader GetTop1(string tableName)
    {
        string query = "select * from " + tableName + " where rowid = 1";
        //Debug.Log(query);
        return ExecuteQuery(query);
    }

    //更新数据  SQL语法：UPDATE table_name SET column1 = value1, column2 = value2....columnN = valueN[WHERE  CONDITION];
    public SqliteDataReader Update(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        //不带单引号查数字，有时候更新不到。
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = '" + colsvalues[0]+"'";

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols[i] + " ='" + colsvalues[i]+"'";
        }

        query += " WHERE " + selectkey + " = '" + selectvalue + "';";
        //Debug.Log("???："+query);
        return ExecuteQuery(query);
    }

    public SqliteDataReader Delete(string tableName, string col, string values)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + col + " = " + values;
        return ExecuteQuery(query);
    }
    //删除表中的内容  DELETE FROM table_name  WHERE  {CONDITION or CONDITION}(删除所有符合条件的内容）
    public SqliteDataReader Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];

        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols[i] + " = " + colsvalues[i];
        }

        return ExecuteQuery(query);
    }

    //插入指定的数据
    public SqliteDataReader Insert(string tableName, string[] cols, string[] values)
    {
        if (cols.Length != values.Length)
        {
            throw new SqliteException("columns.Length != values.Length");
        }

        string query = "INSERT INTO " + tableName + "(" + cols[0];

        for (int i = 1; i < cols.Length; ++i)
        {
            query += ", " + cols[i];
        }

        query += ") VALUES ('" + values[0]+"'";

        for (int i = 1; i < values.Length; ++i)
        {
            query += ", '" + values[i]+"'";
        }

        query += ")";
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    //判断在指定列名中是否存在输入的值
    public bool ExitItem(string tableName, string itemName, string itemValue)
    {
        bool flag = false;

        dbReader = ReadFullTable(tableName);

        while (dbReader.Read())
        {
            for (int i = 0; i < dbReader.FieldCount; i++)
            {
                if (dbReader.GetName(i) == itemName)
                {
                    if (dbReader.GetValue(i).ToString() == itemValue)
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }

        return flag;
    }




    public SqliteDataReader SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {

        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new SqliteException("col.Length != operation.Length != values.Length");
        }

        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {

            query += ", " + items[i];

        }

        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "' ";

        for (int i = 1; i < col.Length; ++i)
        {

            query += " AND " + col[i] + operation[i] + "'" + values[i] + "' ";

        }



        //string query = @"select * from table where rowid = 1";
        //Debug.Log(query);
        return ExecuteQuery(query);
    }








    public SqliteDataReader SelectWhereContain(string tableName, string[] items, string col, string values)
    {
        string query = "SELECT " + items[0];

        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items[i];
        }

        query += " FROM " + tableName + " WHERE " + col + " like '%" + values + "%'";
        //Debug.Log(query);
        return ExecuteQuery(query);
    }



}
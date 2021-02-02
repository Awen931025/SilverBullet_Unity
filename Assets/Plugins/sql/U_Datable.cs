using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
namespace W
{
    public class U_Datable<T> where T : new()
    {
        /// table转实体集合
        public static List<T> ToEntity(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;
            List<T> result = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    T res = new T();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        PropertyInfo propertyInfo = res.GetType().GetProperty(dr.Table.Columns[i].ColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (propertyInfo != null && dr[i] != DBNull.Value)
                        {
                            var value = dr[i];
                            switch (propertyInfo.PropertyType.FullName)
                            {
                                case "System.Decimal":
                                    propertyInfo.SetValue(res, Convert.ToDecimal(value), null); break;
                                case "System.String":
                                    propertyInfo.SetValue(res, value, null); break;
                                case "System.Int32":
                                    propertyInfo.SetValue(res, Convert.ToInt32(value), null); break;
                                default:
                                    propertyInfo.SetValue(res, value, null); break;
                            }
                        }
                    }
                    result.Add(res);
                }
                catch (Exception ex)
                {
                    continue;
                }

            }
            return result;
        }

        /// 实体类集合转table
        public static DataTable ToDataTable(List<T> modelList)
        {
            if (modelList == null || modelList.Count == 0)
                return null;
            DataTable dt = CreatTable(modelList[0]);
            foreach (T model in modelList)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo p in typeof(T).GetProperties())
                {
                    dr[p.Name] = p.GetValue(model, null);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// 根据实体创建table
        static DataTable CreatTable(T model)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(p.Name, p.PropertyType));
            }
            return dt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace Uni.Core.Common.Utils
{
    public class ExcelHelper
    {
        /// <summary>
        /// excel表数据
        /// </summary>
        private static DataTable ExcelDatatable = null;

        /// <summary>
        /// 解析excel
        /// </summary>
        /// <param name="excelpath">excel路径</param>
        /// <param name="sheet">数据表名</param>
        /// <returns></returns>
        public static DataTable ExcelToData(string excelpath, string sheet)
        {
            try {
                if (File.Exists(excelpath))
                {
                    string excelversion = Path.GetExtension(excelpath);
                    if (excelversion == ".xls")
                    {
                        ExcelDatatable = ExcelToData(excelpath, sheet, "2003");
                    }
                    else if (excelversion == ".lsx")
                    {
                        ExcelDatatable = ExcelToData(excelpath, sheet, "2007");
                    }
                }
                return ExcelDatatable;
            }catch(Exception e){
                return null;
            }
            
        }

        /// <summary>
        /// 解析excel
        /// </summary>
        /// <param name="excelpath">excel路径</param>
        /// <param name="sheet">数据表名</param>
        /// <param name="version">excel版本</param>
        /// <returns></returns>
        public static DataTable ExcelToData(string excelpath, string sheet, string version)
        {
            try {
                string selectcontent = string.Format("select * from [{0}$]", sheet);

                switch (version)
                {
                    case "2003":
                        // Extended Properties :HDR=No属性把表头变成数据行 IMEX=1把所有列都做为字符串来读取 
                        string xlsconnect = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + excelpath + ";" + "Extended Properties='Excel 8.0;HDR=No;IMEX=1';";
                        DataSet Exldt = new DataSet();
                        //连接数据源
                        OleDbConnection xlsconn = new OleDbConnection(xlsconnect);
                        xlsconn.Open();
                        OleDbDataAdapter xlsadapter = new OleDbDataAdapter(selectcontent, xlsconnect);
                        xlsadapter.Fill(Exldt, sheet);
                        ExcelDatatable = Exldt.Tables[sheet];
                        xlsconn.Close();
                        break;
                    case "2007":
                        string xlsxconnect = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + excelpath + ";" + "Extended Properties='Excel 12.0;HDR=No;IMEX=1';";
                        DataSet Exlsdt = new DataSet();
                        //连接数据源
                        OleDbConnection lsxconn = new OleDbConnection(xlsxconnect);
                        lsxconn.Open();
                        OleDbDataAdapter lsxadapter = new OleDbDataAdapter(selectcontent, xlsxconnect);
                        lsxadapter.Fill(Exlsdt, sheet);
                        ExcelDatatable = Exlsdt.Tables[sheet]; ;
                        lsxconn.Close();
                        break;
                    default:
                        break;
                }
                return ExcelDatatable;
            }catch(Exception e){
                return null;
            }
            
        }
    }
}

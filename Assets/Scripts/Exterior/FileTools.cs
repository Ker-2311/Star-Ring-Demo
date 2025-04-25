using ExcelDataReader;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Exterior
{
    public class FileTools
    {
        /// <summary>
        /// 转化一个Excel文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="desPath"></param>
        public static void ConvertCSV(string filePath, string desPath,string name = null)
        {
            //var filePath = Application.dataPath + "/../Config/Test.xlsx";
            //var desPath = Application.dataPath + "/../Config/TestCSV.csv";
            var fileName = Path.GetFileName(filePath);
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var excelReader = ExcelReaderFactory.CreateReader(fileStream);
            var dataSet = excelReader.AsDataSet();
            var desPathFile = desPath + fileName.Replace(".xlsx",".csv");
            if (name != null)
            {
                desPathFile = desPath + name;
            }
            //如果没有表则返回
            if (dataSet.Tables.Count < 1) return;
            if (dataSet.Tables[0].Rows.Count < 1) return;

            var firstSheet = dataSet.Tables[0];
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < firstSheet.Rows.Count; i++)
            {
                for (int j = 0; j < firstSheet.Columns.Count; j++)
                {
                    stringBuilder.Append(firstSheet.Rows[i][j]);
                    if (j < (firstSheet.Columns.Count - 1)) stringBuilder.Append(",");
                }
                stringBuilder.Append("\r\n");
            }

            fileStream.Close();

            WriteCoverFile(desPathFile, stringBuilder.ToString());
        }

        /// <summary>
        /// 写一个文件，如果已存在则覆盖
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteCoverFile(string path,string content)
        {
            if (File.Exists(path)) Directory.Delete(path);

            using (var file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (var textWriter = new StreamWriter(file, Encoding.UTF8))
                {
                    textWriter.Write(content);
                }
            }
        }
        
    }
}


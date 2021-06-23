using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 读取配置表的工具类
/// </summary>
public class ExcelData 
{
    //读取加载TXT配置文件
	public static void LoadExcelFormCSV (string cfgName,out Dictionary<string,Dictionary<string,string>> DicContent)
	{
        //创建一个TextAsset，通过Resources.Load来加载配置表
        TextAsset tw = Resources.Load("Cfg/"+ cfgName) as TextAsset;
        //存放数据表里面的所有内容
        string strLine = tw.text;
       // Debug.Log(strLine);
        //创建一个字典，用于存放数据表的内容（从配置表第四行读取）//<字段名，字段值(string数组)>
        //key是字段的名字   value是数组  这个数组里面存储的是  字段值
        Dictionary<string, string[]> content = new Dictionary<string, string[]>();


        //开始解析字符串
		ExplainString (strLine, content,out DicContent );
	}


    //解析字符串
    public static void ExplainString(string strLine,  Dictionary<string, string[]> content, out Dictionary<string, Dictionary<string, string>> DicContent)
	{
        //创建一个字典，用于存放默认值的//<字段名，默认值>
		Dictionary<string, string> initNum = new Dictionary<string, string> ();
         //通过换行符来切割配置表里面的内容，使之成为一行一行的数据
        
		string[] lineArray = strLine.Split (new char[]{'\n'});
        //获取行数   //由于在切割的时候会多去一行  所以要-1   可以在调试的时候测试一下就知道了
        int rows = lineArray.Length - 1;

         //获取列数
        int Columns = lineArray[0].Split(new char[] { ',' }).Length;

        //定义一个数组用于存放字段名
        string[] ColumnName = new string[Columns];//有多少的字段就是有多少列

        for (int i = 0; i < rows; i++)//行
        {
            //每一行数据都根据逗号进行分割， 得到的数据存储到一个string数组中
            string[] Array = lineArray[i].Split(new char[] { ',' });
            for (int j = 0; j < Columns; j++)//列数
            {
                //获取Array的列的值
                string nvalue = Array[j].Trim();
                //可以使用下面的string nvalue = Array[j].ToString()  换
                //string nvalue = Array[j].Trim();
                //string nvalue = Array[j].ToString();
                //第一行字段名
                if (i == 0)
                {
                    //存储字段名
                    ColumnName[j] = nvalue;
                    //实例化字典，长度为rows - 3,因为是从第四行读取
                    content[ColumnName[j]] = new string[rows - 3];
                }
                //if (i == 1)//此时是读到配置表的第二行（字段的注释），不做处理，配置表所填的值，类型都为string
                //{                  
                //}
                else if (i == 2)//第三行默认值
                {
                    //存储对应字段的默认值//<字段名，默认值>
                    initNum[ColumnName[j]] = nvalue;
                }
                else if (i >= 3)//第三行开始是实际数据，存储实际数据
                {
                    //<字段名，字段值(string数组)>
                    content[ColumnName[j]][i - 3] = nvalue;
                    if (nvalue == "")//如果读到的值为空字符，则给予默认值（默认值就是配置表第三行所填的值）
                    {
                        content[ColumnName[j]][i - 3] = initNum[ColumnName[j]];
                        //<字段名，字段值(string数组)>
                    }
                }

            }
        }

        //-------------上面是把字符串解析到字典  content中去------------

        //下面这个函数开始把content中的数据解析到字典中DicContent去
        //开始解析
        ExplainDictionary(content, out DicContent);
	}



    //解析字典中的数据
     public static void ExplainDictionary(Dictionary<string,string[]> content,out Dictionary<string, Dictionary<string, string>> DicContent)
     {
         //实例化字典//<字段名><<ID><字段值>>
         DicContent = new Dictionary<string, Dictionary<string, string>>();
         //获取字典中所有的键(字段名)
         Dictionary<string, string[]>.KeyCollection Allkeys = content.Keys;
         //遍历所有的字段名
         foreach (string key in Allkeys)
         {
             //实例化一个hasData的字典//<ID,字段值>
             Dictionary<string, string> hasData = new Dictionary<string, string>();
             //获取当前遍历到的key(字段名)所对应的字段值，存到数组里面
             string[] Data = content[key];          //<字段名，字段值(string数组)>
             //循环这个数组
             for (int i = 0; i < Data.Length; i++)
             {
                 //<ID><字段值>
                 hasData[content["ID"][i]] = Data[i];
             }
             //最后给DicContent这个字典赋值
             DicContent[key] = hasData;//<字段名><<ID><字段值>>  例如：<Name><<1><红石元>>
         }
     }


    /// <summary>
    /// 根据字段的名字和ID来读取字典中单条的数据信息
    /// </summary>
    /// <returns></returns>
     public static string ReadWithNameAndID(string name,int id, Dictionary<string, Dictionary<string, string>> DicContent)
    {
        string str = "";
        if(DicContent.ContainsKey(name))
        {
            if(DicContent[name].ContainsKey(id.ToString()))
            {
                str = DicContent[name][id.ToString()];
                return str;
            }
        }
        return str;
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    Dictionary<string, Dictionary<string, string>> dic;

    void Start()
    {
        ExcelData.LoadExcelFormCSV("HeroCfg", out dic);
        //在访的时候写上字段名字   ID  就可以了
        //向下面这样的用法就可以了。。。。
        print("打印的应该是吕布就对了。。。。"+dic["HeroName"]["2"]);



        //方法二
        string str = ExcelData.ReadWithNameAndID("Def", 2, dic);
        int a= int.Parse(str);


        print(a);

    }

    void Update()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataControl :Singleton<DataControl>
{
    //存放配包配置表的字典
    public Dictionary<string, Dictionary<string, string>> dicPack;

    public Dictionary<string, Dictionary<string, string>> diclevel;

    //存放物品的配置表的字典

    public Dictionary<string, Dictionary<string, string>> dicItem;


    //加载游戏中所有数据的方法
    public void LoadAllCfg()
    {
        this.LoadPackCfg();
        this.LoadLevelCfg();
        this.LoadItemCfg();

    }

    /// <summary>
    /// 加载背包配置表的方法
    /// </summary>
    public void LoadPackCfg()
    {
        ExcelData.LoadExcelFormCSV("PackCfg", out dicPack);

    }

    public void LoadItemCfg()
    {
        ExcelData.LoadExcelFormCSV("ItemCfg", out dicItem);

    }

    public void LoadLevelCfg()
    {
        ExcelData.LoadExcelFormCSV("LevelCfg", out diclevel);

    }

  

    //读取数据的方法
    //通过字段名  id  来获取数据
    public string ReadData(string keyWord,int id,
        Dictionary<string, Dictionary<string, string>> theDic)
    {
        return theDic[keyWord][id.ToString()];


    }

    //获得配置表的数据量
    public int GetInforCount(Dictionary<string, Dictionary<string, string>> theDic)
    {
        int count = 0;
        foreach (KeyValuePair<string, Dictionary<string, string>> item in theDic)
        {
            count = item.Value.Count;
            break;

        }
        return count;

    }

    
}

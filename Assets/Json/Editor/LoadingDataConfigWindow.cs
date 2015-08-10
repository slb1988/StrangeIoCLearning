using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class LoadingDataConfigWindow : ScriptableWizard
{
    public List<string> NotifyString;
    //改成 Application.persistentDataPath永久存储
    private readonly string LOADING_DATA_CONFIG_URL = Application.dataPath + @"/LoadNotify.data";


    public LoadingDataConfigWindow()
    {
        NotifyString = DataStoreProcessor.SharedInstance.Load<List<string>>(LOADING_DATA_CONFIG_URL, false);
    }

    [MenuItem("GameObject/Data Setting/Loading text")]
    static void CreateWizard()
    {
        LoadingDataConfigWindow window = DisplayWizard<LoadingDataConfigWindow>("配置登陆提示文字", "确认", "取消");
        window.minSize = new Vector2(1024, 768);
    }

    // This is called when the user clicks on the Create button.  
    void OnWizardCreate()
    {
        DataStoreProcessor.SharedInstance.Save(NotifyString, LOADING_DATA_CONFIG_URL, false);
        Debug.Log(string.Format(" 保存成功，共计录入 {0} 数据", NotifyString.Count));
    }

    // Allows you to provide an action when the user clicks on the   
    // other button "Apply".  
    void OnWizardOtherButton()
    {
        Debug.Log("取消");
    }
}

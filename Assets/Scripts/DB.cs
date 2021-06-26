using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DB : MonoBehaviour
{
    [SerializeField] public InfoAccount infoAccount;
    
    public static DB instance;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
        }
           
       
    }
   
    public void SetData(int lvl, string nameVar, string value)
    {
        switch (lvl)
        {
            case 0:
                infoAccount.lvl1.data[nameVar] = value;
                string s1 = "";
                foreach (var item in infoAccount.lvl1.data)
                {
                    s1 += ($"{item.Key} : {item.Value} \n");
                }
                print(s1);
                break;
            case 1:
                infoAccount.lvl2.data[nameVar] = value;
                string s2 = "";
                foreach (var item in infoAccount.lvl2.data)
                {
                    s2 += ($"{item.Key} : {item.Value} \n");
                }
                print(s2);
                break;
            case 2:
                infoAccount.lvl3.data[nameVar] = value;
                string s3 = "";
                foreach (var item in infoAccount.lvl3.data)
                {
                    s3 += ($"{item.Key} : {item.Value} \n");
                }
                print(s3);
                break;
            case 3:
                infoAccount.lvl4.data[nameVar] = value;
                string s4 = "";
                foreach (var item in infoAccount.lvl4.data)
                {
                    s4 += ($"{item.Key} : {item.Value} \n");
                }
                print(s4);
                break;
            case 4:
                infoAccount.lvl5.data[nameVar] = value;
                string s5 = "";
                foreach (var item in infoAccount.lvl5.data)
                {
                    s5 += ($"{item.Key} : {item.Value} \n");
                }
                print(s5);
                break;
        }
        
    }
    public void SetGeneralOption(string namevar, string value)
    {
        infoAccount.generalOption[namevar] = value;
    }
    public void SetDataToQuestion1(string namevar,string value)
    {
        infoAccount.question1[namevar] = value;
    }
    public void SetDataToQuestion2(string namevar, string value)
    {
        infoAccount.question2[namevar] = value;
    }
    //public void SaveJson()
    //{
    //    var json = JsonConvert.SerializeObject(infoAccount,Formatting.Indented);
       
    //    var data = File.CreateText(Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)+"/data.json");
    //    print(json);
    //    data.Write(json);
    //    data.Close();
    //    print("Save Json");

    //}

}
[Serializable]
public class InfoAccount
{
    public string userName = "0", gender = "0", education = "0", major = "0", mail = "0", city="0",age="0";
    public string main_timer = "0";
    public string coin = "0";
    public string score = "0";
    [SerializeField]
    public Dictionary<string, string> generalOption = new Dictionary<string, string>()
    {
        {"wireless","0" },
        {"backup","0" },
        {"reportSecurity","0" }
    };
    [SerializeField] public Lvl1 lvl1;
    [SerializeField] public Lvl2 lvl2;
    [SerializeField] public Lvl3 lvl3;
    [SerializeField] public Lvl4 lvl4;
    [SerializeField] public Lvl5 lvl5;
    [SerializeField]
    public Dictionary<string, string> question1 = new Dictionary<string, string>()
    {
        {"timer","00:00"},
        { "1","0"},
        { "2","0"},
        { "3","0"},
        { "4","0"},
        { "5","0"},
        { "6","0"},
        { "7","0"},
        { "8","0"},
        { "9","0"},
        { "10","0"},
        { "11","0"},
        { "12","0"},
        { "13","0"},
        { "14","0"},
        { "15","0"},
        { "16","0"},
        { "17","0"},
        { "18","0"},
        { "19","0"},
        { "20","0"},
        { "21","0"},

    };
    [SerializeField]
    public Dictionary<string, string> question2 = new Dictionary<string, string>()
    {
        {"timer","00:00"},
        { "1","0"},
        { "2","0"},
        { "3","0"},
        { "4","0"},
        { "5","0"},
        { "6","0"},
        { "7","0"},
        { "8","0"},
        { "9","0"},
        { "10","0"},
        { "11","0"},
        { "12","0"},
    };

    public void IsFullAllSpecification(Action<bool> result)
    {
        if(userName.Length>0 && gender.Length>0 && education.Length>0 && major.Length>0 && mail.Length>0 && city.Length>0 && age.Length>0 )
        result(true);
        else
        result(false);
    }
}
[Serializable]
public class LvlInfo
{
    public string nameLvl = "0";
    public string timerLvl="0:0",timerMiniGame = "0:0";
}
[Serializable]
public class Lvl1 : LvlInfo
{
    public Dictionary<string, string> data = new Dictionary<string, string>()
    {
        {"q1","0" },
        {"q2","0" },
        {"q3","0" },
        {"q4","0" },
        {"q5","0" },
        {"passMail","0" },
    };
    //q1, q2, q3, q4, q5, passMail;
}
[Serializable]
public class Lvl2 : LvlInfo
{
    //public int q1, q2, q3, q4;
    public Dictionary<string, string> data = new Dictionary<string, string>()
    {
        {"q1","0" },
        {"q2","0" },
        {"q3","0" },
        {"q4","0" },
    };
}
[Serializable]
public class Lvl3 : LvlInfo
{
    //public int openPopup, closePopup, sourceTrue, sourceFalse, q1;
    public Dictionary<string, string> data = new Dictionary<string, string>()
    {
        {"openPopup","0" },
        {"blockedPopup","0" },
        {"authenticSource","0" },
        {"invalidSource","0" },
        {"q1","0" },
    };
}
[Serializable]
public class Lvl4 : LvlInfo
{
    // public int q1, q2;
    public Dictionary<string, string> data = new Dictionary<string, string>()
    {
        {"q1","0" },
        {"q2","0" },
    };
}
[Serializable]
public class Lvl5 : LvlInfo
{
   // public int q1, q2, q3;
    public Dictionary<string, string> data = new Dictionary<string, string>()
    {
        {"q1","0" },
        {"q2","0" },
        {"q3","0" },
    };
}





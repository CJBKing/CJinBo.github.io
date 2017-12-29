using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillDescribe : MonoBehaviour {
   
    public  static SkillDescribe _instance;
    private Text textSkillName;
    private Text textCD;
    private Text textTakeMagic;
    private Text SkillDesc;
    private Dictionary<string[], string[]> dicSkill = new Dictionary<string[], string[]>();
    void Awake()
    {
        _instance = this;
        textSkillName = transform.Find("SkillName").GetComponent<Text>();
        textCD = transform.Find("TextCD/Text").GetComponent<Text>();
        textTakeMagic = transform.Find("TextTakeMagic/Text").GetComponent<Text>();
        SkillDesc = transform.Find("TextSkillDescribe").GetComponent<Text>();
    }
    public void ShowSkillInfo(string skillName)
    {
        if(skillName!=null&&textSkillName!=null)
        textSkillName.text = skillName+":";
        for (int i = 0; i < skillName.Length; i++)
        {
         
        }

    }
}

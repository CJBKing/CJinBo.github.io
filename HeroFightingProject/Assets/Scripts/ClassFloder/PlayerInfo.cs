using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerInfo:MonoBehaviour{
    public string heroName
    {
        get;
        set;
    }
    public string  HP
    {
        get;
        set;
    }
    public string Protect
    {
        get;
        set;
    }
    public string Power
    {
        get;
        set;
    }
    public string Magical
    {
        get;
        set;
    }
    public string  HPRecover
    {
        get;
        set;
    }
    public string  MagicalRecover
    {
        get;
        set;
    }
    public string  MoveSpeed
    {
        get;
        set;
    }

    public List<SkillInfo> skillList = new List<SkillInfo>();
    public string skill1Name
    {
        get;
        set;
    }
    public string skill2Name
    {
        get;
        set;
    }
    public string skill3Name
    {
        get;
        set;
    }
    public string skill1Damage
    {
        get;
        set;
    }
    public string skill2Damage { get; set; }
    public string skill3Damage { get; set; }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class ProperityPanel : MonoBehaviour {
    public static ProperityPanel _instance;
    public TextAsset heroInfo;
    private Text heroName;
    private Text textHp;
    private Text textDamage;
    private Text textMagical;
    private Text textHpRecover;
    private Text textMagicalRecover;
    private Text textMoveSpeed;
    private Image skill1;
    private Image skill2;
    private Image skill3;
    private Text skill1Name;
    private Text skill2Name;
    private Text skill3Name;
    private string[] Heros;
    
    public List<PlayerInfo> heroList = new List<PlayerInfo>();
    void Awake()
    {
        _instance = this;
        heroName = transform.Find("TextHeroName").GetComponent<Text>();
        textHp = transform.Find("TextHp/Text").GetComponent<Text>();
        textDamage = transform.Find("TextDamage/Text").GetComponent<Text>();
        textMagical = transform.Find("TextMagical/Text").GetComponent<Text>();
        textHpRecover = transform.Find("TextHpRecover/Text").GetComponent<Text>();
        textMagicalRecover= transform.Find("TextMagicalRecover/Text").GetComponent<Text>();
        textMoveSpeed = transform.Find("TextMoveSpeed/Text").GetComponent<Text>();
        skill1 = transform.Find("Skills/Skill1").GetComponent<Image>();
        skill2 = transform.Find("Skills/Skill2").GetComponent<Image>();
        skill3 = transform.Find("Skills/Skill3").GetComponent<Image>();
        skill1Name = transform.Find("Skills/Skill1/Text").GetComponent<Text>();
        skill2Name = transform.Find("Skills/Skill2/Text").GetComponent<Text>();
        skill3Name = transform.Find("Skills/Skill3/Text").GetComponent<Text>();
        if (heroInfo != null)
        {
            string heroInfoStr = heroInfo.ToString();
            Heros = heroInfoStr.Split('\n');
            for (int i = 0; i < Heros.Length; i++)
            {
                string[] herosInfos= Heros[i].Split('|');
                PlayerInfo playerInfo = new PlayerInfo();
             
                playerInfo.heroName = herosInfos[0].ToString();
                playerInfo.HP =herosInfos[1].ToString();
                playerInfo.HPRecover = herosInfos[2].ToString();
                playerInfo.MagicalRecover = herosInfos[3].ToString();
                playerInfo.Power = herosInfos[10].ToString();
                playerInfo.MoveSpeed = herosInfos[4].ToString();
                playerInfo.Magical = herosInfos[5].ToString();
                for (int j = 0; j < 4; j++)
                {
                    //普攻,技能1，技能2，技能3；Cost,14开始
                    SkillInfo skill = new SkillInfo();
                    skill.Name = herosInfos[6 + j].ToString();
                    skill.Damage = herosInfos[10 + j].ToString();
                    skill.Cost = herosInfos[14 + j].ToString();
                    playerInfo.skillList.Add(skill);
                }
                heroList.Add(playerInfo);
            }
        }
    }

    public void HeroInfoChanged(string nameStr)
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            if (heroList[i].heroName == nameStr)
            {
                heroName.text = heroList[i].heroName;
                textHp.text = heroList[i].HP.ToString();
                textDamage.text = heroList[i].Power.ToString();
                textMagical.text = heroList[i].Magical.ToString();
                textHpRecover.text = heroList[i].HPRecover.ToString();
                textMagicalRecover.text = heroList[i].MagicalRecover.ToString();
                textMoveSpeed.text = heroList[i].MoveSpeed.ToString();
            }
        }
    }

}


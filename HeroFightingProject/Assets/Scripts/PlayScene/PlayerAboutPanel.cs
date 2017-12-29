using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class PlayerAboutPanel : MonoBehaviour {
    public static PlayerAboutPanel _instance;
    public PlayerInfo player;
    public TextAsset heroInfo;
    private string[] Heros;
    public List<PlayerInfo> heroList = new List<PlayerInfo>();

    private Text heroName;
    public Text HPText;
    public Text MPText;
    public Text HPRecoverText;
    public Text MPRecoverText;
    public Text NormalDamage;
    public Text MoveSpeed;
    private Text Skill1Name;
    private Text Skill2Name;
    private Text Skill3Name;
    private Text Skill1Damage;
    private Text Skill2Damage;
    private Text Skill3Damage;
    private Text Skill1Cost;
    private Text Skill2Cost;
    private Text Skill3Cost;
    void Awake()
    {
        _instance = this;
        heroName = transform.Find("HeroName").GetComponent<Text>();
        HPText = transform.Find("HPText").GetComponent<Text>();
        MPText = transform.Find("MPText").GetComponent<Text>();
        HPRecoverText = transform.Find("HPRecoverText").GetComponent<Text>();
        MPRecoverText = transform.Find("MPRecoverText").GetComponent<Text>();
        NormalDamage = transform.Find("NormalDamage").GetComponent<Text>();
        MoveSpeed = transform.Find("MoveSpeed").GetComponent<Text>();
        Skill1Name = transform.Find("Skill1").GetComponent<Text>();
        Skill2Name = transform.Find("Skill2").GetComponent<Text>();
        Skill3Name = transform.Find("Skill3").GetComponent<Text>();
        Skill1Damage = transform.Find("Skill1/damage").GetComponent<Text>();
        Skill2Damage = transform.Find("Skill2/damage").GetComponent<Text>();
        Skill3Damage = transform.Find("Skill3/damage").GetComponent<Text>();

        Skill1Cost = transform.Find("Skill1/cost").GetComponent<Text>();
        Skill2Cost = transform.Find("Skill2/cost").GetComponent<Text>();
        Skill3Cost = transform.Find("Skill3/cost").GetComponent<Text>();

        if (heroInfo != null)
        {
            string heroInfoStr = heroInfo.ToString();
            Heros = heroInfoStr.Split('\n');
            for (int i = 0; i < Heros.Length; i++)
            {
                string[] herosInfos = Heros[i].Split('|');
                PlayerInfo playerInfo = new PlayerInfo();

                playerInfo.heroName = herosInfos[0].ToString();
                playerInfo.HP = herosInfos[1].ToString();
                playerInfo.HPRecover = herosInfos[2].ToString();
                playerInfo.MagicalRecover = herosInfos[3].ToString();
                playerInfo.Power = herosInfos[10].ToString();
                playerInfo.MoveSpeed = herosInfos[4].ToString();
                playerInfo.Magical = herosInfos[5].ToString();
                for (int j = 0; j < 4; j++)
                {
                    //普攻,技能1，技能2，技能3；Cost,14开始
                    SkillInfo skill = new SkillInfo();
                    skill.Name = herosInfos[6+j].ToString();
                    skill.Damage = herosInfos[10+j].ToString();
                    skill.Cost = herosInfos[14 + j].ToString();
                    skill.CD = herosInfos[18 + j].ToString();
                    playerInfo.skillList.Add(skill);
                }
                heroList.Add(playerInfo);
            }
        }
    }
    void Start()
    {
        InitProperity();
    }
    void InitProperity()
    {
        int heroIndex = PlayerPrefs.GetInt("Player");
        Debug.Log("heroIndex:"+heroIndex);
        player = heroList[heroIndex];
        heroName.text = heroList[heroIndex].heroName;
        HPText.text = heroList[heroIndex].HP;
        MPText.text = heroList[heroIndex].Magical;
        HPRecoverText.text = heroList[heroIndex].HPRecover;
        MPRecoverText.text = heroList[heroIndex].MagicalRecover;
        NormalDamage.text = heroList[heroIndex].skillList[0].Damage;
        MoveSpeed.text = heroList[heroIndex].MoveSpeed;
        Skill1Name.text = heroList[heroIndex].skillList[1].Name;
        Skill2Name.text = heroList[heroIndex].skillList[2].Name;
        Skill3Name.text = heroList[heroIndex].skillList[3].Name;
        Skill1Damage.text = heroList[heroIndex].skillList[1].Damage;
        Skill2Damage.text = heroList[heroIndex].skillList[2].Damage;
        Skill3Damage.text = heroList[heroIndex].skillList[3].Damage;
        Skill1Cost.text = heroList[heroIndex].skillList[1].Cost;
        Skill2Cost.text = heroList[heroIndex].skillList[2].Cost;
        Skill3Cost.text = heroList[heroIndex].skillList[3].Cost;
        HeadBar._instance.HPText.text = HPText.text;
        HeadBar._instance.HeroName.text = heroName.text;
        HeadBar._instance.MPText.text = MPText.text;
        StartCoroutine(SetSelfActive());
    }
    IEnumerator SetSelfActive()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}

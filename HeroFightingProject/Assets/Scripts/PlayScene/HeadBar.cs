using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
public class HeadBar : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
    public static HeadBar _instance;
    public int count = 0;
    public List<Sprite> heroCard = new List<Sprite>();
    private GameObject PlayerAboutGo;
    private Button btnEndGame;
    private Slider HPSlider;
    private Slider MPSlider;
    public Text HPText;
    public Text MPText;
    public  Text HeroName;
    private Image headPic;

    private Text TimeText;
    private  Text CountText;

    private Slider turrentHpSlider;
    private Text turrentHp;
    private GameObject endGamePanelGo;
    int heroIndex;
    void Awake()
    {
        _instance = this;
        btnEndGame = transform.Find("BtnEndGame").GetComponent<Button>();
        HPSlider = transform.Find("HP").GetComponent<Slider>();
        MPSlider = transform.Find("Magical").GetComponent<Slider>();
        HPText = transform.Find("HP/HPValue").GetComponent<Text>();
        MPText = transform.Find("Magical/MagicalValue").GetComponent<Text>();
        HeroName = transform.Find("HeroName").GetComponent<Text>();
        headPic = transform.Find("HeadPic").GetComponent<Image>();
        TimeText = transform.Find("Time").GetComponent<Text>();
        CountText=transform.Find("Count").GetComponent<Text>();
        turrentHpSlider = transform.Find("TurrentHp").GetComponent<Slider>();
        turrentHp = transform.Find("TurrentHp/HpValue").GetComponent<Text>();
        endGamePanelGo = transform.parent.Find("EndGamePanel").gameObject;
        PlayerAboutGo = transform.parent.Find("PlayerAboutPanel").gameObject;
        Action a1 = new Action(OnBtnEndGameClick);
        btnEndGame.onClick.AddListener(OnBtnEndGameClick);
        heroIndex=PlayerPrefs.GetInt("Player");
        headPic.sprite =heroCard[heroIndex];

        

    }
    void Start()
    {
        PlayerMoveControl._instance.EventHelthChanged += HealthChangedEvent;
        PlayerMoveControl._instance.EventMPChanged += MPChangedEvent;
        GameObject.FindObjectOfType<Turrent>().turrentHealthChangedEvent += TurrentHealthChanged;

       
    }
    void Update()
    {
        if (GameController._instance.gameState == GameState.Start)
        {
            
            CalculateTime();
        }
        if (GameController._instance.gameState == GameState.End)
        {
            endGamePanelGo.SetActive(true);
            endGamePanelGo.GetComponent<EndGamePanel>().SetResult(CountText.text, TimeText.text);
        }
       
    }
    void CalculateTime()
    {
        if (EnemySource._instance.isMake)
        {
            float time = Time.time;
            float second = (int)time % 60;
            float minute = (int)time / 60;
            if (minute >= 0 && minute <= 9)
            {
                if (second <= 9)
                {
                    TimeText.text = "0" + minute + ":0" + second;
                }
                if (second > 9 && second < 60)
                {
                    TimeText.text = "0" + minute + ":" + second;
                }
            }
            if (minute > 9)
            {
                if (second <= 9)
                {
                    TimeText.text = minute + ":0" + second;
                }
                if (second > 9 && second < 60)
                {
                    TimeText.text = minute + ":" + second;
                }
            }
        }
    }
    public  void DesCountChanged()
    {
        CountText.text = count.ToString();
    }
    void HealthChangedEvent(float HP,float TotalLife)
    {
        if (HP <= TotalLife)
        {
            HPSlider.value = HP / TotalLife;
            HPText.text = HP.ToString();
        }
    }
    void MPChangedEvent(float MP,float totalMP)
    {
        if (MP <= totalMP)
        {
            MPSlider.value = MP / totalMP;
            MPText.text = MP.ToString();
        }
    }
    void TurrentHealthChanged(float damage,float totalHealth)
    {
        if (damage <= totalHealth)
        {
            turrentHpSlider.value = damage / totalHealth;
            turrentHp.text = damage.ToString();
        }
    }
    void OnBtnEndGameClick()
    {
        Application.LoadLevel(0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerAboutGo.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerAboutGo.SetActive(true);
    }
}

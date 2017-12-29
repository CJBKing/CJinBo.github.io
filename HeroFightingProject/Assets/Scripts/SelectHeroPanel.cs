using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SelectHeroPanel : BasePanel {
    private Button btnStartGame;
    private Button btnBack;
    private Text titleText;
    private Button btnSelectHero;
    private Image selectedCard;
    private GameObject HerolistBgPanel;
    private GameObject selectedHero;
    private GameObject otherSelectedPic;
    private Image otherSelectHead;
    int index=-1;
    void Awake()
    {
        btnStartGame = transform.Find("BtnStartGame").GetComponent<Button>();
        btnBack = transform.Find("BtnBacke").GetComponent<Button>();
        btnSelectHero = transform.Find("SelectedHeadPic").GetComponent<Button>();
        selectedCard = transform.Find("SelectedHeadPic/Image").GetComponent<Image>();
        titleText = transform.Find("Title").GetComponent<Text>();
        otherSelectedPic = transform.Find("OtherSelectedPic").gameObject;
        selectedHero = transform.Find("SelectedHero").gameObject;
        otherSelectHead = transform.Find("OtherSelectedPic/Image").GetComponent<Image>();

        btnStartGame.onClick.AddListener(OnStartGameClicked);
        btnBack.onClick.AddListener(OnBackClicked);
        btnSelectHero.onClick.AddListener(OnSelectHeroClicked);
    }
    public override void OnEnter()
    {
        Debug.Log("enter");
        gameObject.SetActive(true);
        iTween.ScaleTo(this.gameObject, new Vector3(1, 1, 1), 0.3f);
    }
    public override void ShowMessage(string msg)
    {
        titleText.text = msg;
        if(msg=="兵临城下")
        {

        }
        else if(msg=="实时对战")
        {
            otherSelectedPic.SetActive(true);
        }
    }
    public void OnStartGameClicked()
    {
        if (index >= 0)
        {
            PlayerPrefs.SetInt("Player", index);
            Application.LoadLevel(1);
        }
        else
        {
            //弹框提示没有选择激活英雄
        }
       
    }
    public void OnBackClicked()
    {
        iTween.ScaleTo(this.gameObject,new Vector3(0,0,0),0.3f);
        UIManager._Instnace.PopPanel();
    }
    public void OnSelectHeroClicked()
    {
        gameObject.SetActive(false);
        UIManager._Instnace.PopPanel();
        UIManager._Instnace.PushPanel(UiPanelType.HeroList);
    }
    public void SetSelectedPic(GameObject card)
    {
        selectedCard.sprite = card.GetComponent<Image>().sprite;
        string heroIndex=card.transform.Find("HeroName").GetComponent<Text>().text;
        int.TryParse(heroIndex,out index);
        HeroManager._instance.ShowSelectedHero(index);
       
    }
   
}

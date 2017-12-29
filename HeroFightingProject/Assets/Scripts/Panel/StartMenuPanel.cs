using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuPanel : BasePanel
{
    private Button fightButton;
    private Button singleButton;
    private Button selectHeroButton;
    void Start()
    {
        fightButton = transform.Find("Menu/Mask/Grid/Fighting").GetComponent<Button>();
        singleButton = transform.Find("Menu/Mask/Grid/Single").GetComponent<Button>();
        selectHeroButton = transform.Find("BtnSelectHero").GetComponent<Button>();

        fightButton.onClick.AddListener(OnFightingButtonClicked);
        singleButton.onClick.AddListener(OnSingleButtonClicked);
        selectHeroButton.onClick.AddListener(OnSelectHeroOnclicked);
    }
    public override UiPanelType uiPanelType
    {
        get
        {
            return base.uiPanelType;
        }

        set
        {
            base.uiPanelType = UiPanelType.StartMenu;
        }
    }
    public override void OnEnter()
    {
        gameObject.SetActive(true);

        StartCoroutine(EnterStartMenu());
    }
    IEnumerator EnterStartMenu()
    {
        yield return new WaitForSeconds(1);
        iTween.ScaleTo(gameObject, new Vector3(1, 1, 1), 0.5f);
    }
    void OnFightingButtonClicked()
    {
        UIManager._Instnace.PopPanel();
        HidenPanel();
        //BasePanel selectHeroPanel = UIManager._Instnace.PushPanel(UiPanelType.SelectHero, true);
        //selectHeroPanel.ShowMessage("实时对战");
    }
    void OnSingleButtonClicked()
    {
        UIManager._Instnace.PopPanel();
        HidenPanel();
        BasePanel selectHeroPanel = UIManager._Instnace.PushPanel(UiPanelType.SelectHero, true);
        selectHeroPanel.ShowMessage("兵临城下");
    }
    void OnSelectHeroOnclicked()
    {
        UIManager._Instnace.PopPanel();
        UIManager._Instnace.PushPanel(UiPanelType.HeroList);
        HidenPanel();
    }
    void HidenPanel()
    {
        iTween.ScaleTo(gameObject, new Vector3(0, 0, 0), 0.5f);
        //gameObject.SetActive(false);
    }

}

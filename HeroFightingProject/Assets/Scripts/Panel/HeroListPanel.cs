using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroListPanel : BasePanel {
    private Button buttonBack;
    public override UiPanelType uiPanelType
    {
        get
        {
            return base.uiPanelType;
        }

        set
        {
            base.uiPanelType = UiPanelType.HeroList;
        }
    }
    void Awake()
    {
        buttonBack = transform.Find("BtnBack").GetComponent<Button>();
        buttonBack.onClick.AddListener(OnButtonBackClicked);
    }
    void OnButtonBackClicked()
    {
        //back to Menu;
        UIManager._Instnace.PopPanel();
    }
    public override void OnEnter()
    {
       
    }
    public override void OnExit()
    {
        iTween.ScaleTo(this.gameObject, new Vector3(0, 0, 0), 0.5f);
    }
}

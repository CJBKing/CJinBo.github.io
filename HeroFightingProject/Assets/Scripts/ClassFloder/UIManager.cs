using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum UiPanelType
{
    Start,
    Register,
    Message,
    Scanner,
    StartMenu,
    HeroList,
    AboutHero,
    SelectHero
}

public class UIManager  {

    private static UIManager instnace;
    public static UIManager _Instnace
    {
        get
        {
            if (instnace == null) instnace = new UIManager();
            return instnace;
        }
    }
    private Transform canvasTrans;
    public Transform CanvasTrans
    {
        get
        {
            if (canvasTrans == null) canvasTrans = GameObject.Find("Canvas").transform;
            return canvasTrans;
        }
    }
    private Stack<BasePanel> panelStack;
    private Dictionary<UiPanelType, string> panelPathDic = new Dictionary<UiPanelType, string>();
    private Dictionary<UiPanelType, BasePanel> basePanelDic = new Dictionary<UiPanelType, BasePanel>();
    public UIManager()
    {
        InitialUIPanel();
    }
    void InitialUIPanel()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("UIPanelType");
        UIPanelInfo uiPanelInfo = JsonUtility.FromJson<UIPanelInfo>(textAsset.text);
        foreach(UIPanel uipanel in uiPanelInfo.infoList)
        {
            if (!panelPathDic.ContainsKey(uipanel.uiPanelType))
            {
                
                panelPathDic.Add(uipanel.uiPanelType,uipanel.path);
            }
        }
    }
    public BasePanel PushPanel(UiPanelType uiPanelType,bool isReturnPanel=false)
    {
        if (panelStack == null) panelStack = new Stack<BasePanel>();
        if(panelStack.Count>0)
        {
            BasePanel firstPanel = panelStack.Peek();
            firstPanel.OnPause();
        }
        BasePanel secondPanel = TryGetBasePanel(uiPanelType);
        secondPanel.OnEnter();
        if (!panelStack.Contains(secondPanel))
            panelStack.Push(secondPanel);
        if (isReturnPanel) return secondPanel;
        return null;
    }
    public void PopPanel(bool isDestroy = false)
    {
        if (panelStack == null || panelStack.Count <= 0)
            return;
        if (panelStack.Count > 0)
        {
            BasePanel basePanel = panelStack.Pop();
            if(isDestroy)
            {
                basePanelDic.Remove(basePanel.uiPanelType);
                GameObject.Destroy(basePanel.gameObject);
            }
            basePanel.OnExit();
        }

        if (panelStack.Count <= 0) return;

        BasePanel basePanel2 = panelStack.Peek();
        basePanel2.OnResume();

    }
    BasePanel TryGetBasePanel(UiPanelType uiPanleType)
    {
        BasePanel getBasePanel = basePanelDic.TryGet<UiPanelType, BasePanel>(uiPanleType);
        if(!getBasePanel)
        {
            string panelPath = panelPathDic.TryGet<UiPanelType, string>(uiPanleType);
            GameObject PanelGo = Resources.Load(panelPath) as GameObject;
            GameObject uiPanelGo = GameObject.Instantiate(PanelGo);
            uiPanelGo.transform.SetParent(CanvasTrans, false);
            BasePanel basePanel = uiPanelGo.GetComponent<BasePanel>();
            if(!basePanelDic.ContainsKey(uiPanleType))
            basePanelDic.Add(uiPanleType, basePanel);
            return basePanel;
        }
        else
            return getBasePanel;
    }
}
[Serializable]
public class UIPanelInfo
{
    public List<UIPanel> infoList;
}
[Serializable]
public class UIPanel : ISerializationCallbackReceiver
{
    public string panelTypeString;
    public string path;
    [NonSerialized]
    public UiPanelType uiPanelType;
    public void OnAfterDeserialize()
    {
        uiPanelType = (UiPanelType)Enum.Parse(typeof(UiPanelType), panelTypeString);
    }

    public void OnBeforeSerialize()
    {
    }
}

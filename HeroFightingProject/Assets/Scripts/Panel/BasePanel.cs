using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {
    private UiPanelType _uiPanelType;
    public virtual UiPanelType uiPanelType
    {
        set
        {
            _uiPanelType = value;
        }
        get
        {
            return _uiPanelType;
        }
    }
	public virtual void OnEnter()
    {

    }
    public virtual void OnExit()
    {

    }
    public virtual void OnPause()
    {

    }
    public virtual void OnResume()
    {

    }
    public virtual void ShowMessage(string msg)
    {

    }
}

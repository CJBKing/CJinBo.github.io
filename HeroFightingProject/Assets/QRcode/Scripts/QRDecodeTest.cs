/// <summary>
/// write by 52cwalk,if you have some question ,please contract lycwalk@gmail.com
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class QRDecodeTest : MonoBehaviour {

	public QRCodeDecodeController e_qrController;

	public Text UiText;
	
	public GameObject scanLineObj;
    private  Button backBtn;
    // Use this for initialization
    void Start () {
        backBtn = transform.Find("btnBack").GetComponent<Button>();
        Action a1 = new Action(OnButtonBackClicked);
        backBtn.onClick.AddListener(OnButtonBackClicked);
		if (e_qrController != null) {
			e_qrController.e_QRScanFinished += qrScanFinished;
		}
	}

	void qrScanFinished(string dataText)
	{
		UiText.text = dataText;
        PlayerPrefs.SetString("HeroName",dataText);
		if(scanLineObj != null)
		{
			scanLineObj.SetActive(false);
		}
	}
    public void OnButtonBackClicked()
    {
        Application.LoadLevel(0);
    }

}

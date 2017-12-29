using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class ScannerPanel : MonoBehaviour
{
    public static ScannerPanel _instance;
    public QRCodeDecodeController qr_CodeDecode;
    public DeviceCameraController e_DeviceController = null;
    private GameObject heroListBgPanel;
    private GameObject bgGo;
    private Button btnBack;
    private Animation handleAnimation;
    private string heroName;
    private int index;
    GameObject markGo;
    void Awake()
    {
        _instance = this;
        if (e_DeviceController == null)
        {
            e_DeviceController = transform.root.Find("DeviceCamera").GetComponent<DeviceCameraController>();
        }
        if (qr_CodeDecode != null)
        {
            qr_CodeDecode.e_QRScanFinished += OnScannerFinished;
        }


        heroListBgPanel = transform.parent.Find("Bg/HeroListBg").gameObject;
        bgGo = transform.parent.Find("Bg").gameObject;

        btnBack = transform.Find("btnBack").GetComponent<Button>();
        handleAnimation = transform.Find("ScannerFrame/Handle").GetComponent<Animation>();
        Action a1 = new Action(OnButtonBackClicked);
        btnBack.onClick.AddListener(OnButtonBackClicked);
    }
    public void OnButtonBackClicked()
    {
        gameObject.SetActive(false);
        //describePanel.SetActive(true);
        iTween.ScaleTo(bgGo, new Vector3(1, 1, 1), 0.5f);
        e_DeviceController.StopWork();
        if (handleAnimation.isPlaying)
        {
            handleAnimation.Stop();
        }
    }
    public void OnScannerFinished(string result)
    {
        if (result != null)
        {
            if (result == heroName)
            {
                qr_CodeDecode.Reset();
                OnButtonBackClicked();
                GameObject markGo=HeroManager._instance.heroCardList[index].transform.Find("Mark").gameObject;
                markGo.transform.Find("Text").GetComponent<Text>().text = "已激活";
                //markGo.SetActive(false);
                Destroy(markGo);
                    //识别到英雄的名字后返回;
            }
        }


        //得到二維碼掃描的内容result；
    }
    public void GetMotivateTarget(int heroIndex)
    {
        index = heroIndex;
        heroName = HeroManager._instance.heroList[heroIndex].transform.Find("HeroName").GetComponent<Text>().text;
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class DescribeBgMenu : MonoBehaviour {
    private  static DescribeBgMenu _Instance;
    public static DescribeBgMenu _instance
    {
        get {
            if (_Instance == null)
                _Instance =new DescribeBgMenu();
            return _Instance;
        }
    }
    public DeviceCameraController e_DeviceController = null;


    private  Animation handleAnima;
    private GameObject ScannerPanel;
    private GameObject heroListGo;

    private Button btnBack;
    private Button btnMotivate;

    private Button btnNext;
    private Button btnLast;
    public int index;
    void Awake()
    {

        if (e_DeviceController == null)
        {
            e_DeviceController = transform.root.Find("ScannerPanel/DeviceCamera/DeviceCamera").GetComponent<DeviceCameraController>();
        }
        ScannerPanel = transform.root.Find("ScannerPanel").gameObject;
        heroListGo = transform.parent.Find("Bg/HeroListBg").gameObject;
        btnBack = transform.Find("BtnBack").GetComponent<Button>();
        btnMotivate = transform.Find("BtnMotivate").GetComponent<Button>();
        btnNext = transform.Find("BtnNext").GetComponent<Button>();
        btnLast = transform.Find("BtnLast").GetComponent<Button>();


        Action a1 = new Action(OnBUttonBackClicked);
        btnBack.onClick.AddListener(OnBUttonBackClicked);

        Action a2 = new Action(OnButtonMotivateClicked);
        btnMotivate.onClick.AddListener(OnButtonMotivateClicked);

        Action a3 = new Action(OnBUttonNextClicked);
        btnNext.onClick.AddListener(OnBUttonNextClicked);

        Action a4 = new Action(OnBUttonLastClicked);
        btnLast.onClick.AddListener(OnBUttonLastClicked);
    }
    void OnBUttonBackClicked()
    {
        gameObject.SetActive(false);
        iTween.ScaleTo(heroListGo.transform.parent.gameObject,new Vector3(1,1,1),0);
    }
    void OnButtonMotivateClicked()
    {
        gameObject.SetActive(false);
        ScannerPanel.SetActive(true);
        ScannerPanel.GetComponent<ScannerPanel>().GetMotivateTarget(index);
        e_DeviceController.StartWork();
    }
    void OnBUttonNextClicked()
    {
        index++;
        if (index >= HeroManager._instance.heroList.Count)
        {
            index = 0;
        }
        HeroManager._instance.SetHeroShow(index);
    }
    void OnBUttonLastClicked()
    {
        index--;
        if (index < 0)
        {
            index = HeroManager._instance.heroList.Count - 1;
        }
        HeroManager._instance.SetHeroShow(index);
    }
    
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EndGamePanel : MonoBehaviour {
    private Text destroyCount;
    private Text protectTime;
    private Button btnBack;
    private Button btnShare;
    void Awake()
    {
        destroyCount = transform.Find("MsgPanel/DestroyEny").GetComponent<Text>();
        protectTime = transform.Find("MsgPanel/ProtectTime").GetComponent<Text>();
        btnBack = transform.Find("MsgPanel/BtnBack").GetComponent<Button>();
        btnShare = transform.Find("MsgPanel/BtnShare").GetComponent<Button>();

        Action a1 = new Action(OnbtnBackClicked);
        btnBack.onClick.AddListener(OnbtnBackClicked);

        Action a2 = new Action(OnbtnShareClicked);
        btnShare.onClick.AddListener(OnbtnShareClicked);
    }
    public  void SetResult(string enemyCount,string time)
    {
        if (enemyCount == null)
            enemyCount = "0";
        destroyCount.text = enemyCount;
        protectTime.text = time;
    }
    void OnbtnBackClicked()
    {
        Application.LoadLevel(0);
    }
    void OnbtnShareClicked()
    {

    }
	
}

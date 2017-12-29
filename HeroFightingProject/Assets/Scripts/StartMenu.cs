using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
public class StartMenu : MonoBehaviour {
    private Button btnStart;
    private GameObject progress;
    private GameObject BgGameobject;
    private GameObject logoImg;


    private Text progressTex;
    private Slider progressSlider;
    bool isShow = false;
    float timer=2;

    void Awake()
    {
        btnStart = transform.Find("Bg/Start/BtnStart").GetComponent<Button>();
        progress = transform.Find("Bg/Start/Progress").gameObject;
        progressTex = transform.Find("Bg/Start/Progress/ProgressValue").GetComponent<Text>();
        progressSlider = transform.Find("Bg/Start/Progress/ProgressBar").GetComponent<Slider>();
        BgGameobject = transform.Find("Bg/Start").gameObject;


        logoImg = transform.Find("Bg/Start/Logo/logoImg").gameObject;
       
        iTween.ScaleTo(logoImg, new Vector3(3, 3, 1), 1f);
       

        Action a =new Action(OnbuttonStartOnclicked);
        btnStart.onClick.AddListener(OnbuttonStartOnclicked);
    }
    void Update()
    {
        if (isShow)
        {
            timer -= Time.deltaTime;
            progressSlider.value = (float)(2-timer) / 2;
            progressTex.text = progressSlider.value * 100 + "%";
            if (progressSlider.value >= 1)
            {
                StartCoroutine(ChangeBg());
                isShow = false;
            }
        }
    }
    public void OnbuttonStartOnclicked()
    {
        progress.SetActive(true);
        isShow = true;

    }
    IEnumerator ChangeBg()
    {
        yield return new WaitForSeconds(0.5f);
        BgGameobject.SetActive(false);
    }
	
}

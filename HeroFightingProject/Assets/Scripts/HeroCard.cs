using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class HeroCard : MonoBehaviour {
    private Button btnCard;
    private Text TextCurrentheroName;
    private Text TextMark;
    private int index;
    DescribeBgMenu DescribeBgScr;
    void Awake()
    {
        TextCurrentheroName = transform.Find("HeroName").GetComponent<Text>();
        TextMark = transform.Find("Mark/Text").GetComponent<Text>();
        btnCard=this.GetComponent<Button>();
        Action a1=new Action(OnbuttonCardClicked);
        btnCard.onClick.AddListener(OnbuttonCardClicked);

       
    }

    void OnbuttonCardClicked()
    {
        if (transform.childCount == 2)
        {
            if (transform.Find("Mark/Text").GetComponent<Text>().text == "未激活")
            {
                int.TryParse(TextCurrentheroName.text, out index);
                HeroManager._instance.SetHeroShow(index);
            }
        }
        else
        {
        }
    }

}

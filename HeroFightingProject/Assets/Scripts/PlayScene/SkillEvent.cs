using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class SkillEvent : MonoBehaviour, IPointerClickHandler
{
   
    public delegate void SkillClicked(string skillName);
    public event SkillClicked OnSkillTouch;
    private Image fill;
    private Text number;
    private int childCount;
    public  float coldTime=15;
    private float costMP;
    float timer = 0;
    private bool isClicked = false;
    private bool isCompete = true;

    private float fillMount;
    void Awake()
    {
        childCount = transform.childCount;
        if (childCount == 2)
        {
            fill = transform.Find("Fill").GetComponent<Image>();
            number = transform.Find("Text").GetComponent<Text>();
            number.text = "";
        }
    }
    void Update()
    {
        SkillButton();
    }
    void SkillButton()
    {
        if (isClicked)
        {
            timer += Time.deltaTime;
            fillMount = (coldTime - timer) / coldTime;
            if ((int)(coldTime - timer) > 0)
            {
                number.text = ((int)(coldTime - timer)).ToString();
            }
            if (timer > coldTime)
            {
                isClicked = false;
                number.text = "";
                timer = 0;
                isCompete = true;
            }
            fill.fillAmount = fillMount;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameController._instance.gameState == GameState.Start)
        {
            if (childCount > 0)
            {
                isClicked = true;
                if (this.OnSkillTouch != null && isCompete)
                {
                    isCompete = false;
                    this.OnSkillTouch(this.gameObject.name);
                }
            }
            else
            {
                if (this.OnSkillTouch != null)
                    this.OnSkillTouch(this.gameObject.name);
            }
        }
    }

}

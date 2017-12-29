using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
public class Skill : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler {
    private GameObject skillDescGo;
    private Text textSkillName;
    void Awake()
    {
        skillDescGo = transform.parent.parent.Find("SkillDes").gameObject;
        textSkillName = transform.Find("Text").GetComponent<Text>();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        skillDescGo.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        skillDescGo.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        skillDescGo.SetActive(true);
        SkillDescribe._instance.ShowSkillInfo(textSkillName.text);
    }
}

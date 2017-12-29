using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class HeroManager : MonoBehaviour {
    public static HeroManager _instance;
    public List<GameObject> heroList = new List<GameObject>();
    public List<GameObject> selectedHero = new List<GameObject>();
    public List<GameObject> heroCardList = new List<GameObject>();
    public Dictionary<string, string> heroDic = new Dictionary<string, string>();
    private SpinWithMouse spinWithMouse;
    string heroName;
    string isMotivate;
    void Awake()
    {
        _instance = this;
        spinWithMouse = this.GetComponent<SpinWithMouse>();
    }
    public void SetHeroShow(int index)
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            if (i == index)
            {
                heroList[i].SetActive(true);
                heroName = heroList[i].transform.Find("HeroName").GetComponent<Text>().text;
                isMotivate = heroList[i].transform.Find("isMark").GetComponent<Text>().text;
                ProperityPanel._instance.HeroInfoChanged(heroName);
                spinWithMouse.GetTargetTrans(heroList[i].GetComponent<Transform>());
                AnimatorPlay(heroList[i]);
            }
            else
            {
                heroList[i].SetActive(false);
            }
        }
    }
    void AnimatorPlay(GameObject player)
    {
        player.GetComponent<Animator>().SetTrigger("isShowPost");
    }
    public void ShowSelectedHero(int index)
    {
        for (int i = 0; i < heroList.Count; i++)
        {
            if (i == index)
            {
                selectedHero[i].SetActive(true);
            }
            else
            {
                selectedHero[i].SetActive(false);
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SetHeroShow : MonoBehaviour
{
    public List<GameObject> PlayerList = new List<GameObject>();
    private int motivatedIndex;
    void Awake()
    {
         motivatedIndex = PlayerPrefs.GetInt("Player");
         PlayerList[motivatedIndex].SetActive(true);
    }
}

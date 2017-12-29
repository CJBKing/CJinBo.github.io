using UnityEngine;
using System.Collections;

public class Turrent : MonoBehaviour {
    public float totalLife = 30;
    public float currentLife = 30;
    public delegate void TurrentHealthChanged(float life,float totalLife);
    public event TurrentHealthChanged turrentHealthChangedEvent;
    void Start()
    {
        int heroIndex = PlayerPrefs.GetInt("Player");
        transform.localPosition = transform.localPosition - new Vector3(heroIndex*20,0,0);
    }
    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        turrentHealthChangedEvent(currentLife,totalLife);
        if (currentLife < 0)
        {
            GameController._instance.gameState = GameState.End;
            Destroy(gameObject); 
        }
    }
	
}

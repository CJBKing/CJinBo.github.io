using UnityEngine;
using System.Collections;

public class EnemySource : MonoBehaviour {
    public static EnemySource _instance;
    public Transform sourcePos;
    public GameObject []Enemy;
    public float coldTime = 5;
    private float timer = 0;
    public bool isMake = false;
    void Awake()
    {
        _instance = this;
    }
    void Update()
    {
        if (isMake&&GameController._instance.gameState==GameState.Start)
        {
            timer += Time.deltaTime;
            MakeEnemy();
        }
       
       
    }
    public void MakeEnemy()
    {
        if (timer >= 5)
        {
            timer = Random.Range(-10, 5.1f);
            int i = Random.Range(0, Enemy.Length);
            Instantiate(Enemy[i], sourcePos.position, Quaternion.identity);
        }
    }

}

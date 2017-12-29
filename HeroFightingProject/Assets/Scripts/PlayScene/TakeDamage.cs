using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerMoveControl>().TakeDamage(transform.root.GetComponent<EnemyControl>().Damage);
        }
        if (collider.tag == "Turrent")
        {
            collider.gameObject.GetComponent<Turrent>().TakeDamage(transform.root.GetComponent<EnemyControl>().Damage);
        }
    }
}

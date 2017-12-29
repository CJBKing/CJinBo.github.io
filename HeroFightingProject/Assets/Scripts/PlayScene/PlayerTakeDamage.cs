using UnityEngine;
using System.Collections;

public class PlayerTakeDamage : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<EnemyControl>().TakeDamage(transform.parent.GetComponent<PlayerMoveControl>().damage);
        }
    }
}

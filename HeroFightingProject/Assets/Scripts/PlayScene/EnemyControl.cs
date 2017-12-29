using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour
{
    public static EnemyControl _instance;
    public GameObject popupDamageGo;
    public Texture2D bg;
    public Texture2D blood;
    public AudioSource audioSur;
    public float Life = 100;
    float totalLife;
    public float Damage = 10;
    public float speed = 0.5f;
    private NavMeshAgent agent;
    private Transform TurrentPos;
    private Animator animator;
    private Transform m_transform;
    private Transform playerTransform;
    float EPdistance;
    float ETdistance;
    void Awake()
    {
        _instance = this;
        m_transform = transform;
        totalLife = Life;
        try
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        catch { }
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        try
        {
            TurrentPos = GameObject.FindGameObjectWithTag("Turrent").GetComponent<Transform>();
        }
        catch { }
        agent.speed = speed;
    }
    void Update()
    {
        if (GameController._instance.gameState == GameState.Start)
        {
            ControlEnv();
        }
        if (GameController._instance.gameState == GameState.End)
        {
            Destroy(gameObject);
        }
    }
    void ControlEnv()
    {
        if (TurrentPos)
        {
            EPdistance = Vector3.Distance(m_transform.position, playerTransform.position);
            ETdistance = Vector3.Distance(m_transform.position, TurrentPos.position);
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.nameHash == Animator.StringToHash("Base Layer.idlebreak"))
            {
                animator.SetBool("isIdle", false);
                animator.SetBool("isWalk", true);
            }
            if (ETdistance > EPdistance)
            {
                agent.ResetPath();
                agent.SetDestination(playerTransform.position+new Vector3(Random.Range(0.5f,1f),0, Random.Range(0.5f, 1f)));
                m_transform.LookAt(playerTransform.position);
                if (EPdistance < 8.5f && EPdistance > 6.2f)
                {
                    agent.Stop();
                    animator.SetBool("isWalk", false);
                    animator.SetBool("isAttack", true);
                }
                else
                {
                    animator.SetBool("isWalk", true);
                    animator.SetBool("isAttack", false);
                }
            }
            if (ETdistance < EPdistance)
            {
                agent.ResetPath();
                agent.SetDestination(TurrentPos.position + new Vector3(Random.Range(0.5f, 1f), 0, Random.Range(0.5f, 1f)));
                m_transform.LookAt(TurrentPos.position);
                if (ETdistance < 7.8f && ETdistance > 6f)
                {
                    agent.Stop();
                    animator.SetBool("isWalk", false);
                    animator.SetBool("isAttack", true);
                }
                else
                {
                    animator.SetBool("isWalk", true);
                    animator.SetBool("isAttack", false);
                }
            }
        }
    }
    
    public void TakeDamage(float damage)
    {
        audioSur.Play();
        GameObject damageGo = Instantiate(popupDamageGo,transform.position+new Vector3(0,10,0),Quaternion.identity) as GameObject;
        damageGo.GetComponent<PopupDamage>().Value = (int)damage;
        Life -= damage;
        if (Life < 0)
        {

            HeadBar._instance.count++;
            HeadBar._instance.DesCountChanged();
            animator.SetBool("isDie",true);
            Destroy(this.GetComponent<CapsuleCollider>());
            Destroy(this.gameObject,4.3f);
        }
    }
    void OnGUI()
    {
        Vector3 headPos = Camera.main.WorldToScreenPoint(m_transform.position + Vector3.up * 2.5f);
        GUI.DrawTexture(new Rect(headPos.x-20,Screen.height-headPos.y,70,4),bg);
        if (Life < 0) Life = 0;
        GUI.DrawTexture(new Rect(headPos.x-20,Screen.height-headPos.y,70*Life/totalLife,4),blood);
    }
}

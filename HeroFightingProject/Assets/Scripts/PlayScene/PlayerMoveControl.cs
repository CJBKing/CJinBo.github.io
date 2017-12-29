using UnityEngine;
using System.Collections;
using Vuforia;
public class PlayerMoveControl : MonoBehaviour
{
    public static PlayerMoveControl _instance;
    public delegate void HelthChanged(float life, float totalLife);
    public event HelthChanged EventHelthChanged;

    public delegate void MPChanged(float MP, float totalMP);
    public event MPChanged EventMPChanged;

    PlayerAboutPanel playerInfo;

    private float totalLife;
    private float currentLife;
    private float totalMP;
    private float MP;
    public float damage;
    private float moveSpeed;
    private float HPRecover;
    private float MPRecover;
    private float timer = 0;
    private Transform m_transform;
    private JoyStick jstk;
    private Rigidbody rigid;
    private Animator animator;
    private SkillEvent[] skillCtrl;
    private bool isSkill = false;
    void Start()
    {
        _instance = this;
        m_transform = transform;
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        jstk = GameObject.FindObjectOfType<JoyStick>();
        jstk.OnJoyStickTouchMove += OnJoyStickMove;
        jstk.OnJoyStickTouchEnd += OnJoyStickEnd;
        skillCtrl = GameObject.FindObjectsOfType<SkillEvent>();
        for (int i = 0; i < skillCtrl.Length; i++)
            skillCtrl[i].OnSkillTouch += PlayAnimator;
        playerInfo = PlayerAboutPanel._instance;
        InitProperity();
    }

    void Update()
    {
        if (GameController._instance.gameState == GameState.Start)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                if (currentLife < totalLife)
                    currentLife += HPRecover;
                if (MP < totalMP)
                    MP += MPRecover;
                EventHelthChanged(currentLife, totalLife);
                EventMPChanged(MP, totalMP);
            }
            if (!isSkill)
            {
                float.TryParse(playerInfo.NormalDamage.text, out damage);
            }
        }
    }
    void InitProperity()
    {

        float.TryParse(HeadBar._instance.HPText.text, out totalLife);
        currentLife = totalLife;
        float.TryParse(HeadBar._instance.MPText.text, out MP);
        totalMP = MP;
        float.TryParse(playerInfo.NormalDamage.text, out damage);
        float.TryParse(playerInfo.MoveSpeed.text, out moveSpeed);
        moveSpeed *= 0.1f;
        float.TryParse(playerInfo.HPRecoverText.text, out HPRecover);
        float.TryParse(playerInfo.MPRecoverText.text, out MPRecover);
    }
    void OnJoyStickMove(Vector2 vec)
    {
        transform.LookAt(transform.position + new Vector3(vec.x, 0, vec.y));
        if (isSkill == false)
        {
            rigid.MovePosition(transform.position + new Vector3(vec.x * moveSpeed, 0, vec.y * moveSpeed));
            animator.SetBool("isStand", false);
            animator.SetBool("isRun", true);
        }
    }
    void OnJoyStickEnd()
    {
        isSkill = false;
        animator.SetBool("isStand", true);
        animator.SetBool("isRun", false);
    }
    void PlayAnimator(string skillName)
    {
        isSkill = true;
        string skill = skillName[5].ToString();
        int index;
        int.TryParse(skill, out index);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Run"))
        //{
        //    animator.SetTrigger(skillName);
        //    SwitchSkillDamage(index);

        //}
        //if (stateInfo.nameHash == Animator.StringToHash("Base Layer.Stand"))
        //{
        animator.SetTrigger(skillName);
        SwitchSkillDamage(index);
        //}
    }
    void SwitchSkillDamage(int skillINdex)
    {
        float.TryParse(playerInfo.player.skillList[skillINdex].Damage, out damage);
        float mpCost;
        float.TryParse(playerInfo.player.skillList[skillINdex].Cost, out mpCost);
        if (MP >= mpCost)
        {
            skillCtrl[skillINdex].enabled = true;
            MP -= mpCost;
            EventMPChanged(MP, totalMP);
        }
        else
        {
            skillCtrl[skillINdex].enabled = false;
        }
    }
    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        if (currentLife < 0)
        {
            Turrent turrent = GameObject.FindObjectOfType<Turrent>();
            currentLife = turrent.currentLife * 0.5f;
            turrent.TakeDamage(damage * 0.9f);
        }
        EventHelthChanged(currentLife, totalLife);
    }

}

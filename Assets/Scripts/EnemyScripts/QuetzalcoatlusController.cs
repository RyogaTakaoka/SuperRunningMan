using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuetzalcoatlusController : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] AudioClip attackSE;
    [SerializeField] private float interval;
    [SerializeField] private GameObject sonicBoomR;
    [SerializeField] private GameObject sonicBoomL;
    private GameObject player;
    private GameObject goal;
    private Rigidbody2D rb;
    private Animator anim;
    private float time;
    private float totalTime;
    private bool isDead = false;
    private bool firstAttack = true;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        goal = GameObject.FindWithTag("GoalFlag");

    }

    // Update is called once per frame
    void Update()
    {
        //強制横スクロール
        rb.velocity = new Vector3(speed, 0, 0);
        
        totalTime += Time.smoothDeltaTime;
        time += Time.smoothDeltaTime;
        if (firstAttack && time >= 3)
        {
            StartCoroutine(Attack());
            time = 0;
            firstAttack = false;
        }
        
        //一定間隔で攻撃
        if(goal.transform.position.x - this.transform.position.x >= 30)
        {
            if (time >= interval && isDead == false)
            {
                //50s経過から二回攻撃
                if(totalTime >= 45)
                {
                    StartCoroutine(DoubleAttack());
                }
                else
                {
                    StartCoroutine(Attack());
                }
                time = 0;
            }
        }
    }

    IEnumerator Attack()
    {
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.8f);

        //発射するソニックブーム
        sonicBoomR.SetActive(true);
        sonicBoomR.transform.position = new Vector3(this.transform.position.x + 2, this.transform.position.y + 2,0) ;
        Rigidbody2D sbRRb = sonicBoomR.GetComponent<Rigidbody2D>();
        sbRRb.velocity = new Vector2(50,0);
        audioSource.PlayOneShot(attackSE);
        yield return new WaitForSeconds(0.5f);
        
        //返ってくるソニックブーム
        sonicBoomL.SetActive(true);
        sonicBoomL.transform.position = new Vector3(this.transform.position.x + 37, player.transform.position.y - 0.1f, 0);
        
        yield return new WaitForSeconds(0.3f);  
        sonicBoomR.SetActive(false);
        
        Rigidbody2D sbLRb = sonicBoomL.GetComponent<Rigidbody2D>();
        sbLRb.velocity = new Vector2(-40, 0);
        audioSource.PlayOneShot(attackSE);
        yield return new WaitForSeconds(1.0f);
        sonicBoomL.SetActive(false);

    }

    IEnumerator DoubleAttack()
    {
        StartCoroutine(Attack());
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(Attack());

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //ゴールにぶつかり死
        if (col.gameObject.tag == "GoalFlag" && player.GetComponent<PlayerController>().isClear)
        {
            isDead = true;
            anim.SetBool("isDead", true);
            speed = 0;
            rb.velocity = new Vector2(0, 0);
            //落ちる演出
            rb.gravityScale = 80;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Block")
        {
            Destroy(col.gameObject);
        }
    }
}

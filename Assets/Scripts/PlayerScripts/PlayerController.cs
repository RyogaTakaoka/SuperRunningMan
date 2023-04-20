using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] AudioClip jumpSE;
    [SerializeField] AudioClip attackSE;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject attackCol;
    [SerializeField] GameManager gm;
    [System.NonSerialized] public Rigidbody2D rb;
    [System.NonSerialized] public float jumpForce = 1200;
    [System.NonSerialized] public float status = 0;
    [System.NonSerialized] public bool isClear = false;
    [System.NonSerialized] public bool isDead = false;
    Animator anim;
    AudioSource audioSource;
    CapsuleCollider2D capCol;
    private float hJumpForce = 1000;
    private float sJumpForce = 1200;
    private float lJumpForce = 1600; 
    private bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.anim = GetComponent<Animator>();
        this.audioSource = GetComponent<AudioSource>();
        this.capCol = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //ステータス変更
        anim.SetFloat("Status", status);

        //クリア・ゲームオーバーした後はアクション不可
        if (isDead == false)
        {
            AutoRun();
            if(isClear == false)
            {
                Action();
            }
        }
    }

    private void FixedUpdate()
    {
        //接地判定
        isGround = false;
        Vector2 groundPos = new Vector2(transform.position.x, transform.position.y - 1.35f);
        Vector2 groundArea = new Vector2(0.5f, 0.5f);
        isGround = Physics2D.OverlapArea(groundPos + groundArea, groundPos - groundArea, groundLayer);
    }

    private void AutoRun()
    {
        //オートラン
        rb.velocity = new Vector2(speed, rb.velocity.y);
        anim.SetFloat("Speed", rb.velocity.x);
    }

    private void Action()
    {
        //Statusに応じてJump力変化
        if(status == 1)
        {
            jumpForce = hJumpForce;

        }else if (status == -1)
        {
            jumpForce = lJumpForce;
        }else
        {
            jumpForce = sJumpForce;
        }

        //ジャンプ
        if (Input.GetButtonDown("Jump") && isGround)
        {
            audioSource.PlayOneShot(jumpSE);
            rb.AddForce(Vector2.up * jumpForce);
            anim.SetBool("isJump", true);
        }

        //上方向の速度でジャンプアニメーション
        if (rb.velocity.y > 0.1f)
        {
            anim.SetBool("isJump", true);
            anim.SetBool("isFall", false);
        }
        //下方向の速度で落下アニメーション
        if (rb.velocity.y < -0.1f)
        {
            anim.SetBool("isFall", true);
            anim.SetBool("isJump", false);
        }

        //地面に着いたらアニメーションを止める
        if (isGround)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
        }

        StartCoroutine(Attack());
    }
    
    IEnumerator Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            audioSource.PlayOneShot(attackSE);
            anim.SetBool("isAttack", true);
            yield return new WaitForSeconds(0.1f);
            attackCol.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            attackCol.SetActive(false);
        }
    }

    public void Clear()
    {
        isClear = true;
        gm.GameClear();
    }

    public void Dead()
    {
        if (isDead == false)
        {
            isDead = true;
            rb.velocity = new Vector2(0, 0);
            //プレイヤーのコライダーを削除
            capCol.enabled = false;
            attackCol.SetActive(false);
            // カメラを揺らす
            var cameraShake = FindObjectOfType<CameraShaker>();
            cameraShake.Shake();
            
            gm.GameOver();

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //落下判定
        if (col.gameObject.tag == "OutArea")
        {
            Dead();
        }

        //ゴール
        if (col.gameObject.tag == "GoalFlag")
        {
            Clear();
        }

        //BOSSに当たると死
        if (col.gameObject.tag == "BOSS")
        {
            Dead();
        }

        //ソニックブーム（BOSSの攻撃）に当たると死
        if (col.gameObject.tag == "SonicBoom")
        {
            Dead();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //敵と正面衝突で死
        if (col.gameObject.tag == "Enemy")
        {
            Dead();
        }
    }
}
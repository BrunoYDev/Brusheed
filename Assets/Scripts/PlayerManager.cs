using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private bool face = true;
    private Transform playerT;
    private float vel = 6f;
    private float force = 7.5f;
    private Rigidbody2D playerRB;
    private Animator anim;

    [SerializeField]private GameObject palletPanel;

    public float playerLife = 1f;
    public Image playerLifeField;

    private bool doJump = false;
    private int jumpTimes = 0;
    [SerializeField]private Transform check;
    [SerializeField]private LayerMask whatIsFloor;
    private float radius = 0.1f;

    public int color = 0;

    public bool nearFinishDoor = false;

    public bool canChangeColor = false;

    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip pickPalletSound;
    public AudioClip pickBucketSound;
    public AudioClip killEnemySound;

    private int redQtd = 0;
    private int greenQtd = 0;
    private int blueQtd = 0;


    [SerializeField] private Image redPallet;
    [SerializeField] private Image greenPallet;
    [SerializeField] private Image bluePallet;

    [SerializeField] private Text redtext;
    [SerializeField] private Text greentext;
    [SerializeField] private Text bluetext;

    void Start()
    {
        playerT = GetComponent<Transform>();
        playerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        redtext.text = redQtd.ToString();
        greentext.text = greenQtd.ToString();
        bluetext.text = blueQtd.ToString();

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) && !face && !Input.GetKey(KeyCode.A))
        {
            Flip();
        }

        else if (Input.GetKey(KeyCode.A) && face && !Input.GetKey(KeyCode.D))
        {
            Flip();
        }
        Walk();
        
        
    }

    void Update()
    {
        doJump = Physics2D.OverlapCircle(check.position, radius, whatIsFloor);
        Jump();
        ChangeColor();
        playerLifeField.fillAmount = playerLife;

        redtext.text = redQtd.ToString();
        greentext.text = greenQtd.ToString();
        bluetext.text = blueQtd.ToString();
        Death();

        if (nearFinishDoor && Input.GetKey(KeyCode.F))
        {
            FindObjectOfType<SceneController>().LoadScene("Finish Game");
        }
    }

    void Flip()
    {
        face = !face;

        Vector3 scale = playerT.localScale;

        scale.x *= -1;

        playerT.localScale = scale;
    }

    void Walk()
    {
        if(color == 0)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", true);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(-vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", true);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Idle", true);
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
            }
        }
        else if(color == 1)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", true);
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(-vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", true);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetBool("idleRed", true);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
            }
        }
        else if (color == 2)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
                anim.SetBool("walkingGreen", true);
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(-vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
                anim.SetBool("walkingGreen", true);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetBool("idleGreen", true);
                anim.SetBool("idleRed", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingBlue", false);
                anim.SetBool("walkingRed", false);
            }
        }
        else if (color == 3)
        {
            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector2(vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingRed", false);
                anim.SetBool("walkingBlue", true);
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector2(-vel * Time.deltaTime, 0));
                anim.SetBool("idleRed", false);
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleBlue", false);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingRed", false);
                anim.SetBool("walkingBlue", true);
            }
            else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                anim.SetBool("idleGreen", false);
                anim.SetBool("idleRed", false);
                anim.SetBool("idleBlue", true);
                anim.SetBool("Idle", false);
                anim.SetBool("Walking", false);
                anim.SetBool("walkingGreen", false);
                anim.SetBool("walkingRed", false);
                anim.SetBool("walkingBlue", false);
            }
        }

    }

    void ChangeColor()
    {
        if (canChangeColor)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && redQtd > 0 && color == 0)
            {
                color = 1;
                redPallet.color = new Color(59f / 255f, 171f / 255f, 36f / 255f);
                greenPallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
                bluePallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
                redtext.text = redQtd.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && greenQtd > 0 && color == 0)
            {
                color = 2;
                greenPallet.color = new Color(59f / 255f, 171f / 255f, 36f / 255f);
                redPallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
                bluePallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
                greentext.text = greenQtd.ToString();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && blueQtd > 0 && color == 0)
            {
                color = 3;
                bluePallet.color = new Color(59f / 255f, 171f / 255f, 36f / 255f);
                greenPallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
                redPallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
                bluetext.text = blueQtd.ToString();
            }
        }
    }


    void Jump()
    {
        if (doJump)
        {
            jumpTimes = 2;
        }
        if(jumpTimes > 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpTimes--;
                AudioManager.inst.PlayAudio(jumpSound);
                playerRB.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            }
        }
        
    }

    void Death()
    {
        if(playerLife <= 0)
        {
            FindObjectOfType<SceneController>().LoadScene("Game Over");
        }
    }

    void Bounce(Vector2 otherPosition)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 bounceDirection = (new Vector2(transform.position.x, transform.position.y) - otherPosition).normalized;
        rb.AddForce(bounceDirection * 4f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Finish"))
        {
                nearFinishDoor = true;
        }



        if (other.gameObject.CompareTag("Pallet"))
        {
            palletPanel.SetActive(true);
            AudioManager.inst.PlayAudio(pickPalletSound);
            canChangeColor = true;
            redQtd = 3;
            greenQtd = 0;
            blueQtd = 0;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("MonsterHead"))
        {

            EnemyBehavior enemy = other.transform.parent.GetComponent<EnemyBehavior>();

            if (enemy != null && enemy.enemyColor == 0)
            {
                Bounce(other.transform.position);
                StartCoroutine(Flicker(0.1f, 5));
                AudioManager.inst.PlayAudio(hitSound);
                playerLife -= 0.25f;
            }
            else if(enemy != null && enemy.enemyColor == color)
            {
                Bounce(other.transform.position);
                if(color == 1 && redQtd > 0)
                {
                    redQtd--;
                    AudioManager.inst.PlayAudio(killEnemySound);
                    Destroy(other.transform.parent.gameObject);
                }
                else if(color == 2 && greenQtd > 0)
                {
                    greenQtd--;
                    AudioManager.inst.PlayAudio(killEnemySound);
                    Destroy(other.transform.parent.gameObject);
                }
                else if(color == 3 && blueQtd > 0)
                {
                    blueQtd--;
                    AudioManager.inst.PlayAudio(killEnemySound);
                    Destroy(other.transform.parent.gameObject);
                }

            }
            
        }

        if (other.gameObject.CompareTag("RedBucket") && canChangeColor)
        {
            redQtd += 5;
            AudioManager.inst.PlayAudio(pickBucketSound);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("GreenBucket") && canChangeColor)
        {
            greenQtd += 5;
            AudioManager.inst.PlayAudio(pickBucketSound);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BlueBucket") && canChangeColor)
        {
            blueQtd += 5;
            AudioManager.inst.PlayAudio(pickBucketSound);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Bounce(other.transform.position);
            StartCoroutine(Flicker(0.1f, 5));
            AudioManager.inst.PlayAudio(hitSound);
            playerLife -= 0.25f;
        }

        if (other.gameObject.CompareTag("Tinner"))
        {
            color = 0;
            redPallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
            greenPallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
            bluePallet.color = new Color(171f / 255f, 123f / 255f, 36f / 255f);
        }

        if(other.gameObject.CompareTag("ColoredBlock"))
        {
            if(other.gameObject.GetComponent<BlockBehavior>().blockColor == color)
            {
                if(color == 1 && redQtd > 0) { redQtd--; }else if(color == 2 && greenQtd > 0) { greenQtd--; }else if(color == 3 && blueQtd > 0) { blueQtd--; }
                Destroy(other.gameObject);
            }
        }

    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        
    }

    private IEnumerator Flicker(float duration, int times)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        for (int i = 0; i < times; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(duration);
        }

        spriteRenderer.enabled = true;
    }


    //void //OnDrawGizmosSelected()
    //{
    // Define a cor do gizmo
    //Gizmos.color = Color.red;

    // Desenha o círculo de colisão no editor
    //Gizmos.DrawWireSphere(check.position, radius);
    //}
}

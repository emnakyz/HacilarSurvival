using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Hareket")]
    public float movementSpeed;  //Hareket hızı.
    private Rigidbody2D rb;  
    private Animator anim; 
    private bool canMove = true;  //Hareket edebilir mi ?
    [Header("Saldırı")]
    [SerializeField] private Transform attackPos;  //Saldırı konumu (bu konumda bir daire oluşturulur değen objeler algılanır )
    public float attackCircleRadius;   //Bu dairenin çapı.
    private bool isAttacking = false;  //Saldırı halinde mi ?
    private bool canAttack = true;  //Saldırabilir mi ? 
    [Header("Yay Ok")]
    [SerializeField] private GameObject arrowPrefab;  //Ok prefabı
    private bool canShoot = true;  //Ok atabilir mi ?

    private bool canDrinkWater = true;  //Su içebilir mi ?

    [SerializeField] private AudioClip grassSound;  //Sesler
    [SerializeField] private AudioClip arrowSound;
    private bool canPlayGrassSound = true; //Çimen sesi oynatılabilir mi ?
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }
    private void Update()
    {
        Animations();
        Attack();
        AttackWithBow();
        Health();
        DrinkWater();
    }
    private void DrinkWater()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canDrinkWater) //Eğer su içebilirse ve tuş basılırsa.
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position,1.5f);  //Daireye giren objeleden
            if (col.Length > 0)
            {
                for (int i = 0; i < col.Length; i++)
                {
                    if (col[i].gameObject.tag == "Water")  //Su olan varsa su ihtiyacı azalır.
                    {
                        if (GameManager.playerData.playerThirst <= 95) GameManager.playerData.playerThirst += 5;
                        else GameManager.playerData.playerThirst = 100;
                    }
                }
            }
            canDrinkWater = false;
            Invoke("CanDrinkWaterAgain",0.5f);
        }
    }
    private void CanDrinkWaterAgain()
    {
        canDrinkWater = true;
    }
    private void Health()  //Can biterse ölür !
    {
        if (GameManager.playerData.playerHealth <= 0)
        {
            GameObject.FindObjectOfType<GameManager>().GameOverWithLose();
        }
    }
    private void AttackWithBow()
    {
        if (GameManager.playerData.currentUsedTool == PlayerData.Tool.Bow)  //Alet yay ise
        {
            GameObject bowGfx = transform.Find("ToolBow").gameObject;  //Yay spritesi
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (Vector2)bowGfx.transform.position - mousePos; 
            float angle;
            if (transform.rotation == Quaternion.Euler(0, 0, 0))      //Burda karakterin bakış yönünde göre yayın çevrileceği yön belirlenir.
            {
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180f;
                bowGfx.transform.eulerAngles = new Vector3(bowGfx.transform.eulerAngles.x, bowGfx.transform.eulerAngles.y, angle);
            }
            else
            {
                angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg + -90f;
                bowGfx.transform.eulerAngles = new Vector3(bowGfx.transform.eulerAngles.x, bowGfx.transform.eulerAngles.y, angle);
            }

            if (GameManager.inventorySystem.GetItemByName("Arrow") != null)  //Eğer ok var ise
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.inventorySystem.GetItemByName("Arrow").count > 0 && canShoot) //Yeterli sayıda ok varsa ve ok atabilrse ok at.
                {
                    GameManager.soundSytem.PlaySfx(arrowSound);
                    GameObject spawnedArrow = Instantiate(arrowPrefab, bowGfx.transform.position, Quaternion.identity);
                    spawnedArrow.GetComponent<Arrow>().movementDirection = -dir;
                    GameManager.inventorySystem.DeleteItem("Arrow",1);
                    canShoot = false;
                    Invoke("CanShootAgain",0.5f);
                }
            }
        }
    }
    private void CanShootAgain() { canShoot = true; }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)  //Saldırabilrse
        {
            if (GameManager.playerData.currentUsedTool != PlayerData.Tool.None)  //Alet varsa
            {
                isAttacking = true;
                StartCoroutine(AttackNumerator());  //Saldırı başlasın
            }
            canAttack = false;
            Invoke("CanAttackAgain", 0.9f);
        }
        if (!isAttacking)
        {
            anim.SetBool("Attack", false);
            canMove = true;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void CanAttackAgain () { canAttack = true; }
    private IEnumerator AttackNumerator()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.7f);
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPos.position,attackCircleRadius); //Atack konumunda bir daire oluştutur.
        if (cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].gameObject.GetComponent<IResources>() != null) //Eğer daireye giren obje bir kaynak ise toplar.
                {
                    if (GameManager.playerData.currentUsedTool != PlayerData.Tool.Bow)
                        cols[i].gameObject.GetComponent<IResources>().GetResoure(1, GameManager.playerData.currentUsedTool,attackPos.position);
                }
                isAttacking = false;
            }
        }
        else isAttacking = false;
    }
    private void PlayerMovement() //Hareket
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (canMove) rb.velocity = new Vector2(horizontal * movementSpeed * Time.fixedDeltaTime,vertical * movementSpeed * Time.fixedDeltaTime);
    }
    private void Animations() //Animasyonlar sağa dola dönme vs.
    {
        if (rb.velocity.magnitude > 0)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk",false);

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (canPlayGrassSound) StartCoroutine(GrassSoundLoop());
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
        {
            if (canPlayGrassSound) StartCoroutine(GrassSoundLoop());
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
    private IEnumerator GrassSoundLoop() //Çim sesi oynatma döngüsü
    {
        canPlayGrassSound = false;
        GameManager.soundSytem.PlaySfx(grassSound);
        yield return new WaitForSeconds(0.3f);
        canPlayGrassSound = true;
    }

    public void GetDamage(int amount) //Hasar alma
    {
        GameManager.playerData.playerHealth -= amount;
    }
}

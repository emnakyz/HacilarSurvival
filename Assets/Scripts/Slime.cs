using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    public int health = 50;  //Can
    public float moveAroundRange;   //Etrafda gezmesi için kendi konumunda bir daire oluşturulur bu daire içersinde rastgele bir konuma hareket eder. Buda onun çapı.
    public float moveSpeed;   //Hareket hızı.
    public float dedectRange;  //Oyunucuyu görme mesafesi


    private Rigidbody2D rb;

    private Vector2 currentMovementTarget;  //Gidilecek mevcut konum.
    private bool canMove = false;  //Hareket edebilir mi ?
    private bool isPlayerDedected = false;   //Oyuncu görüldü mü ?

    public MonsterSpawner spawner { get; set; }  //Mevcut bağlı olunan spawner.

    private GameObject player;   //Oyuncu

    private bool canGiveDamage = true;  //Zarar verebilir mi ?

    [SerializeField] private AudioClip hitSound;  //Vurulma sesi.

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<Player>().gameObject;
    }
    private void Start()
    {
        StartCoroutine(MoveAroundLoop());
    }
    IEnumerator MoveAroundLoop()   //Rastgele bir daire oluşturularak bu daire içersinden rastgele bir konum mevcut konum olur.
    {
        yield return new WaitForSeconds(3f);
        var randomPos = (Vector2)Random.insideUnitCircle * moveAroundRange;
        randomPos += (Vector2)transform.position;
        currentMovementTarget = randomPos;
        canMove = true;
        Invoke("ReMove", 7f);
    }
    private void ReMove()
    {
        StartCoroutine(MoveAroundLoop());
    }
    private void Update()
    {
        Movement();
        DedectAndAttack();
    }
    private void Movement()  //Hareket edebilirsen ve oyuncuyu görmediysen mevcut konuma git.
    {
        if (canMove && !isPlayerDedected)
        {
            rb.position = Vector2.MoveTowards(rb.position, currentMovementTarget, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(rb.position, currentMovementTarget) < 0.1f)
            {
                StartCoroutine(MoveAroundLoop());
                canMove = false;
            }
        }
        else if (isPlayerDedected)  //Oyunucuyu görürsen dal.
        {
            rb.position = Vector2.MoveTowards(rb.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void DedectAndAttack() //Tespit etme mesafesine göre oyuncuyu tespit eder.
    {
        if (Vector2.Distance(player.transform.position, transform.position) < dedectRange)
        {
            isPlayerDedected = true;
        }
        else
        {
            isPlayerDedected = false;
        }
    }

    public void Die() //Ölür
    {
        int randomNumber = Random.Range(1, 4);
        if (randomNumber == 2) 
            Instantiate(GameObject.FindObjectOfType<GameManager>().collectableHealthPref, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    public void GetDamage(int amount) //Zarar alır
    {
        GameManager.soundSytem.PlaySfx(hitSound);
        health -= amount;
        if (health <= 0) Die();
    }
    private void OnCollisionStay2D(Collision2D col)  //Karaktere değdiği süre zarfınca belli süre aralığıyla hasar verir.
    {
        if (col.gameObject.GetComponent<Player>() != null)
        {
            if (canGiveDamage)
            {
                col.gameObject.GetComponent<Player>().GetDamage(10);
                canGiveDamage = false;
                Invoke("CanGiveDamageAgain", 1.5f);

            }
        }
    }
    private void CanGiveDamageAgain() { canGiveDamage = true; }
}


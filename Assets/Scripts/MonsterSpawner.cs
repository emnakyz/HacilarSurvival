using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monster;  //Canavar Prefab
    public float spawnCircleRadius;   //Doğma dairesi (bu daire içersinde rastgele bir konumda spawn olması için)
    public float spawnTime;   //Canavarın doğma süresi aralığı.
    public float dedectPlayerDist;  //Oyuncu yaklaştıysa doğması için mesafe.
    public int maxSpawnEnemyAmount;  //Maks mevcut olabilecek düşman sayısı
    private int currentSpanedEnemyAmount;  //Mevcut olan düşman sayısı
    public int spawnerHealth = 200;  //Spaenr canı

    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
    }
    private void Start()
    {
        StartCoroutine(SpawnerLoop());
    }
    private IEnumerator SpawnerLoop() //Belli aralıklar ile spawnlamayı dener.
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnMonster();
        StartCoroutine(SpawnerLoop());
    }
    private void Update()
    {
        CountOfCurrentSpawnedEnemys();
    }
    private void CountOfCurrentSpawnedEnemys() //Burda bu spawnera bağlı düşman sayısını bulur.
    {
        currentSpanedEnemyAmount = 0;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++) 
        {
            if (GameObject.FindGameObjectsWithTag("Enemy")[i].GetComponent<IEnemy>().spawner == this)
            {
                currentSpanedEnemyAmount++;
            }
        }
    }
    public void SpawnMonster()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < dedectPlayerDist && currentSpanedEnemyAmount < maxSpawnEnemyAmount) //Mecut düşman sayısı maks değilse ve oyuncu spawnerdan düşman spawn olabilecek mesafedeyse.
        {
            var randomPos = (Vector2)Random.insideUnitCircle * spawnCircleRadius;
            randomPos += (Vector2)transform.position;
            GameObject enemy = Instantiate(monster, randomPos, transform.rotation);
            enemy.GetComponent<IEnemy>().spawner = this;
        }
    }
    public void GetDamage(int amount) //Zarar al
    {
        spawnerHealth -= amount;
        if (spawnerHealth <= 0) Die();
    }
    private void Die() //Yok ol
    {
        Destroy(gameObject);
    }
}

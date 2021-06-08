using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;
    public float speed;

    public Vector2 movementDirection;
    private void Awake()
    {
        Destroy(gameObject, 10);
    }
    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.GetComponent<IEnemy>() != null) //Değdiği obje bir düşman ise zarar ver.
        {
            col.gameObject.GetComponent<IEnemy>().GetDamage(damage);
            Vector2 dir = transform.position - col.gameObject.transform.position;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(-dir * 200);

            Destroying();
        } 
        if (col.gameObject.GetComponent<MonsterSpawner>() != null) //Bir spawner ise zarar verir.
        {
            col.gameObject.GetComponent<MonsterSpawner>().GetDamage(10);

            Destroying();
        } 
    }
    private void Destroying()
    {
        if (transform.Find("Flare") != null)
        {
            transform.Find("Flare").gameObject.GetComponent<ParticleSystem>().Stop();
            transform.Find("Flare").gameObject.transform.parent = null;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle);
    }
    private void FixedUpdate()
    {
        movementDirection = movementDirection.normalized;
        GetComponent<Rigidbody2D>().velocity = movementDirection * speed * Time.fixedDeltaTime;
    }
}

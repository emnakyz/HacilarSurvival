using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour, IResources
{
    public int resourceCount { get; set; }

    private Animator anim;
    [SerializeField] private GameObject damageParticle;

    [SerializeField] private AudioClip chopSound;

    private bool isDestroying = false;
    private void Awake()
    {
        resourceCount = Random.Range(50, 150);
        anim = GetComponent<Animator>();
    }

    public void DestoryObject()
    {
        isDestroying = true;
        anim.SetTrigger("Destroyed");
        Instantiate(damageParticle, new Vector2(transform.position.x - 0.3f, transform.position.y), Quaternion.identity);
        Instantiate(damageParticle, new Vector2(transform.position.x + 0.3f, transform.position.y), Quaternion.identity);
        Destroy(gameObject, 1.3f);
    }

    public void GetResoure(int amount, PlayerData.Tool tool, Vector2 attackPos)
    {
        if (tool == PlayerData.Tool.Axe)
        {
            if (resourceCount >= amount * 3)
            {
                Item wood = GetWoodItem(amount * 3);
                GameManager.inventorySystem.AddItem(wood);
                resourceCount -= amount * 3;
            }
            else
            {
                Item wood = GetWoodItem(resourceCount);
                GameManager.inventorySystem.AddItem(wood);
                resourceCount = 0;
            }
        }
        else
        {
            if (resourceCount >= amount)
            {
                Item wood = GetWoodItem(amount);
                GameManager.inventorySystem.AddItem(wood);
                resourceCount -= amount;
            }
            else
            {
                Item wood = GetWoodItem(resourceCount);
                GameManager.inventorySystem.AddItem(wood);
                resourceCount = 0;
            }
        }
        Item fruit = new Item()
        {
            itemDisplayName = "Meyve",
            itemNormalName = "Fruit",
            count = 1,
            itemImg = GameManager.inventorySystem.fruitSprite,
            canUsable = true
        };
        int randomNumber = Random.Range(1, 4);
        if (randomNumber == 2) GameManager.inventorySystem.AddItem(fruit);

        GameManager.soundSytem.PlaySfx(chopSound);
        GameManager.camShake.ShakeCam(0.7f, 0.2f);
        Instantiate(damageParticle, attackPos, Quaternion.identity);
        if (!isDestroying) anim.Play("DamagedAnim");
        GameManager.inventoryDisplayer.RefreshDisplay();

        if (resourceCount <= 0) DestoryObject();
    }
    private Item GetWoodItem(int count)
    {
        Item wood = new Item()
        {
            itemDisplayName = "Tahta",
            itemNormalName = "Wood",
            count = count,
            itemImg = GameManager.inventorySystem.GetItemSprite("Wood"),
            canUsable = false
        };
        return wood;
    }
}

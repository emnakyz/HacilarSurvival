using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour,IResources
{
    public int resourceCount { get; set; }

    private Animator anim;
    [SerializeField] private GameObject damageParticle;

    [SerializeField] private AudioClip stoneSound;

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
        if (tool == PlayerData.Tool.Pickaxe)
        {
            if (resourceCount >= amount * 3)
            {
                Item stone = GetStoneItem(amount * 3);
                GameManager.inventorySystem.AddItem(stone);
                resourceCount -= amount * 3;
            }
            else
            {
                Item stone = GetStoneItem(resourceCount);
                GameManager.inventorySystem.AddItem(stone);
                resourceCount = 0;
            }
        }
        else
        {
            if (resourceCount >= amount)
            {
                Item stone = GetStoneItem(amount);
                GameManager.inventorySystem.AddItem(stone);
                resourceCount -= amount;
            }
            else
            {
                Item stone = GetStoneItem(resourceCount);
                GameManager.inventorySystem.AddItem(stone);
                resourceCount = 0;
            }
        }
        GameManager.soundSytem.PlaySfx(stoneSound);
        GameManager.camShake.ShakeCam(0.7f, 0.2f);
        Instantiate(damageParticle, attackPos, Quaternion.identity);
        if (!isDestroying) anim.Play("DamagedStone");
        GameManager.inventoryDisplayer.RefreshDisplay();

        if (resourceCount <= 0) DestoryObject();
    }
    private Item GetStoneItem(int count)
    {
        Item stone = new Item()
        {
            itemDisplayName = "Taş",
            itemNormalName = "Stone",
            count = count,
            itemImg = GameManager.inventorySystem.GetItemSprite("Stone"),
            canUsable = false
        };
        return stone;
    }
}

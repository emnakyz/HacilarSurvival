using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsedToolChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer toolGfx;  //Eşya spritesi
    [SerializeField] private SpriteRenderer toolBowGfx;   //Yay eşyası spritesi (ayrı olması gerkiyor normal tooldan)

    [SerializeField] private Sprite axeSprite;  //Balta
    [SerializeField] private Sprite pickAxeSprite;  //Kazma
    [SerializeField] private Sprite bowSprite;  //Yay

    private void Update()     //Basılan tuşa göre seçer
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeTool(PlayerData.Tool.Axe);
            toolGfx.gameObject.SetActive(true);
            toolBowGfx.gameObject.SetActive(false);
            toolGfx.sprite = axeSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeTool(PlayerData.Tool.Pickaxe);
            toolGfx.gameObject.SetActive(true);
            toolBowGfx.gameObject.SetActive(false);
            toolGfx.sprite = pickAxeSprite;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeTool(PlayerData.Tool.Bow);
            toolGfx.gameObject.SetActive(false);
            toolBowGfx.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeTool(PlayerData.Tool.None);
            toolGfx.gameObject.SetActive(true);
            toolBowGfx.gameObject.SetActive(false);
            toolGfx.sprite = null;
        }
    }
    private void ChangeTool(PlayerData.Tool tool)
    {
        GameManager.playerData.currentUsedTool = tool; //Gelen toola atar mevcut toolu.
    }
}

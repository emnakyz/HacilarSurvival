using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResources
{
    int resourceCount { get; set; }
    void GetResoure(int amount, PlayerData.Tool tool,Vector2 attackPos);
    void DestoryObject();
}

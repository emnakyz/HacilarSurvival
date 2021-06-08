using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    MonsterSpawner spawner { get; set; }
    void GetDamage(int amount);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public TileMap map;

    public GameObject[] enemies;

    public void enemyTurn()
    {
        print("enemyTurn");
        foreach(GameObject e in enemies)
        {
            print("enemy");
            map.enemyPathMove(map.enemyMovement(e));
        }
    }
}

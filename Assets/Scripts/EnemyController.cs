using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;

    private TileMap map;

    public TileMap Map { get => map; set => map = value; }

    public GameObject[] enemies;

    public List<Enemy> Enemies;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        map = new TileMap();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
        Enemies = new List<Enemy>();
        GameObject.FindGameObjectsWithTag("Enemy").CopyTo(enemies,0);
        if (enemies.Length == 0)
        {
            print("enemies length 0");
        }
    }

    public void AddEnemy(Enemy e)
    {
        Enemies.Add(e);
    }

    public void enemyTurn()
    {
        print("enemyTurn");
        foreach(Enemy e in Enemies)
        {
            print("enemy");
            map.enemyPathMove(map.enemyMovement(e.gameObject));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool attackable = false;
    private UnitController control;
    private EnemyController EControl;

    public int xCoord {get; private set; }
    public int yCoord { get; private set; }

    public List<TileMap.Node> currentPath = null;

    private void Start()
    {
        control = UnitController.Instance;
        EControl = EnemyController.Instance;
        EControl.AddEnemy(this);
    }

    void OnMouseDown()
    {
       if(attackable == true)
        {
            control.battle(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        xCoord = other.GetComponent<Clickable>().xCoord;
        yCoord = other.GetComponent<Clickable>().yCoord;
    }
}

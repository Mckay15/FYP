using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool attackable = false;
    public UnitController control;

    public int xCoord;
    public int yCoord;

    public List<TileMap.Node> currentPath = null;

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

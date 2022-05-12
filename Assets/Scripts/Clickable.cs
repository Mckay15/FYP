using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public int xCoord;
    public int yCoord;

    public GameObject enemyObj;
    public bool enemy = false;
    public bool friend = false;

    public TileMap map;
    public Color col;
    public Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        col = mat.color;
    }

    public void changeColourActive()
    {
        mat.color = Color.red;
    }

    public void changeColourDeactive()
    {
        mat.color = col;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemy = true;
            enemyObj = other.gameObject;
        }
        if(other.CompareTag("Friend"))
        {
            friend = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        enemy = false;
        friend = false;
    }

    //public void activateCharacter()
    //{
    //    map.generatePathTo(xCoord, yCoord);
    //}
}

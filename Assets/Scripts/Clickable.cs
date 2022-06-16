using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    private UnitController U_Control;

    public int xCoord;
    public int yCoord;

    public GameObject enemyObj;
    public bool enemy = false;
    public bool friend = false;

    public TileMap map;
    public Color col;
    public Material mat;

    private void Awake()
    {
        U_Control = UnitController.Instance;
    }

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
        //if (U_Control.activeCharacter == true)
        //{
            if (other.CompareTag("Enemy"))
            {
                enemy = true;
                enemyObj = other.gameObject;
            }
            if (other.CompareTag("Friend"))
            {
                friend = true;
            }
        //}
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

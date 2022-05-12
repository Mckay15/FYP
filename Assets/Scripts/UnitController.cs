using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public TileMap map;

    public Unit currentUnit;

    public bool activeCharacter = false;
    bool isActive = false;

    private Canvas battleScene;

    const int MOUSE = 0;

    private void Start()
    {
        GameObject tempCanvas = GameObject.FindGameObjectWithTag("bScene");
        if (tempCanvas != null)
        {
            print("found bScene");
            battleScene = tempCanvas.GetComponent<Canvas>();
            if (battleScene == null)
            {
                print("battleScene ref failed!");
            }
        }
    }

    public void activateCharacter(GameObject _current, int _x, int _y)
    {
        //currentUnit = _current;
        map.generatePathTo(_current);
    }
    public void placeCharacter(int _x, int _y)
    {
        print("place");
        map.isItInPath(_x, _y);
        activeCharacter = true;
    }

    public void attackRange()
    {
        
        GameObject[] Tester = map.detectEnemy();
        for (int i = 0; i < Tester.Length; i++)
        {
            print(Tester[i]);
        }
    }

    public void colourReturn()
    {
        map.colourReturn();
    }

    public bool attack_Menu()
    {
        return true;
    }

    public void battle(Enemy _enemy) //battle
    {
        attack_Menu();
        if (Random.Range(0, 100) < currentUnit.acc)
        {
            print("hit");
        }
        else
        {
            print("miss");
        }
        map.colourReturn();
        activeCharacter = false;
        currentUnit.active = false;
    }
}

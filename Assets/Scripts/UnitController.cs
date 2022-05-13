using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    private TileMap map;

    private Unit currentUnit;

    public Unit _currentUnit
    {
        get { return currentUnit; }
        set { currentUnit = value; }
    }

    public TileMap Map { get => map; set => map = value; }

    public bool activeCharacter = false;

    bool isActive = false;

    private Canvas battleScene;

    const int MOUSE = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        map = new TileMap();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
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

    public void activateCharacter(Unit _current, int _x, int _y)
    {
        if (map != null)
        {
            _current = _currentUnit;
            map.generatePathTo(_current.gameObject);
        }
        else
        {
            print("map is NULL");
        }
        //currentUnit = _current;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public static UnitController Instance;

    public GameManager Core;

    private TileMap map;

    private Unit currentUnit;

    public Unit _currentUnit
    {
        get { return currentUnit; }
        set { currentUnit = value; }
    }

    private GameObject BattleObject;

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

        Core = GameManager.Core;

        BattleObject = GameObject.FindGameObjectWithTag("bScene");
        //BattleObject.SetActive(false);

        if (BattleObject != null)
        {
            print("found bObject");
            battleScene = BattleObject.GetComponent<Canvas>();
            if (battleScene == null)
            {
                print("battleScene ref failed!");
            }
        }
        else
        {
            print("bObject not found");
        }
    }

    public void activateCharacter(Unit _current, int _x, int _y)
    {
        if (map != null)
        {
            //_current = _currentUnit;
            map.generatePathTo(_currentUnit.gameObject);
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
        
        //if (Random.Range(0, 100) < currentUnit.acc)
        //{
        //    print("hit");
        //}
        //else
        //{
        //    print("miss");
        //}
        map.colourReturn();
        activeCharacter = false;
        currentUnit.active = false;
    }
}

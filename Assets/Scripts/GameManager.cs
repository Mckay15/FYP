using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Core;

    private int State = 0;

    private void Awake()
    {
        Core = this;
    }

    public string get_State()
    {
        switch(State)
        {
            case 0:  //players turn
                return "Players_Turn";
            case 1:  //enemies turn
                return "Enemies_Turn";
        }

        return "invalid int";
    }

    public void End_Player_Turn()
    {
        if (State == 0)
        {
            State = 1;
        }
        else
        {
            Debug.Log("invalid use or call timing Player");
        }
    }

    public void End_Enemy_Turn()
    {
        if (State == 1)
        {
            State = 0;
        }
        else
        {
            Debug.Log("invalid use or call timing Enemy");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

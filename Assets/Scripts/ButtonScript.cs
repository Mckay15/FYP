using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public UnitController control;
    public EnemyController eControl;

    public void stop()
    {
        control.colourReturn();
        control.activeCharacter = false;
        control.currentUnit.active = true;
    }
    public void cancel()
    {
        control.colourReturn();
        control.currentUnit.active = true;
        control.activeCharacter = false;
    }
    public void endTurn()
    {
        eControl.enemyTurn();
    }
}

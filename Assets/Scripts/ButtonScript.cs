using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private UnitController control;

    public EnemyController eControl;
    private void Start()
    {
        control = UnitController.Instance;
    }
    public void stop()
    {
        control.colourReturn();
        control.activeCharacter = false;
        control._currentUnit.active = true;
    }
    public void cancel()
    {
        control.colourReturn();
        control._currentUnit.active = true;
        control.activeCharacter = false;
    }
    public void endTurn()
    {
        eControl.enemyTurn();
    }
}

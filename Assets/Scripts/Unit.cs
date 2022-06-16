using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GameManager Core;

    public bool active = true;
    public Vector3 targetPos;
    public int acc = 95;
    public bool isActive = false;
    const int MOUSE = 0;
    public int xCoord;
    public int yCoord;
    public float Testing_Float = 0;
    public int startXCoord;
    public int startYCoord;

    public UnitController control;

    public List<TileMap.Node> currentPath = null;

    public List<TileMap.Node> range = null;

    private void Start()
    {
        Core = GameManager.Core;
        control = UnitController.Instance;
        if(control == null)
        {
            print("control is NULL");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(MOUSE))
        {
            setTargetPosition();
        }
        if (isActive == true)
        {
            moveObject();
        }
    }

    public void setTargetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0.0f;

        if (plane.Raycast(ray, out point))
            targetPos = ray.GetPoint(point);

        //isActive = true;
    }

    public void moveObject()
    {
        transform.position = new Vector3(targetPos.x,0.0f,targetPos.z);

        //if (transform.position == targetPos)
        //    isActive = false;
        Debug.DrawLine(transform.position, targetPos, Color.red);
    }

    private void OnMouseDown()
    {
        if (Core.get_State() == "Players_Turn")
        {
            if (control.activeCharacter == false && active == true)
            {
                isActive = true;
                startXCoord = xCoord;
                startYCoord = yCoord;
                // control._currentUnit = this;
                if (control == null)
                {
                    print("wait really :(");
                }
                control._currentUnit = this;
                control.activateCharacter(this, xCoord, yCoord);
                print("Click");
            }
        }
    }

    private void OnMouseUp()
    {
        if (control.activeCharacter == false && active == true)
        {
            isActive = false;
            print("up");
            print(xCoord + " " + yCoord);
            control.placeCharacter(xCoord, yCoord);
            control.colourReturn();
            control.attackRange();
            active = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.GetComponent<Clickable>() == null)
        {
            Debug.Log("Clickable null?");
        }
        xCoord = other.GetComponent<Clickable>().xCoord;
        yCoord = other.GetComponent<Clickable>().yCoord;
    }
}

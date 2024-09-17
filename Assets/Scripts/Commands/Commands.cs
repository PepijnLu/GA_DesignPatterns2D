using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NullCommand : Command
{
    public override void Execute(GameObject obj) {Debug.Log($"{obj.name}: null command");}
}

public class MoveUnitCommand : Command
{
    int xMovement, yMovement;
    int xBefore, yBefore;
    GameObject movedObj;
    public MoveUnitCommand(int _x, int _y) : base() 
    {
        xMovement = _x;
        yMovement = _y;
    }
    public override void Execute(GameObject obj)
    {
        // xBefore = (int)obj.transform.position.x;
        // yBefore = (int)obj.transform.position.y;
        movedObj = obj;
        obj.transform.position += new Vector3(xMovement, yMovement, 0);
        Debug.Log($"Moved Unit from x: {xBefore}, y: {yBefore} to x: {obj.transform.position.x}, y: {obj.transform.position.y}");
    }

    public override void Undo()
    {
        xMovement = -xMovement;
        yMovement = -yMovement;
        Execute(movedObj);
    }
}

public class QCommand : Command
{
    public override void Execute(GameObject obj)
    {
        Debug.Log($"{obj.name}: Pressed Q");
    }
}

public class WCommand : Command
{
    public override void Execute(GameObject obj)
    {
        Debug.Log($"{obj.name}: Pressed W");
    }
}

public class ECommand : Command
{
    public override void Execute(GameObject obj)
    {
        Debug.Log($"{obj.name}: Pressed E");
    }
}

public class RCommand : Command
{
    public override void Execute(GameObject obj)
    {
        Debug.Log($"{obj.name}: Pressed R");
    }
}

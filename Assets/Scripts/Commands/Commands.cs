using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NullCommand : Command
{
    public override void Execute(GameObject obj) {Debug.Log($"{obj.name}: null command");}
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

using System.Collections.Generic;
using UnityEngine;


public class NullCommand : Command
{
    public override bool Execute(Object _obj, bool _isUndo) {return true;}
}

public class MoveUnitCommand : Command
{
    public Vector2 direction;
    public bool justCheck;

    public MoveUnitCommand(Vector2 _direction, bool _justCheck) : base() 
    {
        direction = new Vector2(_direction.x, _direction.y);
        justCheck = _justCheck;
    }
    
    public override bool Execute(Object _obj, bool _isUndo)
    {   
        bool moveIsAllowed = true;

        Vector2 currentPosition = _obj.transform.position;
        Vector2 newPosition = currentPosition + direction;

        if(moveIsAllowed && !_isUndo) 
        {
            moveIsAllowed = Notify("ObjectMoved", newPosition, direction, justCheck);
        }

        if(moveIsAllowed && !justCheck) 
        {
            _obj.transform.position = newPosition;
        }
        
        return moveIsAllowed;
    }

    public override void Undo(Object _obj)
    {
        direction.x *= -1;
        direction.y *= -1;
        Execute(_obj, true);
    }
}

public class UndoCommand : Command
{
    public UndoCommand(List<Object> _objectsInScene) : base()
    {
        foreach(Object _obj in _objectsInScene)
        {
            if(_obj.usedMoveCommands.Count > 0) 
            {
                _obj.usedMoveCommands.Pop().Undo(_obj);
            }
            else Debug.Log($"{_obj.name} has nothing to undo");
        }
    }
}

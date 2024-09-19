using System.Collections.Generic;
using UnityEngine;

//Observer pattern
//Base subject class
public class Subject
{
    //List of observers
    public static List<Observer> observers = new();

    //Base notify function
    protected virtual void Notify(string _myEvent)
    {
        foreach(Observer _observer in observers)
        {
            _observer.OnNotify(_myEvent);
        }
    }

    //Overload for passing through parameter necessary for checking collision (move implentation to command!!)
    protected virtual bool Notify(string _myEvent, Vector2 _newPosition, Vector2 _direction, Object _otherObject, bool _justCheck)
    {
        bool moveIsAllowed;

        foreach(Observer observer in observers)
        {
            moveIsAllowed = observer.OnNotify(_myEvent, _newPosition, _direction, _otherObject, _justCheck);
            if(!moveIsAllowed) return false;
        }
        
        return true;
    }

    //Overload for passing through parameter necessary for checking statements (move implentation to command!!)
    protected virtual TextObject Notify(string _myEvent, Vector2 _newPosition, Object _otherObject)
    {
        foreach(Observer observer in observers)
        {
            observer.OnNotify(_myEvent, _newPosition, _otherObject);
        }
        return null;
    }
}

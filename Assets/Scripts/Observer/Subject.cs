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

    //Overload for passing through parameter necessary for checking collision
    protected virtual bool Notify(string _myEvent, Vector2 _newPosition, Vector2 _direction, Object _otherObject, bool _justCheck) {throw new System.Exception("Base notify called");}

    //Overload for passing through parameter necessary for checking statements
    protected virtual Word Notify(string _myEvent, Vector2 _newPosition, Object _otherObject) {throw new System.Exception("Base notify called");}
}

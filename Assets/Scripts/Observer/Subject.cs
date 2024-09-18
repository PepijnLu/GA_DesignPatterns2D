using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    public static List<Observer> observers = new();
    protected void AddObserver(Observer _observer)
    {
        if(!observers.Contains(_observer)) 
        {
            observers.Add(_observer);
        }
    }
    protected void RemoveObserver(Observer _observer)
    {
        if(observers.Contains(_observer)) 
        {
            observers.Remove(_observer);
        }
    }

    protected virtual void Notify(string _myEvent)
    {
        foreach(Observer _observer in observers)
        {
            _observer.OnNotify(_myEvent);
        }
    }

    protected virtual bool Notify(string _myEvent, Vector2 _newPosition, Vector2 _direction, bool _justCheck)
    {
        bool moveIsAllowed;

        foreach(Observer observer in observers)
        {
            moveIsAllowed = observer.OnNotify(_myEvent, _newPosition, _direction, _justCheck);
            if(!moveIsAllowed) return false;
        }
        
        return true;
    }
}

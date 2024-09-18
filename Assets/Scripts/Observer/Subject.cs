using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Subject
{
    public static List<Observer> observers = new();
    protected void AddObserver(Observer observer)
    {
        if(!observers.Contains(observer)) observers.Add(observer);
    }

    protected void RenewList()
    {
        foreach(Observer observer in Observer.sharedObservers)
        {
            if(!observers.Contains(observer)) observers.Add(observer);
            //Debug.Log($"Event: {observer} added to list");
        }
    }
    protected void RemoveObserver(Observer observer)
    {
        if(observers.Contains(observer)) observers.Remove(observer);
    }

    protected virtual void Notify(string _myEvent)
    {
        foreach(Observer observer in observers)
        {
            observer.OnNotify(_myEvent);
        }
    }

    protected virtual void Notify(string _myEvent, Vector2 _newPosition, Vector2 _direction)
    {
        if(observers.Count != Observer.sharedObservers.Count) 
        {
            //Debug.Log("Event: List renewed");
            RenewList();
        }

        //Debug.Log($"Event: observer count = {observers.Count}");

        foreach(Observer observer in observers)
        {
            observer.OnNotify(_myEvent, _newPosition, _direction);
        }
        
    }
}

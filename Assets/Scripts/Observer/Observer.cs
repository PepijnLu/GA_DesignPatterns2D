using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public static List<Observer> sharedObservers = new();
    virtual protected void Start()
    {
        sharedObservers.Add(this);
    }
    virtual public void OnNotify(string _myEvent) {}
    virtual public bool OnNotify(string _myEvent, Vector2 newPosition, Vector2 _direction, Object _obj, bool _justCheck) {return true;}
}

using UnityEngine;

//Observer pattern
//Base observer class
public class Observer : MonoBehaviour
{
    virtual protected void Start()
    {
        Subject.observers.Add(this);
    }
    //Base OnNotify method
    virtual public void OnNotify(string _myEvent) {}

    //Overload OnNotify method for handling collision
    virtual public bool OnNotify(string _myEvent, Vector2 newPosition, Vector2 _direction, Object _otherObject, bool _justCheck) {throw new System.Exception("Base notify called");}

    //Overload OnNotify method for handling checking statements
    virtual public TextObject OnNotify(string _myEvent, Vector2 newPosition, Object _otherObject) {throw new System.Exception("Base notify called");}
}


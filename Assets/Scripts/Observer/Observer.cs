using UnityEngine;

//Observer pattern
//Base observer class
public class Observer : MonoBehaviour
{
    virtual protected void Start()
    {
        Subject.observers.Add(this);
    }
    virtual public void OnNotify(string _myEvent) {}
    virtual public bool OnNotify(string _myEvent, Vector2 newPosition, Vector2 _direction, Object _otherObject, bool _justCheck) {return true;}
    virtual public TextObject OnNotify(string _myEvent, Vector2 newPosition, Object _otherObject) {return null;}
}


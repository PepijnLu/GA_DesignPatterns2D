using UnityEngine;

public class Observer : MonoBehaviour
{
    virtual protected void Start()
    {
        Subject.observers.Add(this);
    }
    virtual public void OnNotify(string _myEvent) {}
    virtual public bool OnNotify(string _myEvent, Vector2 newPosition, Vector2 _direction, bool _justCheck) {return true;}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private Object placeholderObj;
    private void Awake()
    {
        InitializeStates();
    }

    private void InitializeStates()
    {
        new You();
        new Push();
        new Stop();

        new StartChar(placeholderObj);
        new Wall(placeholderObj);
        new Crate(placeholderObj);
    }
    public void SetType(ObjectType state, Object _obj)
    {
        state.OnStateExit(_obj);
        _obj.objectType = state;
        state.OnStateEnter(_obj);
    }
}

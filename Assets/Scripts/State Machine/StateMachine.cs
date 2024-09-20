using System;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private void Awake()
    {
        InitializeStates<ObjectProperty>();
        InitializeStates<ObjectType>();
    }

    //Initialize all subclasses that derive of a base class with type T
    private void InitializeStates<T>()
    {
        var allSubTypes = System.Reflection.Assembly.GetAssembly(GetType()).GetTypes()
            .Where(typeToCheck => 
            {
                while(typeToCheck.BaseType != null)
                {
                    if(typeToCheck.BaseType == typeof(T))
                        return true;
                    typeToCheck = typeToCheck.BaseType;
                }
                return false;
            }).ToList();
        foreach(var subType in allSubTypes)
        {
            Debug.Log("Activator: " + Activator.CreateInstance(subType));
        }
    }

    //Set the type of an object to a new tpye
    public void SetType(ObjectType state, Object _obj)
    {
        if(!ObjectType.objectTypes.ContainsKey(state.GetType().Name))
        {
            state = new();
        }
        
        state.OnStateExit(_obj);
        _obj.objectType = state;
        state.OnStateEnter(_obj);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : Subject
{
    protected Command() {}
    public virtual bool IsExecutable(Object obj) {return true;}
    public virtual void Execute(Object obj, bool isUndo){}
    public virtual void Undo(Object obj){}
    public bool executed;
}

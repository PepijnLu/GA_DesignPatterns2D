public class Command : Subject
{
    protected Command() {}
    public virtual bool IsExecutable(Object _obj) {return true;}
    public virtual bool Execute(Object _obj, bool _isUndo){return true;}
    public virtual void Undo(Object _obj){}
}

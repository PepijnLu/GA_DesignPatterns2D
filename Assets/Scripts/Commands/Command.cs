//Base command class
public class Command : Subject
{
    protected Command() {}
    public virtual bool Execute(Object _obj, bool _isUndo){return true;}
    public virtual void Undo(Object _obj){}
}

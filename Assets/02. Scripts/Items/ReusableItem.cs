public class ReusableItem : ActiveItem
{
    public string _name;
    public string _description;

    public override string Name => _name;
    public override string Description => _description;

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }
}

public class Weapon : Equpiment
{
    public string _name;
    public string _description;
    public int _hp;

    public override string Name => _name;
    public override string Description => _description;
    public int Hp => _hp;

    public override void Equip()
    {
        throw new System.NotImplementedException();
    }

    public override void UnEquip()
    {
        throw new System.NotImplementedException();
    }
}

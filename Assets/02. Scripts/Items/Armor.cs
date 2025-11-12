public class Armor : Equpiment
{
    public string _name;
    public string _description;
    public int _atk;

    public override string Name => _name;
    public override string Description => _description;
    public int Atk => _atk;

    public override void Equip()
    {
        throw new System.NotImplementedException();
    }

    public override void UnEquip()
    {
        throw new System.NotImplementedException();
    }
}

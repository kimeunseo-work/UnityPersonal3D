using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Weapon")]
public class Weapon : Equpiment
{
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] Sprite _icon;
    [SerializeField] int _atk;

    public override string Name => _name;
    public override string Description => _description;
    public override Sprite Icon => _icon;
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

using UnityEngine;
[CreateAssetMenu(menuName = "SO/Item/ReusableItem")]
public class ReusableItem : ActiveItem
{
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] Sprite _icon;

    public override string Name => _name;
    public override string Description => _description;
    public override Sprite Icon => _icon;

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }
}

using UnityEngine;
[CreateAssetMenu(menuName = "SO/Item/SingleUseItem")]
public class SingleUseItem : ActiveItem
{
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] Sprite _icon;
    [SerializeField] int _stackCount;

    public override string Name => _name;
    public override string Description => _description;
    public override Sprite Icon => _icon;
    public int StackCount => _stackCount;

    public override Skill Activate()
    {
        throw new System.NotImplementedException();
    }
}

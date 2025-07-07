using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Potion", menuName = "SO/Item/Potion")]
public class ItemPotionSO : ItemSO
{
    [SerializeField] private List<EffectSO> _effect;
    [SerializeField] private float _effectRadius;
    public override void Use(ActionContainer useObjectController, Vector2 direction)
    {
        GameObject potion = Instantiate(Prefab);
        potion.GetComponent<Potion>().SetEffects(_effect).SetRadius(_effectRadius);

        useObjectController.Thrower.ThroweObject(potion, direction);
    }
}
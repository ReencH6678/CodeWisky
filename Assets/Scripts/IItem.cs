using UnityEngine;

public interface IItem : IThrowable
{
    public void Use(GameObject target);
}

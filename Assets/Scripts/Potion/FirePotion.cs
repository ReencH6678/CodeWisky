using UnityEngine;

public class FirePotion : Potion
{
    public GameObject Copy()
    {
        return this.gameObject;
    }

    public void FallDawn()
    {
        throw new System.NotImplementedException();
    }

    public void Fly()
    {
        throw new System.NotImplementedException();
    }

    public void GiveEffect()
    {
        Debug.Log("Fire effect");
    }
}

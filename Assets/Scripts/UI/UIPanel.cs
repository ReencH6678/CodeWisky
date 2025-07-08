using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public static UIManager manager;

    private void Start()
    {
        manager = UIManager.Instance;
    }
}

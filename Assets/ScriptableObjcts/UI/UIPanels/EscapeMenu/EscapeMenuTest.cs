using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenuTest : UIPanel
{
    public void btnContinue()
    {
        manager.ClosePanel("Escape_Menu");
    }

    public void btnSettings() 
    {
        manager.OpenPanel("Settings_Menu");
    }
}

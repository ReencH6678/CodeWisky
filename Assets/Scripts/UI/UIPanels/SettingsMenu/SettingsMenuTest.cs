using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuTest : UIPanel
{
    public void btnSettingsCategory1()
    {
        manager.OpenPanel("Settings_Category1");
    }
    public void btnSettingsCategory2()
    {
        manager.OpenPanel("Settings_Category2");
    }
    public void btnSettingsCategory3()
    {
        manager.OpenPanel("Settings_Category3");
    }

    public void btnSettingsBack()
    {
        manager.OpenPanel("Escape_Menu");
    }
}

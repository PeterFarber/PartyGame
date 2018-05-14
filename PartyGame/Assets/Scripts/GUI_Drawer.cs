using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_Drawer : MonoBehaviour {

    private Rect windowRect = new Rect(20, 20, 200, 130);

    void OnGUI()
    {
        windowRect = GUI.Window(0, windowRect, WindowFunction, "Score Board");
    }

    void WindowFunction(int windowID)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.green;
        GUI.Label(new Rect(25, 25, 100, 30), "Player 1: ", style);
        style.normal.textColor = Color.red;
        GUI.Label(new Rect(25, 50, 100, 30), "Player 2: ", style);
        style.normal.textColor = Color.blue;
        GUI.Label(new Rect(25, 75, 100, 30), "Player 3: ", style);
        style.normal.textColor = Color.magenta;
        GUI.Label(new Rect(25, 100, 100, 30), "Player 4: ", style);
    }

}

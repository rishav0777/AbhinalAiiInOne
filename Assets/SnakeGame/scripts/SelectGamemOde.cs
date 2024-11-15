using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGamemOde : MonoBehaviour
{
    // Start is called before the first frame update
   public void Mode(int val)
    {
        if (val == 1) DataSaver2.Instance.SetMode(1);
        else if (val == 2) DataSaver2.Instance.SetMode(2);
        else if (val == 3) Application.Quit();
    }
}

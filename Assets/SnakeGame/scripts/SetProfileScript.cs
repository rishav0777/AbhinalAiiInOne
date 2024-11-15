using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetProfileScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DataSaver2.Instance.profileValue = 0;
    }
    public void ChooseProfile(int val)
    {
        DataSaver2.Instance.profileValue = val;
    }

    
}

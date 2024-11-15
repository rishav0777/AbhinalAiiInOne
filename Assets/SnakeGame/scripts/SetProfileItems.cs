using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetProfileItems : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        SetProfile();
    }

    public void SetProfile()
    {
        transform.gameObject.GetComponent<Image>().sprite = DataSaver2.Instance.profileIcon[DataSaver2.Instance.profileValue];
    }
}

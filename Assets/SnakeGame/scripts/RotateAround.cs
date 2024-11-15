using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectR;
    public float _rotate=10.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectR.transform.Rotate(new Vector3(0f, 0f, _rotate * Time.deltaTime));
    }
}

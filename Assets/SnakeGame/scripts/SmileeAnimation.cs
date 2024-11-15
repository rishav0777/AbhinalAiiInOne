using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmileeAnimation : MonoBehaviour
{
    private float popupElapsetime=0;
    private float popdownElapsetime=0;

    public float popuptime=3f; 
    public float popdowntime=3f;

    private Vector3 initialScale;
    public float finalscale;
    private Vector3 FfinalScale;

    public float holdTime = 5.0f;

    public GameObject ob;

    // Start is called before the first frame update
    private void OnEnable()
    {
        popupElapsetime = 0; popdownElapsetime = 0;
        initialScale = ob.transform.localScale;
        FfinalScale = ob.transform.localScale;
        Debug.Log("Ffinal " + FfinalScale+"  "+ initialScale);
        StartCoroutine(popUp());
    }

    IEnumerator popUp()
    {
        
        Debug.Log("popup"+popupElapsetime+" "+popuptime);
        while (popupElapsetime < popuptime)
        {
            ob.transform.localScale = Vector3.Lerp(initialScale, Vector3.one * finalscale, popupElapsetime / popuptime);
            popupElapsetime += Time.deltaTime;
            yield return null;
        }
        initialScale = ob.transform.localScale;
        Debug.Log("init "+initialScale);
        StartCoroutine(popDown());
        
    }
    IEnumerator popDown()
    {
        //yield return new WaitForSeconds(holdTime);
        Debug.Log("popdown");
        while (popdownElapsetime < popdowntime)
        {
            ob.transform.localScale = Vector3.Lerp(initialScale, FfinalScale, popdownElapsetime / popdowntime);
            popdownElapsetime += Time.deltaTime;
            yield return null;
        }
        Invoke("SetDeActive", 5.0f);

    }
    public void SetDeActive()
    {
        transform.gameObject.SetActive(false);
    }
}

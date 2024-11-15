using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerSelection : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Sprite> player = new List<Sprite>();
    private int index=0;
    public GameObject left, right;
    private void Start()
    {
        index = player.Count / 2;
    }


    public void RightMovement()
    {
       if((index+1)<player.Count) AnimateMovement(player[index++]);
    }
    public void LeftMovement()
    {
        if ((index - 1) >=0) AnimateMovement(player[index--]);
    }

    public void AnimateMovement(Sprite s)
    {
        Change();
        StartCoroutine(popUp(s));
    }


    private float popupElapsetime = 0;
    private float popdownElapsetime = 0;

    public float popuptime = 3f;
    public float popdowntime = 3f;

    private Vector3 initialScale;
    public float finalscale;
    private Vector3 FfinalScale;

    public float holdTime = 5.0f;

    public GameObject ob;

    // Start is called before the first frame update
    public void Change()
    {
        popupElapsetime = 0; popdownElapsetime = 0;
        initialScale = ob.transform.localScale;
        FfinalScale = ob.transform.localScale;
        Debug.Log("Ffinal " + FfinalScale + "  " + initialScale);

    }
    public void CheckHandle()
    {
        if ((index) == player.Count - 1) right.SetActive(false);
        else if ((index) == 0) left.SetActive(false);
        else
        {
            left.SetActive(true);
            right.SetActive(true);
        }
    }

    IEnumerator popUp(Sprite s)
    {

        Debug.Log("popup" + popupElapsetime + " " + popuptime);
        while (popupElapsetime < popuptime)
        {
            ob.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero * finalscale, popupElapsetime / popuptime);
            popupElapsetime += Time.deltaTime;
            yield return null;
        }
        initialScale = ob.transform.localScale;
        Debug.Log("init " + initialScale);
        ob.GetComponent<Image>().sprite = s;
        StartCoroutine(popDown(s));

    }
    IEnumerator popDown(Sprite s)
    {
        //yield return new WaitForSeconds(holdTime);
        Debug.Log("popdown");
        while (popdownElapsetime < popdowntime)
        {
            ob.transform.localScale = Vector3.Lerp(initialScale, FfinalScale, popdownElapsetime / popdowntime);
            popdownElapsetime += Time.deltaTime;
            yield return null;
        }
        CheckHandle();
        //Invoke("SetDeActive", 5.0f);

    }
    public void SetDeActive()
    {
        transform.gameObject.SetActive(false);
    }
}

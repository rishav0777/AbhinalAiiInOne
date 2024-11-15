using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int currentPosition = 0;
    [SerializeField] private GameObject[] Path;

    private float singlePathSpeed = 0.3f;
    private float MoveToStartPositionSpeed = 0.25f;
    private GameObject HomePosition;
    private bool isHome = true;
    public SnakeLadder snakeladder;
    private int steps;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeMovement(int step)
    {
        steps = step;
        if (isHome && step == 1)
        {
            GoToStartPosition();
        }
        else if (!isHome)
        {
            MoveBySteps(step);
        }
    }

    public void GoToStartPosition()
    {
        isHome = false;
        Debug.Log("Go to start Position");
        currentPosition = 1;

        StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, Path[currentPosition],0,false));

    }

    public void MoveBySteps(int steps)
    {
        Highlight();
        for (int i = 0; i < steps; i++)
        {
            bool last = false;
            if (i == steps - 1 ) last = true;
            currentPosition++;

            StartCoroutine(MoveDelayed(i, singlePathSpeed, last, true, Path[currentPosition],0,false));
        }
    }

    private float elapseTime = 0;
    public float timeDuration = 6;
    private IEnumerator MoveDelayed(int delay, float time, bool last, bool playSound, GameObject parent,int final,bool flag)
    {
        //timetoShrink = time / 2;
        //highlight.SetActive(true);
        if (!playSound)
        {
            Debug.Log("Huddle ");
            yield return new WaitForSeconds(delay * 3f);
            transform.SetParent(parent.transform);
            while (elapseTime < timeDuration)
            {
                transform.localPosition = Vector3.Lerp(transform.position, Vector3.zero, elapseTime / timeDuration);
                elapseTime += Time.deltaTime;
               // yield return null;
            }
        }

        /*if (!playSound && Mathf.Abs(currentPosition - final) > 2)
        {
            yield return new WaitForSeconds(delay * 1f);
            Debug.Log("before last go home");
            transform.SetParent(parent.transform);
            transform.localPosition = Vector2.zero;
            if(!flag)StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, false, false, Path[currentPosition -= 2],final,flag));
            else StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, false, false, Path[currentPosition += 2], final, flag));
        }
        else if (!playSound &&  Mathf.Abs(currentPosition-final) <= 2)
        {
            Debug.Log("before last");
            currentPosition = final;
            StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, Path[final], final,flag));
        }*/

        if (playSound)
        {
            yield return new WaitForSeconds(delay * 1f);
            transform.SetParent(parent.transform);
            transform.localPosition = Vector2.zero;
        }

        if (last)
        {
            if (currentPosition == 99) snakeladder.WinStatus(snakeladder.GetMyTurn());
            if (CheckForHuddle() && steps != 6)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                snakeladder.SetTurn();
                snakeladder.Setflag(snakeladder.GetMyTurn());
            }
            else if(steps == 6 && CheckForHuddle()) snakeladder.Setflag(snakeladder.GetMyTurn());

        }
    }


    public bool CheckForHuddle()
    {
        foreach(var snake in DataSaver2.Instance.SnakeMap)
        {
            int val = snake.Key;
            if (val == currentPosition)
            {
                currentPosition = snake.Value;
                StartCoroutine(MoveDelayed(0, singlePathSpeed, true, false, Path[snake.Value],snake.Value,false));
                return false;
            }
        }
        foreach(var ladder in DataSaver2.Instance.LadderMap)
        {
            int val = ladder.Key;
            if (val == currentPosition)
            {
                currentPosition = ladder.Value;
                StartCoroutine(MoveDelayed(0, singlePathSpeed, true, false, Path[ladder.Value], ladder.Value, true));
                return false;
            }
        }
        return true;
    }




    public void Highlight()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }


    public int GetCurrentPosition()
    {
        return currentPosition;
    }

    public bool GetHomestatus()
    {
        return isHome;
    }
    public int GetPawnPosition()
    {
        return currentPosition;
    }


}

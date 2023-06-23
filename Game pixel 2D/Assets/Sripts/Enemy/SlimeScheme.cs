using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeScheme : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private int waitTime;


    private float waitTimer;
    private Vector3 posArrive;
    private Vector3 originPos;
    private bool isTurn;
    private bool isNext;
    private float direct;
    private bool isTurnDirect;

    private void OnEnable()
    {
        originPos = transform.position;
    }

    private void Start()
    {
        isTurn = false;
        isNext = true;
        waitTimer = 0;
    }

    private void Update()
    {
        GenerateWaypointPos();

        direct = GetDirection(originPos, posArrive);
        if (isTurnDirect) direct = -direct;
        FlipSide(direct);

        MoveBetweenWaypoint();
    }

    private void GenerateWaypointPos()
    {
        if (isNext)
        {
            isTurn = false;
            isTurnDirect = false;
            float a = Random.Range(-360, 361);
            posArrive = new Vector3(originPos.x + radius * Mathf.Cos(a * Mathf.PI / 180),
                        originPos.y + radius * Mathf.Sin(a * Mathf.PI / 180), 0);

            isNext = false;
        }
    }

    private void MoveBetweenWaypoint()
    {

        if (!isTurn)
        {
            transform.position = Vector3.MoveTowards(transform.position, posArrive, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, posArrive) < 0.02f)
            {
                waitTimer += Time.deltaTime;
                if (waitTimer >= waitTime)
                {
                    waitTimer = 0;
                    isTurn = true;
                }
                
                isTurnDirect = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originPos) < 0.01f)
            {
                isNext = true;
            }
        }
    }
    

    private float GetDirection(Vector3 originPos, Vector3 posArrive)
    {
        float direction;
        if (originPos.x < posArrive.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        return direction;
    }

    private void FlipSide(float direction)
    {
        if (direction == 1)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (direction == -1)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

    }


}

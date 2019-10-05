using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    int numberOfTargets = 2;
    int targetsHit = 0;
    bool hitTarget = false;
    float targetHitTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer(ref hitTarget, ref targetHitTimer);
        Debug.Log(targetsHit);
        if(targetsHit == numberOfTargets)
        {
            Debug.Log("TargetsHit reached");
        }
    }

    public void TargetCount()
    {
        Debug.Log("inside Targetcount");
        targetsHit++;
        targetHitTimer = 1.0f;
        hitTarget = true;
    }
    public bool Timer(ref bool isChanging, ref float timer)
    {
        if (isChanging)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                isChanging = false;
            }
        }
        return isChanging;
    }
}

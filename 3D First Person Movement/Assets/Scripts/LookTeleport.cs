using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTeleport : MonoBehaviour
{
    public float desiredDistance = 10000000000f;
    public Transform character;
    public float moveSpeed = 50;

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            //Given some means of determining a target point.
            var targetPoint = character.transform.forward * desiredDistance;
            //However you want to do that.

            MoveTowardsTarget(targetPoint);
        }
    }

    void MoveTowardsTarget(Vector3 target)
    {
        var cc = GetComponent<CharacterController>();
        var offset = target - transform.position;
        //Get the difference.
        if (offset.magnitude > .1f)
        {
            //If we're further away than .1 unit, move towards the target.
            //The minimum allowable tolerance varies with the speed of the object and the framerate. 
            // 2 * tolerance must be >= moveSpeed / framerate or the object will jump right over the stop.
            offset = offset.normalized * moveSpeed;
            //normalize it and account for movement speed.
            cc.Move(offset * Time.deltaTime);
            //actually move the character.
        }
    }
}

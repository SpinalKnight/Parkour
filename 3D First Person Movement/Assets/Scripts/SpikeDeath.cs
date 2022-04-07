using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDeath : MonoBehaviour
{
    public Transform GroundCheck;
    public float SpikeDistance = 0.5f;
    public LayerMask Spikes;
    public GameObject Player;
    public Transform Checkpoint;

    bool touchingSpike;

    /*
    private void Update()
    {
        touchingSpike = Physics.CheckSphere(GroundCheck.position, SpikeDistance, Spikes);
        print(touchingSpike);

        if (touchingSpike)
        {
            print("RESPAWNING");
            print(Player.transform.position);
            print(Checkpoint.transform.position);
            Player.transform.position = Checkpoint.transform.position;
        }
    */

    public float desiredDistance = 10000000000f;
    public Transform character;
    public float moveSpeed = 50;

    void Update()
    {
        touchingSpike = Physics.CheckSphere(GroundCheck.position, SpikeDistance, Spikes);
        if (touchingSpike)
        {
            //Given some means of determining a target point.
            var targetPoint = Checkpoint.transform.position;
            //However you want to do that.

            MoveTowardsTarget(targetPoint);
        }
    }

    void MoveTowardsTarget(Vector3 targetPoint)
    {
        var cc = GetComponent<CharacterController>();
        var offset = targetPoint - transform.position;
        //Get the difference.
        if (offset.magnitude > .1f)
        {
            if (Checkpoint.transform.position != character.transform.position)
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
}
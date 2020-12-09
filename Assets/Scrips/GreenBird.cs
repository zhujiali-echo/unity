using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird :Bird {
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rigidbody.velocity;
        speed.x *= -1;
        rigidbody.velocity = speed;

    }
	
}

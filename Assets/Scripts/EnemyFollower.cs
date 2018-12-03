using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollower : Enemy {
    Transform target;
    Animator _anim;

    protected override void Start()
    {
        base.Start();

        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    protected override void  Update () {
        base.Update();

        if (velocity.magnitude == 0)
        {
            transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
            _anim.speed = 1;
        }
        else
        {
            _anim.speed = 0;
        }

	}
}

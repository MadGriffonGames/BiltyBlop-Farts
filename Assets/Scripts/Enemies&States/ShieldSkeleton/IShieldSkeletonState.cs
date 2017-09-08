﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShieldSkeletonState
{
    void Execute();
    void Enter(ShieldSkeleton enemy);
    void Exit();
    void OnCollisionEnter2D(Collision2D other);
}

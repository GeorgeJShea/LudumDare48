using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitable
{
    public float Health { get; set; }
    public void Damage(float damage, bool makeHitSound = true);
    public void Hit(Projectile projectile, out bool destroy);
}

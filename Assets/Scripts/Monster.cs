using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _hitSprite;
    [SerializeField] ParticleSystem _particleSystem;

    private SpriteRenderer _spriteRenderer;

    bool _isHit = false;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsHitFromCollision(collision))
        {
            StartCoroutine(DoHit());
        }
    }

    bool IsHitFromCollision(Collision2D collision)
    {
        if (_isHit)
            return false;

        //Bird bird = collision.gameObject.GetComponent<Bird>();
        //if (bird != null)
        //    return true;

        float collisionForce = collision.GetImpactForce();
        //Debug.Log("collisionForce " + collisionForce);
        if (collisionForce > 200.0F)
            return true;

        //    if (collision.GetContact(i).normal.y < -0.5)
        //        return true;

        return false;
    }

    IEnumerator DoHit()
    {
        _isHit = true;
        _spriteRenderer.sprite = _hitSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

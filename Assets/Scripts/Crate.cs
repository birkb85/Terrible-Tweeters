using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Crate : MonoBehaviour
{
    [SerializeField] Sprite _sprite1;
    [SerializeField] Sprite _sprite2;
    [SerializeField] ParticleSystem _particleSystem;

    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;

    bool _isHit = false;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _spriteRenderer.sprite = Random.value > 0.5 ? _sprite1 : _sprite2;
        _spriteRenderer.flipX = Random.value > 0.5;
        _spriteRenderer.flipY = Random.value > 0.5;
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

        float collisionForce = collision.GetImpactForce();
        //Debug.Log("collisionForce " + collisionForce);
        if (collisionForce > 200.0F)
            return true;

        return false;
    }

    IEnumerator DoHit()
    {
        _isHit = true;
        _rigidbody2D.simulated = false;
        _spriteRenderer.enabled = false;
        _particleSystem.Play(); // TODO BB Not showing when not active.
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}

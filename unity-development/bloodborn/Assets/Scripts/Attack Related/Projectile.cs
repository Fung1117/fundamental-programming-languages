
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackData attackData;
    [SerializeField] private float speed = 15f;
    [SerializeField] private float despawnTime = 8f;
    [SerializeField] private bool vampaitic = false;
    [SerializeField] private bool ignoreWall = false;
    [SerializeField] private TargetType target;
    [SerializeField] private GameObject healEffect;
    [SerializeField] private GameObject spawnOnHit;

    enum TargetType
    {
        ALL,
        ENEMY,
        PLAYER
    }
    private void Start()
    {
        Destroy(gameObject, despawnTime);
    }

    void Update()
    {
        if (vampaitic)
        {
            transform.Translate(Player.player.gameObject.GetComponent<CharacterController2D>().TotalSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    public void Initiate(AttackData _attaackData)
    {
        attackData = _attaackData;
    }
    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.layer == 11)
        {
            if (target == TargetType.ENEMY)
            {
                int damage = obj.gameObject.GetComponent<Character>().battleBehavior.TakeDamage(attackData);
                if (vampaitic)
                {
                    GameObject bloodStealEffect = Instantiate(healEffect, Player.player.gameObject.transform.position, Player.player.gameObject.transform.rotation);
                    float recoverAmount = damage * 0.5f;
                    if (Player.player.GetStat().bp+(int)recoverAmount< Player.player.GetStat().max_bp)
                    {
                        Player.player.battleBehavior.TakeDamage(-(int)recoverAmount);
                    }
                    else if(Player.player.GetStat().bp == Player.player.GetStat().max_bp)
                    {
                        Player.player.battleBehavior.TakeDamage(-0);
                    }
                    else
                    {
                        Player.player.battleBehavior.TakeDamage(Player.player.GetStat().bp - Player.player.GetStat().max_bp);
                    }
                    Destroy(bloodStealEffect, 1f);
                }
                if (spawnOnHit != null) Instantiate(spawnOnHit, obj.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(this.gameObject);
            }
            else if (target == TargetType.ALL)
            {
                obj.gameObject.GetComponent<Character>().battleBehavior.TakeDamage(attackData);
                if (spawnOnHit != null) Instantiate(spawnOnHit, obj.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
        else if (obj.gameObject.layer == 12)
        {
            if (target == TargetType.PLAYER)
            {
                Player.player.battleBehavior.TakeDamage(attackData);
                if (spawnOnHit != null) Instantiate(spawnOnHit, obj.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(this.gameObject);
            }
            else if (target == TargetType.ALL)
            {
                Player.player.battleBehavior.TakeDamage(attackData);
                if (spawnOnHit != null) Instantiate(spawnOnHit, obj.ClosestPoint(transform.position), Quaternion.identity);
                Destroy(this.gameObject);
            }

        }
        else if (obj.gameObject.layer == 8 && !ignoreWall)
        {
            if (spawnOnHit != null) Instantiate(spawnOnHit, obj.ClosestPoint(transform.position), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

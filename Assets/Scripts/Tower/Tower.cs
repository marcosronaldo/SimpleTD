using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    public enum Type
    {
        Range,
        Damage,
        Slow
    }

    public int damage;
    public bool slow;
    public float range;
    public float attacksPerSecond;
    
    private float timer = 0;
    private float attackTime => 1f/attacksPerSecond;
    
    private void Awake()
    {
        GetComponent<SphereCollider>().radius = range;
    }

    public List<Enemy> inRange = new List<Enemy>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            inRange.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            inRange.Remove(other.GetComponent<Enemy>());
        }
    }

    private void Update()
    {
        if (timer > attackTime)
        {
            if (inRange.Count > 0)
            {
                Attack(inRange[Random.Range(0, inRange.Count)]);
                timer = 0;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void Attack(Enemy enemy)
    {
        print("Attack Enemy! "+enemy.gameObject.name);
        //TODO IMPLEMENT
    }
}
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum Type
    {
        Range,
        Damage,
        Slow
    }

    public GameObject shotPrefab;

    public int damage;
    public bool slow;
    public float range;
    public float attacksPerSecond;
    public int cost;

    public List<Enemy> inRange = new List<Enemy>();

    private float timer;
    private float attackTime => 1f / attacksPerSecond;

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = range;
    }

    private void Update()
    {
        inRange.RemoveAll(r => r == null);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) inRange.Add(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) inRange.Remove(other.GetComponent<Enemy>());
    }

    private void Attack(Enemy enemy)
    {
        var sphere = transform.Find("Sphere");
        var shot = Instantiate(shotPrefab, sphere).GetComponent<Shot>();
        shot.tower = this;
        shot.transform.localPosition = Vector3.zero;
        var dir = (enemy.transform.position - sphere.transform.position).normalized;
        shot.GetComponent<Rigidbody>().AddForce(dir * 800);
    }
}
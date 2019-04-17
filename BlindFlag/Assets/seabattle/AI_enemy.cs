using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.UIElements;

public class AI_enemy : MonoBehaviour
{
    private static int HP;
    private static int Money;
    private static int XP;
    private static int Lvl;
    public static int Damage;
    private static int BlindShip_LVL;

    private static float FireRadius;
    public GameObject target;
    private Transform BlindShip;
    public GameObject Cannonball;

    private int time;

    
    // Start is called before the first frame update
    void Start()
    {
        BlindShip_LVL = BlindShip_Stat.Lvl;
        
        if (BlindShip_LVL < 6) Lvl = Random.Range(1, Lvl);
        else Lvl = Random.Range(BlindShip_LVL - 5, BlindShip_LVL + 5);

        Damage = Lvl * 3;
        
        XP = (Lvl * 100)/Random.Range(2, 10);
        Money = (Lvl * 1000)/Random.Range(2, 50);

        HP = Lvl * 100;

        FireRadius = 100f;
        BlindShip = target.transform;

        time = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Dead();

        float distance = Vector3.Distance(BlindShip.position, transform.position);
        
        FaceTarget();
        
        if (distance > FireRadius) transform.Rotate(0, 90, 0);
        else
        {
            if (BlindShip.position.z > transform.position.z) Fire(true);
            else if (BlindShip.position.z < transform.position.z) Fire(false);
        }
        transform.Translate(Vector3.left * 15 * Time.deltaTime);
        
    }

    void Fire(bool tribord)
    {
        if (time == 0)
        {
            Vector3 cannonball_pos_1 = new Vector3(0f, 0f, 0f);
            Vector3 cannonball_pos_2 = new Vector3(0f, 0f, 0f);
            Vector3 cannonball_pos_3 = new Vector3(0f, 0f, 0f);
            Vector3 cannonball_pos_4 = new Vector3(0f, 0f, 0f);
            Vector3 cannonball_pos_5 = new Vector3(0f, 0f, 0f);
            Vector3 cannonball_pos_6 = new Vector3(0f, 0f, 0f);

            Quaternion cannonball_rot = new Quaternion();

            if (tribord)
            {
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_1 = transform.Find("Cannon_T (0)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_2 = transform.Find("Cannon_T (1)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_3 = transform.Find("Cannon_T (2)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_4 = transform.Find("Cannon_T (3)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_5 = transform.Find("Cannon_T (4)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_6 = transform.Find("Cannon_T (5)").position);

                UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_rot = transform.rotation);
            }
            else
            {
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_1 = transform.Find("Cannon_B (0)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_2 = transform.Find("Cannon_B (1)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_3 = transform.Find("Cannon_B (2)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_4 = transform.Find("Cannon_B (3)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_5 = transform.Find("Cannon_B (4)").position);
                UnityMainThreadDispatcher.Instance()
                    .Enqueue(() => cannonball_pos_6 = transform.Find("Cannon_B (5)").position);

                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                    cannonball_rot = Quaternion.LookRotation(-transform.forward, Vector3.up));
            }

            UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_1.y = 2);
            UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_2.y = 2);
            UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_3.y = 2);
            UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_4.y = 2);
            UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_5.y = 2);
            UnityMainThreadDispatcher.Instance().Enqueue(() => cannonball_pos_6.y = 2);

            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Instantiate(Cannonball, cannonball_pos_1, cannonball_rot));
            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Instantiate(Cannonball, cannonball_pos_2, cannonball_rot));
            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Instantiate(Cannonball, cannonball_pos_3, cannonball_rot));
            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Instantiate(Cannonball, cannonball_pos_4, cannonball_rot));
            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Instantiate(Cannonball, cannonball_pos_5, cannonball_rot));
            UnityMainThreadDispatcher.Instance()
                .Enqueue(() => Instantiate(Cannonball, cannonball_pos_6, cannonball_rot));

            time = 50;
        }
        else
        {
            time -= 1;
        }
    }


    void FaceTarget()
    {
        Vector3 direction = (BlindShip.position - transform.position).normalized;
        Quaternion Rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, Time.deltaTime * 50f);
    }

    void Dead()
    {
        BlindShip_Stat.Money += Money;
        BlindCaptain_Stat.XP += XP;
        Destroy(gameObject, 0f);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Cannonball(Clone)")
        {
            Debug.Log("vous l'avez touche");
            HP -= BlindShip_Stat.Damage;
            Destroy(other.gameObject, 0f);
        }
    }
    
    
    
    private void OnGUI()
    {
        GUI.Box(new Rect(1000, 10, 150, 25), "Enemy's STATS");
        GUI.Box(new Rect(1000, 36, 150, 25), "HP: " + HP + " / " + Lvl * 100);
        GUI.Box(new Rect(1000, 62, 150, 25), "Lvl: " + Lvl);
    }
}

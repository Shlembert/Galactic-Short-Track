using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] Pos;
    public GameObject Repire;
    public GameObject Shield;
    public Player_1 player_1;
    public Player_2 player_2;

    private bool ready=true;
    private bool R_S;

    private void Start()
    {
        player_1.seizing = true;
        player_2.seizing = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(15);
        ready = true;
    }
    void Update()
    {
        if (player_2.seizing || player_1.seizing)
        {
            if (ready)
            {
                int x = Random.Range(0, 8);
                if (!R_S)
                {
                    _ = Instantiate(Repire, Pos[x].position, Quaternion.identity);
                    R_S = true;
                    player_1.seizing = false;
                    player_2.seizing = false;
                }
                else
                {
                    _ = Instantiate(Shield, Pos[x].position, Quaternion.identity);
                    R_S = false;
                    player_1.seizing = false;
                    player_2.seizing = false;
                }
                ready = false;
                StartCoroutine(Wait());
            }
        }
    }
}

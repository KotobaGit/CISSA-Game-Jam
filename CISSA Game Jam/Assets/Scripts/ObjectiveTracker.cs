using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    private int ObjectivesAmount = 3;
    [SerializeField] public static bool[] Objective = new bool[3 - 1]; //number of objectives -1
    [SerializeField] private GameObject[] ObjectiveTicks;
    [SerializeField] private GameObject HiddenObjective;
    [SerializeField] private GameObject HiddenObjectiveTick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        for (int i = 0; i < ObjectivesAmount; i++) //at the start of every scene
        {
            TickObjective(i, Objective[i]);
        }
    }
    public void TickObjective(int num,bool ticked)
    {
        Objective[num] = ticked;
        ObjectiveTicks[num].SetActive(ticked);
        for (int i = 0; i < ObjectivesAmount; i++)
        {
            if (!Objective[i])
            {
                HiddenObjective.SetActive(false);
                HiddenObjectiveTick.SetActive(false);
                return;
            }
            //if it passes the for loop then we can start the last hidden objective
        }
        HiddenObjective.SetActive(true);
    }

    // Update is called once per frame
    public void TickHiddenObjective()
    {
        HiddenObjectiveTick.SetActive(true);
    }
}

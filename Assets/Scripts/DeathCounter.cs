using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCounter:MonoBehaviour
{
    int deaths = 0;
    public delegate void OnDeathCountChanged(int deathsNow);
    public event OnDeathCountChanged onDeathCountChangedEvent;

    private void Start()
    {
        onDeathCountChangedEvent?.Invoke(deaths);
    }

    public void AddDeath()
    {
        deaths++;
        onDeathCountChangedEvent?.Invoke(deaths);

    }
}

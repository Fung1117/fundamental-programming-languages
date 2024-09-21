using UnityEngine;

public class BattleEngager
{
    private float lastBattle = 0;
    private float regenStartCD = 0;

    public BattleEngager(float _regenStartCD = 5)
    {
        regenStartCD = _regenStartCD;
    }

    public bool isEngaged()
    {
        return (Time.time - lastBattle) < regenStartCD;
    }

    public void Engage()
    {
        lastBattle = Time.time;
    }
}

using Mirror;

public class Targetable : NetworkBehaviour
{
    public void Heal()
    {
        GetComponent<Health>().currHealth = 100;
    }

    public bool IsDead()
    {
        return GetComponent<Unit>().isDead;
    }
}

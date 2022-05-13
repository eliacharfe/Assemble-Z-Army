using UnityEngine;

public class Targeter : MonoBehaviour
{
    private Targetable target;

    public void SetTarget(GameObject targetObject)
    {
        if (!targetObject.TryGetComponent<Targetable>(out Targetable targetable)) { return; }

        this.target = targetable;
    }

    public void ClearTarget()
    {
        target = null;
    }

    public Targetable GetTargetable()
    {
        return target;
    }

}

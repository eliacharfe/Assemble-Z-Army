using UnityEngine;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour
{
    [SerializeField] private Mana mana = null;
    [SerializeField] private GameObject manaBar = null;
    [SerializeField] public Image manaBarImage = null;

    private void Awake()
    {
        manaBar.SetActive(true);
        mana.ClientOnManaUpdate += HandleManaUpdated;
    }

    private void OnDestroy()
    {
        mana.ClientOnManaUpdate -= HandleManaUpdated;
    }

    public void HandleManaUpdated(int currMana, int maxMana)
    {
        manaBarImage.fillAmount = (float)currMana / maxMana;
    }
}

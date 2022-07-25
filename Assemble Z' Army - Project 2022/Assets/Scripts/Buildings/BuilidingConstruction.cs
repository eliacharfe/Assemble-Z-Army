using UnityEngine;
using Mirror;

public class BuilidingConstruction : NetworkBehaviour
{
    [SerializeField] private CostumeSlider buldingConstructionSlider = null;
    [SerializeField] [SyncVar(hook =nameof(HandleConstructionUpdated))] float timePassed = 0;

    [SerializeField] private float constructionTime = 5f;

    [SerializeField] private GameObject hammerSprite;

    [SerializeField] private Transform[] buildingPositions = null;
    public int buildingPoistionIndex = 0;

    private GameObject mouseTopIcon = null;

    private void Start()
    {
        buldingConstructionSlider.resetSlider();
    }

    // Todo - increase time by worker.
    private void Update()
    {
        if(buldingConstructionSlider.FillAmount() >= 1f) {
            FinishConstruction();
        }
    }

    private void OnMouseEnter()
    {
        if(FindObjectOfType<RTSController>().HasWorkers())
        mouseTopIcon = Instantiate(hammerSprite, Utilities.Utils.GetMouseIconPos(), Quaternion.identity);
    }
    private void OnMouseOver()
    {

        if (mouseTopIcon)
            mouseTopIcon.transform.position = Utilities.Utils.GetMouseIconPos();
    }

    private void OnMouseExit()
    {
        if (mouseTopIcon)
        {
            Destroy(mouseTopIcon);
        }
    }

    #region Server

    // Add building time.
    [Command]
    public void CmdIncreasingBuildingTime(float value)
    {
        timePassed += value / constructionTime;
    }
    #endregion


    // Enable building functionallity and disable construction
    // elements.
    private void FinishConstruction()
    {
        FindObjectOfType<AudioPlayer>().PlayConstructionCompletedBuilding();
        GetComponent<Building>().enabled = true;
        buldingConstructionSlider.gameObject.SetActive(false);
        this.enabled = false;
    }

    // Get building time passed.
    private float GetBuildingConstructionTime()
    {
        return buldingConstructionSlider.FillAmount();
    }

    private void HandleConstructionUpdated(float oldTime,float newTime)
    {
        buldingConstructionSlider.setValue(newTime);
    }

    public Vector3 GetBuildingPoint()
    {
        if(buildingPoistionIndex < buildingPositions.Length)
        {
            return buildingPositions[buildingPoistionIndex].position;
        }
        return Vector3.negativeInfinity;
    }


    public void IncreaseIndex()
    {
        buildingPoistionIndex++;
    }

    public void DecreaseIndex()
    {
        if (buildingPoistionIndex <= 0)
        {
            return;
        }
        buildingPoistionIndex--;
    }
}

using UnityEngine;
using Mirror;

public class BuilidingConstruction : NetworkBehaviour
{
    [SerializeField] private CostumeSlider buldingConstructionSlider = null;
    [SerializeField] [SyncVar(hook =nameof(HandleConstructionUpdated))] float timePassed = 0;

    [SerializeField] private float constructionTime = 5f;

    [SerializeField] private GameObject hammerSprite;

    [SerializeField] private Transform[] buildingPositions = null;
    public bool[] buildingPostionsPicked;

    public int buildingPoistionIndex = 0;

    private GameObject mouseTopIcon = null;

    private void Start()
    {
        buldingConstructionSlider.resetSlider();
        buildingPostionsPicked = new bool[6] { false, false, false, false, false, false };
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

    public (Vector3,int) GetNearestBuildingPoint(Vector2 workerPosition)
    {
        double minDistancePos = double.MaxValue;
        int loopIndex = 0;
        bool foundIndex = false;
        print(buildingPositions.Length);
        print(buildingPostionsPicked.Length);
        foreach (var pos in buildingPositions)
        {
            var distnace = Vector2.Distance(workerPosition, pos.position);
            if (distnace < minDistancePos && !buildingPostionsPicked[loopIndex])
            {

                minDistancePos = distnace;
                buildingPoistionIndex = loopIndex;
                foundIndex = true;
            }
            loopIndex++;
        }
        if( foundIndex) {
            buildingPostionsPicked[buildingPoistionIndex] = true;
            return (buildingPositions[buildingPoistionIndex].position,buildingPoistionIndex);
        }
        return (Vector3.negativeInfinity,-1);
    }

    public void FreeBuildingPoint(int index)
    {
        if (index < buildingPostionsPicked.Length) {
            buildingPostionsPicked[index] = false;
        }
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


    public bool HasFreeSpace()
    {
        foreach (var avaiblePosition in buildingPostionsPicked)
            if (!avaiblePosition)
                return true;
        return false;
    }
}

public static class ForEachExtensions
{
    //public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
    //{
    //    int idx = 0;
    //    foreach (T item in enumerable)
    //        handler(item, idx++);
    //}
}
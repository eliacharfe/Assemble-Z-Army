using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour
{

    // Type of solider building recived and create.
    [SerializeField] float spawnTime = 5f;

    [SerializeField] Transform spawnPoint = null;
    [SerializeField] Transform enterPoint = null;

    [SerializeField] GameObject token = null;
    [SerializeField] CostumeSlider timeSlider = null;

    public Macros.Buildings Id;

    // Units waiting to be recruited.
    public List<Unit> waitingUnit = new List<Unit>();

    // Prefab of the current unit need to be spawned.
    private Unit spawnUnitPrefab = null;

    private bool inProgess = false;
    private float timeLeft = 0;

    private UnitsFactory unitsFactory = null;






    // Start is called before the first frame update
    void Start()
    {
        //Set the building accoridng to the existed navmesh z position.
        Vector3 pos = FindObjectOfType<NavMeshScript>().transform.position;

        gameObject.transform.position = new Vector3
            (gameObject.transform.position.x, gameObject.transform.position.y, pos.z);

        unitsFactory = FindObjectOfType<UnitsFactory>();
    }


    // Update is called once per frame
    void Update()
    {

        if (!this.enabled)
        {
            return;
        }

        // Hide UI elements.
        if (Input.GetMouseButtonDown(0))
        {
            token.SetActive(false);

            ShowBuildingPanel(false);
        }


        SpawningProgession();
    }


    // Show building token and panel.
    private void OnMouseUp()
    {
        token.SetActive(true);

        ShowBuildingPanel(true);
    }


    // When mouse hover.
    private void OnMouseEnter()
    {
        if (!token.activeSelf)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        }
    }


    // When mouse doesnt hover.
    private void OnMouseExit()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }


    // Try to recruit if there is waiting units.
    void TryToRecruitUnit()
    {

        if (waitingUnit.Count <= 0)
        {
            return;
        }

        Unit unit = waitingUnit[0];

        if (!unit) { return; }

        if (Vector2.Distance(unit.transform.position, enterPoint.position) > 1)
        {
            return;
        }

        spawnUnitPrefab = unitsFactory.GetBuildingOutputUnit(this.Id, unit.id);

        this.RemoveUnitFromWaitingList(unit);

        if (spawnUnitPrefab)
        {
            inProgess = true;

            Destroy(unit.gameObject);
        }

    }


    //Spawn new unit.
    private void SpawnNewUnit()
    {
        Unit unit = Instantiate(spawnUnitPrefab, spawnPoint.position, Quaternion.identity) as Unit;

        unit.MoveTo(spawnPoint.position + new Vector3(10f, 0, 0));
    }


    // Intilize recruitment data time and allow recruit other unit.
    private void FreeBuildingSpace()
    {
        timeLeft = 0f;
        inProgess = false;
        timeSlider.resetSlider();
    }


    // Show the building panel info for description or options.
    private void ShowBuildingPanel(bool value)
    {
        BuildinPanelDisplay panel = FindObjectOfType<BuildinPanelDisplay>();

        if (panel)
        {
            panel.GetComponent<Image>().enabled = value;
        }

    }


    // Deal with unit spawing time.
    private void SpawningProgession()
    {
        if (!inProgess)
        {
            TryToRecruitUnit();
        }
        else
        {
            if (timeLeft < 0.1f)
            {
                timeLeft += Time.deltaTime;
            }
            else
            {
                timeSlider.IncreaseSlider(0.1f / spawnTime);
                timeLeft = 0;
            }
        }

        if (timeSlider.SliderFinished())
        {
            SpawnNewUnit();

            FreeBuildingSpace();
        }
    }


    // Enter Unit into the waiting list recruitemnt.
    public Vector3 EnterWaitingRecruitment(Unit unit)
    {

        if (!waitingUnit.Contains(unit) && unitsFactory.GetBuildingOutputUnit(this.Id, unit.id))
        {
            waitingUnit.Add(unit);

            return enterPoint.position + new Vector3(-20 * (waitingUnit.Count - 1), 0, 0);
        }

        return Vector3.zero;
    }


    // Remove the unit from waiting and move the other units whom waiting.
    public void RemoveUnitFromWaitingList(Unit unit)
    {

        if (waitingUnit.Contains(unit))
        {
            waitingUnit.Remove(unit);

            for (int i = 0; i < waitingUnit.Count; i++)
            {
                waitingUnit[i].MoveTo(enterPoint.position + new Vector3(-20 * i, 0, 0));
            }
        }
    }


    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }
}

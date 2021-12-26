using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    private Unit spawnUnitPrefab = null;
    // Type of solider building recived and create.
    [SerializeField] float spawnTime = 5f;

    [SerializeField] Transform spawnPoint = null;
    [SerializeField] Transform enterPoint = null;
    
    [SerializeField] GameObject token = null;
    [SerializeField] CostumeSlider timeSlider = null;

    private List<Unit> waitingUnit = new List<Unit>();

    private bool inProgess = false;
    private float timeLeft = 0;

    private UnitsFactory unitsFactory = null;

    public Macros.Buildings id;

    // Start is called before the first frame update
    void Start()
    {
        //Set the building accoridng to the existed navmesh z position.
        Vector3 pos = FindObjectOfType<NavMeshScript>().transform.position;

        gameObject.transform.position =  new Vector3
            (gameObject.transform.position.x, gameObject.transform.position.y,pos.z);

        unitsFactory = FindObjectOfType<UnitsFactory>();
    }


    // Update is called once per frame
    void Update()
    {
        // Hide UI elements.
        if(Input.GetMouseButtonDown(0))
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


    // Try to recruit.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!enabled) { return; }
        TryToRecruitUnit(collision);
    }


    // When unit approch insert it into the list.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enabled) { return; }

        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit.waitingToBeRecruited && unitsFactory.GetBuildingOutputUnit(this.id,unit.id))
        {
            if(!waitingUnit.Contains(unit))
            {
                waitingUnit.Add(unit);
            }

        }
    }

    // Try to recruit if building is not middle of recruitment.
    void TryToRecruitUnit(Collider2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (!unit || inProgess)
        {
            return;
        }

        spawnUnitPrefab = unitsFactory.GetBuildingOutputUnit(this.id, unit.id);

        // Compare the building unit prefab tag is equal the incoming unit tag.
        if (spawnUnitPrefab && unit.waitingToBeRecruited)
        {
            inProgess = true;

            waitingUnit.Remove(unit);

            Destroy(unit.gameObject);
        }

    }


    //Spawn new unit.
    private void SpawnNewUnit()
    {
        Unit unit = Instantiate(spawnUnitPrefab, spawnPoint.position, Quaternion.identity) as Unit;

        unit.MoveTo(spawnPoint.position + new Vector3(10f,0,0));
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
        if (!inProgess) { return; }

        if (timeLeft < 0.1f)
        {
            timeLeft += Time.deltaTime;
        }
        else
        {
            timeSlider.IncreaseSlider(0.1f / spawnTime);
            timeLeft = 0;
        }

        if (timeSlider.SliderFinished())
        {
            SpawnNewUnit();

            FreeBuildingSpace();
        }
    }


    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }

    public Transform GetBuildingEnteringPoint()
    {
        return enterPoint;
    }
}

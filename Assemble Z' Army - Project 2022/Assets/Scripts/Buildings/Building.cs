using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    // Type of solider building recived and create.
    [SerializeField] Unit unitInPrefab = null;// TODO - set array of possibles options.
    [SerializeField] Unit unitOutPrefab = null;// Create a class which create the wanted unit according.

    [SerializeField] float spawnTime = 5f;

    [SerializeField] Transform spawnPoint = null;
    
    [SerializeField] GameObject token = null;
    [SerializeField] UnitSlider timeSlider = null;

    private List<Unit> waitingUnit = new List<Unit>();

    private bool inProgess = false;
    private float timeLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Set the building accoridng to the existed navmesh z position.
        Vector3 pos = FindObjectOfType<NavMeshScript>().transform.position;
        gameObject.transform.position =  new Vector3
            (gameObject.transform.position.x, gameObject.transform.position.y,pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            token.SetActive(false);

            showBuildingPanel(false);
        }

        if (!inProgess)
        {
            return;
        }

        if (timeLeft / spawnTime < 1)
        {
            timeSlider.setFill(timeLeft / spawnTime);
            timeLeft += Time.deltaTime;
        }
        else
        {
            spawnNewUnit();

            FreeBuildingSpace();
        }
    }

    private void OnMouseUp()
    {
        token.SetActive(true);

        showBuildingPanel(true);

    }

    private void OnMouseEnter()
    {
        if (!token.activeSelf)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        tryToRecruitUnit(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit.waitingToBeRecruited && unit.tag == unitInPrefab.tag)
        {
            if(!waitingUnit.Contains(unit))
            {
                waitingUnit.Add(unit);
            }

        }
    }


    public Sprite GetBuildingSprite()
    {
        return GetComponent<Sprite>();
    }

    // Try to recruit if building is not middle of recruitment.
    void tryToRecruitUnit(Collider2D collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (!unit || inProgess)
        {
            return;
        }

        // Compare the building unit prefab tag is equal the incoming unit tag.
        if (unit.waitingToBeRecruited && unit.tag == unitInPrefab.tag)
        {
            inProgess = true;

            waitingUnit.Remove(unit);

            // Todo - destroy unit and add it to player list.
            Destroy(unit.gameObject);

            
        }
        else
        {
            Debug.Log("Unit can not be recruited here(Check the tag fit the building unit)." 
                + unit.name);
        }
    }


    //Spawn new unit.
    private void spawnNewUnit()
    {
        Unit unit = Instantiate(unitOutPrefab, spawnPoint.position, Quaternion.identity) as Unit;

        // To do - move the unit away from the building.
        //unit.MoveTo(new Vector3(spawnPoint.position.x+100f, 0,0));
        
    }


    // Intilize recruitment data time and allow recruit other unit.
    private void FreeBuildingSpace()
    {
        timeLeft = 0f;
        inProgess = false;
        timeSlider.resetSlider();
    }

    // Show the building panel info for description.
    private void showBuildingPanel(bool value)
    {
        BuildinPanelDisplay panel = FindObjectOfType<BuildinPanelDisplay>();

        if (panel)
        {
            panel.GetComponent<Image>().enabled = value;
        }

    }
}

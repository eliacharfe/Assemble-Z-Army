using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathsTraining : MonoBehaviour
{
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private GameObject pathsTrainingPanel = null;

    public void ActivatePathsCanvas()
    {
        landingPagePanel.SetActive(false);
        pathsTrainingPanel.SetActive(true);

    }

    public void DeactivatePathsCanvas()
    {
        landingPagePanel.SetActive(true);
        pathsTrainingPanel.SetActive(false);

    }


}
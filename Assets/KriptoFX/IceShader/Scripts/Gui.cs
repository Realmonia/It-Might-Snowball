using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour
{

  public GameObject plane;
  public Material icePlane, iceBall;
  public Light dirLight;
  public Material day, night;

  private bool isDay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  private void OnGUI()
  {
    if (GUI.Button(new Rect(10, 10, 150, 25), "Freeze/Unfreeze plane")) {
      var freezeBehaviour = plane.GetComponent<FreezeBehaviour>();
        freezeBehaviour.isFrozen = !freezeBehaviour.isFrozen;
    }
    if (GUI.Button(new Rect(10, 50, 150, 25), "Day/Night"))
    {
      if (!isDay) {
        iceBall.SetFloat("_LightStr", 0.1f);
        icePlane.SetFloat("_LightStr", 0.1f);
        dirLight.intensity = 0.1f;
        RenderSettings.skybox = night;
        RenderSettings.ambientLight = new Color(10/255f, 10/255f, 10/255f);
      }
      else {
        iceBall.SetFloat("_LightStr", 1f);
        icePlane.SetFloat("_LightStr", 1f);
        dirLight.intensity = 0.5f;
        RenderSettings.skybox = day;
        RenderSettings.ambientLight = new Color(50/255f, 50/255f, 50/255f);
      }
      isDay = !isDay;
    }
  }
}

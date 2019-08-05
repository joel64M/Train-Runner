
using UnityEngine;

public class ScreenResolutionScript : MonoBehaviour
{
    public static ScreenResolutionScript instance;
    [Range(30,100)]
    public int resolutionPercentage=75;

    private void Awake()
    {
        if (instance !=null && instance!=this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
           // DontDestroyOnLoad(this.gameObject);
            int height = (int)(Screen.currentResolution.height * resolutionPercentage) / 100;
            int width = (int)(Screen.currentResolution.width * resolutionPercentage) / 100;
            //print(width);
            //print(height);
            Screen.SetResolution(width, height, true);
            DontDestroyOnLoad(this.gameObject);
        }
  
    }

}

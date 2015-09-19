using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Collections;
//using GeoUtility;
//using GeoUtility.GeoSystem;
using System.Timers;




public class VelocityTracker : MonoBehaviour {
	
	bool gpsInit = false;
	LocationInfo currentGPSPosition;
	
	
	// Use this for initialization
	IEnumerator Start () {
		{
			// First, check if user has location service enabled
			if (!Input.location.isEnabledByUser)
				yield break;
			
			// Start service before querying location
			Input.location.Start(0.5f);
			
			// Wait until service initializes
			int maxWait = 20;
			while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
			{
				yield return new WaitForSeconds(1);
				maxWait--;
			}
			
			// Service didn't initialize in 20 seconds
			if (maxWait < 1)
			{
				GameObject.Find ("GPS_text").GetComponent<Text> ().text ="Timed out";
				yield break;
			}
			
			// Connection has failed
			if (Input.location.status == LocationServiceStatus.Failed)
			{
				GameObject.Find ("GPS_text").GetComponent<Text> ().text ="Unable to determine device location";
				yield break;
			}
			else
			{
				gpsInit= true;
				InvokeRepeating("RetrieveGPSData",0,1);
				
			}
			
			// Stop service if there is no need to query location updates continuously
			Input.location.Stop();
		}
	}
	
	void RetrieveGPSData() {
		
		currentGPSPosition = Input.location.lastData;
		string gpsString = "::" + currentGPSPosition.latitude + "//" + currentGPSPosition.longitude;
		GameObject.Find ("GPS_text").GetComponent<Text> ().text = gpsString;
	}
}


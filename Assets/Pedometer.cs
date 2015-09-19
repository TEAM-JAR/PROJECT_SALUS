using UnityEngine;
using System.Collections;

/**
 * Tracks the user's number of steps taken based on bounds of accelleration
 * lower bound: reached after a step
 * upper bound: reached in between a step
 * 
 * An accelleration traveling from lower bound to upper bound to lower bound is considered one step
 */
public class Pedometer : MonoBehaviour {

	int steps;
	int stepMultiplier;

	const int STEPS_ON_INCREMENT = 1;
	const string LIFETIME_MULTIPLIER_KEY = "stepMultiplier";

	/**
	 * Bounds for accelleration gained by the device
	 */
	public int upperBound;
	public int lowerBound;

	const string LIFETIME_STEPS_KEY = "lifetimeSteps";
	bool isTracking;

	float debugTime;

	private float loLim = 0.005F;
	private float hiLim = 0.3F;
	private bool stateH = false;
	private float fHigh = 8.0F;
	private float curAcc= 0F;
	private float fLow = 0.2F;
	private float avgAcc;
	
	// Use this for initialization
	void Start () {

		/**
		 * Grab past steps from PlayerPrefs, 0 otherwife if doesn't exist
		 */
		this.steps = PlayerPrefs.GetInt(LIFETIME_STEPS_KEY, 0);

		/**
		 * Grab past step multiplier from PlayerPrefs...
		 * 1 is the standard amount incremented
		 */
		this.stepMultiplier = PlayerPrefs.GetInt(LIFETIME_MULTIPLIER_KEY, 1);
		isTracking = false;
	}

	public void setStepMultipler(int multiplier) {
		stepMultiplier = multiplier;
	}

	public int getStepMultiplier() {
		return stepMultiplier;
	}

	public int getSteps() {
		return steps;
	}

	void toggleTrack() {
		isTracking = !isTracking;
	}

	/**
	 * Increases step by a set amount
	 */
	void addSteps(int stepsToAdd) {
		steps += stepsToAdd * stepMultiplier;
	}

	/**
	 * Increments step by 1
	 */
	void incrementStep() {
		addSteps(STEPS_ON_INCREMENT);
	}

	/**
	 * Method for calculating if a movement performed counts as a step
	 */
	bool isStep() {
		if (!isTracking) {
			return false;
		} else {
			return false;
		}
	}

	void stepMeasurements() {
		Debug.Log (debugTime + "/" + Input.acceleration.magnitude + "/" + Input.acceleration + "/" + Input.accelerationEventCount);
		debugTime += Time.deltaTime;

	}

	void trackSteps() {

	}

	public int stepDetector(){
		curAcc = Mathf.Lerp (curAcc, Input.acceleration.magnitude, Time.deltaTime * fHigh);
		avgAcc = Mathf.Lerp (avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
		float delta = curAcc - avgAcc;
		if (!stateH) { 
			if (delta > hiLim) {
				stateH = true;
				steps++;
			} else if (delta < loLim) {
				stateH = false;
			}
			stateH = false;
		}
		avgAcc = curAcc;
		
		return steps;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(stepDetector()) ;
	}
}

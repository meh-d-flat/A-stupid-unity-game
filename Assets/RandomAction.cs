﻿using System;
using UnityEngine;

public class RandomAction : MonoBehaviour
{
	static int minimumTime, maximumTime;

	Action action;
	int randomNumOfFrames, currentFrameNumber;
	int minimum, maximum;
	bool counting;

	public bool IsCounting {
		get { return counting; }
	}

	public static RandomAction CreateInstance(GameObject destination, int minTime, int maxTime)
	{
		RandomAction rt = destination.AddComponent<RandomAction>();
		minimumTime = minTime;
		maximumTime = maxTime;
		return rt;
	}

	void Start()
	{
		minimum = minimumTime;
		maximum = maximumTime;
	}

	void Update()
	{
		if (!MenuCanvas.GamePaused)
		{
			RAUpdate1();
			RAUpdate2();
		}
	}

	void LateUpdate()
	{
		//if (!MenuCanvas.GamePaused)
		//{
		//	RAUpdate1();
		//	RAUpdate2();
		//}
	}

	public void ActionAdd(Action a) {
		action += a;
	}
	public void ActionSet(Action a) {
		action = a;
	}
	public void ActionClear() {
		action = null;
	}

	void RAUpdate1()
	{
		if (!counting)
		{
			currentFrameNumber = 0;
			randomNumOfFrames = new System.Random().Next(minimum, maximum);
			counting = true;
		}
	}
	void RAUpdate2()
	{
		if (currentFrameNumber++ == randomNumOfFrames)
		{
			counting = false;
			action.Invoke();
		}
	}
}

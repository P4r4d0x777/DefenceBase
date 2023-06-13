using System.Collections.Generic;
using Code.ProvidersModule;
using NTC.Global.Pool;
using UnityEngine;

namespace Code.ServiceModule
{
	public static class StepsTraceService
	{
		private static List<GameObject> stepTraces;
		
		public static void Init(GameObject stepTrace)
		{
			stepTraces = new List<GameObject>();
			
			for (int i = 0; i < 20; i++)
			{
				GameObject trace = NightPool.Spawn(stepTrace, Vector3.zero, Quaternion.identity);
				
				stepTraces.Add(trace);
			}
		}

		public static GameObject GetTrace()
		{
			foreach (GameObject trace in stepTraces)
			{
				if (trace.GetComponent<StepTraceProvider>().TraceImage.enabled == false)
				{
					return trace;
				}
			}
			
			return null;
		}
	}
}

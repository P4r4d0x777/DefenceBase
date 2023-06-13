using Code.ECSModule.Events;
using Code.ServiceModule;
using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace Code.ProvidersModule
{
	public class HeroAnimatorProvider : MonoBehaviour
	{
		[SerializeField] private Transform LeftFoot;
		[SerializeField] private Transform RightFoot;

		public EcsEntity playerEntity;
		
		public void DoLeftTrace()
		{
			var trace = StepsTraceService.GetTrace();
			trace.transform.position = LeftFoot.position;
			
			trace.GetComponent<StepTraceProvider>().DoFade();
		}

		public void DoRightTrace()
		{
			var trace = StepsTraceService.GetTrace();
			trace.transform.position = RightFoot.position;
			
			trace.GetComponent<StepTraceProvider>().DoFade();
		}
		public void StopJumping()
		{
			playerEntity.Del<PlayerStopMovingEvent>();
			playerEntity.Del<PlayerStartMovingOnPlayerBaseEvent>();
			
			playerEntity.Del<PlayerJumping>();
		}
	}
}

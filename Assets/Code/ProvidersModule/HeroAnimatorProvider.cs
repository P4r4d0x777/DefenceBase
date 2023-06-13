using Code.ECSModule.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Code.ProvidersModule
{
	public class HeroAnimatorProvider : MonoBehaviour
	{
		public EcsEntity playerEntity;
		
		public void StopJumping()
		{
			playerEntity.Del<PlayerStopMovingEvent>();
			playerEntity.Del<PlayerStartMovingOnPlayerBaseEvent>();
			
			playerEntity.Del<PlayerJumping>();
		}
	}
}

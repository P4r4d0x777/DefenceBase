using System;
using DG.Tweening;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Code.ProvidersModule
{
	public class StepTraceProvider : MonoBehaviour
	{
		[SerializeField] private Image traceImage;
		private Color DefaultColor; 
		public Image TraceImage => traceImage;
		
		private void Awake()
		{
			DefaultColor = traceImage.color;
			traceImage.enabled = false;
		}
		public void DoFade()
		{
			traceImage.enabled = true;
			traceImage.DOFade(0f, 3f).onComplete = Despawning;
		}

		private void Despawning()
		{
			traceImage.color = DefaultColor;
			traceImage.enabled = false;
		}
	}
}

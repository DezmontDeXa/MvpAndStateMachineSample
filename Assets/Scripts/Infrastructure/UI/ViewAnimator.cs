using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure
{
	public class ViewAnimator : MonoBehaviour
	{
		[SerializeField] private float _animationDuration = 0.22f;
		[SerializeField] private bool _showOnAwake = false;
		[SerializeField] private ShowAnimation _showAction;
		[SerializeField] private HideAnimation _hideAction;

		private CanvasGroup _canvasGroup;
		private RectTransform _rectTransform;
		private GraphicRaycaster _graphicRaycaster;

		private void Awake()
		{
			_rectTransform ??= GetComponent<RectTransform>();
			_canvasGroup = GetComponent<CanvasGroup>();
			_graphicRaycaster = GetComponent<GraphicRaycaster>();

			if (_showOnAwake)
			{
				ShowInstant();
			}
			else
			{
				HideInstant();
			}
		}

		[Button]
		public void Show()
		{
			var from = GetTweenPosition(_showAction.From);
			var to = GetTweenPosition(_showAction.To);

			Animate(from, to, 1, true);
		}

		public void Restore()
		{
			var from = GetTweenPosition(_hideAction.To);
			var to = GetTweenPosition(_showAction.To);

			Animate(from, to, 1, true);
		}

		public void ShowInstant()
		{
			var position = GetTweenPosition(_showAction.To);

			_rectTransform.anchoredPosition = position;
			_canvasGroup.alpha = 1;
			_graphicRaycaster.enabled = true;
		}



		[Button]
		public void Hide()
		{
			var from = GetTweenPosition(_hideAction.From);
			var to = GetTweenPosition(_hideAction.To);

			Animate(from, to, 0, false);
		}

		public void Hide(PositionOnScreen fromSide, PositionOnScreen toSide)
		{
			var from = GetTweenPosition(fromSide);
			var to = GetTweenPosition(toSide);

			Animate(from, to, 0, false);
		}

		public void Back()
		{
			var from = GetTweenPosition(_hideAction.From);
			var to = GetTweenPosition(_showAction.From);
			Animate(from, to, 0, false);
		}

		public void HideInstant()
		{
			var position = GetTweenPosition(_hideAction.To);

			_rectTransform.anchoredPosition = position;
			_canvasGroup.alpha = 0;
			_graphicRaycaster.enabled = false;
		}


		private void Animate(Vector2 from, Vector2 to, float fade, bool raycasterEnabled)
		{
			_rectTransform.anchoredPosition = from;
			_rectTransform.DOAnchorPos(to, _animationDuration);
			_canvasGroup.DOFade(fade, _animationDuration);
			_graphicRaycaster.enabled = raycasterEnabled;
		}

		private Vector2 GetTweenPosition(PositionOnScreen position)
		{
			switch (position)
			{
				case PositionOnScreen.Center:
					return new Vector2(0, 0);
				case PositionOnScreen.Left:
					return new Vector2(-Screen.width, 0);
				case PositionOnScreen.Right:
					return new Vector2(Screen.width, 0);
				case PositionOnScreen.Top:
					return new Vector2(0, Screen.height);
				case PositionOnScreen.Bottom:
					return new Vector2(0, -Screen.height);
				default:
					throw new NotImplementedException();
			}
		}
	}

	[Serializable]
	public class ShowAnimation
	{
		[SerializeField] private PositionOnScreen _from = PositionOnScreen.Left;
		[SerializeField] private PositionOnScreen _to = PositionOnScreen.Center;

		public PositionOnScreen From => _from;
		public PositionOnScreen To => _to;
	}

	[Serializable]
	public class HideAnimation
	{
		[SerializeField] private PositionOnScreen _from = PositionOnScreen.Center;
		[SerializeField] private PositionOnScreen _to = PositionOnScreen.Right;
		public PositionOnScreen From => _from;
		public PositionOnScreen To => _to;
	}

	public enum PositionOnScreen
	{
		Center,
		Left,
		Right,
		Top,
		Bottom,
	}
}


namespace Base.Effects
{
	public class InitializeEffectsEffect : RoutingEffect
	{
		public static readonly BindableProperty InitProperty = BindableProperty.CreateAttached("Init", typeof(bool), typeof(InitializeEffectsEffect), default(bool), propertyChanged: OnInitChanged);

		public static bool GetInit(BindableObject bindable)
		{
			return (bool)bindable.GetValue(InitProperty);
		}

		public static void SetInit(BindableObject bindable, bool value)
		{
			bindable.SetValue(InitProperty, value);
		}

		public static void OnInitChanged(BindableObject bindable, object oldValue, object newValue)
		{
			if (!(bindable is Element element))
			{
				return;
			}

			if (!element.Effects.Any(e => e is InitializeEffectsEffect))
			{
				element.Effects.Add(new InitializeEffectsEffect());
			}
		}

		public InitializeEffectsEffect() : base($"Base.{nameof(InitializeEffectsEffect)}")
		{
		}
	}
}
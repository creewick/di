using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;

namespace FractalPainting.App.Actions
{
	public class DragonFractalAction : IUiAction//, INeed<IImageHolder>
	{
	    private readonly IDragonPainterFactory factory;

	    public DragonFractalAction(IDragonPainterFactory factory)
	    {
	        this.factory = factory;
	    }

		//public void SetDependency(IImageHolder dependency)
		//{
		//	imageHolder = dependency;
		//}

		public string Category => "Фракталы";
		public string Name => "Дракон";
		public string Description => "Дракон Хартера-Хейтуэя";

		public void Perform()
		{
			var dragonSettings = CreateRandomSettings();
			// редактируем настройки:
			SettingsForm.For(dragonSettings).ShowDialog();
			// создаём painter с такими настройками
			//var container = new StandardKernel();
            factory.Create(dragonSettings).Paint();
			//container.Bind<IImageHolder>().ToConstant(imageHolder);
			//container.Bind<DragonSettings>().ToConstant(dragonSettings);
			//container.Get<DragonPainter>().Paint();
		}

		private static DragonSettings CreateRandomSettings()
		{
			return new DragonSettingsGenerator(new Random()).Generate();
		}
	}
}
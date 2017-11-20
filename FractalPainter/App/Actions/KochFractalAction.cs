﻿using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App.Actions
{
	public class KochFractalAction : IUiAction//, INeed<IImageHolder>, INeed<Palette>
	{
	    private readonly KochPainter painter;
	    private IImageHolder imageHolder;
		private Palette palette;

	    public KochFractalAction(KochPainter painter)
	    {
	        this.painter = painter;
	        //imageHolder = holder;
	        //this.palette = palette;
	    }

		//public void SetDependency(IImageHolder dependency)
		//{
		//	imageHolder = dependency;
		//}

		//public void SetDependency(Palette dependency)
		//{
		//	palette = dependency;
		//}

		public string Category => "Фракталы";
		public string Name => "Кривая Коха";
		public string Description => "Кривая Коха";

		public void Perform()
		{
			//var container = new StandardKernel();
			//container.Bind<IImageHolder>().ToConstant(imageHolder);
			//container.Bind<Palette>().ToConstant(palette);

			//container.Get<KochPainter>().Paint();
            painter.Paint();
		}
	}
}
using System;
using System.Linq;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure;
using Ninject;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = new StandardKernel();
                container.Bind<IUiAction>().To<SaveImageAction>();
                container.Bind<IUiAction>().To<DragonFractalAction>();
                container.Bind<IUiAction>().To<KochFractalAction>();
                container.Bind<IUiAction>().To<ImageSettingsAction>();
                container.Bind<IUiAction>().To<PaletteSettingsAction>();
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>()
                    .WhenInjectedInto<SettingsManager>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>()
                    .WhenInjectedInto<SettingsManager>();
                container.Bind<AppSettings, IImageDirectoryProvider>()
                    .ToMethod(context => context.Kernel.Get<SettingsManager>().Load())
                    .InSingletonScope();

                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<KochPainter>().ToSelf();
                var form = container.Get<MainForm>();

                Application.Run(form);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
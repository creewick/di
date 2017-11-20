﻿using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App
{
	public class MainForm : Form
	{
		//public MainForm()
		//	: this(
		//		new IUiAction[]
		//		{
		//			new SaveImageAction(),
		//			new DragonFractalAction(),
		//			new KochFractalAction(),
		//			new ImageSettingsAction(),
		//			new PaletteSettingsAction()
		//		})
		//{
		//}

		public MainForm(IUiAction[] actions, PictureBoxImageHolder pictureBox, Palette palette)
		{
			var imageSettings = CreateSettingsManager().Load().ImageSettings;
			ClientSize = new Size(imageSettings.Width, imageSettings.Height);

			var mainMenu = new MenuStrip();
			mainMenu.Items.AddRange(actions.ToMenuItems());
			Controls.Add(mainMenu);

			//var pictureBox = new PictureBoxImageHolder();
			pictureBox.RecreateImage(imageSettings);
			pictureBox.Dock = DockStyle.Fill;
			Controls.Add(pictureBox);

			DependencyInjector.Inject<IImageHolder>(actions, pictureBox);
			DependencyInjector.Inject<IImageDirectoryProvider>(actions, CreateSettingsManager().Load());
			DependencyInjector.Inject<IImageSettingsProvider>(actions, CreateSettingsManager().Load());
			DependencyInjector.Inject(actions, palette);
		}

		private static SettingsManager CreateSettingsManager()
		{
			var container = new StandardKernel();
			container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
			container.Bind<IBlobStorage>().To<FileBlobStorage>();
			return container.Get<SettingsManager>();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Text = "Fractal Painter";
		}

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
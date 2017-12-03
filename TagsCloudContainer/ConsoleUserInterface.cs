﻿using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class ConsoleUserInterface
    {
        private static Dictionary<string, IColoringAlgorithm> coloringAlgorithms = new Dictionary<string, IColoringAlgorithm>
        {
            { "Random", new RandomColoring() },
            { "Sequence", new SequenceColoring() },
            { "Length", new LengthColoring() }
        };

        public static void Run()
        {
            Console.Write("Input file: ");
            var input = Console.ReadLine();
            Console.Write("File with rejected words: ");
            var rejected = Console.ReadLine();
            Console.Write("Output file: ");
            var output = Console.ReadLine();
            Console.Write("Colors: ");
            var colorsArray = Console.ReadLine().Split(',');
            var colors = new List<Color>();
            foreach (var color in colorsArray)
                colors.Add(Color.FromName(color.Trim()));
            Console.Write("Coloring algorithm: ");
            var algorithm = coloringAlgorithms[Console.ReadLine()];
            Console.Write("Fontname: ");
            var font = Console.ReadLine();
            Console.Write("Width: ");
            var width = int.Parse(Console.ReadLine());
            Console.Write("Height: ");
            var height = int.Parse(Console.ReadLine());
            Console.Write("Spiral step in degrees: ");
            var step = double.Parse(Console.ReadLine());
            Console.Write("Spiral multiplier: ");
            var factor = double.Parse(Console.ReadLine());


            var container = new WindsorContainer();
            container.Register(Component.For<TagsCloudContainer>()
                                        .DependsOn(Dependency.OnValue<string>(input)));
            container.Register(Component.For<IWordsParser>()
                                        .ImplementedBy<MyWordsParser>());
            container.Register(Component.For<IWordsFilter>()
                                        .ImplementedBy<MyWordsFilter>()
                                        .DependsOn(Dependency.OnValue<string>(rejected)));
            container.Register(Component.For<IWordsChanger>()
                                        .ImplementedBy<MyWordsChanger>());
            container.Register(Component.For<ICloudBuilder>()
                .ImplementedBy<MyCloudBuilder>()
                .DependsOn(Dependency.OnValue("colors", colors ))
                .DependsOn(Dependency.OnValue<IColoringAlgorithm>(algorithm))
                .DependsOn(Dependency.OnValue("fontNames", new[] { font }))
                .DependsOn(Dependency.OnValue<Size>(new Size(width, height)))
                .DependsOn(Dependency.OnValue("step", step))
                .DependsOn(Dependency.OnValue("factor", factor)));
            var tagsCloudBuilder = container.Resolve<TagsCloudContainer>();
            tagsCloudBuilder.SaveAsImage(output);
        }
    }
}

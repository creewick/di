using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CommandLine;

namespace TagsCloudContainer
{
    class Options
    {
        [Option('i', "input", Default = "input.txt", HelpText = "Input filename")]
        public string InputFilename { get; set; }
        
        [Option('r', "reject", Default = "reject.txt", HelpText = "Rejected words filename")]
        public string RejectedFilename { get; set; }
        
        [Option('o', "output", Default = "output.png", HelpText = "Output filename")]
        public string OutputFilename { get; set; }
        
        [Option('c', "colors", Default = new[]{"red", "green"}, HelpText = "List of used colors separated with [,]")]
        public string[] Colors { get; set; }
        
        [Option('l', "algorithm", Default = "Random", HelpText = "Choose one of the coloring algorithms")]
        public string Algorithm { get; set; }
        
        [Option('f', "font", Default = new[]{"Calibri"}, HelpText = "Fontname")]
        public string[] Fontname { get; set; }
        
        [Option('s', "size", Default = new[]{800, 800}, HelpText = "Size of resulting image. [width height]")]
        public int[] Size { get; set; }
        
        [Option('d', "degrees", Default = 5, HelpText = "Spiral step in degrees")]
        public double Step { get; set; }
        
        [Option('f', "factor", Default = 1, HelpText = "Spiral multiplier")]
        public double Factor { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(ResolveAndRun);
        }

        private static void ResolveAndRun(Options options)
        {
            var container = Resolve(options);
            var tagsCloudBuilder = container.Resolve<TagsCloudContainer>();
            var image = new Bitmap(options.Size[0], options.Size[1]);
            tagsCloudBuilder.Draw(Graphics.FromImage(image));
            image.Save(options.OutputFilename);
        }

        private static WindsorContainer Resolve(Options options)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IWordParser>()
                .ImplementedBy<WordParser>()
                .DependsOn(Dependency.OnValue<string>(options.InputFilename)));
            container.Register(Component.For<IRejectedWordsProvider>()
                .ImplementedBy<RejectedWordsProvider>()
                .DependsOn(Dependency.OnValue<string>(options.RejectedFilename)));
            container.Register(Component.For<IWordFilter>()
                .ImplementedBy<WordFilter>());
            container.Register(Component.For<IWordTransformation>()
                .ImplementedBy<WordTransformation>());
            container.Register(Component.For<IFrequencyProvider>()
                .ImplementedBy<FrequencyProvider>());
            container.Register(Component.For<TagsCloudContainer>()
                .DependsOn(Dependency.OnValue<IWordFilter[]>
                    (new[] { container.Resolve<IWordFilter>() }))
                .DependsOn(Dependency.OnValue<IWordTransformation[]>
                    (new[] { container.Resolve<IWordTransformation>() })));
            container.Register(Component.For<ICloudBuilder>()
                .ImplementedBy<CloudBuilder>()
                .DependsOn(Dependency.OnValue("colors", options.Colors.Select(Color.FromName)))
                .DependsOn(Dependency.OnValue<IColoringAlgorithm>(algorithms[options.Algorithm]))
                .DependsOn(Dependency.OnValue("fontNames", options.Fontname ))
                .DependsOn(Dependency.OnValue<Size>(new Size(options.Size[0], options.Size[1])))
                .DependsOn(Dependency.OnValue("step", options.Step))
                .DependsOn(Dependency.OnValue("factor", options.Factor)));
            return container;
        }
        
        private static readonly Dictionary<string, IColoringAlgorithm> algorithms = new Dictionary<string, IColoringAlgorithm>
        {
            { "Random", new RandomColoring() },
            { "Sequence", new SequenceColoring() },
            { "Length", new LengthColoring() }
        };
    }
}

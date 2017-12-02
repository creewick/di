using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Drawing;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<TagsCloudContainer>()
                                        .DependsOn(Dependency.OnValue<string>("123.txt")));
            container.Register(Component.For<IWordsParser>()
                                        .ImplementedBy<MyWordsParser>());
            container.Register(Component.For<IWordsFilter>()
                                        .ImplementedBy<MyWordsFilter>());
            container.Register(Component.For<IWordsChanger>()
                                        .ImplementedBy<MyWordsChanger>());
            container.Register(Component.For<ICloudBuilder>()
                            .ImplementedBy<MyCloudBuilder>());
            var tagsCloudBuilder = container.Resolve<TagsCloudContainer>();
            tagsCloudBuilder.SaveAsImage("123.png", new[] { Color.Red },
                                         new[] { "Calibri" }, new Size(1000, 1000));
        }
    }
}

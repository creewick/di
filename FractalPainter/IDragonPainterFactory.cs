using FractalPainting.App.Fractals;

namespace FractalPainting
{
    public interface IDragonPainterFactory
    {
        DragonPainter Create(DragonSettings settings);
    } 
}

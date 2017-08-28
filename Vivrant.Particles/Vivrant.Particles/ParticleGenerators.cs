using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Vivrant.Particles
{
    public class SnowGenerator : ParticleGenerator
    {
        static Color color = Colors.White;
        static List<Particle> particles =
            new List<Particle>()
            {
                    new Particle("*", color),
                    new Particle("\u2735", color), // &#10037
                    new Particle("\u273C", color), // &#10044
                    new Particle("\u2741", color), // &#10049
                    //new Particle("\u2744", color), // &#10052
                    new Particle("\u2745", color), // &#10053
                    new Particle("\u2746", color), // &#10054
                    new Particle("\u2749", color) // &#10057
            };
        static Tuple2<int, int> sizeRange = new Tuple2<int, int>(18, 64);
        static Tuple2<TimeSpan, TimeSpan> delayRange = new Tuple2<TimeSpan, TimeSpan>(TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(100));
        static Tuple2<TimeSpan, TimeSpan> animationDurationRange = new Tuple2<TimeSpan, TimeSpan>(TimeSpan.FromMilliseconds(1200), TimeSpan.FromMilliseconds(3500));
        public SnowGenerator(int maxSnowFlakes) : base(maxSnowFlakes, delayRange, sizeRange, animationDurationRange, particles, 0, 2) { }
        public SnowGenerator() : this(100) { }
    }
    public class RainGenerator : ParticleGenerator
    {
        static List<Color> colors = new List<Color>()
            {
                Colors.LightGray,
                Colors.Gray,
                Colors.DarkGray,
                Color.FromArgb(Convert.ToByte(163),Convert.ToByte(189),Convert.ToByte(156),Convert.ToByte(1)),
                Color.FromArgb(Convert.ToByte(119),Convert.ToByte(136),Convert.ToByte(153),Convert.ToByte(1))
            };
        static Tuple2<int, int> sizeRange = new Tuple2<int, int>(32, 64);
        static Tuple2<TimeSpan, TimeSpan> delayRange = new Tuple2<TimeSpan, TimeSpan>(TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(100));
        static Tuple2<TimeSpan, TimeSpan> animationDurationRange = new Tuple2<TimeSpan, TimeSpan>(TimeSpan.FromMilliseconds(650), TimeSpan.FromMilliseconds(1850));
        public RainGenerator(int maxRainDrops) : base(maxRainDrops, delayRange, sizeRange, animationDurationRange, new Particle("'", colors, 180), 0, 10) { }
        public RainGenerator() : this(500) { }
    }
}
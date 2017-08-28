using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivrant.Particles
{
    public class ParticleGenerator
    {
        private int max = 1;
        private int hDeviation = 1;
        static Random rnd = new Random();
        private readonly ObservableCollection<Particle> particles = new ObservableCollection<Particle>();
        private Tuple2<int, int> sizes = new Tuple2<int, int>(10, 35);
        Tuple2<TimeSpan, TimeSpan> animationSpeed = new Tuple2<TimeSpan, TimeSpan>(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(1200));
        Tuple2<TimeSpan, TimeSpan> intervals = new Tuple2<TimeSpan,TimeSpan>(TimeSpan.FromMilliseconds(350), TimeSpan.FromMilliseconds(550));
        public ParticleGenerator(int maxParticles, Tuple2<TimeSpan, TimeSpan> particleDelayRange, Tuple2<int,int> sizeRange, Tuple2<TimeSpan, TimeSpan> animationSpeedRange, int defaultRotation = 0, int horizontalDeviation = 0)
        {
            max = maxParticles;
            this.DefaultRotation = defaultRotation;
            sizes = sizeRange;
            animationSpeed = animationSpeedRange;
            intervals = particleDelayRange;
            hDeviation = horizontalDeviation;
        }
        public ParticleGenerator(int maxParticles, Tuple2<TimeSpan, TimeSpan> particleDelayRange, Tuple2<int, int> sizeRange, Tuple2<TimeSpan, TimeSpan> animationSpeedRange, IEnumerable<Particle> particleCollection, int defaultRotation = 0, int horizontalDeviation = 0)
            : this(maxParticles, particleDelayRange, sizeRange, animationSpeedRange, defaultRotation, horizontalDeviation)
        {
            if(particleCollection != null)
                foreach (var p in particleCollection)
                    this.particles.Add(p);
        }
        public ParticleGenerator(int maxParticles, Tuple2<TimeSpan, TimeSpan> particleDelayRange, Tuple2<int, int> sizeRange, Tuple2<TimeSpan, TimeSpan> animationSpeedRange, Particle particle, int defaultRotation = 0, int horizontalDeviation = 0)
            : this(maxParticles, particleDelayRange, sizeRange, animationSpeedRange, new List<Particle>() { particle }, defaultRotation, horizontalDeviation) { }
        public Particle GetParticle()
        {
            if (particles.Count == 0)
                return null;
            int r = rnd.Next(particles.Count);
            return particles[r];
        }
        public int DefaultRotation
        {
            get;
            internal set;
        }
        public int HorizontalDeviation
        {
            get
            {
                if (hDeviation > 0)
                    return hDeviation;
                return 1;
            }
            internal set
            {
                hDeviation = value;
            }
        }
        public int MaxParticles
        {
            get
            {
                return max;
            }
            internal set
            {
                max = value;
            }
        }
        public TimeSpan AnimationMinLength
        {
            get
            {
                if (animationSpeed.Item1 > animationSpeed.Item2)
                    return animationSpeed.Item2;
                return animationSpeed.Item1;
            }
        }
        public TimeSpan AnimationMaxLength
        {
            get
            {
                if (animationSpeed.Item1 > animationSpeed.Item2)
                    return animationSpeed.Item1;
                return animationSpeed.Item2;
            }
        }
        public int ParticleStartSize
        {
            get
            {
                return sizes.Item1;
            }
        }
        public int ParticleEndSize
        {
            get
            {
                return sizes.Item2;
            }
        }
        public TimeSpan GetDelayInterval()
        {
            int r = rnd.Next(Convert.ToInt32(Math.Round(intervals.Item1.TotalMilliseconds, 0)), Convert.ToInt32(Math.Round(intervals.Item2.TotalMilliseconds, 0)));
            var delay = TimeSpan.FromMilliseconds(r);
            return delay;
        }
    }
}

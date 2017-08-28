using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Vivrant.Particles
{
    public class Particle
    {
        static Random rnd = new Random();
        private ObservableCollection<Color> colors = new ObservableCollection<Color>();
        public Particle(string sprite, Color color, int startRotation, int endRotation = 0)
        {
            colors.Add(color);
            this.Sprite = String.Format("{0}", sprite);
            this.StartRotation = startRotation;
            this.EndRotation = endRotation;
        }
        public Particle(string sprite, IEnumerable<Color> colorList, int startRotation, int endRotation = 0)
        {
            if (colorList != null)
                foreach (var c in colorList)
                    colors.Add(c);
            this.Sprite = String.Format("{0}", sprite);
            this.StartRotation = startRotation;
            this.EndRotation = endRotation;
        }
        public Particle(string sprite, Color color, int rotation = 0) : this(sprite, color, rotation, rotation) { }
        public Particle(string sprite, IEnumerable<Color> colorList, int rotation = 0) : this(sprite, colorList, rotation, rotation) { }
        public string Sprite { get; internal set; }
        public Color GetColor()
        {
            if (colors.Count == 0)
                return Colors.Transparent;
            int r = rnd.Next(colors.Count);
            return (Color)colors[r];
        }
        public int StartRotation { get; internal set; }
        public int EndRotation { get; internal set; }
    }
}

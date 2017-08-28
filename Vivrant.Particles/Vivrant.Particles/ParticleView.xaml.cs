using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Vivrant.Particles
{
    public partial class ParticleView : UserControl
    {
        public ParticleView()
        {
            this.InitializeComponent();
        }
        public ParticleView(object content)
        {
            this.InitializeComponent();
            this.ContentPanel.Text = content.ToString();
        }
    }
}

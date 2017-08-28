using CSHTML5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Vivrant.Particles
{
    public partial class WeatherPanel : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;
        object renderLock = new object();
        bool isFrameVisibile = true;
        int currentParticles = 0;
        int delayInterval = 350;
        int focusCheckInterval = 1500;
        readonly Random rnd = new Random((int)DateTime.Now.Ticks);
        public WeatherPanel()
        {
            this.InitializeComponent();
            this.Loaded += WeatherPanel_Loaded;
        }
        private void WeatherPanel_Loaded(object sender, RoutedEventArgs e)
        {
            CheckFocus();
        }
        private void CheckFocus()
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                Interop.ExecuteJavaScript("$0(document.hasFocus());", (Action<object>)ResumeIf);
            });
        }
        public void ResumeIf(object value)
        {
            if (Convert.ToBoolean(value))
                Resume();
            else
                Pause();
        }
        public void Pause()
        {
            IsFrameActive = false;
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(focusCheckInterval);
                CheckFocus();
            });
        }
        public void Resume()
        {
            if (!IsFrameActive)
            {
                IsFrameActive = true;
                Task.Factory.StartNew(RenderParticles);
            }
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(focusCheckInterval);
                CheckFocus();
            });
        }
        private async Task RenderParticles()
        {
            if (this.ParticleGenerator != null)
            {
                if (isFrameVisibile)
                {
                    string height = String.Empty;
                    string width = String.Empty;
                    lock (renderLock)
                    {
                        FrameworkElement element = this;
                        while (element != null && element.ActualHeight <= 0)
                        {
                            if (element.Parent == null)
                                element = null;
                            else
                                element = (FrameworkElement)element.Parent;
                        }
                        if (element != null)
                        {
                            height = String.Format("{0}", element.ActualHeight);
                            width = String.Format("{0}", element.ActualWidth);
                        }
                    }
                    if (!String.IsNullOrEmpty(height) && !String.IsNullOrEmpty(width))
                        if (height.IsNumeric() && width.IsNumeric())
                        {
                            try
                            {
                                int w = Convert.ToInt32(width);
                                int h = Convert.ToInt32(height);
                                this.Dispatcher.BeginInvoke(() => Animate(w, h));
                            }
                            catch (Exception ex)
                            {
                                // format exception, NaN
                            }
                        }
                }
            }
            await Task.Delay(delayInterval);
        }
        private void Animate(int _width, int _height)
        {
            if (currentParticles < this.ParticleGenerator.MaxParticles
                && _width != Double.NaN && _height != Double.NaN)
            {
                currentParticles++;
                var typeofd = _width.GetType();
                var width = Convert.ToInt32(_width);
                var yAmount = Convert.ToInt32(_height);
                var xAmount = rnd.Next(0, width);
                var meta = this.ParticleGenerator.GetParticle();
                var part = new ParticleView(meta.Sprite)
                {
                    Width = xAmount,
                    Height = 0,
                    Foreground = new SolidColorBrush() { Color = meta.GetColor() },
                    FontSize = this.ParticleGenerator.ParticleStartSize
                };
                var duration = new Duration(
                    TimeSpan.FromMilliseconds(
                        rnd.Next(
                            (int)Convert.ToInt32(Math.Round(this.ParticleGenerator.AnimationMinLength.TotalMilliseconds)),
                            (int)Convert.ToInt32(Math.Round(this.ParticleGenerator.AnimationMaxLength.TotalMilliseconds))
                            )));
                var minEndX = xAmount - (xAmount / this.ParticleGenerator.HorizontalDeviation);
                var maxEndX = xAmount + (xAmount / this.ParticleGenerator.HorizontalDeviation);

                var animationX = GenerateAnimation(rnd.Next(minEndX, maxEndX), duration, part, new PropertyPath(ParticleView.WidthProperty));
                var animationY = GenerateAnimation(yAmount, duration, part, new PropertyPath(ParticleView.HeightProperty));
                var animationSize = GenerateAnimation(this.ParticleGenerator.ParticleEndSize, duration, part, new PropertyPath(ParticleView.FontSizeProperty));
                var animationOpacity = GenerateAnimation(0, duration, part, new PropertyPath(ParticleView.OpacityProperty));
                Storyboard story = new Storyboard();
                story.Children.Add(animationX);
                story.Children.Add(animationY);
                story.Children.Add(animationSize);
                story.Children.Add(animationOpacity);
                animationOpacity.Completed += (sender, e) =>
                {
                    currentParticles--;
                    ContentCanvas.Children.Remove(part);
                    Task.Factory.StartNew(RenderParticles);
                };
                part.Loaded += (sender, args) =>
                {
                    story.Begin();
                };
                part.SetInheritedValue(Canvas.ZIndexProperty, 9000, true);
                ContentCanvas.Children.Add(part);
                Task.Factory.StartNew(RenderParticles);
            }
        }
        private static DoubleAnimation GenerateAnimation(int x, Duration duration, ParticleView flake, PropertyPath propertyPath)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = x,
                Duration = duration
            };
            Storyboard.SetTarget(animation, flake);
            Storyboard.SetTargetProperty(animation, propertyPath);
            return animation;
        }
        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ParticleGeneratorProperty =
            DependencyProperty.Register("ParticleGenerator", typeof(ParticleGenerator), typeof(WeatherPanel), new PropertyMetadata(new SnowGenerator(), new PropertyChangedCallback(OnGeneratorChanged)));
        public ParticleGenerator ParticleGenerator
        {
            get { return (ParticleGenerator)GetValue(ParticleGeneratorProperty); }
            set { SetValue(ParticleGeneratorProperty, value); }
        }
        public bool IsFrameActive
        {
            get { return isFrameVisibile; }
            set
            {
                isFrameVisibile = value;
                OnPropertyChanged("IsFrameActive");
            }
        }
        private async static void OnGeneratorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            if ((sender as WeatherPanel) != null)
            {
                if ((sender as WeatherPanel).ParticleGenerator != null)
                {
                    (sender as WeatherPanel).delayInterval = (int)Convert.ToInt32(Math.Round((sender as WeatherPanel).ParticleGenerator.GetDelayInterval().TotalMilliseconds));
                    await (sender as WeatherPanel).RenderParticles();
                }
            }
        }

        // Create the OnPropertyChanged method to raise the event
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

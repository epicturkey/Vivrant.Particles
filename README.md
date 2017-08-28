# Vivrant.Particles
Particles and Weather for CSHTML5 (http://cshtml5.com/)

# Demo:
http://vivrant-weathertests.bitballoon.com/

# Milestones:

**8-28-2017: ALPHA 0.1**

    - Snow and Rain particle generators completed
    - Allow multiple instances of Particle Frames on same page
    - CPU\Lifecycle Pause: When browser window\tab is not focused, animations will slow, and eventually pause. This uses JS 'document.hasFocus()'
    - CPU\Lifecycle Resume: Once browser window\tab regains focus, animations will begin again. This uses JS 'document.hasFocus()'

# Limitations:
    Does not work in Simulator! I don't know why.

# Usage:

 - Add a reference to [b]Vivrant.Particles.dll[/b]
 - Create a new XAML window or Control
 - Add a XAML Namespace: 
    `xmlns:vp="clr-namespace:Vivrant.Particles;assembly=Vivrant.Particles"`
 - Add the the following to your window or Control (this can replace any existing Canvas or Grid objects)
`<Canvas HorizontalAlignment="Stretch" VerticalAlignment="Stretch">
	<vp:WeatherPanel Name="RainPanel">
		<vp:WeatherPanel.ParticleGenerator>
			<vp:SnowGenerator />
		</vp:WeatherPanel.ParticleGenerator>
	</vp:WeatherPanel>
	<StackPanel>
		<!-- Other controls to underlay the Particle Animations -->
	</StackPanel>
</Canvas>`

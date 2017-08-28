function getParticleSprite()
{
    // sprite
    var spriteparts = $0();
     // c# get sprite property array
    var sprite = spriteparts["sprite"];
    // color
    var color = spriteparts["color"];
    // speed
    var speed = spriteparts["speed"];
    // size
    var startSize = spriteparts["startSize"];
    var endSize = spriteparts["endSize"];
    // rotation
    var startRotation = spriteparts["startRotation"];
    var endRotation = spriteparts["endRotation"];
    // horizontal shift
    var HorizontalDeviation = spriteparts["HorizontalDeviation"];
    // Particle start and end coordinates
    var startY = -25;
    var startX = Math.floor(Math.random() * $($1()).width())
    var minEndX = startX - (startX / HorizontalDeviation);
    var maxEndX = startX + (startX / HorizontalDeviation);
    var endX = RandomNumber(minEndX, maxEndX);
    var endY = $($1()).height().toString();
    if (endX > $($1()).width())
        endX = $($1()).width();
    if (endX < 0)
        endX = 0;
    // return 
    return {
        "sprite": sprite,
        "startRotation": startRotation,
        "endRotation": endRotation,
        "color": color,
        "startSize": startSize,
        "endSize": endSize,
        "speed": speed,
        "endX": endX,
        "endY": endY,
        "startX": startX,
        "startY": startY
    }
}

//var startSize = (RandomNumber(1, 6) * 20) + 50;
//var endSize = RandomNumber(-75, 150) + startSize;
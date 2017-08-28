
function renderParticle() {
    // generate particle element
    var spriteParts = getParticleSprite();
    var particlePanel = $('<div class=$0 style="position:absolute;"></div>');
    particlePanel.html(spriteParts["sprite"]);
    $(containerClass).prepend(particlePanel);

    //create particle animation values
    setElementStartRotation(particlePanel, spriteParts["startRotation"])
    particlePanel.css({
        color: spriteParts["color"].toString(),
        top: spriteParts["startY"].toString() + "px",
        left: spriteParts["startX"].toString() + "px",
        "font-size": spriteParts["startSize"].toString() + "%"
    });
    particlePanel.animate({
        top: spriteParts["endY"].toString() + "px",
        left: spriteParts["endX"].toString() + "px",
        "font-size": spriteParts["endSize"].toString() + "%",
        opacity: "0"
    }, spriteParts["speed"], function () {
        $(this).remove();
        $1();//decrement particle count
        generateParticles();
    });
}
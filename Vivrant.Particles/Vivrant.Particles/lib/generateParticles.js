function generateParticles()
{
    incrParticleCount = $0();
    setTimeout(function () {
    renderParticle();
    if (incrParticleCount <= $1())
        generateParticles();
    }, $2());
}
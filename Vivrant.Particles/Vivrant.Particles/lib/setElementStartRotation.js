
    function setElementStartRotation(el, angle)
    {
        //set start values
        if(angle != 0)
        {
          while(angle < 0) angle = angle + 360;
          while(angle > 360) angle = angle - 360;
          var ieQuadRotation = angle / 4;
          var ieRotation = 0;
          if(ieQuadRotation > 315 || ieQuadRotation < 45) ieRotation = 0;
          if(ieQuadRotation >= 45 && ieQuadRotation < 135) ieRotation = 1;
          if(ieQuadRotation >= 135 && ieQuadRotation < 225) ieRotation = 2;
          if(ieQuadRotation >= 225 && ieQuadRotation < 315) ieRotation = 3;
          el.css({
          /* Safari */
          "-webkit-transform": "rotate(" + angle.toString() + "deg)",
          /* Firefox */
          "-moz-transform": "rotate(" + angle.toString() + "deg)",
          /* IE */
          "-ms-transform": "rotate(" + angle.toString() + "deg)",
          /* Opera */
          "-o-transform": "rotate(" + angle.toString() + "deg)",
          /* Internet Explorer */
          "filter": "progid:DXImageTransform.Microsoft.BasicImage(rotation=" + ieQuadRotation.toString() + ")"
          });
        }
    }
////let mousePosition = document.getElementById('MousePosition')
//let mousePosOfElement = document.getElementById('MousePosOfElement')
//let mousePosOfElement = document.getElementById('NeoItemPosOfElement')
let mousePos;
function LengthBetweenPoints(x1, y1, x2, y2) {
    return Math.sqrt(Math.pow((x1 - x2), 2) + Math.pow((y1 - y2), 2));
}

const neoItems = document.getElementsByClassName('neo');
onmousemove = function (e) { mousePos = { x: e.clientX, y: e.clientY } }
setInterval(getMousePosition, 50); // setInterval repeats every X ms
function getMousePosition() {
    var pos = mousePos;

    if (!pos) {
    }
    else {
        //if (mousePosition) mousePosition.innerText = "Mouse: " + "x: " + pos.x + ", y: " + pos.y;
        for (var i = 0; i < neoItems.length; ++i) {
            var neoItem = neoItems[i];

            var neoItemPos= {
                'x': neoItem.offsetLeft + neoItem.scrollWidth / 2,
                'y': neoItem.offsetTop + neoItem.scrollHeight / 2
            }

            var xOffSet = (mousePos.x - (neoItem.offsetLeft + neoItem.scrollWidth / 2));
            var yOffSet = (mousePos.y - (neoItem.offsetTop + neoItem.scrollHeight / 2));
            var lightLength = LengthBetweenPoints(xOffSet, yOffSet, 0, 0);
            var shadowLenght = lightLength / 9;

            var xCalculated = xOffSet / shadowLenght;
            if (xCalculated > shadowLenght) xCalculated = shadowLenght;
            else if (xCalculated < -shadowLenght) xCalculated = -shadowLenght
            var yCalculated = yOffSet / shadowLenght;
            if (yCalculated > shadowLenght) yCalculated = shadowLenght;
            else if (yCalculated < -shadowLenght) yCalculated = -shadowLenght

            //if (yOffSet < -1) yOffSet = Math.abs(yOffSet) * 2;
            //if (xOffSet < -1) xOffSet = Math.abs(xOffSet) * 2;
            var rad = Math.atan2(mousePos.y - neoItem.offsetLeft + neoItem.scrollWidth / 2, xOffSet - neoItem.offsetTop + neoItem.scrollHeight / 2);
            var grad = rad * 180 / Math.PI;

            var t = {
                a: mousePos.x - neoItemPos.x,
                b: mousePos.y - neoItemPos.y,
                c: 0
            };
            t.c = Math.sqrt(t.a * t.a + t.b * t.b);
            let cos = Math.acos(t.a / t.c) / Math.PI * 180;
            cos = cos - 90;
            //if (NeoItemPosOfElement) NeoItemPosOfElement.innerText = "grad: " + cos;
            neoItem.style = 'box-shadow:  ' + -xCalculated + 'px ' + -yCalculated + 'px 20px 1px #b1b1b1, '
                + xCalculated + 'px ' + yCalculated + 'px 17px 2px #ffffff;'
                // +'background: linear-gradient(' + cos + 'grad, #f0f0f0, #9b9b9b);' +
                //'transition: .0s linear';
            //if(mousePosOfElement)mousePosOfElement.innerText = "OfElement: " +
            //    "x: " + xOffSet + ", " +
            //    "y: " + yOffSet;
        }
        mousePos = null;
    }
}
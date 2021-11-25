function BtnClick(btn){
    
    var classes = btn.classList;

    if(!classes.contains(classes[0]+'-animated'))
        classes.add(classes[0]+'-animated')
    window.setTimeout(function(){
        classes.remove(classes[0]+'-animated');
    }, 300);
}
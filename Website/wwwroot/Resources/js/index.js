function SwitchSideBarView(){
    var checkbox = document.getElementById("sidebar_checkbox")
    let expander = document.getElementById("sidebar_expander");
    switch (checkbox.checked) {
        case true:
            checkbox.checked=false;
            if(expander.classList.contains("sidebar_expander-expanded"))
                expander.classList.remove("sidebar_expander-expanded")
            var sidebar = document.getElementById("side_bar")
            sidebar.style.transform="translateX(-50px)";
            var sidebar_titles = document.getElementsByClassName("side_bar_menu_item_title");
            for (let i = 0; i < sidebar_titles.length; i++) {
                const title = sidebar_titles[i];
                
                title.style.width=0;
                title.style.visibility='collapse'
                title.style.marginLeft="0"
            }
            break;
        case false:
            checkbox.checked=true;
            if(!expander.classList.contains("sidebar_expander-expanded"))
                expander.classList.add("sidebar_expander-expanded")
            var sidebar = document.getElementById("side_bar");
            sidebar.removeAttribute("style")
            var sidebar_titles = document.getElementsByClassName("side_bar_menu_item_title");
            window.setTimeout(function(){
                for (let i = 0; i < sidebar_titles.length; i++) {
                    let title = sidebar_titles[i];
                    title.removeAttribute("style")
                }
            },600);
            break;
    }
}
SwitchSideBarView();

const onLinkClicked =function BlockLink(){
    return;
}
function SetView(btn){
    var link = btn.parentElement.href;

    var frame = document.getElementById("iframe");
    if(frame.src!=link){
        
        frame.src=link;
    }
    
    btn.parentElement.removeAttribute("href");
    window.setTimeout(function(){
        btn.parentElement.href=link;
    }, 1);
    AnimateElement(btn);
}
function AnimateElement(btn){
    var classes = btn.classList;
    classes.add(classes[0]+'-active');
    if(!classes.contains(classes[0]+'-animated'))
        classes.add(classes[0]+'-animated')
    window.setTimeout(function(){
        classes.remove(classes[0]+'-animated');
    }, 300);
    var btns = document.getElementsByClassName(classes[0])
    for (let i = 0; i < btns.length; i++) {
        let btn_ = btns[i];
        if(btn_!=btn && btn_.id!='logo' && btn_.id!='sidebar_expander')
            btn_.className=classes[0];
    }
}
function ResizeFrame(){
    var frame = document.getElementById("iframe");
    frame.height=0;
    var body = document.body;
    html = document.documentElement;
    var height = Math.max( body.scrollHeight, body.offsetHeight,html.clientHeight, html.scrollHeight, html.offsetHeight );
    if(frame.height>height){
        while(frame.height>height){
            frame.height=frame.height-1;
        }
    }
    frame.height=height-57;
}
ResizeFrame()
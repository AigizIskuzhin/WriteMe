const modalWrapperId = 'page_modal_wrap';

/*=============== SHOW MODAL ===============*/
//const showModal = (openButton, modalContent) =>{
//    const openBtn = document.getElementById(openButton),
//    modalContainer = document.getElementById(modalContent)
    
//    if(openBtn && modalContainer){  
//        openBtn.addEventListener('click', ()=>{
//            modalContainer.classList.add('show-modal')
//        })
//    }
//}
//showModal('open-modal', 'modal-container');

const showModal = (modalContent) => {
    var modalWrap = document.getElementById(modalContent)
    var closeBtn = document.querySelectorAll('.close-modal')
    closeBtn.forEach(c => c.addEventListener('click', closeModal))
        if (modalWrap) {
        modalWrap.classList.add('show-modal');  
    }
}
//showModal('page_modal_wrap');

/*=============== CLOSE MODAL ===============*/
//const closeBtn = document.querySelectorAll('.close-modal')

function closeModal(){
    const modalWrap = document.getElementById(modalWrapperId)

    modalWrap.classList.remove('show-modal')
    setTimeout(function () {
        modalWrap.innerHTML = "";
        modalWrap.classList.remove('show-modal')
    },500);
}
//closeBtn.forEach(c => c.addEventListener('click', closeModal))


function LoadWrapperForModal(view) {
    var wrap = document.getElementById(modalWrapperId);
    wrap.innerHTML = view;
    ShowModalWrapper();
}
function ShowModalWrapper() {
    var wrap = document.getElementById(modalWrapperId);
    if (!wrap.classList.contains('show-modal'))
        wrap.classList.add('show-modal');
    LoadCloseModalEvents();
}
function HideModalWrapper() {
    var wrap = document.getElementById(modalWrapperId);
    if (wrap.classList.contains('show-modal'))
        wrap.classList.remove('show-modal');
}

function LoadCloseModalEvents() {
    var wrapper = document.getElementById(modalWrapperId)
    wrapper.querySelectorAll('.close-modal').forEach(btn => btn.addEventListener('click', closeModal));
}

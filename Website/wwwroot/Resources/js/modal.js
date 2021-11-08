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
    const modalWrap = document.getElementById('page_modal_wrap')

    modalWrap.classList.remove('show-modal')
    setTimeout(function () {
        modalWrap.innerHTML = "";
        modalWrap.classList.remove('show-modal')
    },500);
}
//closeBtn.forEach(c => c.addEventListener('click', closeModal))

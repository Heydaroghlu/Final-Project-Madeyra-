let hesab=document.getElementsByClassName("hesab-btn")
let menu=document.getElementById("dropdown-menu1")
let array=[...hesab]
array.forEach(element => {
    element.addEventListener("click",function()
    {
        menu.classList.toggle("menu-class")
    })
});

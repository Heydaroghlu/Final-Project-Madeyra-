let hesab=document.getElementsByClassName("hesab-btn")
let menu=document.getElementById("dropdown-menu1")
let array=[...hesab]
array.forEach(element => {
    element.addEventListener("click",function()
    {
        menu.classList.toggle("menu-class")
    })
});
let navbar=document.getElementById("header-bottom")
document.addEventListener("scroll",function()
{
    navbarscroll()
})
function navbarscroll()
{
    if(document.documentElement.scrollTop>600)
    {
        navbar.classList.add("navbar-class")
        navbar.classList.add("animate__animated","animate__fadeInDown")
        
    }
    else{
        navbar.classList.remove("navbar-class")
        navbar.classList.remove("animate__animated","animate__fadeInDown")
    }
}
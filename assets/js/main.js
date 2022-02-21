let hesab=document.getElementsByClassName("hesab-btn")
let menu=document.getElementById("category-menu")
let category_menu=document.getElementById("category-menu-ul")
let array=[...hesab]
array.forEach(element => {
    element.addEventListener("click",function()
    {
        menu.classList.toggle("menu-class")
    })
});
let navbar=document.getElementById("header-bottom")
let mobil_nav=document.getElementById("navbar-mobil")

document.addEventListener("scroll",function()
{
    navbarscroll()
})
function navbarscroll()
{
    if(document.documentElement.scrollTop>200)
    {
        navbar.classList.add("navbar-class")
        // $("#category-menu-ul").slideUp("fast")
        navbar.classList.add("animate__animated","animate__fadeInDown")
        mobil_nav.classList.add("navbar-class")
        mobil_nav.classList.add("animate__animated","animate__fadeInDown")
    }
    else{
        navbar.classList.remove("navbar-class")
        // $("#category-menu-ul").slideDown("fast")
        navbar.classList.remove("animate__animated","animate__fadeInDown")
        mobil_nav.classList.remove("navbar-class")
        mobil_nav.classList.remove("animate__animated","animate__fadeInDown")
    }
}


if(window.location!="file:///C:/Users/heyde/OneDrive/Desktop/Madeyra/index.html")
{
    $(document).ready(function()
    {
        $("#category-menu-ul").slideUp("fast")
        $("#category-menu").click(function()
        {
            $("#category-menu-ul").slideToggle("slow")
        })
    })  
}
else
{
    $(document).ready(function()
    {
        $("#category-menu").click(function()
        {
            $("#category-menu-ul").slideToggle("slow")
        })
    })  
}


let menu_open=document.getElementById("menu-open")
let menu_closed=document.getElementById("menu-close")
let left_menu=document.getElementById("left-menu")
let body_overlay=document.getElementById("body-overlay")
menu_open.addEventListener("click",function()
{
    left_menu.classList.remove("animate__animated","animate__fadeOutLeft")
    left_menu.classList.add("animate__animated","animate__fadeInLeft")
        left_menu.classList.add("left-menu-class")
        left_menu.style.transform="translate(0px,0px)"
        body_overlay.classList.add("body-overlay-class")
})
menu_closed.addEventListener("click",function()
{
    left_menu.classList.remove("animate__animated","animate__fadeInLeft")
    left_menu.classList.add("animate__animated","animate__fadeOutLeft")
        left_menu.classList.remove("left-menu-class")
        body_overlay.classList.remove("body-overlay-class")
})
let accordion_button=document.querySelectorAll(".accordion-button")
let down_show=document.querySelectorAll(".accordion-collapse")
accordion_button.forEach(element => {
    element.addEventListener("click",function()
    {
        element.classList.toggle("accordion-class")
    })
});
let mobil_search=document.querySelectorAll(".mobil-search")
let mobil_search_input=document.getElementById("down-search")
let search_open=document.getElementById("search_open")
let search_close=document.getElementById("search_closed")
search_open.addEventListener("click",function()
{
    search_open.classList.remove("search-class")
    search_close.classList.add("search-class")
})
search_close.addEventListener("click",function()
{
    search_close.classList.remove("search-class")
    search_open.classList.add("search-class")
})
mobil_search.forEach(x=>{
    x.addEventListener("click",function()
    {
       mobil_search_input.classList.toggle("down-search-class")
       
    })
})
let fixed_top=document.querySelector("#fixed-top")
fixed_top.addEventListener("click",function()
{
    window.scrollTo(0,0)
})
let sebet_icon=document.querySelectorAll(".sebet")
let sebet=document.getElementById("sebet-list")
let sebet_mobil=document.getElementById("sebet-list-mobil")

sebet_icon.forEach(element => {
   
    element.addEventListener("click",function()
    {
        sebet.classList.toggle("sebet-class")
    sebet_mobil.classList.toggle("sebet-class")
    })
});
let sebet_delete=document.querySelectorAll(".sebet-delete")
sebet_delete.forEach(x=>
    {
        
        x.addEventListener("click",function(e)
        {
            e.preventDefault()
            alert("sil")
        })
    })
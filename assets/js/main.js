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
document.addEventListener("scroll",function()
{
    navbarscroll()
})
function navbarscroll()
{
    if(document.documentElement.scrollTop>200)
    {
        navbar.classList.add("navbar-class")
        $("#category-menu-ul").slideUp("fast")
        navbar.classList.add("animate__animated","animate__fadeInDown")
        
    }
    else{
        navbar.classList.remove("navbar-class")
        $("#category-menu-ul").slideDown("fast")
        navbar.classList.remove("animate__animated","animate__fadeInDown")
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
else if(window.location=="file:///C:/Users/heyde/OneDrive/Desktop/Madeyra/index.html" || document.documentElement.scrollTop>300)
{
    $(document).ready(function()
    {
        $("#category-menu").click(function()
        {
            $("#category-menu-ul").slideToggle("slow")
        })
    })  
}




    console.log("sweet")


let hesab = document.getElementsByClassName("hesab-btn")
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
        // $("#category-menu-ul").slideDown("fast")
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


if (window.location !="http://localhost:10529/")
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

sebet_icon.forEach(element => {
   
    element.addEventListener("click",function(e)
    {
        sebet.classList.toggle("sebet-class")
    })
});
/*$(document).on("click", ".sebet", function () {
    alert("salam qaqa")
    $(".sebet-list").classList.toggle("sebet-class")
    $("#sebet-list-mobil").classList.toggle("sebet-class")
})*/

    
  let filter_icon=document.getElementById("filter-icon")
  let qiymet=document.querySelector(".qiymet")
  let qiymet_range=document.querySelector(".qiymet-range")
  let qiymet_icon=document.getElementById("qiymet-icon")
  if(filter_icon)
  {
    filter_icon.addEventListener("click",function()
    {
      qiymet_range.classList.toggle("filter-class")
  
    })
  }
 if(qiymet_icon)
 {
    qiymet_icon.addEventListener("click",function()
    {
      qiymet_range.classList.toggle("filter-class")
    })
 }
  //grid
  let grid2=document.getElementById("grid2")
  let grid3=document.getElementById("grid3")
  let mehsul_card=document.querySelectorAll(".favorite .col-xl-4")
if(grid2)
{
    grid2.onclick=function()
{
    grid3.style.color="black"
    grid2.style.color="red"
    mehsul_card.forEach(x=>
        {
            x.className=""
        })
        mehsul_card.forEach(x=>
            {
                x.classList.add("col-xl-6","col-lg-6","col-md-6","col-sm-12","col-12")
            })
}
}
if(grid3)
{
    grid3.onclick=function()
{
    grid2.style.color="black"
    grid3.style.color="red"
    mehsul_card.forEach(x=>
        {
            x.className=""
        })
        mehsul_card.forEach(x=>
            {
                x.classList.add("col-xl-4","col-lg-4","col-md-6","col-sm-12","col-12")
            })
}
}
console.log("salam")
let map_open=document.querySelectorAll(".map-open")

map_open.forEach(x=>
    {
        x.addEventListener("click",function(e)
        {
            x.nextElementSibling.classList.toggle("map-class2")
        })
    })

    //kredit-range in detail

    let search_input=document.getElementById("search-input")
    let search_icon=document.getElementById("search-icon")
    let search_neticeler=document.getElementById("search-neticeler")
   

    
    search_input.addEventListener("click",function()
    {
        search_neticeler.classList.add("search-neticeler-class")
    })
    search_icon.addEventListener("click",function()
    {
        search_neticeler.classList.toggle("search-neticeler-class")
    })


//add basket
$(document).on("click", ".add-basket", function (e) {
    e.preventDefault();

    var id = $(this).attr("data-id");


    fetch('http://localhost:10529/product/addtobasket/' + id)
        .then(response => response.text())
        .then(data => {
            $(".sebet").html(data)
           
        });
                

});
//remove basket
$(document).on("click", ".remove-basket", function (e) {
    e.preventDefault();

    var id = $(this).attr("data-id");


    fetch('http://localhost:10529/product/RemoveBasket/' + id)
        .then(response => response.text())
        .then(data => {
            $(".sebet").html(data)

        });
});
$(document).on("click", ".remove-basket2", function (e) {
    e.preventDefault();


    var id = $(this).attr("data-id");


    fetch('http://localhost:10529/product/RemoveBasket/' + id)
        .then(response => response.text())
        .then(data => {
            $(".sebet").html(data)
        });
    location.reload()

   
});
$(function () {
    $("#search-input").keyup(function () {
        let search1 = $(this).val();
        fetch('http://localhost:10529/home/search?search1=' + search1)
            .then(response => response.text())
            .then(data => {
                console.log(data)
                var road = document.querySelector("#search-neticeler");
                road.innerHTML = data;
            })
    })
})

$(function () {
    $(document).on("click", ".delete-btn", function (e) {
        e.preventDefault();
        let id = $(this).attr("data-id")
        let name = $(this).attr("data-name")
        Swal.fire({
            title: 'Razisiz?',
            text: "Data ni Silmək isdəyirsiniz?",
            icon: 'Tehluke',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(`/adminpanel/${name}/delete?id=${id}`)
                    .then(Response => Response.text())
                    .then(data => {

                        window.location.reload(true);
                    })
            }
        })
    })


})
$(function () {
    $(document).on("click", ".berpa-btn", function (e) {
        e.preventDefault();
        let id = $(this).attr("data-id")
        let name = $(this).attr("data-name")
        Swal.fire({
            title: 'Razisiz?',
            text: "Data ni Bərpa etmək istəyirsinizmi ?",
            icon: 'Tehluke',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Bərpa!'
        }).then((result) => {
            if (result.isConfirmed) {
                fetch(`/adminpanel/${name}/delete?id=${id}`)
                    .then(Response => Response.text())
                    .then(data => {

                        window.location.reload(true);
                    })
            }
        })
    })


})
let input = document.getElementById("update-input1")
let div = document.getElementById("img-div1")
input.onchange = function (e) {
    let files = e.target.files
    let filesarr = [...files]
    filesarr.forEach(x => {
        if (x.type.startsWith("image/")) {
            let reader = new FileReader()
            reader.onload = function () {
                div.innerHTML = ""
                let newimg = document.createElement("img")
                newimg.style.width = "150px"
                newimg.setAttribute("src", reader.result)
                div.appendChild(newimg)
            }
            reader.readAsDataURL(x)
        }
    })
}
let input2 = document.getElementById("update-input2")
let div2 = document.getElementById("img-div2")
input2.onchange = function (e) {
    let files = e.target.files
    let filesarr = [...files]
    filesarr.forEach(x => {
        if (x.type.startsWith("image/")) {
            let reader = new FileReader()
            reader.onload = function () {
                div2.innerHTML = ""
                let newimg = document.createElement("img")
                newimg.style.width = "150px"
                newimg.setAttribute("src", reader.result)
                div2.appendChild(newimg)
            }
            reader.readAsDataURL(x)
        }
    })
}
//$(document).on("click", ".AddProduct", function () {
//    $("#AddProduct").slideDown(2000);
//});


$(document).on("click", ".EditProduct", function () {
    $("#EditProduct").slideDown(2000);
});
$(document).on("click", "#displayNonePro", function () {
    $("#EditProduct").slideUp(2000);
});

$(document).on("click", ".AddBrands", function () {
    $("#AddBrands").slideDown(2000);
});
$(document).on("click", ".EditBrands", function () {
    $("#EditBrands").slideDown(2000);
});

$(document).on("click", "#displayNoneBrands", function () {
    $("#AddBrands").slideUp(2000);
});
$(document).on("click", "#displayNoneBrands", function () {
    $("#EditBrands").slideUp(2000);
});








//$('#addImage').click(function () {
//    var newDiv = $('<div class="thumb-upload"><div class="thumb-edit"><input type="file" id="thumbUpload07" class="ec-image-upload" accept=".png, .jpg, .jpeg"><label for="imageUpload"><img src="../images/icons/edit.svg" class="svg_img header_svg" alt="edit"></label></div><div class="thumb-preview ec-preview"><div class="image-thumb-preview"><img class="image-thumb-preview ec-image-preview" src="../images/uploadimage/vender-upload-preview.jpg" alt="edit"></div></div></div > ');
//    //newDiv.style.background = "#000";
//    $('.thumb-upload-set').append(newDiv);
//});

//var fileInput = document.getElementById('UploadImage');
//var imageElement = document.getElementById('ImageURL');

//fileInput.addEventListener('change', function (event) {
//    debugger
//    var file = event.target.files[0]; // Get the selected file

//    if (file) {
//        imageElement.setAttribute('src', "/images/" + file.name);


//        reader.readAsDataURL(file);
//    }
//});
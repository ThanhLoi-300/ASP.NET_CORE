
function swal_login(username, id) {
    if (username == null) {
        Swal.fire({
            icon: 'warning',
            title: 'Oops...',
            text: 'Bạn phải đăng nhập mới được mua hàng.',
        })
    } else {
        var size = document.getElementById("btn_sizes").value;
        var quantity = document.getElementById('input_quantity').value
        $.ajax({
            url: '/User/Cart/AddCart',
            type: 'POST',
            data: {
                id: id,
                size: size,
                quantity: quantity
            },
            success: function (rs) {
                //if (rs.success)
                //    location.href = "/Admin/Product/Edit_Img_Product/" + id_product;
            }
        })
    }
}
//js size
let btnSize = document.getElementsByClassName('btn_size');
let QSize = document.getElementsByClassName('QSize');
let warning_Quantity = document.getElementById('warning_Quantity');

warning_Quantity.style.display = 'none';

btnSize[0].style.background = "blue"
btnSize[0].style.color = "white"

for (let i = 0; i < btnSize.length; i++) {

    btnSize[i].addEventListener('click', () => {
        for (let i = 0; i < btnSize.length; i++) {
            btnSize[i].style.background = "none"
            btnSize[i].style.color = "black"
        }
        btnSize[i].style.background = "blue"
        btnSize[i].style.color = "white"
        document.getElementById("btn_sizes").value = btnSize[i].value
        let Quantity_Product = document.getElementById('Quantity_Product');
        Quantity_Product.innerText = QSize[i].value
        if (QSize[i].value == 0) warning_Quantity.style.display = 'block';
        else warning_Quantity.style.display = 'none';

        let Quantity = document.getElementById('input_quantity')
        if (parseInt(Quantity_Product.innerHTML) == 0) {
            Quantity.value = 0
        } else Quantity.value = 1

    })
}

//js số lượng

function down() {
    let Quantity = document.getElementById('input_quantity').value
    if (Quantity > 1)
        Quantity--
    document.getElementById('input_quantity').value = Quantity
}

function up() {
    let Quantity = document.getElementById('input_quantity').value
    let Quantity_Product = document.getElementById('Quantity_Product');
    if (parseInt(Quantity) >= parseInt(Quantity_Product.innerHTML)) {
        Swal.fire({
            icon: 'warning',
            title: 'Oops...',
            text: 'Đã đặt đến số lượng tối đa',
        })
    } else {
        Quantity++
        document.getElementById('input_quantity').value = Quantity
    }
}


function rederdata() {
    let tabs = document.getElementsByClassName('exclusive-tab');
    for (i = 0; i < tabs.length; i++) {
        tabs[i].style.color = '#c9c9c9';
    }
    tabs[0].style.color = '#0b1b20';
}
rederdata()
document.getElementById('men').style.display = 'block';
function changeBestSeller(type, element) {
    let tabs = document.getElementsByClassName('exclusive-tab');
    for (i = 0; i < tabs.length; i++) {
        tabs[i].style.color = '#c9c9c9';
    }
    element.style.color = '#0b1b20';
    document.getElementById(type).style.display = 'block';
    switch (type) {
        case 'women':
            document.getElementById('men').style.display = 'none';
            document.getElementById('kids').style.display = 'none';
            break;
        case 'men':
            document.getElementById('women').style.display = 'none';
            document.getElementById('kids').style.display = 'none';
            break;
        case 'kids':
            document.getElementById('men').style.display = 'none';
            document.getElementById('women').style.display = 'none';
            break;
        default:
            break;
    }

}

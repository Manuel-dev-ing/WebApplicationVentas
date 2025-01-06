const buttonGenerar = document.querySelector('#generarCodigoBarras');
const cardCodigoBarras = document.querySelector('#card-codigoBarras');
const cardBodyCodigoBarras = document.querySelector('.card-body-codigoBarras');

//input codigo de barras
const inputCodigoBarras = document.querySelector('#inputCodigoBarrras');
const imagenCodigoBarras = document.querySelector('#barcode');


events();
function events() {
    buttonGenerar.addEventListener('click', CodigoBarras)
}


async function generarCodigoBarras() {
    spinner();
    //spinner
    const spinnerElement = document.querySelector('#div-spinner');

    var code = inputCodigoBarras.value;
    var url = `/api/productos/GenerarCodigoBarras?code=${encodeURIComponent(code)}`;
    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (respuesta.ok) {

        spinnerElement.remove();
        var data = await respuesta.blob();
        console.log(data)
        const url = URL.createObjectURL(data);

        imagenCodigoBarras.src = url;


    }

}

function CodigoBarras() {

    const mensaje = document.createElement('p');
    mensaje.classList.add('text-center', 'text-danger')
    mensaje.textContent = 'El Codigo de Barras es Requerido';

    if (inputCodigoBarras.value.length === 0) {
        cardCodigoBarras.insertBefore(mensaje, cardBodyCodigoBarras);

        setTimeout(() => {
            mensaje.remove();
        }, 3000)            

        return;

    }

    generarCodigoBarras();
}



function spinner() {
    //const divcontenedor = document.getElementById('id-spinner');

    const divSpinner = document.createElement('div');
    divSpinner.className = 'd-flex justify-content-center mt-5 mb-5';
    divSpinner.id = 'div-spinner';

    const spinner = document.createElement('div');
    spinner.className = 'spinner-border';
    spinner.setAttribute('role', 'status');
    spinner.id = 'spinner';

    const span = document.createElement('span');
    span.className = 'visually-hidden';

    spinner.appendChild(span);

    divSpinner.appendChild(spinner);
    console.log(divSpinner)

    cardCodigoBarras.insertBefore(divSpinner, cardBodyCodigoBarras);
    //divcontenedor.appendChild(divSpinner);

}




console.log("Consultar ventas")



const inputFechaInicio = document.querySelector('#fechaInicio')
const inputFechaFin = document.querySelector('#fechaFin')
const btnBuscar = document.querySelector('#btnBuscar')
var contenedorTabla = document.querySelector('#contenedor-tabla') 

var fechaInicio;
var fechaFin;

events()

function events() {
    inputFechaInicio.addEventListener('change', fnFechaInicio)
    inputFechaFin.addEventListener('change', fnFechaFin)
    btnBuscar.addEventListener('click', buscar)

    document.addEventListener('DOMContentLoaded', () => {

        mostrarFechas()

        myfunction()
    })
}

function fnFechaInicio(e) {
    fechaInicio = e.target.value
    console.log("Fecha Inicio: ", fechaInicio)
    myfunction()
}
function fnFechaFin(e) {
    fechaFin = e.target.value
    console.log("Fecha Fin: ", fechaFin)
    myfunction()

}


function myfunction() {

    if (fechaInicio && fechaFin) {
        btnBuscar.disabled = false
        //consultar()
        //buscar()
        return;
    } 
    btnBuscar.disabled = true
}


function buscar() {
    console.log("click buscando...")
    consultar()

}


async function consultar() {
    console.log("consultar...")

    const data = {
        fechaInicio: fechaInicio,
        fechaFin: fechaFin
    }

    try {

        const url = "/api/productos/obtenerVentasFecha"
        console.log("url: ", url)
        const respuesta = await fetch(url, {
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                "Content-Type": "application/json",
            }
        });


        if (respuesta.ok) {
            mostrarTablaCompras();
            const resultado = await respuesta.json();
            console.log(resultado)

            mostrarVentas(resultado)

        } else {
            const resultado = await respuesta.json();
            console.log(resultado)
            mostrarAlertaError(resultado.mensaje)
        }


    } catch (e) {
        console.log(e)
    }

}

function mostrarVentas(resultado) {
    const tbody = document.querySelector('#tbody')

    resultado.forEach(element => {

        const row = document.createElement('tr')

        let fecha = new Date(element.fecha) 

        row.innerHTML += `
            <td>${element.id}</td>
            <td>${element.usuario}</td>
            <td>${element.cliente}</td>
            <td>${element.subTotal}</td>
            <td>${element.total}</td>
            <td>${fecha.toLocaleString()}</td>
            <td><a onclick="consultarMostrarDetalle(${element.id})" class="btn btn-sm btn-outline-primary" data-id='${element.id}' title="mostrar detalle" data-bs-toggle="modal" data-bs-target="#staticBackdrop"><i class="bi bi-card-list"></i></a></td>
        `;

        tbody.appendChild(row)
    })
}

function mostrarTablaCompras() {
    contenedorTabla.innerHTML = `
        <div class="d-flex mb-1">

            <div class="ms-auto p-2">
                <input type="search" class="form-control" placeholder="buscar" id="buscar" />
            </div>

        </div>

        <table class="table table-bordered table-hover table-responsive mb-5 table-sm">
            <thead class="cabecera-tabla">
                <tr>
                    <th></th>
                    <th>Proveedor</th>
                    <th>Almacen</th>
                    <th>Sub Total</th>
                    <th>Total</th>
                    <th>Fecha</th>
                    <th></th>
                </tr>
            </thead>
            <tbody id="tbody">
               
            </tbody>
        </table>
    `;
}


// Mostrar Detalle de Venta
async function consultarMostrarDetalle(id) {
    console.log('click consultarMostrarDetalle con id: ', id)

    const url = `/api/productos/${id}`

    console.log(url)

    const respuesta = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    if (respuesta.ok) {
        const resultado = await respuesta.json();
        mostrarDetalleVenta(resultado)
    }
    
}

function mostrarDetalleVenta(resultado) {
    const tbody = document.querySelector('#tbody-detalle-venta')
    while (tbody.firstChild) {
        tbody.removeChild(tbody.firstChild)
    }

    resultado.forEach(element => {
        const row = document.createElement('tr')


        row.innerHTML += `
            <td>${element.id}</td>
            <td>${element.producto}</td>
            <td>${element.cantidad}</td>
            <td>${element.precio}</td>
            <td>${element.total}</td>
        `;

        tbody.appendChild(row)
    })     


}


// Fin Mostrar Detalle de Venta

function mostrarFechas() {
    var hoy = new Date()

    let FechaInicio = document.querySelector('#fechaInicio')
    let FechaFin = document.querySelector('#fechaFin')

    let dias = 1
    hoy.setDate(hoy.getDate() - dias).toLocaleString();

    FechaInicio.value = hoy.toISOString().split("T")[0];
    fechaInicio = hoy.toISOString().split("T")[0];

    let dia = 1
    hoy.setDate(hoy.getDate() + dia).toLocaleString();


    FechaFin.value = hoy.toISOString().split("T")[0]
    fechaFin = hoy.toISOString().split("T")[0]

}

function mostrarAlertaError(mensaje) {
    const contenedor = document.querySelector('#alerta')

    const div = document.createElement('div')
    div.classList.add('alert', 'alert-warning', 'text-center', 'text-uppercase', 'fw-bold')
    div.role = 'alert'
    div.textContent = mensaje;


    contenedor.appendChild(div)


    setTimeout(() => {
        div.remove();
    }, 5000)
}

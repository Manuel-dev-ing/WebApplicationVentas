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

        console.log(respuesta)

        if (respuesta.ok) {
            mostrarTablaVentas();
            const resultado = await respuesta.json();
            console.log(resultado)

            mostrarVentas(resultado)

        }


    } catch (e) {
        console.log(e)
    }

}

function mostrarVentas(resultado) {
    const tbody = document.querySelector('#tbody')
    console.log(tbody)

    resultado.forEach(element => {
        console.log(typeof element.fecha)

        const row = document.createElement('tr')

        let fecha = new Date(element.fecha) 

        row.innerHTML += `
            <td>${element.id}</td>
            <td>${element.usuario}</td>
            <td>${element.cliente}</td>
            <td>${element.subTotal}</td>
            <td>${element.total}</td>
            <td>${fecha.toLocaleString()}</td>
            <td><a class="btn btn-sm btn-outline-warning" data-id='${element.id}' title="Editar"><i class="bi bi-pencil"></i></a></td>
        `;

        tbody.appendChild(row)
    })



}

function mostrarTablaVentas() {
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
                    <th>Usuario</th>
                    <th>Cliente</th>
                    <th>Sub Total</th>
                    <th>Total</th>
                    <th>Fecha</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody id="tbody">
               
            </tbody>
        </table>
    `;
}

function mostrarFechas() {
    var hoy = new Date()

    let FechaInicio = document.querySelector('#fechaInicio')
    let FechaFin = document.querySelector('#fechaFin')

    FechaInicio.value = hoy.toISOString().split("T")[0];
    fechaInicio = hoy.toISOString().split("T")[0];

    let dias = 1
    hoy.setDate(hoy.getDate() + dias).toLocaleString();


    FechaFin.value = hoy.toISOString().split("T")[0]
    fechaFin = hoy.toISOString().split("T")[0]

}
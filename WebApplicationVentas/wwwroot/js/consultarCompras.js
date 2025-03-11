(function() {
    console.log("consultar compras")

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
        console.log("consultar compra...")

        const data = {
            fechaInicio: fechaInicio,
            fechaFin: fechaFin
        }

        try {

            const url = "/api/productos/obtenerComprasFecha"
            console.log("url: ", url)
            const respuesta = await fetch(url, {
                method: "POST",
                body: JSON.stringify(data),
                headers: {
                    "Content-Type": "application/json",
                }
            });


            if (respuesta.ok) {
                mostrarTablaCompras()
                const resultado = await respuesta.json();
                console.log(resultado)

                mostrarCompras(resultado)

            } else {
                const resultado = await respuesta.json();
                console.log(resultado)
                mostrarAlertaError(resultado.mensaje)
            }


        } catch (e) {
            console.log(e)
        }

    }

    function mostrarCompras(resultado) {
        const tbody = document.querySelector('#tbody-compra')

        resultado.forEach(element => {

            const row = document.createElement('tr')


            const arow = document.createElement('tr')

            const a = document.createElement('a')
            a.classList.add('btn', 'btn-sm', 'btn-outline-primary')
            a.title = 'mostrar detalle'
            a.setAttribute('data-bs-toggle', 'modal')
            a.setAttribute('data-bs-target', '#staticBackdrop')
            a.onclick = function () {

                detalleCompra(element.id )
            }

            const icono = document.createElement('i')
            icono.classList.add('bi', 'bi-card-list')

            a.appendChild(icono)

            arow.appendChild(a)

            let fecha = new Date(element.fechaRegistro)

            row.innerHTML += `
            <td>${element.id}</td>
            <td>${element.proveedor}</td>
            <td>${element.almacen}</td>
            <td>${element.subTotal}</td>
            <td>${element.total}</td>
            <td>${fecha.toLocaleString()}</td>
            `;
            row.appendChild(arow)

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

            <table class="table table-hover table-responsive mb-5 table-sm">
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
                <tbody id="tbody-compra">
               
                </tbody>
            </table>
        `;
    }



    // Mostrar Detalle de Venta
   

    async function detalleCompra(id) {
        console.log('click consultarMostrarDetalle Compras con id: ', id)

        const url = `/api/productos/detalleCompra/${id}`

        console.log(url)

        const respuesta = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })

        if (respuesta.ok) {
            const resultado = await respuesta.json();
            console.log(resultado)
            mostrarDetalleCompra(resultado)
        }

    }

    function mostrarDetalleCompra(resultado) {
        const tbody = document.querySelector('#tbody-detalle-compra')
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







})()

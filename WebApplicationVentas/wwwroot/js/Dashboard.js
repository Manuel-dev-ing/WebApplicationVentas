(function () {
    console.log("Dashboard...")
    
    const btnHoy = document.querySelector('#hoy')
    const btnMes = document.querySelector('#mes')
    const btnAnio = document.querySelector('#anio')

    const btnRevenueHoy = document.querySelector('#revenue-hoy')
    const btnRevenueMes = document.querySelector('#revenue-mes')
    const btnRevenueAnio = document.querySelector('#revenue-anio')

    const activityNotificacion = document.querySelector('#activity-notificacion')

    eventos()
    function eventos() {
        btnHoy.addEventListener('click', obtenerVentasDia)
        btnMes.addEventListener('click', obtenerVentasMes)
        btnAnio.addEventListener('click', obtenerVentasAnio)

        btnRevenueHoy.addEventListener('click', obtenerRevenueDia)
        btnRevenueMes.addEventListener('click', obtenerRevenueMes)
        btnRevenueAnio.addEventListener('click', obtenerRevenueAnio)


        document.addEventListener('DOMContentLoaded', () => {

            obtenerVentasDia()
            obtenerRevenueDia();

            //obtener notificaciones
            obtener()

            obtenerComprasDia()

            obtenerComras()

        })

    }

    //ventas
    async function obtenerVentasDia() {

        try {

            const respuesta = await fetch('/api/productos/obtenerVentasDia', {
                method: "POST",
                body: JSON.stringify(obtenerFechaDia()),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            const resultado = await respuesta.json();
            mostrar('| Hoy', resultado, 'venta')

        } catch (e) {
            console.log(e)
        }


    }

    async function obtenerVentasMes() {

        const data = {
            fechaInicio: obtenerFechaMesInicio(),
            fechaFin: obtenerFechaMesFin()
        }

        try {

            const respuesta = await fetch('/api/productos/obtenerVentasDashboard', {
                method: "POST",
                body: JSON.stringify(data),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            if (respuesta.ok) {
                const resultado = await respuesta.json();
                mostrar('| Mes', resultado, 'venta')
            }



        } catch (e) {

            console.log(e)

        }


    }

    async function obtenerVentasAnio() {

        try {

            const respuesta = await fetch('/api/productos/obtenerVentasDashboard', {
                method: "POST",
                body: JSON.stringify(obtenerFechaAnio()),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            if (respuesta.ok) {
                const resultado = await respuesta.json();
                mostrar('| Año', resultado, 'venta')
            }

        } catch (e) {

            console.log(e)

        }

    }

    //revenue
    async function obtenerRevenueMes() {


        const data = {
            fechaInicio: obtenerFechaMesInicio(),
            fechaFin: obtenerFechaMesFin()
        }

        try {

            const respuesta = await fetch('/api/productos/obtenerGanancias', {
                method: "POST",
                body: JSON.stringify(data),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            if (respuesta.ok) {
                const resultado = await respuesta.json();

                console.log(resultado)
                mostrar('| Mes', resultado, 'revenue')
            }

        } catch (e) {
            console.log(e)
        }


    }

    async function obtenerRevenueDia() {
        try {

            const respuesta = await fetch('/api/productos/obtenerGananciasDia', {
                method: "POST",
                body: JSON.stringify(obtenerFechaDia()),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            if (respuesta.ok) {
                const resultado = await respuesta.json();
                mostrar('| Hoy', resultado, 'revenue')
            }

        } catch (e) {

            console.log(e)

        }
    }

    async function obtenerRevenueAnio() {

        try {

            const respuesta = await fetch('/api/productos/obtenerGanancias', {
                method: "POST",
                body: JSON.stringify(obtenerFechaAnio()),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            if (respuesta.ok) {
                const resultado = await respuesta.json();
                mostrar('| Año', resultado, 'revenue')
            }

        } catch (e) {

            console.log(e)

        }


    }

    function mostrar(texto, total, tipo) {

        if (tipo == 'revenue') {
            document.querySelector('#spanTipoRevenue').textContent = texto
            document.querySelector('#H6totalRevenue').textContent = total

        }

        if (tipo == 'venta') {
            document.querySelector('#spanTipo').textContent = texto
            document.querySelector('#H6total').textContent = total

        }

        if (tipo == 'compras') {
            document.querySelector('#Spancompras').textContent = total

        }

    }

    function obtenerFechaMesInicio() {
        const dia = "01"
        let mes = new Date().getMonth();
        const anio = new Date().getFullYear();


        if (mes <= 9) {
            mes += 1
            mes = "0" + mes
        } else {
            mes += 1

        }

        let fecha = anio + "-" + mes + "-" + dia

        return fecha
    }

    function obtenerFechaMesFin() {
        const dia = "01"
        let mes = new Date().getMonth();
        let anio = new Date().getFullYear();


        if (mes <= 9) {
            mes += 2

            mes = "0" + mes
        }

        if (mes === 11) {
            anio += 1
            mes = "01"
        }

        let fecha = anio + "-" + mes + "-" + dia

        return fecha
    }

    function obtenerFechaAnio() {

        const dia = "01"
        let mes = "01";
        const anio = new Date().getFullYear();
        let fecha = anio + "-" + mes + "-" + dia
        let fechaFin = (Number(anio) + 1) + "-" + mes + "-" + dia

        const objAnio = {
            fechaInicio: fecha,
            fechaFin: fechaFin
        }


        return objAnio
    }

    function obtenerFechaDia() {

        let dia = new Date().getDate();
        let mes = new Date().getMonth() + 1;
        const anio = new Date().getFullYear();

        mes = "0" + mes
        if (dia <= 9) {
            dia = "0" + 9
        }


        let fecha = anio + '-' + mes + '-' + dia 

        return fecha
    }

    //Notificaciones
    async function obtener() {
        try {
            const url = '/api/productos/obtenerProductosStock'
            const respuesta = await fetch(url, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (respuesta.ok) {
                const resultado = await respuesta.json()
                mostrarNotificaciones(resultado)
            }



        } catch (e) {
            console.log(e)
        }

    }
    function mostrarNotificaciones(resultado) {

        resultado.forEach(elemento => {

            //crear el li 
            var li = document.createElement('li')
            li.classList.add('list-group-item')

            let producto = elemento.listadoProductos[0]


            if (elemento.stockActual < producto.stockMinimo) {


                if (elemento.stockActual === 0) {

                    li.innerHTML += `
                        <a>
                            <p class="fs-6 fw-semibold">${producto.descripcion}</p>
                            <p class="fs-6">El Stock actual es de ${elemento.stockActual}</p>
                        </a>
                    `;

                    activityNotificacion.appendChild(li)

                } else {
                    li.innerHTML += `
                        <a>
                            <p class="fs-6 fw-semibold">${producto.descripcion}</p>
                            <p class="fs-6">El Stock actual del producto tiene ${elemento.stockActual}</p>
                            
                        </a>
                    `;

                    activityNotificacion.appendChild(li)
                }

            }


        })

    }

    //Compras
    async function obtenerComprasDia() {

        try {

            const respuesta = await fetch('/api/productos/obtenerComprasDia', {
                method: "POST",
                body: JSON.stringify(obtenerFechaDia()),
                headers: {
                    "Content-Type": "application/json",
                }
            })

            const resultado = await respuesta.json();
            mostrar('| Hoy', resultado, 'compras')

        } catch (e) {
            console.log(e)
        }
    }

    async function obtenerComras() {

        try {
            const respuesta = await fetch('/api/productos/obtenerCompras', {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                }
            })

            const resultado = await respuesta.json();
            console.log(resultado)
            agruparPorAnio(resultado)
            //mostrar('| Hoy', resultado, 'compras')


        } catch (e) {
            console.log()
        }

    }


    function agruparPorAnio(arrRespuesta) {

        let years = []
        let totales = []
        let Year = ""

        arrRespuesta.forEach((item, indice) => {


            const year = new Date(item.fechaRegistro).getFullYear().toString();

            if (!years.includes(year)) {
                //console.log(true)
                years.push(year)
                let pos = years.indexOf(year)
                Year = years[pos]

                totales.push(0)

            } 

            if (Year === year) {
                
                let posicion = years.indexOf(year)
            
                totales[posicion] += item.total

            } 
          

        })


        console.log("years: ", years)
        console.log("Totales: ", totales)
        new Chart(
            document.getElementById('acquisitions'),
            {
                type: 'bar',
                data: {
                    labels: years,
                    datasets: [
                        {
                            label: 'Compras por Año',
                            data: totales
                        }
                    ]
                }
            }
        );

    }




})()






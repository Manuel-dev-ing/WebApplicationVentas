(function () {
    var cantidadNotificacion = 0;
    var ulNotificacion = document.querySelector('#ul-notificacion')

    document.addEventListener('DOMContentLoaded', () => {

        obtener()

    })

    //Obtiene los productos con su stock
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
                mostrar(resultado)
            }



        } catch (e) {
            console.log(e)
        }

    }

    function mostrar(resultado) {
        //li dropdown-header
        const dropdownHeader = document.createElement('li')
        dropdownHeader.classList.add('dropdown-header')
        dropdownHeader.textContent = 'Tienes notificaciones'


        //link ver todas las notificaciones
        const a = document.createElement('a')
        a.href = '#'

        //span
        const span = document.createElement('span')
        span.classList.add('badge', 'rounded-pill', 'bg-primary', 'p-2', 'ms-2')
        span.textContent = 'Ver todos'

        dropdownHeader.appendChild(a)

        a.appendChild(span)

        ulNotificacion.appendChild(dropdownHeader)


        resultado.forEach(elemento => {

            //crear el li 
            var li = document.createElement('li')
            li.classList.add('notification-item')

            let producto = elemento.listadoProductos[0]

        
            if (elemento.stockActual < producto.stockMinimo) {

                cantidadNotificacion = cantidadNotificacion + 1;

                if (elemento.stockActual === 0) {

                    li.innerHTML += `
                        <i class="bi bi-exclamation-circle text-danger"></i>
                        <div>
                            <h4>${producto.descripcion}</h4>
                            <p>El Stock actual es de ${elemento.stockActual}</p>
                        </div>
                        <li>
                            <hr class="dropdown-divider">
                        </li>
                    `;
                    ulNotificacion.appendChild(li)

                } else {
                    li.innerHTML += `
                        <i class="bi bi-exclamation-circle text-warning"></i>
                        <div>
                            <h4>${producto.descripcion}</h4>
                            <p>El Stock actual del producto tiene ${elemento.stockActual}</p>
                            
                        </div>
                        <li>
                            <hr class="dropdown-divider">
                        </li>
                    `;
                    ulNotificacion.appendChild(li)
                }

            }


        })

        mostrarCantidadNotificacion()
    }

    function mostrarCantidadNotificacion() {
        const badgeNumero = document.querySelector('#badge-numero')

        badgeNumero.textContent = cantidadNotificacion

    }


})()








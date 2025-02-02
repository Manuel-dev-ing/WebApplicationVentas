console.log("compras js")
const tablaProductos = document.querySelector('.table tbody')
const Tabla = document.querySelector('.table')
const tbody = document.querySelector('tbody')
const lista = document.querySelector('#lista');

const card = document.querySelector('.datosProducto')

const btnPago = document.querySelector('#pago')

var productosLS = JSON.parse(localStorage.getItem('Productos')) || []


events()

function events() {
    card.addEventListener('click', seleccionarTipoBusqueda)
    btnPago.addEventListener('click', realizarCompra)
    document.addEventListener('DOMContentLoaded', () => {
        var arrayProductos = [];

        mostrarProductos()
        obtenerAlmacenes()
        obtenerProveedores()
        obtenerProductos()
        tipoBusqueda()

    })


}

// DATOS

async function obtenerAlmacenes() {
    var url = "/api/productos/obtenerAlmacenes"

    const respuest = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    var almacenes = await respuest.json();
    mostrarAlmacenes(almacenes)
}

function mostrarAlmacenes(almacenes) {

    almacenes.forEach(almacen => {
        const option = document.createElement('option');
        option.value = almacen.id
        option.innerText = almacen.descripcion
        if (option.value === '1') {
            option.selected = true;
        }

        document.querySelector('#almacenes').appendChild(option)

    })

}

async function obtenerProveedores() {
    var url = "/api/productos/obtenerProveedores"

    const respuest = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    var proveedores = await respuest.json();
    mostrarProveedores(proveedores)
}

function mostrarProveedores(proveedores) {
    proveedores.forEach(proveedor => {
        const option = document.createElement('option');
        option.value = proveedor.id
        option.innerText = proveedor.nombre
        if (option.value === '1') {
            option.selected = true;
        }

        document.querySelector('#proveedor').appendChild(option)

    })


}

async function obtenerProductos() {
    var url = "/api/productos/obtenerProductos"

    const respuest = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    arrayProductos = await respuest.json();

}


// Buscar Productos
// funcion buscar por..
function tipoBusqueda() {
    document.querySelectorAll("input[type='radio']").forEach(radio => {
        if (radio.checked && radio.id === 'radioCodigoBarras') {
            
            buscarPorCodigoBarras()
            return;
        }
        if (radio.checked && radio.id === 'radioNombreProducto') {
            buscarProductos();

        }
    })

}
function seleccionarTipoBusqueda(e) {

    if (e.target.classList.contains('radioCodigoBarras')) {
        console.log("seleccionaste buscar por codigo de barras..")
        mostrarToast();
        buscarPorCodigoBarras()

        return;
    }

    if (e.target.classList.contains('radioNombreProducto')) {
        mostrarToast();
        console.log("seleccionaste buscar por nombre producto..")

        buscarProductos();
        return;
    }

}
function buscarPorCodigoBarras() {

    document.querySelector('.search').addEventListener('keyup', (e) => {
        if (e.code === 'Enter') {
            const codigo = e.target.value

            console.log('buscar producto...')
            console.log('codigo de barras: ', e.target.value)
            let productos = arrayProductos.filter((product) => product.codigoBarras.includes(codigo))

            if (productos.length === 0) {
                console.log('El producto no existe')
                mostrarAlerta('El producto no existe')
                return;
            }

            let prod = productos[0]
            prod["cantidad"] = 1


            insertarProducto(prod)
        }

    })


}


function buscarProductos() {
    document.querySelector('#buscar_producto').value = '';
    document.querySelector('#buscar_producto').addEventListener('keyup', (event) => {
        console.log('buscar productos')

        var query = event.target.value

        if (query != "") {
            const productos = arrayProductos.filter((product) => product.descripcion.includes(query))
            console.log(productos);
            mostrarListadoProductos(productos);

        } else {
            query = "";
            mostrarListadoProductos(query);

        }


    })
}

//mostrar listado productos
function mostrarListadoProductos(productos) {
    var precio;
    var id;

    lista.innerHTML = '';

    if (productos != "") {
        var products = {}
        productos.forEach(product => {
            const li = document.createElement('li');
            li.classList.add("list-group-item", 'list-item')
            li.setAttribute("id", "lista-producto")
            li.textContent = product.descripcion
            li.onclick = function () {
                products = {
                    id: product.id,
                    descripcion: product.descripcion,
                    codigoBarras: product.codigoBarras,
                    categoria: product.categoria,
                    marca: product.marca,
                    precio: product.precio,
                    cantidad: 1
                }

                insertarProducto(products)
                const listItem = document.querySelectorAll(".list-item")
                lista.innerHTML = '';
            }
            precio = product.precio;



            lista.appendChild(li)
        })


    }
}

function insertarProducto(products) {

    //console.log(products)
    const { id, descripcion, codigoBarras, categoria, marca, precio, cantidad } = products

    const row = document.createElement('tr');

    //Filas
    const tdDescirpcion = document.createElement('td')
    tdDescirpcion.textContent = `${descripcion}`

    //Presio
    const tdPrecio = document.createElement('td')
    tdPrecio.textContent = `${precio}`

    //cantidad
    const tdCantidad = document.createElement('td')
    const inputCantidad = document.createElement('input')
    inputCantidad.type = 'number';
    inputCantidad.id = 'cantidad';
    inputCantidad.min = 1;
    inputCantidad.step = 1;
    inputCantidad.value = `${cantidad}`
    inputCantidad.onchange = function () {
        let cantidad = parseInt(inputCantidad.value)
        actualizarCantidad({ ...products, cantidad })
    }

    tdCantidad.appendChild(inputCantidad)
    //fin cantidad

    //total
    const tdTotal = document.createElement('td')
    tdTotal.textContent = `${precio}`

    //btn borrar
    const tdBorrar = document.createElement('td')

    const aBorrar = document.createElement('a')
    aBorrar.classList.add('borrarProducto')
    aBorrar.dataset.id = `${id}`
    aBorrar.title = 'Quitar producto'

    const icono = document.createElement('i')
    icono.classList.add('bi', 'bi-x-octagon')

    aBorrar.appendChild(icono)
    tdBorrar.appendChild(aBorrar)
    //fin btn borrar

    //Agrega las filas en el row
    row.appendChild(tdDescirpcion)
    row.appendChild(tdPrecio)
    row.appendChild(tdCantidad)
    row.appendChild(tdTotal)
    row.appendChild(tdBorrar)


    tablaProductos.appendChild(row)

    guardarProductosLS(products)
    mostrarTotales();

    document.querySelector('.search').value = ''
}

function guardarProductosLS(products) {
    productosLS = [...productosLS, products]

    localStorage.setItem('Productos', JSON.stringify(productosLS))
}

function mostrarProductos() {
    limpiarTBody()

    productosLS.forEach(prodt => {

        const { id, descripcion, codigoBarras, categoria, marca, precio, cantidad } = prodt

        const row = document.createElement('tr');

        //Filas
        const tdDescirpcion = document.createElement('td')
        tdDescirpcion.textContent = `${descripcion}`

        //Presio
        const tdPrecio = document.createElement('td')
        tdPrecio.textContent = `${precio}`

        //cantidad
        const tdCantidad = document.createElement('td')
        const inputCantidad = document.createElement('input')
        inputCantidad.type = 'number';
        inputCantidad.id = 'cantidad';
        inputCantidad.min = 1;
        inputCantidad.step = 1;
        inputCantidad.value = `${cantidad}`
        inputCantidad.onchange = function () {
            let cantidad = parseInt(inputCantidad.value)
            actualizarCantidad({ ...prodt, cantidad })
        }

        tdCantidad.appendChild(inputCantidad)
        //fin cantidad

        //total
        const tdTotal = document.createElement('td')
        tdTotal.textContent = calcularTotal(parseInt(inputCantidad.value), precio)

        //btn borrar
        const tdBorrar = document.createElement('td')

        const aBorrar = document.createElement('a')
        aBorrar.classList.add('borrarProducto')
        aBorrar.dataset.id = `${id}`
        aBorrar.title = 'Quitar producto'
        aBorrar.onclick = function () {
            eliminarProducto(id)
        }

        const icono = document.createElement('i')
        icono.classList.add('bi', 'bi-x-octagon')

        aBorrar.appendChild(icono)
        tdBorrar.appendChild(aBorrar)
        //fin btn borrar

        //Agrega las filas en el row
        row.appendChild(tdDescirpcion)
        row.appendChild(tdPrecio)
        row.appendChild(tdCantidad)
        row.appendChild(tdTotal)
        row.appendChild(tdBorrar)


        tablaProductos.appendChild(row)
    })
    mostrarTotales()


}

function calcularTotal(cantidad, total) {
    let Total = cantidad * total

    return Total.toFixed(2)
}

//Eliminar Productos
function eliminarProducto(id) {
    if (id) {
        console.log('Si existe el id.')
        productosLS = productosLS.filter(prod => prod.id !== id)
        sincronizarLS()
        mostrarProductos();
    }
}

function limpiarTBody() {
    while (tbody.firstChild) {
        tbody.removeChild(tbody.firstChild)
    }
}

function sincronizarLS() {
    localStorage.setItem('Productos', JSON.stringify(productosLS))
}


//Muestra Totales
function actualizarCantidad(producto) {

    //primero actualiza la cantidad para despues mostrar los totales
    if (producto.cantidad > 0) {

        if (productosLS.some(articulo => articulo.id === producto.id)) {
            const productoActualizado = productosLS.map(articulo => {
                if (articulo.id === producto.id) {
                    articulo.cantidad = producto.cantidad
                    mostrarTotales()
                }

                return articulo;
            })
            productosLS = [...productoActualizado]
            sincronizarLS()
            mostrarProductos()

        }

    }

}

function mostrarTotales() {
    const subTotal = document.querySelector('#subtotal')
    const Iva = document.querySelector('#iva')
    const Total = document.querySelector('#total')
    var total = 0;
    var subtotal = 0;

    productosLS.forEach(producto => {
        const { id, descripcion, codigoBarras, categoria, marca, precio, cantidad } = producto

        subtotal += cantidad * precio

    })

    let iva = (16 / 100) * subtotal
    total = subtotal + iva

    subTotal.textContent = `$${subtotal.toFixed(2)}`
    Iva.textContent = `$${iva.toFixed(2)}`
    Total.textContent = `$${total.toFixed(2)}`

}

// Mostrar Alertas
function mostrarAlerta(mensaje) {
    const alertaPrevia = document.querySelector('.alerta-danger')

    if (!alertaPrevia) {

        const alerta = document.createElement('div')
        alerta.classList.add('alert', 'alert-danger', 'text-center', 'alerta-danger', 'fw-semibold', 'text-uppercase');
        alerta.textContent = mensaje

        titulo.insertBefore(alerta, null)

        setTimeout(() => {
            alerta.remove()

        }, 3000)
    }
}

function mostrarToast() {

    const toastLiveExample = document.getElementById('liveToast')
    const toast = new bootstrap.Toast(toastLiveExample)

    toast.show()
    console.log('Mostrando toast...')

}

async function realizarCompra() {
    console.log('click btn realizar Venta: ')
    const idAlmacen = document.querySelector('#almacenes').value
    const proveedor = document.querySelector('#proveedor').value
    let subtotal = document.querySelector('#subtotal').textContent
    let total = document.querySelector('#total').textContent
    let products = []

    let caracter = '$'
    total = parseFloat(total.replace(caracter, ''))
    subtotal = parseFloat(subtotal.replace(caracter, ''))

    let data = {
        IdProveedor: proveedor,
        IdAlmacen: idAlmacen,
        SubTotal: subtotal,
        Total: total,
        productos: []
    };

    productosLS.forEach(x => {
        const { id, descripcion, codigoBarras, categoria, marca, precio, cantidad } = x

        let item = {
            IdProducto: id,
            DescripcionProducto: descripcion,
            Cantidad: cantidad,
            Precio: precio,
            Total: cantidad * precio

        }

        data.productos.push(item)
    })

    console.log("Objeto Productos: ", data)

    try {
        const url = "/api/productos/entradaProducto"
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
            console.log("OK")
            mostrarAlerta('Productos registrados correctamente', 'exito')
            limpiarTBody()
            limpiarTotales()
            localStorage.clear();
        }

    } catch (e) {
        console.log(e)
    }
}

function mostrarAlerta(mensaje, tipo) {

    if (tipo === 'exito') {
        Swal.fire({
            icon: "success",
            title: mensaje,
            showConfirmButton: false,
            timer: 3000
        });

        return;
    }

    if (tipo === 'error') {
        Swal.fire({
            icon: "error",
            title: mensaje,
            showConfirmButton: false,
            timer: 5000
        });

        return;
    }


}
function limpiarTotales() {
    let subtotal = document.querySelector('#subtotal')
    let iva = document.querySelector('#iva')
    let total = document.querySelector('#total')

    subtotal.textContent = '$0'
    iva.textContent = '$0'
    total.textContent = '$0'
}







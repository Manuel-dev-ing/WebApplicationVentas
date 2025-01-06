console.log("Ventas js")
const tablaProductos = document.querySelector('.table tbody')
const Tabla = document.querySelector('.table')
const tbody = document.querySelector('tbody')
const lista = document.querySelector('#lista');
const titulo = document.querySelector('.titulo-datosProductos')
const btnPago = document.querySelector('#pago')
const btnVenta = document.querySelector('#btnventa')

const card = document.querySelector('.datosProducto')

var productosLS = JSON.parse(localStorage.getItem('Productos')) || []

events()

function events() {
    card.addEventListener('click', seleccionarTipoBusqueda)
    btnPago.addEventListener('click', realizarPago)
    btnVenta.addEventListener('click', realizarVenta)

    document.addEventListener('DOMContentLoaded', (event) => {
        var arrayProductos = [];

        mostrarProductos();
        obtenerClientes();
        obtenerDocumentosVentas();
        tipoBusqueda();
        obtenerProductos();

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

// DATOS DEL CLIENTE

async function obtenerClientes() {
    var url = "/api/productos/obtenerClientes"

    const respuest = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    var clientes = await respuest.json();
    mostrarClientes(clientes)
}

function mostrarClientes(clientes) {

    clientes.forEach(cliente => {
        const option = document.createElement('option');
        option.value = cliente.id
        option.innerText = cliente.nombre + " " + cliente.apellidos
        if (option.value === '6') {
            option.selected = true;
        }

        document.querySelector('#Clientes').appendChild(option)

    })

}


async function obtenerDocumentosVentas() {
    var url = "/api/productos/obtenerDocumentosVentas"

    const respuest = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })

    var docVentas = await respuest.json();

    mostrarDocumentosVentas(docVentas)
}

function mostrarDocumentosVentas(docVentas) {

    docVentas.forEach(doc => {

        const option = document.createElement("option");
        option.value = doc.id
        option.innerText = doc.descripcion

        document.querySelector('#tipo_documento_venta').appendChild(option);
    })


}
// FIM Datos del cliente


//DATOS DEL PRODUCTO

//obtener productos

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


//mostrar productos
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
    console.log("eliminando... ", id)

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

// Venta
function realizarPago() {
    const total = document.querySelector('#total').textContent
    const inputTotal = document.querySelector('#totalPago')
    inputTotal.value = total
    //let caracter = '$'
    //total = parseInt(total.replace(caracter, ''))
    console.log(total)
    calcularCambio(inputTotal.value)
}
function calcularCambio(total) {
    const inputCambio = document.querySelector('#cambio')

    document.querySelector('#recibe').addEventListener('keyup', (e) => {
        let Cantidad = parseInt(e.target.value)
        let caracter = '$'
        let Total = parseFloat(total.replace(caracter, ''))
        let resultado = Cantidad - Total

        resultado = Math.floor(resultado)

        inputCambio.value = resultado

    })
}

async function realizarVenta() {
    console.log('click btn realizar Venta: ')
    const idDocVenta = document.querySelector('#tipo_documento_venta').value
    const idCliente = document.querySelector('#Clientes')
    const nombreCliente = idCliente.options[idCliente.selectedIndex].text
    let subtotal = document.querySelector('#subtotal').textContent
    let total = document.querySelector('#total').textContent
    let products = []

    let caracter = '$'
    total = parseFloat(total.replace(caracter, ''))
    subtotal = parseFloat(subtotal.replace(caracter, ''))

    let data = {
        idDocVenta: idDocVenta,
        NombreCliente: nombreCliente,
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
        const url = "/api/productos/venta"
        console.log("url: ", url)
        const respuesta = await fetch(url, {
            method: "POST",
            body: JSON.stringify(data),
            headers: {
                "Content-Type": "application/json",
            }
        });

        if (!respuesta.ok) {
            throw new Error(`Error HTTP: ${respuesta.status}`);
        }

        const resultado = await respuesta.json();
        console.log(resultado);

    } catch (e) {
        console.log(e)
    }
    
}




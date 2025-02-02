console.log("Stock de Productos");
(function () {
    const btnsChecks = document.querySelector('.btnChbx');
    const lista = document.querySelector('#lista')

    events()
    function events() {
        btnsChecks.addEventListener('click', seleccionarTipoBusqueda)

        document.addEventListener('DOMContentLoaded', () => {
            var arrayProductos = [];

            obtenerProductos();
            tipoBusqueda();
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
        console.log(arrayProductos)
    }

    //Buscar Productos
    function tipoBusqueda() {
        document.querySelectorAll("input[type='radio']").forEach(radio => {
            if (radio.checked && radio.id === 'radioCodigoBarras') {
                console.log("busccando por codigo de barras...")
                buscarPorCodigoBarras()
                return;
            }
            if (radio.checked && radio.id === 'radioNombreProducto') {
                console.log("busccando por nombre de producto...")
                buscarProductos();
                return;
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

        document.querySelector('#buscarProducto').addEventListener('keyup', (e) => {
            if (e.code === 'Enter') {
                const codigo = e.target.value

                console.log('codigo de barras: ', e.target.value)
                let productos = arrayProductos.filter((product) => product.codigoBarras.includes(codigo))
                if (productos.length === 0) {
                    alert('El producto no existe')
                    return;
                }
                const productoObj = productos[0]
                mostrarProducto(productoObj)
            }

        })
    }


    function buscarProductos() {
        document.querySelector('#buscarProducto').value = ''
        document.querySelector('#buscarProducto').addEventListener('keyup', (event) => {

            var query = event.target.value

            if (query != "") {
                const productos = arrayProductos.filter((product) => product.descripcion.includes(query))
                mostrarListadoProductos(productos);

            } else {
                query = "";
                mostrarListadoProductos(query);

            }
        })
    }

    function mostrarListadoProductos(productos) {
        console.log(productos)

        lista.innerHTML = '';

        if (productos != "") {
            var products = {}
            productos.forEach(product => {
                const li = document.createElement('li');
                li.classList.add("list-group-item", 'list-item')
                li.setAttribute("id", "lista-producto")
                li.textContent = product.descripcion
                li.onclick = function () {
                    document.querySelector('#buscarProducto').value = ''

                    lista.innerHTML = '';
                    products = {
                        id: product.id,
                        descripcion: product.descripcion,
                        codigoBarras: product.codigoBarras,
                        categoria: product.categoria,
                        marca: product.marca,
                        precio: product.precio
                    }

                    mostrarProducto(products)
                    
                }

                lista.appendChild(li)
            })


        }
    }

    function mostrarProducto(productoObj) {
        
        const inputCategoria = document.querySelector('#categoria')
        const inputMarca = document.querySelector('#marca')
        const inputDescripcion = document.querySelector('#descripcion')
        const inputPrecio = document.querySelector('#precio')
        const inputId = document.querySelector('#idProduct')

        inputCategoria.value = productoObj.categoria
        inputMarca.value = productoObj.marca
        inputDescripcion.value = productoObj.descripcion
        inputPrecio.value = productoObj.precio
        inputId.value = productoObj.id
    }

    function mostrarToast() {

        const toastLiveExample = document.getElementById('liveToast')
        const toast = new bootstrap.Toast(toastLiveExample)

        toast.show()
        console.log('Mostrando toast...')

    }

})()


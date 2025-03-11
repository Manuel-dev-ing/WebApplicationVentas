console.log("Desde productos");



document.querySelectorAll("#eliminar").forEach(btn => {
    btn.addEventListener('click', async (e) => {
        console.log("click eliminar: ", e.target.parentElement)

        if (e.target.parentElement.classList.contains("btnEliminar")) {
            let elementoId = e.target.parentElement.getAttribute('data-id')

            console.log('Eliminando id... ', elementoId)

            try {
                const url = `/api/productos/tieneStock/${elementoId}`
                const respuesta = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });
             
                if (respuesta.ok) {
                    const resultado = await respuesta.json()
                    console.log("resultado: ", resultado)
                    if (resultado.tipo === "error") {
                        MostrarAlertaError(resultado, elementoId)
                    } else {
                        Swal.fire({
                            icon: "success",
                            title: resultado.mensaje,
                            showConfirmButton: false,
                            timer: 3500
                        });
                        setTimeout(() => {
                            window.location.href = "/Productos/Index";

                        }, 3600)
                    }
                    
                }



            } catch (e) {
                console.log(e)
            }
       
        }
    })

})

function MostrarAlertaError(obj, id) {
    Swal.fire({
        title: obj.mensaje,
        text: "No podrás revertir esto!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Si, Eliminarlo!"
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {

                const respuesta = await fetch('/api/productos/eliminarProducto', {
                    method: 'POST',
                    body: JSON.stringify(id),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                });

                if (respuesta.ok) {

                    Swal.fire({
                        title: "Eliminado!",
                        text: "El producto a sido eliminado.",
                        icon: "success"
                    });

                    setTimeout(() => {
                        window.location.href = "/Productos/Index";

                    }, 1600)
                }

            } catch (e) {
                console.log(e)
            }

        }
    });
}



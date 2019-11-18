package tareasvivas;

import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import clases.Cliente;
import clases.Direccion;
import mysql.Mysql;

@Path("/clientes")
public class Clientes {
	
	/**
	 * Obtiene los datos del cliente seleccionado.
	 * @param id ID cliente.
	 * @return Cliente.
	 */
	@GET
	@Path("getcliente/{id}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public Cliente getCliente(@PathParam("id") int id) {
		try {
			// Obtengo y devuelvo cliente
			return new Mysql().getCliente(id);
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Obtiene la direccion del cliente seleccionado.
	 * @param id ID cliente.
	 * @return Direccion.
	 */
	@GET
	@Path("getdireccion/{id}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public Direccion getDireccion(@PathParam("id") int id) {
		try {
			// Obtengo y devuelvo direccion
			return new Mysql().getDireccionCliente(id);
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Guarda cliente nuevo.
	 * @param c Cliente.
	 * @return ID cliente nuevo.
	 */
	@POST
	@Path("nuevo")
	@Secured
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.TEXT_PLAIN)
	public Response getDireccion(Cliente c) {
		try {
			// Guarda nuevo cliente y devuelve ID
			return Response.ok(new Mysql().addCliente(c)).build();
		} catch (Exception e) {
			return null;
		}
	}
}
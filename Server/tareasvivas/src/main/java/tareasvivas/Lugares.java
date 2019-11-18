package tareasvivas;

import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import clases.Direccion;
import clases.Ciudad;
import clases.Provincia;
import clases.Pais;

import mysql.Mysql;

@Path("/lugares")
public class Lugares {
	
	/**
	 * Obtiene la direccion seleccionada.
	 * @param id ID direccion.
	 * @return Direccion.
	 */
	@GET
	@Path("getdireccion/{id}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public Direccion getDireccion(@PathParam("id") int id) {
		try {
			// Obtengo y devuelvo direccion
			return new Mysql().getDireccion(id);
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Obtiene la ciudad seleccionada.
	 * @param id ID ciudad.
	 * @return Ciudad.
	 */
	@GET
	@Path("getciudad/{id}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public Ciudad getCiudad(@PathParam("id") int id) {
		try {
			// Obtengo y devuelvo ciudad
			return new Mysql().getCiudad(id);
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Obtengo provincia seleccionada.
	 * @param id ID provincia.
	 * @return Provincia.
	 */
	@GET
	@Path("getprovincia/{id}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public Provincia getProvincia(@PathParam("id") int id) {
		try {
			// Obtengo y devuelvo provincia
			return new Mysql().getProvincia(id);
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Obtiene pais seleccionado.
	 * @param id ID pais.
	 * @return Pais.
	 */
	@GET
	@Path("getpais/{id}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public Pais getPais(@PathParam("id") int id) {
		try {
			// Obtengo y devuelvo pais
			return new Mysql().getPais(id);
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Guardo nueva direccion.
	 * @param d Direccion.
	 * @return ID direccion creada.
	 */
	@POST
	@Path("nueva")
	@Secured
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.TEXT_PLAIN)
	public Response setDireccion(Direccion d) {
		try {
			// Guardo direccion y devuelvo ID
			return Response.ok(new Mysql().addDireccion(d)).build();
		} catch (Exception e) {
			return null;
		}
	}
}
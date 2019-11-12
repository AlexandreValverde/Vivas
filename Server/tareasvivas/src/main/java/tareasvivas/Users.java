package tareasvivas;

import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import mysql.Mysql;

@Path("/users")
public class Users {
	
	/**
	 * Añade usuario.
	 * @param nombre Nombre usuario.
	 * @param email Email usuario.
	 * @param pass Password usuario.
	 * @return OK.
	 */
	@POST
	@Path("nuevousuario")
	@Produces(MediaType.TEXT_PLAIN)
	public Response nuevoUsuario(@QueryParam("nombre") String nombre, @QueryParam("email") String email, @QueryParam("pass") String pass) {
		try {
			// Añado usuario
			new Mysql().nuevoUsuario(nombre, email, pass);
			
			// Respuesta
			return Response.ok().build();
		} catch (Exception e) {
			return Response.status(Status.NOT_FOUND).build();
		}
	}
	
	/**
	 * Loguea al usuario.
	 * @param email Email usuario.
	 * @param pass Password usuario.
	 * @return ID usuario.
	 */
	@GET
	@Path("login")
	@Produces(MediaType.TEXT_PLAIN)
	public Response login(@QueryParam("email") String email, @QueryParam("pass") String pass) {
		try {
			// Obtengo ID
			return Response.ok(new Mysql().login(email, pass)).build();
		} catch (Exception e) {
			return Response.status(Status.NOT_FOUND).build();
		}
	}
}

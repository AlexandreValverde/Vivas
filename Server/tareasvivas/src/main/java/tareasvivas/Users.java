package tareasvivas;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import aes.Encryptor;
import mysql.Mysql;

@Path("/users")
public class Users {
	
	/**
	 * Añade nuevo usuario administrador.
	 * @param nombre Nombre administrador.
	 * @param email Email administrador.
	 * @param pass Password administrador.
	 * @return OK.
	 */
	@GET
	@Path("nuevoadmin")
	@Produces(MediaType.TEXT_PLAIN)
	public Response nuevoAdmin(@QueryParam("nombre") String nombre, @QueryParam("email") String email, @QueryParam("pass") String pass) {
		try {
			// Añado admin
			new Mysql().nuevoAdmin(nombre, email, Encryptor.encrypt(pass));
			
			// Respuesta
			return Response.ok().build();
		} catch (Exception e) {
			e.printStackTrace();
			return Response.status(Status.NOT_FOUND).build();
		}
	}
	
	/**
	 * Añade usuario.
	 * @param header Token con id usuario.
	 * @param nombre Nombre usuario.
	 * @param email Email usuario.
	 * @param pass Password usuario.
	 * @return OK.
	 */
	@GET
	@Path("nuevousuario")
	@Produces(MediaType.TEXT_PLAIN)
	public Response nuevoUsuario(@QueryParam("nombre") String nombre, @QueryParam("email") String email, @QueryParam("pass") String pass) {
		try {
			// Añado usuario
			new Mysql().nuevoUsuario(nombre, email, Encryptor.encrypt(pass));
			
			// Respuesta
			return Response.ok().build();
		} catch (Exception e) {
			e.printStackTrace();
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
			return Response.ok(new Mysql().login(email, Encryptor.encrypt(pass))).build();
		} catch (Exception e) {
			return Response.status(Status.NOT_FOUND).build();
		}
	}
	
	/**
	 * Obtiene el nombre del usuario seleccionado.
	 * @param id ID usuario.
	 * @return Nombre de usuairo.
	 */
	@GET
	@Path("name/{id}")
	@Secured
	@Produces(MediaType.TEXT_PLAIN)
	public Response nombre(@PathParam("id") int id) {
		try {
			// Obtengo nombre
			return Response.ok(new Mysql().getNombre(id)).build();
		} catch (Exception e) {
			return Response.status(Status.NOT_FOUND).build();
		}
	}
}

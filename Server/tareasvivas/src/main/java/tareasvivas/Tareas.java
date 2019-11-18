package tareasvivas;

import java.util.ArrayList;

import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.HttpHeaders;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import clases.Tarea;
import io.jsonwebtoken.Jwts;
import mysql.Mysql;

@Path("/tareas")
public class Tareas {
	
	/**
	 * Obtiene las tareas nuevas.
	 * @param fecha Fecha de obtencion de la ultima tarea.
	 * @return Lista de tareas nuevas.
	 */
	@GET
	@Path("get/{fecha}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public ArrayList<Tarea> getTareas(@PathParam("fecha") String fecha) {
		try {
			// Tareas
			ArrayList<Tarea> tareas = null;
			
			// Obtengo todas las tareas
			tareas = new Mysql().getTareasNuevas(fecha);
			
			// Devuelvo tareas
			return tareas;
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Completa tarea.
	 * @param header Token con id usuario.
	 * @param idTarea ID tarea.
	 * @return OK.
	 */
	@PUT
	@Path("completada/{id}")
	@Secured
	@Produces(MediaType.TEXT_PLAIN)
	public Response nombre(@Context HttpHeaders header, @PathParam("id") int idTarea) {
		try {
			// Obtiene ID usuario
            int idUsuario = getID(header.getRequestHeader(HttpHeaders.AUTHORIZATION).get(0));
            
			// Completa tarea
            new Mysql().completaTarea(idTarea, idUsuario);
            
            // Respuesta
			return Response.ok().build();
		} catch (Exception e) {
			return Response.status(Status.NOT_FOUND).build();
		}
	}
	
	/**
	 * Añade una nueva tarea.
	 * @param t Tarea.
	 * @return OK.
	 */
	@POST
	@Path("nueva")
	@Secured
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.TEXT_PLAIN)
	public Response nombre(Tarea t) {
		try {
			// Añade tarea
            new Mysql().addTarea(t);
            
            // Respuesta
			return Response.ok().build();
		} catch (Exception e) {
			return Response.status(Status.NOT_FOUND).build();
		}
	}
	
	/**
	 * Obtiene el ID del token del usuario.
	 * @param token Token usuario.
	 * @return ID usuario.
	 */
	private int getID (String token) {
		return Integer.parseInt(Jwts.parser().setSigningKey(TokenFilter.KEY).parseClaimsJws(token).getBody().getSubject());
	}
}

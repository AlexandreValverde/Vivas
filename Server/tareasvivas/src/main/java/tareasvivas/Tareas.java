package tareasvivas;

import java.util.ArrayList;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;

import clases.Tarea;
import mysql.Mysql;

@Path("/tareas")
public class Tareas {

	@GET
	@Path("get/{fecha}")
	@Secured
	@Produces(MediaType.APPLICATION_JSON)
	public ArrayList<Tarea> getTareas(@PathParam("fecha") String fecha) {
		try {
			// Tareas
			ArrayList<Tarea> tareas = null;
			
			// Compruebo fecha
			if (fecha.equals("null")) {
				// Obtengo todas las tareas
				tareas = new Mysql().getTareas();
			} else {
				// Obtengo tareas nuevas
				tareas = new Mysql().getTareasNuevas(fecha);
			}
			
			// Devuelvo tareas
			return tareas;
		} catch (Exception e) {
			return null;
		}
	}
}

package tareasvivas;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

@Path("/users")
public class Users {
	
	@GET
	@Path("prueba")
	@Produces(MediaType.TEXT_PLAIN)
	public Response pruebaArchivo() {
		return Response.ok("Hello world!").build();
	}
	
}

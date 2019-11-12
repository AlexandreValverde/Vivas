package tareasvivas;

import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import aes.Encryptor;

@Path("/aes")
public class Aes {

	@POST
	@Path("creakey")
	@Produces(MediaType.TEXT_PLAIN)
	public Response nuevoUsuario() {
		try {
			// Creo key
			Encryptor.createKey();
			
			// Respuesta
			return Response.ok().build();
		} catch (Exception e) {
			e.printStackTrace();
			return Response.status(Status.NOT_FOUND).build();
		}
	}
}

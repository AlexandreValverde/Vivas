package tareasvivas;

import java.util.Calendar;
import java.util.Date;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import aes.Encryptor;
import io.jsonwebtoken.Jwts;
import mysql.Mysql;

@Path("/token")
public class Token {

	/**
	 * Obtiene token usuario.
	 * @param id ID usuario.
	 * @param pass Password usuario.
	 * @return Token.
	 */
    @GET
    @Path("login")
    @Produces(MediaType.TEXT_PLAIN)
    public Response authenticateUser(@QueryParam("id") int id, @QueryParam("pass") String pass) {
        try {
        	// Autentifica usuario
        	if (new Mysql().token(id, Encryptor.encrypt(pass))) {
        		// Devuelvo token
        		return Response.ok(getToken(String.valueOf(id))).build();
        	}
        	
        	// Autentificacion fallida
        	return Response.status(Response.Status.NOT_FOUND).build();
        } catch (Exception e) {
            return Response.status(Response.Status.NOT_FOUND).build();
        }
    }
 
    /**
     * Crea el token del dispositivo.
     * @param data ID dispositivo encriptado.
     * @return Token.
     */
    private String getToken(String data) {
    	//Calculamos la fecha de expiracion del token
    	Date issueDate = new Date();
    	Calendar calendar = Calendar.getInstance();
    	calendar.setTime(issueDate);
    	calendar.add(Calendar.HOUR, 24);
        Date expireDate = calendar.getTime();
        
        //Creamos el token
        String jwtToken = Jwts.builder()
                .setSubject(data)
                .setIssuedAt(issueDate)
                .setExpiration(expireDate)
                .signWith(TokenFilter.KEY)
                .compact();
        return jwtToken;
    }
}

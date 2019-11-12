package tareasvivas;

import java.security.Key;

import javax.annotation.Priority;
import javax.ws.rs.Priorities;
import javax.ws.rs.container.ContainerRequestContext;
import javax.ws.rs.container.ContainerRequestFilter;
import javax.ws.rs.core.HttpHeaders;
import javax.ws.rs.core.Response;
import javax.ws.rs.ext.Provider;

import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.security.Keys;
import mysql.Mysql;

@Provider
@Secured
@Priority(Priorities.AUTHENTICATION)
public class TokenFilter implements ContainerRequestFilter {

	public static final Key KEY = Keys.secretKeyFor(SignatureAlgorithm.HS256);
	 
    @Override
    public void filter(ContainerRequestContext requestContext) {
        try {
        	// Extrae el token de la cabecera
            String token = requestContext.getHeaderString(HttpHeaders.AUTHORIZATION);
            
            // Obtiene id
            int id = Integer.parseInt(Jwts.parser().setSigningKey(KEY).parseClaimsJws(token).getBody().getSubject());
            
            // Comprueba validez
            if (!new Mysql().verifyToken(id)) {
            	requestContext.abortWith(Response.status(Response.Status.UNAUTHORIZED).build());
            }
        } catch (Exception e) {
            requestContext.abortWith(Response.status(Response.Status.UNAUTHORIZED).build());
        }
    }
}

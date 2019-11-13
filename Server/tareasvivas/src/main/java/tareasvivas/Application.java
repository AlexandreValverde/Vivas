package tareasvivas;

import javax.ws.rs.ApplicationPath;
import org.glassfish.jersey.media.multipart.MultiPartFeature;
import org.glassfish.jersey.server.ResourceConfig;

@ApplicationPath("app")
public class Application extends ResourceConfig {
    public Application() {
        // Paquete donde estan nuestros servicios REST
        packages("tareasvivas");
        // Registramos Multipart
        register(MultiPartFeature.class);
    }
}

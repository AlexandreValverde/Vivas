package clases;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Direccion {
	@XmlElement public int id_direccion;
	@XmlElement public String nombre;
	@XmlElement public String direccion;
	@XmlElement public String numero;
	@XmlElement public String codigo_postal;
	@XmlElement public String nombre_ciudad;
	@XmlElement public String nombre_provincia;
	@XmlElement public String nombre_pais;
	@XmlElement public int id_ciudad;
	@XmlElement public double lat;
	@XmlElement public double lng;
}
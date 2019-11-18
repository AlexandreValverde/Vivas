package clases;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Ciudad {
	@XmlElement public int id_ciudad;
	@XmlElement public int id_provincia;
	@XmlElement public String nombre;
}
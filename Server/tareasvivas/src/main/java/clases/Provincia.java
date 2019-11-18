package clases;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Provincia {
	@XmlElement public int id_provincia;
	@XmlElement public int id_pais;
	@XmlElement public String nombre;
}
package clases;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Pais {
	@XmlElement public int id_pais;
	@XmlElement public String nombre;
}
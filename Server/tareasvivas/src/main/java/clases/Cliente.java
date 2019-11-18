package clases;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Cliente {
	@XmlElement public int id_cliente;
	@XmlElement public String nombre;
	@XmlElement public String apellidos;
	@XmlElement public String telefono;
	@XmlElement public int id_direccion;
}
package clases;

import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class Tarea {
	@XmlElement public int id_tarea;
	@XmlElement public int id_cliente;
	@XmlElement public boolean tipo;
	@XmlElement public int id_direccion;
	@XmlElement public boolean urgente;
	@XmlElement public boolean op2;
	@XmlElement public String notas;
	@XmlElement public String fecha_creacion;
	@XmlElement public int id_usuario;
	@XmlElement public String fecha_realizada;
}

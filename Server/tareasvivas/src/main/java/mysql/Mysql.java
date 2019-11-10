package mysql;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

public class Mysql {

	// Variables
	private Connection con;
	private PreparedStatement ps;
	private ResultSet data;
	
	/**
	 * Constructor. Obtengo y abro conexion.
	 * @throws SQLException Error al establecer la conexion con la base de datos.
	 */
	public Mysql() throws SQLException {
		con = MysqlPool.getConnection();
	}
	
	public void nuevoUsuario (String nombre, String email, String pass) throws SQLException {
		// Creo consulta
		ps = con.prepareStatement("call stptd(?, ?)");
		ps.setString(1, nombre);
		ps.setString(2, email);
		ps.setString(3, pass);
		
		// Ejecuto consulta
		ps.executeUpdate();
		
		// Cierro consulta
		ps.close();
	}
}

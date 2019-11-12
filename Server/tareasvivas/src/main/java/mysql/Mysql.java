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
	
	/**
	 * Crea un nuevo usuario.
	 * @param nombre Nombre del usuario.
	 * @param email Email.
	 * @param pass Password.
	 * @throws SQLException Error con la base de datos.
	 */
	public void nuevoUsuario (String nombre, String email, String pass) throws SQLException {
		// Creo consulta
		ps = con.prepareStatement("call nuevo_usuario(?, ?, ?)");
		ps.setString(1, nombre);
		ps.setString(2, email);
		ps.setString(3, pass);
		
		// Ejecuto consulta
		ps.executeUpdate();
		
		// Cierro consulta
		ps.close();
	}

	/**
	 * Loguea el usuario.
	 * @param email Email usuario.
	 * @param pass Password usuario.
	 * @return ID usuario.
	 * @throws SQLException Error con la base de datos.
	 */
	public int login (String email, String pass) throws SQLException {
		// Repuesta
		int r = -1;
		
		// Creo consulta
		ps = con.prepareStatement("call login(?, ?)");
		ps.setString(1, email);
		ps.setString(2, pass);
		
		// Ejecuto consulta
		data = ps.executeQuery();
		
		// Leo datos
		if (data.next()) {
			r = data.getInt(1);
		}
		
		// Cierro consulta
		ps.close();
		
		// Devuelvo respuesta
		return r;
	}
	
	/**
	 * Comprueba si el id corresponde con el password.
	 * @param id ID usuario.
	 * @param pass Password usuario.
	 * @return True correcto, false erroneo.
	 * @throws SQLException Error con la base de datos.
	 */
	public boolean token (int id, String pass) throws SQLException {
		// Repuesta
		boolean r = false;
		
		// Creo consulta
		ps = con.prepareStatement("call token(?, ?)");
		ps.setInt(1, id);
		ps.setString(2, pass);
		
		// Ejecuto consulta
		data = ps.executeQuery();
		
		// Leo datos
		if (data.next()) {
			r = data.getBoolean(1);
		}
		
		// Cierro consulta
		ps.close();
		
		// Devuelvo respuesta
		return r;
	}
	
	/**
	 * Verifica si el token es correcto.
	 * @param id ID del usuario.
	 * @return True token valido, false no lo es.
	 * @throws SQLException Error con la base de datos.
	 */
	public boolean verifyToken (int id) throws SQLException {
		// Repuesta
		boolean r = false;
		
		// Creo consulta
		ps = con.prepareStatement("call verify_token(?)");
		ps.setInt(1, id);
		
		// Ejecuto consulta
		data = ps.executeQuery();
		
		// Leo datos
		if (data.next()) {
			r = data.getBoolean(1);
		}
		
		// Cierro consulta
		ps.close();
		
		// Devuelvo respuesta
		return r;
	}
}

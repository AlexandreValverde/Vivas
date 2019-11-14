package mysql;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import clases.Tarea;

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
	 * Añade usuario administrador.
	 * @param nombre Nombre administrador.
	 * @param email Email administrador.
	 * @param pass Password administrador.
	 * @throws SQLException Error con la base de datos.
	 */
	public void nuevoAdmin(String nombre, String email, String pass) throws SQLException {
		// Creo consulta
		ps = con.prepareStatement("call nuevo_admin(?, ?, ?)");
		ps.setString(1, nombre);
		ps.setString(2, email);
		ps.setString(3, pass);
		
		// Ejecuto consulta
		ps.executeUpdate();
		
		// Cierro consulta
		ps.close();
	}
	
	/**
	 * Crea un nuevo usuario.
	 * @param id ID del usuario.
	 * @param nombre Nombre del usuario.
	 * @param email Email.
	 * @param pass Password.
	 * @throws SQLException Error con la base de datos.
	 */
	public void nuevoUsuario (int id, String nombre, String email, String pass) throws SQLException {
		// Creo consulta
		ps = con.prepareStatement("call nuevo_usuario(?, ?, ?, ?)");
		ps.setInt(1, id);
		ps.setString(2, nombre);
		ps.setString(3, email);
		ps.setString(4, pass);
		
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
	
	/**
	 * Obtiene el nombre de usuario.
	 * @param id ID usuario.
	 * @return Nombre usuario.
	 * @throws SQLException Error con la base de datos.
	 */
	public String getNombre (int id) throws SQLException {
		// Repuesta
		String r = null;
		
		// Creo consulta
		ps = con.prepareStatement("call nombre_usuario(?)");
		ps.setInt(1, id);
		
		// Ejecuto consulta
		data = ps.executeQuery();
		
		// Leo datos
		if (data.next()) {
			r = data.getString(1);
		}
		
		// Cierro consulta
		ps.close();
		
		// Devuelvo respuesta
		return r;
	}
	
	/**
	 * Obtiene todas las tareas.
	 * @return Lista de tareas.
	 * @throws SQLException Error con la base de datos.
	 */
	public ArrayList<Tarea> getTareas () throws SQLException {
		// Tareas
		ArrayList<Tarea> tareas = new ArrayList<Tarea>();
		
		// Creo consulta
		ps = con.prepareStatement("call get_tareas()");
		
		// Ejecuto consulta
		data = ps.executeQuery();
		
		// Leo tareas
		while (data.next()) {
			Tarea t = new Tarea();
			t.id_tarea = data.getInt(1);
			t.id_cliente = data.getInt(2);
			t.tipo = data.getByte(3) == 1;
			t.id_direccion = data.getInt(4);
			t.urgente = data.getByte(5) == 1;
			t.op2 = data.getByte(6) == 1;
			t.notas = data.getString(7);
			t.fecha_creacion = data.getString(8);
			t.id_usuario = data.getInt(9);
			t.fecha_realizada = data.getString(10);
			tareas.add(t);
		}
		
		// Cierro consulta
		ps.close();
		
		// Devuelvo tareas
		return tareas;
	}
	
	/**
	 * Obtiene las tareas nuevas.
	 * @param date Fecha ultima tarea obtenida.
	 * @return Lista de tareas.
	 * @throws SQLException Erro con la base de datos.
	 */
	public ArrayList<Tarea> getTareasNuevas (String date) throws SQLException {
		// Tareas
		ArrayList<Tarea> tareas = new ArrayList<Tarea>();
		
		// Creo consulta
		ps = con.prepareStatement("call get_tareas_nuevas(?)");
		ps.setString(1, date);
		
		// Ejecuto consulta
		data = ps.executeQuery();
		
		// Leo tareas
		while (data.next()) {
			Tarea t = new Tarea();
			t.id_tarea = data.getInt(1);
			t.id_cliente = data.getInt(2);
			t.tipo = data.getByte(3) == 1;
			t.id_direccion = data.getInt(4);
			t.urgente = data.getByte(5) == 1;
			t.op2 = data.getByte(6) == 1;
			t.notas = data.getString(7);
			t.fecha_creacion = data.getString(8);
			t.id_usuario = data.getInt(9);
			t.fecha_realizada = data.getString(10);
			tareas.add(t);
		}
		
		// Cierro consulta
		ps.close();
		
		// Devuelvo tareas
		return tareas;
	}
}

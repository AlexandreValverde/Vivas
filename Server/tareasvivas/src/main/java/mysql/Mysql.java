package mysql;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import clases.*;

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
		try {
			// Creo consulta
			ps = con.prepareStatement("call nuevo_admin(?, ?, ?)");
			ps.setString(1, nombre);
			ps.setString(2, email);
			ps.setString(3, pass);
			
			// Ejecuto consulta
			ps.executeUpdate();
			
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
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
		try {
			// Creo consulta
			ps = con.prepareStatement("call nuevo_usuario(?, ?, ?, ?)");
			ps.setInt(1, id);
			ps.setString(2, nombre);
			ps.setString(3, email);
			ps.setString(4, pass);
			
			// Ejecuto consulta
			ps.executeUpdate();
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
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
		
		try {
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
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
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
		
		try {
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
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
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
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call verify_token(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				r = data.getBoolean(1);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
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
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call nombre_usuario(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				r = data.getString(1);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo respuesta
		return r;
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
		
		try {
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
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo tareas
		return tareas;
	}
	
	/**
	 * Obtiene la direccion seleccionada.
	 * @param id ID direccion.
	 * @return Direccion.
	 * @throws SQLException Error con la base de datos.
	 */
	public Direccion getDireccion (int id) throws SQLException {
		// Direccion
		Direccion d = null;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call get_direccion(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				d = new Direccion();
				d.id_direccion = data.getInt(1);
				d.nombre = data.getString(2);
				d.direccion = data.getString(3);
				d.numero = data.getString(4);
				d.codigo_postal = data.getString(5);
				d.id_ciudad = data.getInt(6);
				d.lat = data.getDouble(7);
				d.lng = data.getDouble(8);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo direccion
		return d;
	}
	
	/**
	 * Obtiene la direccion seleccionada.
	 * @param id ID ciudad.
	 * @return Ciudad.
	 * @throws SQLException Error con la base de datos.
	 */
	public Ciudad getCiudad (int id) throws SQLException {
		// Ciudad
		Ciudad c = null;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call get_ciudad(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				c = new Ciudad();
				c.id_ciudad = data.getInt(1);
				c.id_provincia = data.getInt(2);
				c.nombre = data.getString(3);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo ciudad
		return c;
	}
	
	/**
	 * Obtiene la provincia seleccionada.
	 * @param id ID provincia.
	 * @return Provincia.
	 * @throws SQLException Error con la base de datos.
	 */
	public Provincia getProvincia (int id) throws SQLException {
		// Provincia
		Provincia p = null;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call get_provincia(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				p = new Provincia();
				p.id_provincia = data.getInt(1);
				p.id_pais = data.getInt(2);
				p.nombre = data.getString(3);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo provincia
		return p;
	}
	
	/**
	 * Obtiene el pais seleccionado.
	 * @param id ID pais.
	 * @return Pais.
	 * @throws SQLException Error con la base de datos.
	 */
	public Pais getPais (int id) throws SQLException {
		// Pais
		Pais p = null;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call get_pais(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				p = new Pais();
				p.id_pais = data.getInt(1);
				p.nombre = data.getString(2);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo pais
		return p;
	}
	
	/**
	 * Obtiene el cliente seleccionado.
	 * @param id ID cliente.
	 * @return Cliente.
	 * @throws SQLException Error con la base de datos.
	 */
	public Cliente getCliente (int id) throws SQLException {
		// Cliente
		Cliente c = null;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call get_cliente(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				c = new Cliente();
				c.id_cliente = data.getInt(1);
				c.nombre = data.getString(2);
				c.apellidos = data.getString(3);
				c.telefono = data.getString(4);
				c.id_direccion = data.getInt(5);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo cliente
		return c;
	}
	
	/**
	 * Obtiene la direccion del cliente seleccionado.
	 * @param id ID cliente.
	 * @return Direccion.
	 * @throws SQLException Error con la base de datos.
	 */
	public Direccion getDireccionCliente (int id) throws SQLException {
		// Direccion
		Direccion d = null;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call get_direccion_cliente(?)");
			ps.setInt(1, id);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				d = new Direccion();
				d.id_direccion = data.getInt(1);
				d.nombre = data.getString(2);
				d.direccion = data.getString(3);
				d.numero = data.getString(4);
				d.codigo_postal = data.getString(5);
				d.id_ciudad = data.getInt(6);
				d.lat = data.getDouble(7);
				d.lng = data.getDouble(8);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo direccion
		return d;
	}
	
	/**
	 * Completa la tarea.
	 * @param idTarea ID tarea.
	 * @param idUsuario ID usuario.
	 * @throws SQLException Error con la base de datos.
	 */
	public void completaTarea (int idTarea, int idUsuario) throws SQLException {
		try {
			// Creo consulta
			ps = con.prepareStatement("call completa_tarea(?, ?)");
			ps.setInt(1, idTarea);
			ps.setInt(2, idUsuario);
			
			// Ejecuto consulta
			ps.executeUpdate();
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
	}
	
	/**
	 * Añade una nueva direccion.
	 * @param d Direccion.
	 * @return ID nueva direccion.
	 * @throws SQLException Error con la base de datos.
	 */
	public int addDireccion (Direccion d) throws SQLException {
		// Respuesta
		int id = 0;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call nueva_direccion(?, ?, ?, ?, ?, ?, ?, ?)");
			ps.setString(1, d.nombre_pais.toUpperCase());
			ps.setString(2, d.nombre_provincia.toUpperCase());
			ps.setString(3, d.nombre_ciudad.toUpperCase());
			ps.setString(4, d.nombre.toUpperCase());
			ps.setString(5, d.direccion.toUpperCase());
			ps.setString(6, d.numero.toUpperCase());
			ps.setDouble(7, d.lat);
			ps.setDouble(8, d.lng);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				id = data.getInt(1);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo ID nueva direccion
		return id;
	}
	
	/**
	 * Añade un nuevo cliente. 
	 * @param c Cliente.
	 * @return ID cliente añadido.
	 * @throws SQLException Error con la base de datos.
	 */
	public int addCliente (Cliente c) throws SQLException {
		// Respuesta
		int id = 0;
		
		try {
			// Creo consulta
			ps = con.prepareStatement("call nuevo_cliente(?, ?, ?, ?)");
			ps.setString(1, c.nombre.toUpperCase());
			ps.setString(2, c.apellidos.toUpperCase());
			ps.setString(3, c.telefono);
			ps.setInt(4, c.id_direccion);
			
			// Ejecuto consulta
			data = ps.executeQuery();
			
			// Leo datos
			if (data.next()) {
				id = data.getInt(1);
			}
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
		
		// Devuelvo ID nuevo cliente
		return id;
	}
	
	/**
	 * Añade una nueva tarea.
	 * @param t Tarea.
	 * @throws SQLException Erro con la base de datos.
	 */
	public void addTarea (Tarea t) throws SQLException {
		try {
			// Creo consulta
			ps = con.prepareStatement("call nueva_tarea(?, ?, ?, ?, ?, ?)");
			ps.setInt(1, t.id_cliente);
			ps.setBoolean(2, t.tipo);
			ps.setInt(3, t.id_direccion);
			ps.setBoolean(4, t.urgente);
			ps.setBoolean(5, t.op2);
			ps.setString(6, t.notas);
			
			// Ejecuto consulta
			ps.executeUpdate();
		} catch (Exception e) {}
		
		// Cierro consulta
		ps.close();
		
		// Cierro conexion
		con.close();
	}
}
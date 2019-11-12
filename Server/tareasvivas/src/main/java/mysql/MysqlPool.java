package mysql;

import java.sql.Connection;
import java.sql.SQLException;

import org.apache.commons.dbcp2.BasicDataSource;

public class MysqlPool {

private static BasicDataSource ds = new BasicDataSource();
	
	static {
        ds.setDriverClassName("com.mysql.cj.jdbc.Driver");
        ds.setUsername("uservivas");
        ds.setPassword("123456");
        ds.setUrl("jdbc:mysql://127.0.0.1:3306/tareas_vivas");
        ds.setMinIdle(50);
        ds.setMaxIdle(100);
        ds.setMaxOpenPreparedStatements(200);
    }
	
	public static Connection getConnection() throws SQLException {
        return ds.getConnection();
    }
     
    private MysqlPool(){ }
}

package aes;

import java.io.File;
import java.io.FileOutputStream;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Base64;

import javax.crypto.Cipher;
import javax.crypto.KeyGenerator;
import javax.crypto.SecretKey;
import javax.crypto.spec.SecretKeySpec;

public class Encryptor {

	// PATH KEYS
	final private static String keysPath = "/Users/alex/Projects/vivas/Server/";
	
	// CIPHER
	private static Cipher cpe, cpd;
	
	// Inicializo
	static {
		try {
			// Clave secreta
			SecretKeySpec sk = new SecretKeySpec(Files.readAllBytes(Paths.get(keysPath + "aes.bin")), "AES");
			cpe = Cipher.getInstance("AES/ECB/PKCS5Padding");
			cpe.init(Cipher.ENCRYPT_MODE, sk);
			cpd = Cipher.getInstance("AES/ECB/PKCS5Padding");
			cpd.init(Cipher.DECRYPT_MODE, sk);
		} catch (Exception e) {
		}
	}
	
	/**
	 * Encripta.
	 * @param txt Texto a encriptar.
	 * @return Texto encriptado.
	 */
	public static String encrypt(String txt) {
		try {
			return Base64.getEncoder().encodeToString(cpe.doFinal(txt.getBytes("UTF-8")));
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
	 * Desencripta.
	 * @param txt Texto a desencriptar.
	 * @return Texto desencriptado.
	 */
	public static String decrypt(String txt) {
		try {
			return new String(cpd.doFinal(Base64.getDecoder().decode(txt)));
		} catch (Exception e) {
			return null;
		}
	}
	
	/**
     * Crea fichero con clave AES.
     * @throws Exception Error al crear la clave.
     */
    public static void createKey() throws Exception {
    	// Crea fichero
    	File file = new File(keysPath + "aes.bin");
    	
    	// Crea clave
    	KeyGenerator kgen = KeyGenerator.getInstance("AES");
		SecretKey skey = kgen.generateKey();
		
		// Escribe clave en el fichero
		FileOutputStream out = new FileOutputStream(file);
		byte[] keyb = skey.getEncoded();
		out.write(keyb);
		
		// Cierra escritura
		out.close();
    }
}

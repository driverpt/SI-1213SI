package pt.isel.adeetc.leic;

import java.io.File;
import java.io.FileInputStream;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.util.HashMap;
import java.util.Map;

public class SIUtils {
    private static final Map<String, String> FILE_EXTENSION_PROVIDER = new HashMap< String, String >();
    private static final String FILE_EXTENSION_SEPARATOR = "\\.";
    private static final int MIN_FILENAME_WITH_EXTENSION = 2;
    
    static {
        FILE_EXTENSION_PROVIDER.put("pfx", "PKCS12");
        FILE_EXTENSION_PROVIDER.put("cer", "JKS");
    }
    
    private SIUtils() {
    }

    public static KeyStore loadFileIntoKeyStore( File file, String password ) throws KeyStoreException {
        return loadFileIntoKeyStore( null, file, password );
    }

    public static KeyStore loadFileIntoKeyStore( KeyStore ks, File file, String password ) throws KeyStoreException {
        if( ks == null ) {
            String ksType = FILE_EXTENSION_PROVIDER.get( getFileExtension( file ) );
            if( ksType == null ) {
                ksType = KeyStore.getDefaultType();
            }
            ks = KeyStore.getInstance( ksType );
        }
        
        FileInputStream fis = null; 
        try {
            fis = new FileInputStream( file );
            ks.load(fis, password.toCharArray());
        } catch ( Exception e ) {
            e.printStackTrace();
            return null;
        }
        
        return ks;
    }
    
    private static String getFileExtension( File file ) {
        String[] fileNameParted = file.getName().split( FILE_EXTENSION_SEPARATOR );
        if ( fileNameParted.length < MIN_FILENAME_WITH_EXTENSION ) {
            return null;
        }
        return fileNameParted[fileNameParted.length - 1];
    }

    
}

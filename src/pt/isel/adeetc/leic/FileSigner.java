package pt.isel.adeetc.leic;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.security.GeneralSecurityException;
import java.security.Key;
import java.security.KeyPair;
import java.security.KeyStore;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.Signature;
import java.security.cert.X509Certificate;
import java.util.Enumeration;

public class FileSigner {
    public static final String PRIVATE_KEY_FILE_EXTENSION      = "pfx";
    public static final String JAVA_KEYSTORE_FILE_EXTENSION    = "jks";
    public static final String X509_CERTIFICATE_FILE_EXTENSION = "cer";

    public static final int BYTE_BUFFER_LENGTH = 8;
    
    private KeyStore           parentKeyStore;
    private X509Certificate    cert;
    private KeyPair            keyPair;
    private String             password;

    public FileSigner( KeyStore ks, String password ) {
        parentKeyStore = ks;
        this.password = password;
    }

    private void ensureKS() throws GeneralSecurityException {
        Enumeration< ? > enumeration = parentKeyStore.aliases();
        boolean found = false;
        for ( ; enumeration.hasMoreElements(); ) {
            String alias = (String) enumeration.nextElement();

            if ( parentKeyStore.isKeyEntry( alias ) ) {
                Key key = parentKeyStore.getKey( alias, password.toCharArray() );
                if ( key instanceof PrivateKey ) {
                    PrivateKey priv = (PrivateKey) key;
                    cert = (X509Certificate) parentKeyStore.getCertificate( alias );
                    PublicKey publicKey = cert.getPublicKey();
                    keyPair = new KeyPair( publicKey, priv );
                    found = true;
                    break;
                }
            }
        }
        if ( !found ) {
            throw new InvalidKeyStoreException();
        }
    }

    public boolean signFile( File file ) throws GeneralSecurityException {
        ensureKS();
        Signature sign = Signature.getInstance( cert.getSigAlgName() );
        sign.initSign( keyPair.getPrivate() );
        FileInputStream fileInputStream = null;
        FileOutputStream fileOutputStream = null;
        try {
            fileInputStream = new FileInputStream( file );
            byte[] buffer = new byte[ BYTE_BUFFER_LENGTH ];
            int bytesRead;
            while ( (bytesRead = fileInputStream.read( buffer, 0, BYTE_BUFFER_LENGTH )) != -1 ) {
                sign.update( buffer, 0, bytesRead );
            }
            byte[] signatureBytes = sign.sign();
            String filePath = file.getAbsolutePath();
            filePath = filePath.substring( 0, filePath.lastIndexOf( File.separator ) );
            fileOutputStream = new FileOutputStream( filePath + "/" + file.getName() + ".sig" );
            fileOutputStream.write( signatureBytes );

            fileOutputStream.close();

            fileOutputStream = new FileOutputStream( filePath + "/" + file.getName() + ".pub" );
            fileOutputStream.write( keyPair.getPublic().getEncoded() );
            fileOutputStream.close();
            return true;
        } catch ( IOException e ) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        } finally {
            if ( fileInputStream != null ) {
                try {
                    fileInputStream.close();
                } catch ( IOException ignored ) {
                }
            }
            if ( fileOutputStream != null ) {
                try {
                    fileOutputStream.close();
                } catch ( IOException ignored ) {
                }
            }            
        }
        return false;
    }
}

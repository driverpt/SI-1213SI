package pt.isel.adeetc.leic;

import java.io.File;
import java.security.GeneralSecurityException;
import java.security.KeyStore;

import junit.framework.TestCase;

import org.junit.Before;
import org.junit.Test;

public class SignatureTests extends TestCase {

    private FileSigner signer;
    private File       fileToSign;
    private File       keyStoreFileLocation;
    private String     password;
    private KeyStore   keyStore;
    
    
    @Before
    @Override
    protected void setUp() throws Exception {
        super.setUp();
        
        fileToSign = new File("message.txt");
        keyStoreFileLocation = new File("cert/pfx/Alice_1.pfx");
        System.out.println(keyStoreFileLocation.exists());
        password = "changeit";
        keyStore = SIUtils.loadFileIntoKeyStore( keyStoreFileLocation, password );
        signer = new FileSigner( keyStore, password );
    }
    
    @Test
    public void testIfSignWorks() throws Exception {
        assertTrue( signer.signFile( fileToSign ) );
    }
    
}

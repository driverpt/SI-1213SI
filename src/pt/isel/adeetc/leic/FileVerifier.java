package pt.isel.adeetc.leic;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.security.GeneralSecurityException;
import java.security.KeyStore;
import java.security.Signature;
import java.security.cert.CertPathBuilder;
import java.security.cert.CertPathBuilderResult;
import java.security.cert.CertStore;
import java.security.cert.CertificateFactory;
import java.security.cert.PKIXBuilderParameters;
import java.security.cert.X509CertSelector;
import java.security.cert.X509Certificate;

import org.apache.commons.io.FileUtils;

public class FileVerifier {

    /**
     * @param args
     */

    public static final String X509_TYPE = "X.509";

    private CertStore           certChain;
    private KeyStore            anchors;

    public FileVerifier( CertStore certStoreChain, KeyStore anchors ) {
        this.certChain = certStoreChain;
        this.anchors = anchors;
    }

    public boolean verify( File originalFile, File signatureFile, File certificateFile ) throws GeneralSecurityException {
        CertificateFactory certificateFactory = CertificateFactory.getInstance( X509_TYPE );
        X509Certificate certificate = null;
        try {
            InputStream inputStream = FileUtils.openInputStream( certificateFile );
            certificate = (X509Certificate) certificateFactory.generateCertificate( inputStream );
            inputStream.close();
        } catch ( IOException e ) {
            e.printStackTrace();
            System.exit( -1 );
        }

        Signature signature = Signature.getInstance( certificate.getSigAlgName() );

        signature.initVerify( certificate );
        byte[] signatureBytes = null;
        try {
            byte[] originalFileBytes = FileUtils.readFileToByteArray( originalFile );
            signature.update( originalFileBytes );
            signatureBytes = FileUtils.readFileToByteArray( signatureFile );
        } catch ( IOException e ) {
            e.printStackTrace();
            System.exit( -1 );
        }
        
        if ( !signature.verify( signatureBytes ) ) {
            return false;
        }

        X509CertSelector selector = new X509CertSelector();
        selector.setCertificate( certificate );

        // This is the only supported CertPathBuilder Instance Type
        CertPathBuilder certPathBuilder = CertPathBuilder.getInstance( "PKIX" );
        
        PKIXBuilderParameters pkixBuilderParameters = new PKIXBuilderParameters( anchors, selector );
        pkixBuilderParameters.addCertStore( certChain );
        
        // This option must be used if we're using Self-Signed Certificates
        // And since that is the case
        pkixBuilderParameters.setRevocationEnabled( false );

        CertPathBuilderResult result = certPathBuilder.build( pkixBuilderParameters );
        return true;
    }
}

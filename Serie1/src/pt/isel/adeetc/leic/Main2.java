package pt.isel.adeetc.leic;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.security.KeyStore;
import java.security.cert.CertStore;
import java.security.cert.Certificate;
import java.security.cert.CertificateFactory;
import java.security.cert.CollectionCertStoreParameters;
import java.security.cert.X509Certificate;
import java.util.Collection;
import java.util.LinkedList;

import org.apache.commons.io.FileUtils;

public class Main2 {
    public static void main( String... args ) throws Exception {
        File originalFile = new File( "message.txt" );
        File signatureFile = new File( "message.txt.sig" );
        File certificateFile = new File( "message.txt.cer" );

        File caIntermediateDirectory = new File( "cert/cert.CAintermedia" );
        File trustAnchorsDirectory = new File( "cert/trust.anchors" );
        CertificateFactory factory = CertificateFactory.getInstance( "X.509" );

        InputStream inputStream;
        Collection< Certificate > certificatesCollection = new LinkedList< Certificate >();
        Collection< File > files = FileUtils.listFiles( caIntermediateDirectory, new String[] { "cer" }, true );
        for ( File file : files ) {
            inputStream = FileUtils.openInputStream( file );
            X509Certificate cert = (X509Certificate) factory.generateCertificate( inputStream );
            certificatesCollection.add( cert );
            inputStream.close();
        }

        CertStore certStoreChain = CertStore.getInstance( "Collection", new CollectionCertStoreParameters(
                certificatesCollection ) );

        KeyStore ks = KeyStore.getInstance( "JCEKS" );

        inputStream = new FileInputStream( "cert/trust.anchors/CA1.raiz.jks" );

        ks.load( inputStream, "changeit".toCharArray() );

        FileVerifier verifier = new FileVerifier( certStoreChain, ks );
        boolean result = verifier.verify( originalFile, signatureFile, certificateFile );
        System.out.println( result );
    }
}

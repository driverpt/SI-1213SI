package pt.isel.adeetc.leic;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.GeneralSecurityException;
import java.security.InvalidAlgorithmParameterException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.CertStore;
import java.security.cert.Certificate;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.CollectionCertStoreParameters;
import java.security.cert.X509Certificate;
import java.util.Collection;
import java.util.LinkedList;

import org.apache.commons.io.FileUtils;
import org.kohsuke.args4j.Option;

public class VerificationArgumentsBean implements Runnable {
	
	@Option( name = "-f", metaVar = "<target>", usage="target file to sign or verify", required = true )
    private String fileToSign;
	
    @Option( name = "-a", metaVar = "<anchors>", usage="keystore containing the anchors", required = true )
    private String keyStore;

    @Option( name = "-m", metaVar = "<intermedia>", usage="directory that contains CA-Intermedia Certificates" )
    private String certificateDirectory;
    
    private File targetFile;
    private String password;
    
    public VerificationArgumentsBean(File targetFile, String password) {
    	this.targetFile = targetFile;
    	this.password = password;
    }

	@Override
	public void run() {
		// TODO: This is a bit hard-coded, change this as soon as possible
		File signatureFile = new File(targetFile.getName().concat(".sig"));
		File certificateFile = new File(targetFile.getName().concat(".cer"));
		
		File caIntermediateDirectory = new File(certificateDirectory);
		
		CertificateFactory factory = null;
		try {
			factory = CertificateFactory.getInstance(FileVerifier.X509_TYPE);
		} catch (CertificateException e1) {
			System.out.println("X509 Certificates not supported in this platform");
			System.exit(-1);
		}
		
		InputStream inputStream;
		Collection<Certificate> certificatesCollection = new LinkedList<Certificate>();
		Collection<File> files = FileUtils.listFiles(caIntermediateDirectory, new String[]{ "cer" }, true);
		try {
			for( File file : files ) {
				inputStream = FileUtils.openInputStream(file);
				X509Certificate cert = (X509Certificate) factory.generateCertificate(inputStream);
				certificatesCollection.add(cert);
				inputStream.close();
			}
		} catch( IOException e ) {
			System.out.println("Something went wrong when reading files");
			System.exit(-1);
		} catch (CertificateException e) {
			System.out.println("Something went wrong when importing the certificate from one of the files");
			System.exit(-1);
		}
		
		try {
			CertStore certStoreChain = CertStore.getInstance("Collection", new CollectionCertStoreParameters(certificatesCollection));
			// For now we will only support JCEKS Keystores, .pem certificates required a bit more of code, we'll postpone that for now
			KeyStore ks = KeyStore.getInstance("JCEKS");
			inputStream = new FileInputStream(keyStore);
			ks.load(inputStream, password.toCharArray());
			FileVerifier verifier = new FileVerifier(certStoreChain, ks);
			boolean result = verifier.verify(targetFile, signatureFile, certificateFile);
		} catch( IOException e ) {
			
		} catch (InvalidAlgorithmParameterException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (NoSuchAlgorithmException ignored) {
			// It is specified in the JVM that the "Collection" type Certificate Store must be always supported
		} catch (KeyStoreException e) {
			System.out.println("Something went wrong when importing the Trust Anchors");
		} catch (CertificateException e) {
			System.out.println("Unable to ");
		} catch (GeneralSecurityException e) {
			System.out.println("Unable to verify the file");
		}
		
		
	}
}

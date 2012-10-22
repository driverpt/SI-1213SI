package pt.isel.adeetc.leic;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.security.GeneralSecurityException;
import java.security.KeyStore;
import java.security.Signature;
import java.security.cert.CertPathBuilder;
import java.security.cert.CertPathBuilderResult;
import java.security.cert.CertStore;
import java.security.cert.Certificate;
import java.security.cert.CertificateFactory;
import java.security.cert.PKIXBuilderParameters;
import java.security.cert.TrustAnchor;
import java.security.cert.X509CertSelector;
import java.security.cert.X509Certificate;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

import org.apache.commons.io.FileUtils;

public class FileVerifier {

    /**
     * @param args
     */
    
	private static final String X509_TYPE = "X.509";
	
	private CertStore certChain;
	private KeyStore anchors;
	
    public FileVerifier(CertStore certStoreChain, KeyStore anchors) {
        certChain = certStoreChain;
        this.anchors = anchors;
    }

    public boolean verify(File signatureFile, File certificateFile) throws GeneralSecurityException {
    	CertificateFactory certificateFactory = CertificateFactory.getInstance(X509_TYPE);
    	X509Certificate certificate = null;
    	try {
			InputStream inputStream = new FileInputStream(certificateFile);
			certificate = (X509Certificate) certificateFactory.generateCertificate(inputStream);
			inputStream.close();
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(-1);
		}
    	
    	Signature signature = Signature.getInstance(certificate.getSigAlgName());
    	
    	signature.initVerify(certificate);
    	byte[] signatureBytes = null;
    	try {
			signatureBytes = FileUtils.readFileToByteArray(signatureFile);
		} catch (IOException e) {
			e.printStackTrace();
			System.exit(-1);
		}
    	
    	if( !signature.verify(signatureBytes) ) {
    		return false;
    	}
    	
    	X509CertSelector selector = new X509CertSelector();
    	selector.setCertificate(certificate);
    	
    	CertPathBuilder certPathBuilder = CertPathBuilder.getInstance("PKIX");
    	
    	PKIXBuilderParameters pkixBuilderParameters = new PKIXBuilderParameters(anchors, selector);
    	pkixBuilderParameters.addCertStore(certChain);
    	
    	CertPathBuilderResult result = certPathBuilder.build(pkixBuilderParameters);

    	return true;
    }
    
}

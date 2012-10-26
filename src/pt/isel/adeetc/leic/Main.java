package pt.isel.adeetc.leic;

import java.io.File;

import org.kohsuke.args4j.Argument;
import org.kohsuke.args4j.CmdLineException;
import org.kohsuke.args4j.CmdLineParser;
import org.kohsuke.args4j.Option;

public class Main {

	public static final String VERIFY = "verify";
	public static final String SIGN = "sign";
	
    @Argument( index = 0, required = true, usage="Operation to execute", metaVar="<operation>" )
    private String operation;

	// Common Options for both operations  
    @Option( name = "-p", metaVar = "<passphrase>", usage="password used for all the certificates and keystores" )
    private String password;

    @Option( name = "-f", metaVar = "<target>", usage="target file to sign or verify", required = true )
    private String fileToSign;
    
    /**
     * @param args
     */
    public static void main( String[] args ) {
        // Will receive 2 files, one with the keystore, another with the file to
        // sign, and one with the password for the Certificate
    	Main main = new Main();
    	CmdLineParser commonArgumentsParser = new CmdLineParser(main);
    	try {
    		commonArgumentsParser.parseArgument(args);
		} catch (CmdLineException e) {
			commonArgumentsParser.printSingleLineUsage(System.out);
			System.out.println();
			commonArgumentsParser.printUsage(System.out);
			System.exit(-1);
		}
    	
    	// TODO: Improve this code, it is ugly as hell
    	if( main.operation.equals(VERIFY) ) {
    		VerificationArgumentsBean verificationArgumentsBean = new VerificationArgumentsBean(new File(main.fileToSign), main.password);
    		CmdLineParser parser = new CmdLineParser(verificationArgumentsBean);
    		try {
    			parser.parseArgument(args);
    		} catch (CmdLineException e) {
    			commonArgumentsParser.printUsage(System.out);
    			parser.printSingleLineUsage(System.out);
    			System.exit(-1);
    		}
    	}
    	if( main.operation.equals(SIGN) ) {
    		SignArgumentsBean signArgumentsBean = new SignArgumentsBean(new File(main.fileToSign), main.password);
    		CmdLineParser parser = new CmdLineParser(signArgumentsBean);
    		try {
    			parser.parseArgument(args);
    		} catch (CmdLineException e) {
    			commonArgumentsParser.printSingleLineUsage(System.out);
    			System.out.println();    			
    			commonArgumentsParser.printUsage(System.out);
    			parser.printSingleLineUsage(System.out);
    			System.exit(-1);
    		}
    	}    	
    }
}

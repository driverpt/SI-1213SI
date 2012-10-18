package pt.isel.adeetc.leic;

import org.kohsuke.args4j.Argument;
import org.kohsuke.args4j.Option;

public class Main {

    @Argument( index = 1, required = true )
    private String operation;

    @Option( name = "-p", metaVar = "PASSWORD" )
    private String password;

    @Option( name = "-f" )
    private String fileToSign;
    
    @Option( name = "-k" )
    private String keyStore;

    /**
     * @param args
     */
    public static void main( String[] args ) {
        // Will receive 2 files, one with the keystore, another with the file to
        // sign, and one with the password for the Certificate
    }

}

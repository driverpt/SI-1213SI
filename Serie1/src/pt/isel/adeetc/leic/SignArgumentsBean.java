package pt.isel.adeetc.leic;

import java.io.File;

import org.kohsuke.args4j.Option;

public class SignArgumentsBean implements Runnable {
	
    @Option( name = "-k" )
    private String keyStore;
    
    private File targetFile;
    private String password;
    
    public SignArgumentsBean(File targetFile, String password) {
    	this.targetFile = targetFile;
    	this.password = password;
    }

	@Override
	public void run() {
		
	}
}

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sieciowo;
import java.io.*;
import java.net.*;
import java.util.*;
import java.util.logging.Logger;
import java.net.ServerSocket;
import java.util.logging.Level;

public class TaskClassServer implements Runnable {
        private static ServerSocket servSock;
    private static final int PORT = 1233;
    private static void handleClient()    
    {
        Socket link = null;
        try{
            link = servSock.accept();
            Scanner input = new Scanner(link.getInputStream());
            PrintWriter output = new PrintWriter(link.getOutputStream(),true);
            int numMessages = 0;
            String message = input.nextLine();
            while (!message.equals("***CLOSE***")){
                System.out.println("Message received.");
                numMessages++;
                output.println("Message "+ numMessages + ": "+ message);
                message = input.nextLine();
            }
            output.println(numMessages + " messages received.");
        }
        catch(IOException ioEx)
                {
                    ioEx.printStackTrace();
                }
        finally{
            try{
                System.out.println("\n* Closing Connection... *");
                link.close();
            }
            catch(IOException ioEx)
                    {
                        System.out.println("Unable to disconnect!");
                        System.exit(1);
                    }
        }
    }
    public void run() {
        // TODO code application logic here

        System.out.println("Opening port..\n");
        try {
            servSock = new ServerSocket(PORT);
        }
        catch(IOException ioEx){
            System.out.println("Unable to attach to port!");
            System.exit(1);
        }
        do{
            handleClient();
        }
        while (true);
    }
}

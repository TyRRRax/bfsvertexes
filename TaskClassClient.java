/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package sieciowo;

import java.io.*;
import java.net.*;
import java.util.*;
import java.lang.Runnable;


public class TaskClassClient implements Runnable {
    
    private static InetAddress host;
    private static final int PORT = 1233;
    
    private static void accessServer()
    {  
        try{
        host = InetAddress.getLocalHost();
        // host = InetAddress.getByName("150.254.11.236");
        System.out.println("HOST: " + host);
        // 10.160.36.85 ; 150.254.11.236 / 227
        }
        catch(IOException ioEx)
                {
                    System.out.println("Unable to get host");
                }
        Socket link = null;
        try{
            link = new Socket(host,PORT);
            Scanner input = new Scanner(link.getInputStream());
            PrintWriter output = new PrintWriter(link.getOutputStream(),true);
            Scanner userEntry = new Scanner(System.in);
            String message, response;
            do{
                System.out.print("Enter message: ");
                message = userEntry.nextLine();
                output.println(message);
                response = input.nextLine();
                System.out.println("\nSERVER>" + response);
            }while(!message.equals("***CLOSE***"));
        }
        catch(IOException ioEx){
            ioEx.printStackTrace();  
        }
        finally{
            try{
                System.out.println("\n* Closing connection...*");
                link.close();
            }
            catch(IOException ioEx){
                System.out.println("Unable to disconnect!");
                System.exit(1);
            }
        }
    }
    public void run(){
        try{
            host = InetAddress.getLocalHost();
        }
        catch(UnknownHostException uhEx){
            System.out.println("Host ID not found!");
            System.exit(1);
        }
        accessServer();
    }
}

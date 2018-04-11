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
import java.lang.Runnable;
/**
 *
 * @author student
 */
public class gadugadu {
    
    public static void main(String[] args) {
           TaskClassServer server = new TaskClassServer();
           Thread threadS = new Thread(server);
           threadS.start();
    }
    
}

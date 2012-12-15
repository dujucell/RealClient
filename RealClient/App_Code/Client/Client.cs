using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using RealLicenseLibrary;

/// <summary>
/// Summary description for Client
/// </summary>
public class Client
{
    //create references 
    private NetworkStream input;
    private NetworkStream output;
    private TcpClient socket;

    public Client()
    {
        this.CreateConnection();
        this.getStreams();
    }

    //establish connection on socket
    public void CreateConnection()
    {
        try
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8888);
            socket = new TcpClient();
            socket.Connect(endPoint);
        }
        catch (Exception ex)//if host unresolved
        {
            throw ex;
        }

    }

    //close connection on socket
    public void CloseConnection()
    {
        try
        {
            socket.Close(); //close connection to server
            output.Flush();
            output.Close();	//close output stream
            input.Flush();
            input.Close();		//close input stream
        }
        catch (Exception ex) //if connection or stream closure failed
        {
            throw ex;
        }
    }

    //receive streams from socket connection
    public void getStreams()
    {
        try
        {
            output = socket.GetStream();
            input = socket.GetStream();//get input stream from connection
            output.Flush();	//flush output stream
            input.Flush();
        }
        catch (Exception ex)	//if streams were not received
        {
            throw ex;
        }
    }

    public void sendAuthenticationRequest(RealLicense obj)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
            formatter.Serialize(output, obj);
        }
        catch (Exception ex)	//if failed to communicate with server
        {
            throw ex;
        }

    }

    public Boolean receiveAuthenticationResponse()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
            return (Boolean)formatter.Deserialize(input);
        }
        catch (Exception ex)	//if failed to communicate with server
        {
            throw ex;
        }

    }
}
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <iostream>
#include <string>
#include <sstream>
#include <conio.h>
#include <winsock2.h>
#include <windows.h>
#include <thread>
#include <string>
using namespace std;
#pragma comment (lib, "ws2_32.lib")
#pragma comment (lib, "mswsock.lib")



struct WATF
{

	ULONG IP_SRC;
	ULONG IP_DEST;
	int MSG_LEN;
	char MSG[500];


	WATF()
	{
		IP_SRC = 0;
		IP_DEST = 0;
		MSG_LEN = 0;
		strcpy_s(MSG, "");
	}
};






class Client
{

	SOCKET		Socketlisten;
	SOCKET		SocketSend;
	WSADATA		ws;
	sockaddr_in	sockaddrListen;
	sockaddr_in	sockaddrSend;

	char ip[21];
	int port;

public :
	Client()
	{
		


	}

private :
	void listenmsgThread();
public :


	void listenmsg();
	void sendMessage(WATF message);
	
	void setConfigtServer(char *Ip, int Port);
	char *getIp()
	{
		return ip;
	}
	int getPort()
	{
		return port;
	}

};
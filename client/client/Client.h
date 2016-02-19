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

	unsigned long IP_SRC;
	unsigned long IP_DEST;
	int MSG_LEN;
	char MSG[200];


	WATF(unsigned long ip_SRC,
		 unsigned long ip_DEST,
		 int msg_LEN,
		 char msg[])
	{
		IP_SRC = ip_SRC;
		IP_DEST = ip_DEST;
		MSG_LEN = msg_LEN;
		strcpy_s(MSG, msg);
	}
};






class Client
{
	SOCKET		Socket;
	SOCKET		Socketlisten;
	SOCKET		Socketmesg;
	WSADATA		ws;
	sockaddr_in	sockaddrin;
	sockaddr_in	sock_bind;
	sockaddr_in	sock_bind2;
	sockaddr_in	new_ca;
	char ip[21];
	int port;

public :
	Client()
	{
		


	}

private :
	void sendMessageThread();
	
public :
	void listenmsg();
	void sendMessage();
	
	bool connectServer(char *Ip, int Port);
	char *getIp()
	{
		return ip;
	}
	int getPort()
	{
		return port;
	}

};
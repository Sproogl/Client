#pragma once


#include <iostream>
#include <string>

#include <stdio.h>
#include <winsock2.h>
#include <windows.h>
#include <thread>
#include <string.h>
#include <map> 


static std::map <unsigned int, SOCKET> ClientList;

struct WATF
{
	byte type;
	unsigned int ID_SRC;
	unsigned int ID_DEST;
	int MSG_LEN;
	char messange[500];

	WATF()

	{
		type = 111;
		ID_SRC = 111;
		ID_DEST = 111;
		MSG_LEN = 111;

	}


};


class Server
{
public:
	Server();
	~Server();

	bool startServer();
	void stop();
private:

	SOCKET		s, new_conn;
	WSADATA		ws;
	sockaddr_in	sock_bind, new_ca;
	int			new_len, i = 0;
	char		buff[520];

	// Init 
	bool flag = true;
	
	void createMesg(WATF mesg1, SOCKET new_connT);
	void sendMessage(WATF msg);
	int hashing(char ip[50]);

	
};


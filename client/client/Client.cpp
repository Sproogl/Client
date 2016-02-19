#include "Client.h"
#include <string>
using namespace std;
/*
value ip and port 
if connection error then return false
else return true
*/
bool Client::connectServer(char *Ip , int Port)        
{
	strcpy_s(ip, Ip);
	port = Port;
	if (FAILED(WSAStartup(0x0002, &ws))) return false;


	if (INVALID_SOCKET == (Socket = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))
		ZeroMemory(&sock_bind, sizeof(sock_bind));


	sock_bind.sin_family = AF_INET;
	sock_bind.sin_addr.S_un.S_addr = inet_addr(ip);
	sock_bind.sin_port = htons(u_short(port));
	if (connect(Socket, (sockaddr*)&sock_bind, sizeof(sock_bind)) == SOCKET_ERROR) {
		return false;

	}
	
	return true;
}



void Client::listenmsg()
{
	char buff[200];
	// Создаём сокет
	if (INVALID_SOCKET == (Socketlisten = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))

		// Выполняем привязку
		ZeroMemory(&sock_bind2, sizeof(sock_bind2));
	sock_bind2.sin_family = AF_INET;
	sock_bind2.sin_addr.S_un.S_addr = inet_addr(ip);

	sock_bind2.sin_port = htons(1322);				// Порт 1234



	bind(Socketlisten, (sockaddr*)&sock_bind2, sizeof(sock_bind2));


	// Устанавливаем "слушающий" режим для сокета
	if (FAILED(listen(Socketlisten, 10)))
	{
		return;
	}

	while (1)
	{
		ZeroMemory(&new_ca, sizeof(new_ca));
		int new_len = sizeof(new_ca);
		printf("Wait for incoming connections...\n");
		if (FAILED(Socketmesg = accept(Socketlisten, (sockaddr*)&new_ca, &new_len)))
		{

			return ;
		}
		int msg_len;
		if (FAILED(msg_len = recv(Socketmesg, (char*)&buff, 200, 0)))
			break;
		WATF *wtf;
		wtf = (WATF *)&buff;
		// Печатем сообщение.
		std::cout << wtf->MSG << std::endl;
	}
	closesocket(Socketmesg);

}



void Client::sendMessageThread()
{
	char message[200];


	while (1)
	{
		cin.getline(message,200);
		WATF wtf(10, 11, 12, message);
		char * test = (char *)&wtf;
		if (send(Socket, test, sizeof(struct WATF) + 1, 0) == SOCKET_ERROR)
		{
			std::cout << "error_send";

		}

	}
}


void Client::sendMessage()
{
	thread thread2(&Client::sendMessageThread, this);
	if (thread2.joinable()) thread2.detach();

}
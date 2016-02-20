#include "Client.h"
#include <string>
using namespace std;
/*
value ip and port 
if connection error then return false
else return true
*/

/*


�������� ������ winsock2 if (FAILED(WSAStartup(0x0002, &ws))) return false;


������� ����� : ��� ������ = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)

������ sockadr 

�����_�������.sin_family = AF_INET;

�����_�������.sin_addr.S_un.S_addr = inet_addr(ip);

�����_�������.sin_port = htons(u_short(port));


������������ � ������  : connect(���_������, (sockaddr*)&�����_�������, sizeof(�����_�������))


��������� ��������� : send(���_������, (char*)&���������, sizeof(struct WATF) + 1, 0)


������� ��������� : recv(���_������, (char*)&���������_����_��������_���������, ������ ���������, 0))


*/


void Client::setConfigtServer(char *Ip, int Port)  // ����� ��������� ����������� � �������
{

	strcpy_s(ip,Ip);
	port = port;



}


void Client::listenmsg()
{
	thread listenTh(&Client::listenmsgThread, this);
	if (listenTh.joinable()) listenTh.join();
	listenTh.

}



void Client::listenmsgThread()
{

	WATF message;
	int msgLen;

	if (INVALID_SOCKET == (Socketlisten = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))

		// ��������� ��������
		ZeroMemory(&sockaddrListen, sizeof(sockaddrListen));
	sockaddrListen.sin_family = AF_INET;
	sockaddrListen.sin_addr.S_un.S_addr = inet_addr(ip);
	sockaddrListen.sin_port = htons(port);				


	if (connect(Socketlisten, (sockaddr*)&sockaddrListen, sizeof(sockaddrListen)) == SOCKET_ERROR) {
		cout << "Error connection to server in listenMessage";
		return;

	}

	message.IP_SRC = 322;
	message.MSG[0] = '0';
	message.MSG[1] = '\0';



	if (send(Socketlisten, (char*)&message , sizeof(struct WATF), 0) == SOCKET_ERROR)
	{
		std::cout << "error_send";

	}


	while (true)
	{

		if (FAILED(msgLen = recv(Socketlisten, (char*)&message, sizeof(struct WATF), 0)))
			break;

		if (message.MSG_LEN > 500 || message.MSG_LEN < 0)
		{
			continue;
		}
		msgLen = message.MSG_LEN;

		message.MSG[msgLen] = '\0';

		cout << message.MSG<<endl;

	}

	cout << "closed connection";
	closesocket(Socketlisten);

}




void Client::sendMessage(WATF message)
{
	if (INVALID_SOCKET == (SocketSend = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP)))
	ZeroMemory(&sockaddrSend, sizeof(sockaddrSend));
	sockaddrSend.sin_family = AF_INET;
	sockaddrSend.sin_addr.S_un.S_addr = inet_addr(ip);
	sockaddrSend.sin_port = htons(port);
	
	
	if (connect(SocketSend, (sockaddr*)&sockaddrSend, sizeof(sockaddrSend)) == SOCKET_ERROR) {
		cout << "Error connection to server in sendmessage";
		return;

	}

	message.MSG_LEN = strlen(message.MSG);
	
	send(SocketSend, (char*)&message, sizeof(struct WATF),0);



	closesocket(SocketSend);
}
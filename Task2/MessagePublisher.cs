using System;
using System.IO;

namespace Task2;

public delegate void MessageEventHandler(string message);

public class MessagePublisher
{
    public event MessageEventHandler MessageSent;

    public void Send(string message)
    {
        MessageSent?.Invoke(message);
    }
}
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour {
    private readonly List<IListenerObject> list = new List<IListenerObject>();

    public void addListener(IListenerObject obj) {
        list.Add(obj);
    }

    public void addMessage(GameMessage message) {
        foreach (var obj in list) {
            obj.readMessage(message);
        }
    }
}

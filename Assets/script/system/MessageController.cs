using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageController : MonoBehaviour {
    private List<IListenerObject> list = new List<IListenerObject>();

    public void addListener(IListenerObject obj) {
        list.Add(obj);
    }

    public void addMessage(GameMessage message) {
        foreach (IListenerObject obj in list) {
            obj.readMessage(message);
        }
    }
}

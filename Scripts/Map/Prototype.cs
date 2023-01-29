using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Prototype")]
public class Prototype : ScriptableObject
{
    public int meshRotation;
    public GameObject prefab;
    public WFC_Socket posX;
    public WFC_Socket negX;
    public WFC_Socket posZ;
    public WFC_Socket negZ;
    public List<Attribute> attributes = new List<Attribute>();
    public NeighbourList validNeighbours;
}
[System.Serializable] public class NeighbourList
{
    public List<Prototype> posX = new List<Prototype>();
    public List<Prototype> posZ = new List<Prototype>();
    public List<Prototype> negX = new List<Prototype>();
    public List<Prototype> negZ = new List<Prototype>();

}
public enum WFC_Socket{Socket_Road, Socket_Curb, Socket_Buildings_Pos, Socket_Buildings_Neg, Socket_FullBuildings}

[System.Serializable] public class SocketConnection
{
    public WFC_Socket socket;
    public List<WFC_Socket> validConnections;
}
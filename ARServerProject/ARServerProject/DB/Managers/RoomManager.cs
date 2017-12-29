using ARCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARServerProject.DB.Managers
{
    public class RoomManager
    {
        public IList<Room> GetAllRoom()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var roomList = session.QueryOver<Room>();
                    transaction.Commit();
                    return roomList.List();
                }
            }
        }
        public IList<Room> GetRoomName(string roomName)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var roomList = session.QueryOver<Room>().Where(x => x.RoomName == roomName);
                    transaction.Commit();
                    return roomList.List();
                }
            }
        }
        public void RemoveRoomByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    IList<Room>  roomList=GetRoomName(name);
                    Room room = new Room();
                    room.ID = roomList[0].ID;
                    session.Delete(room);
                    transaction.Commit();
                }
            }
        }
        public void AddRoom(Room room)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(room);
                    transaction.Commit();
                }
            }
        }
    }
}

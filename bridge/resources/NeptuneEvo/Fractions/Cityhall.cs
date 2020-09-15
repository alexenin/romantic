using System;
using System.Collections.Generic;
using GTANetworkAPI;
using NeptuneEvo.Core;
using Redage.SDK;
using NeptuneEvo.GUI;

namespace NeptuneEvo.Fractions
{
    class Cityhall : Script
    {
        private static nLog Log = new nLog("Cityhall");
        public static int lastHourTax = 0;
        public static int canGetMoney = 999999;

        [ServerEvent(Event.ResourceStart)]
        public void onResourceStartHandler()
        {
            try
            {
                NAPI.TextLabel.CreateTextLabel("~g~Tom Logan", new Vector3(253.9357, 228.9332, 102.6832), 5f, 0.3f, 0, new Color(255, 255, 255), true, NAPI.GlobalDimension);
                NAPI.TextLabel.CreateTextLabel("~g~Lorens Hope", new Vector3(262.7953, 220.5285, 102.6832), 5f, 0.3f, 0, new Color(255, 255, 255), true, NAPI.GlobalDimension);
                

                Cols.Add(0, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[0], 1f, 2, 0)); // Оружейка
                Cols[0].OnEntityEnterColShape += city_OnEntityEnterColShape;
                Cols[0].OnEntityExitColShape += city_OnEntityExitColShape;
                Cols[0].SetData("INTERACT", 9);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Открыть шкафчик"), new Vector3(CityhallCheckpoints[0].X, CityhallCheckpoints[0].Y, CityhallCheckpoints[0].Z + 0.7), 5F, 0.4F, 0, new Color(255, 255, 255));

                Cols.Add(1, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[1], 1f, 2, 0)); // Раздевалка
                Cols[1].OnEntityEnterColShape += city_OnEntityEnterColShape;
                Cols[1].OnEntityExitColShape += city_OnEntityExitColShape;
                Cols[1].SetData("INTERACT", 1);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Раздевалка"), CityhallCheckpoints[1] + new Vector3(0, 0, 0.7), 5F, 0.4F, 0, new Color(255, 255, 255));

                for (int i = 2; i < 4; i++)
                {
                    Cols.Add(i, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[i], 1, 2, 0));
                    Cols[i].OnEntityEnterColShape += city_OnEntityEnterColShape;
                    Cols[i].OnEntityExitColShape += city_OnEntityExitColShape;
                    Cols[i].SetData("INTERACT", 5);
                    NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Press E"), new Vector3(CityhallCheckpoints[i].X, CityhallCheckpoints[i].Y, CityhallCheckpoints[i].Z + 1), 5F, 0.3F, 0, new Color(255, 255, 255));
                    NAPI.Marker.CreateMarker(21, CityhallCheckpoints[i] + new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 0.8f, new Color(255, 255, 255, 60));
                }
                /*int door = 0;
                for (int i = 4; i < 6; i++)
                {
                    Cols.Add(i, NAPI.ColShape.CreateCylinderColShape(CityhallChecksCoords[i], 1, 2, 0));
                    Cols[i].OnEntityEnterColShape += city_OnEntityEnterColShape;
                    Cols[i].OnEntityExitColShape += city_OnEntityExitColShape;
                    Cols[i].SetData("INTERACT", 3);
                    Cols[i].SetData("DOOR", door);
                    door++;
                }*/

               /* Cols.Add(12, NAPI.ColShape.CreateCylinderColShape(new Vector3(255.2283, 223.976, 102.3932), 3, 2, 0));
                Cols[12].OnEntityEnterColShape += city_OnEntityEnterColShape;
                Cols[12].OnEntityExitColShape += city_OnEntityExitColShape;
                Cols[12].SetData("INTERACT", 4);*/

                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[0] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[1] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[6] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[7] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[8] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[9] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                #region lift in cityhall
                for (int i = 8; i < 9; i++)
                {
                    Cols.Add(i, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[i], 1, 2, 0)); // lift in cityhall 
                    Cols[i].OnEntityEnterColShape += city_OnEntityEnterColShape;
                    Cols[i].OnEntityExitColShape += city_OnEntityExitColShape;
                    Cols[i].SetData("INTERACT", 602);
                    NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Вход на крышу"), new Vector3(CityhallCheckpoints[i].X, CityhallCheckpoints[i].Y, CityhallCheckpoints[i].Z + 1), 5F, 0.3F, 0, new Color(255, 255, 255));
                    NAPI.Marker.CreateMarker(22, CityhallCheckpoints[i] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                }
                for (int i = 5; i < 8; i++)
                {
                    Cols.Add(i, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[i], 1, 2, 0)); // lift on roof
                    Cols[i].OnEntityEnterColShape += city_OnEntityEnterColShape;
                    Cols[i].OnEntityExitColShape += city_OnEntityExitColShape;
                    Cols[i].SetData("INTERACT", 602);
                    NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Выход на на 2 этаж"), new Vector3(CityhallCheckpoints[8].X, CityhallCheckpoints[8].Y, CityhallCheckpoints[8].Z + 1), 5F, 0.3F, 0, new Color(255, 255, 255));
                    NAPI.Marker.CreateMarker(1, CityhallCheckpoints[i] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                }
                #endregion

                #region back door
                Cols.Add(9,NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[9], 1, 2, 0)); // back door exit 
                Cols[9].OnEntityEnterColShape += city_OnEntityEnterColShape;
                Cols[9].OnEntityExitColShape += city_OnEntityExitColShape;
                Cols[9].SetData("INTERACT", 606);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Запасной выход/вход"), new Vector3(CityhallCheckpoints[9].X, CityhallCheckpoints[9].Y, CityhallCheckpoints[9].Z + 1), 5F, 0.3F, 0, new Color(255, 255, 255));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[9] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));

                Cols.Add(10, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[10], 1, 2, 0)); // back door enter 
                Cols[10].OnEntityEnterColShape += city_OnEntityEnterColShape;
                Cols[10].OnEntityExitColShape += city_OnEntityExitColShape;
                Cols[10].SetData("INTERACT", 606);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Запасной вход"), new Vector3(CityhallCheckpoints[10].X, CityhallCheckpoints[10].Y, CityhallCheckpoints[10].Z + 1), 5F, 0.3F, 0, new Color(255, 255, 255));
                NAPI.Marker.CreateMarker(1, CityhallCheckpoints[10] - new Vector3(0, 0, 0.7), new Vector3(), new Vector3(), 1, new Color(255, 255, 255, 220));
                #endregion


                /*Cols.Add(6, NAPI.ColShape.CreateCylinderColShape(CityhallCheckpoints[6], 1f, 2, 0)); // Оружейка
                Cols[6].OnEntityEnterColShape += city_OnEntityEnterColShape;
                Cols[6].OnEntityExitColShape += city_OnEntityExitColShape;
                Cols[6].SetData("INTERACT", 62);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~g~Открыть шкафчик"), new Vector3(CityhallCheckpoints[6].X, CityhallCheckpoints[6].Y, CityhallCheckpoints[6].Z + 0.7), 5F, 0.4F, 0, new Color(255, 255, 255));*/

                NAPI.Object.CreateObject(0x4f97336b, new Vector3(260.651764, 203.230209, 106.432785), new Vector3(0, 0, 160.003571), 255, 0);
                NAPI.Object.CreateObject(0x4f97336b, new Vector3(258.209259, 204.120041, 106.432785), new Vector3(0, 0, -20.0684872), 255, 0);

                NAPI.Object.CreateObject(0x4f97336b, new Vector3(259.09613, 212.803894, 106.432793), new Vector3(0, 0, 70.0000153), 255, 0);
                NAPI.Object.CreateObject(0x4f97336b, new Vector3(259.985962, 215.246399, 106.432793), new Vector3(0, 0, -109.999962), 255, 0);
            } catch(Exception e)
            {
                Log.Write("EXCEPTION AT\"FRACTIONS_CITYHALL\":\n" + e.ToString(), nLog.Type.Error);
            }
        }
        
        private static Dictionary<int, ColShape> Cols = new Dictionary<int, ColShape>();
        public static List<Vector3> CityhallCheckpoints = new List<Vector3>
        {
            new Vector3(-545.6683, -196.8308, 46.29496), // оружейка в мэрии 0z
            new Vector3(-541.4794, -193.0653, 46.30305), // раздевалка в мэрии
            new Vector3(-545.0524, -204.0801, 37.09514), // main door enter
            new Vector3(233.312, 216.0169, 105.1667), // main door exit
            new Vector3(256.9124, 220.4567, 105.2864), // door 1
            new Vector3(265.8495, 218.1592, 109.283), // door 2
            new Vector3(252.9623, 226.9354, 100.5633), // gun stock 6
            new Vector3(-555.2877, -196.6146, 46.29496), // lift in cityhall
            new Vector3(-555.2161, -186.2178, 51.08306), // lift on cityhall's roof
            new Vector3(-534.6564, -165.966, 37.20432), // back door exit
            new Vector3(0, 0, 119.8431), // back door enter
        };

        private void city_OnEntityEnterColShape(ColShape shape, Client entity)
        {
            try
            {
                NAPI.Data.SetEntityData(entity, "INTERACTIONCHECK", shape.GetData("INTERACT"));
                if (shape.HasData("DOOR")) NAPI.Data.SetEntityData(entity, "DOOR", shape.GetData("DOOR"));
            }
            catch (Exception e) { Log.Write("city_OnEntityEnterColShape: " + e.Message, nLog.Type.Error); }
        }

        private void city_OnEntityExitColShape(ColShape shape, Client entity)
        {
            try
            {
                NAPI.Data.SetEntityData(entity, "INTERACTIONCHECK", 0);
            }
            catch (Exception e) { Log.Write("city_OnEntityExitColShape: " + e.Message, nLog.Type.Error); }
        }

        public static void interactPressed(Client player, int interact)
        {
            switch (interact)
            {
                case 3:
                    if (Main.Players[player].FractionID == 6 && Main.Players[player].FractionLVL > 1)
                    {
                        Doormanager.SetDoorLocked(player.GetData("DOOR"), !Doormanager.GetDoorLocked(player.GetData("DOOR")), 0);
                        string msg = "Вы открыли дверь";
                        if (Doormanager.GetDoorLocked(player.GetData("DOOR"))) msg = "Вы закрыли дверь";
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, msg, 3000);
                    }
                    return;
                case 4:
                    SafeMain.OpenSafedoorMenu(player);
                    return;
                case 5:
                    if (player.IsInVehicle) return;
                    if (player.HasData("FOLLOWING"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вас кто-то тащит за собой", 3000);
                        return;
                    }
                    if (player.Position.Z < 50)
                    {
                        NAPI.Entity.SetEntityPosition(player, CityhallCheckpoints[3] + new Vector3(0, 0, 1.12));
                        Main.PlayerEnterInterior(player, CityhallCheckpoints[3] + new Vector3(0, 0, 1.12));
                    }
                    else
                    {
                        NAPI.Entity.SetEntityPosition(player, CityhallCheckpoints[2] + new Vector3(0, 0, 1.12));
                        Main.PlayerEnterInterior(player, CityhallCheckpoints[2] + new Vector3(0, 0, 1.12));
                    }
                    return;
                case 62:
                    if (Main.Players[player].FractionID != 6)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не сотрудник полиции", 3000);
                        return;
                    }
                    if (!NAPI.Data.GetEntityData(player, "ON_DUTY"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы должны начать рабочий день", 3000);
                        return;
                    }
                    if (!Stocks.fracStocks[6].IsOpen)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Склад закрыт", 3000);
                        return;
                    }
                    if (!Manager.canUseCommand(player, "openweaponstock")) return;
                    player.SetData("ONFRACSTOCK", 6);
                    GUI.Dashboard.OpenOut(player, Stocks.fracStocks[6].Weapons, "Склад оружия", 6);
                    return;
            }
        }

        public static void beginWorkDay(Client player)
        {
            if (Main.Players[player].FractionID == 6)
            {
                if (!NAPI.Data.GetEntityData(player, "ON_DUTY"))
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы начали рабочий день", 3000);
                    Manager.setSkin(player, 6, Main.Players[player].FractionLVL);
                    NAPI.Data.SetEntityData(player, "ON_DUTY", true);
                    if (Main.Players[player].FractionLVL >= 3)
                        player.Armor = 100;
                    return;
                }
                else
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы закончили рабочий день", 3000);
                    Customization.ApplyCharacter(player);
                    if (player.HasData("HAND_MONEY")) player.SetClothes(5, 45, 0);
                    else if (player.HasData("HEIST_DRILL")) player.SetClothes(5, 41, 0);
                    NAPI.Data.SetEntityData(player, "ON_DUTY", false);
                    return;
                }
            }
            else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не сотрудник мэрии", 3000);
        }

        #region menu
        public static void OpenCityhallGunMenu(Client player)
        {

            if (Main.Players[player].FractionID != 6)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Вы не имеете доступа", 3000);
                return;
            }
            if (!Stocks.fracStocks[6].IsOpen)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Склад закрыт", 3000);
                return;
            }
            Trigger.ClientEvent(player, "govguns");
        }
        [RemoteEvent("govgun")]
        public static void callback_cityhallGuns(Client client, int index)
        {
            try
            {
                switch (index)
                {
                    case 0: //"stungun":
                        Fractions.Manager.giveGun(client, Weapons.Hash.StunGun, "stungun");
                        return;
                    case 1: //"pistol":
                        Fractions.Manager.giveGun(client, Weapons.Hash.Pistol, "pistol");
                        return;
                    case 2: //"assaultrifle":
                        Fractions.Manager.giveGun(client, Weapons.Hash.AdvancedRifle, "assaultrifle");
                        return;
                    case 3: //"gusenberg":
                        Fractions.Manager.giveGun(client, Weapons.Hash.Gusenberg, "gusenberg");
                        return;
                    case 4: //"armor":
                        if (!Manager.canGetWeapon(client, "armor")) return;

                        var aItem = nInventory.Find(Main.Players[client].UUID, ItemType.BodyArmor);
                        if (aItem != null)
                        {
                            Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "У Вас уже есть бронежилет", 3000);
                            return;
                        }
                        nInventory.Add(client, new nItem(ItemType.BodyArmor, 1, 100.ToString()));
                        GameLog.Stock(Main.Players[client].FractionID, Main.Players[client].UUID, "armor", 1, false);
                        Notify.Send(client, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы получили бронежилет", 3000);
                        return;
                    case 5:
                        if (!Manager.canGetWeapon(client, "Medkits")) return;

                        if (Fractions.Stocks.fracStocks[6].Medkits == 0)
                        {
                            Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "На складе нет аптечек", 3000);
                            return;
                        }
                        var hItem = nInventory.Find(Main.Players[client].UUID, ItemType.HealthKit);
                        if (hItem != null)
                        {
                            Notify.Send(client, NotifyType.Error, NotifyPosition.BottomCenter, "У Вас уже есть аптечка", 3000);
                            return;
                        }
                        Fractions.Stocks.fracStocks[6].Medkits--;
                        Fractions.Stocks.fracStocks[6].UpdateLabel();
                        nInventory.Add(client, new nItem(ItemType.HealthKit, 1));
                        GameLog.Stock(Main.Players[client].FractionID, Main.Players[client].UUID, "medkit", 1, false);
                        Notify.Send(client, NotifyType.Success, NotifyPosition.BottomCenter, $"Вы получили аптечку", 3000);
                        return;
                    case 6:
                        if (!Manager.canGetWeapon(client, "PistolAmmo")) return;
                        Fractions.Manager.giveAmmo(client, ItemType.PistolAmmo, 12);
                        return;
                    case 7:
                        if (!Manager.canGetWeapon(client, "SMGAmmo")) return;
                        Fractions.Manager.giveAmmo(client, ItemType.SMGAmmo, 30);
                        return;
                    case 8:
                        if (!Manager.canGetWeapon(client, "RiflesAmmo")) return;
                        Fractions.Manager.giveAmmo(client, ItemType.RiflesAmmo, 30);
                        return;
                }
            }
            catch (Exception e) { Log.Write("Govgun: " + e.Message, nLog.Type.Error); }
        }
        #endregion
    }
}

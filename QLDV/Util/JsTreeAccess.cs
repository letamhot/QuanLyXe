using QLDV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLDV.Util
{
    public class JsTreeAccess
    {
        public void AddChildItems(List<DonVi> lsDonVi, Node parentNode, int parentID)
        {
            List<DonVi> result = new List<DonVi>();
            foreach (var item in lsDonVi)
            {
                if (item.DonViCha == parentID)
                {
                    result.Add(item);
                }
            }
            try
            {
                foreach (var item in result)
                {
                    Node node = new Node();
                    node.id = item.DonViId;
                    node.text = item.TenDonVi;
                    node.icon = "fa fa-user text-danger fa-lg";
                    node.children = new List<Node>();
                    if (node != null)
                    {
                        parentNode.children.Add(node);
                    }
                    AddChildItems(lsDonVi, node, item.DonViId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddChildItemsToChuc(List<DonViToChuc_Map> lsToChuc, Node parentNode, int parentID)
        {
            List<DonViToChuc_Map> result = new List<DonViToChuc_Map>();
            foreach (var item in lsToChuc)
            {
                if (item.ToChucChaId == parentID)
                {
                    result.Add(item);
                }
            }
            try
            {
                foreach (var item in result)
                {
                    Node node = new Node();
                    node.id = item.Id;
                    node.text = item.TenToChuc;
                    node.icon = "fa fa-user text-danger fa-lg";
                    node.children = new List<Node>();
                    if (node != null)
                    {
                        parentNode.children.Add(node);
                    }
                    AddChildItemsToChuc(lsToChuc, node, item.Id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddChildItemsQuyen(List<QuyenMenu> lsQuyen, Node parentNode, int parentID)
        {
            List<QuyenMenu> result = new List<QuyenMenu>();
            foreach (var item in lsQuyen)
            {
                if (item.QuyenCha == parentID)
                {
                    result.Add(item);
                }
            }
            try
            {
                foreach (var item in result)
                {
                    Node node = new Node();
                    node.id = item.QuyenMenuId;
                    node.text = item.TenQuyen;
                    node.icon = "fa fa-user text-danger fa-lg";
                    node.children = new List<Node>();
                    if (node != null)
                    {
                        parentNode.children.Add(node);
                    }
                    AddChildItemsQuyen(lsQuyen, node, item.QuyenMenuId);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
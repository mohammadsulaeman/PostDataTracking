using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class MetaBLL
    {
        MetaDAO metaDAO = new MetaDAO();

        public bool AddMeta(MetaDTO model)
        {
            Meta meta = new Meta();
            meta.Name = model.Name;
            meta.MetaContent = model.MetaContent;
            meta.AddDate = DateTime.Now;
            meta.LastUpdateUserID = UserStatic.UserID;
            meta.LastUpdateDate = DateTime.Now;
            int MetaID = metaDAO.AddMeta(meta);
            LogDAO.AddLog(General.ProcessType.MetaAdd, General.TableName.Meta, MetaID);
            if (MetaID != 0)
            {
                return true;
            }
            return false;
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> metaDTOs = new List<MetaDTO>();
            metaDTOs = metaDAO.GetMetaData();
            return metaDTOs;
        }

        public MetaDTO GetMetaWithID(int iD)
        {
            MetaDTO metaDTO = new MetaDTO();
            metaDTO = metaDAO.GetMetaWithID(iD);
            return metaDTO;
        }

        public bool UpdateMeta(MetaDTO model)
        {
            int MetaID = metaDAO.UpdateMeta(model);
            if(MetaID != 0)
            {
                LogDAO.AddLog(General.ProcessType.MetaUpdate, General.TableName.Meta, MetaID);
                return true;
            }
            return false;
        }
    }
}

using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MetaDAO: PostContext
    {
        public int AddMeta(Meta meta)
        {
            try
            {
                db.Metas.Add(meta);
                db.SaveChanges();
                return meta.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MetaDTO> GetMetaData()
        {
            List<MetaDTO> metaDTOs = new List<MetaDTO>();

            List<Meta> metas = db.Metas.Where(x=> x.isDeleted == false).OrderBy(x=> x.AddDate).ToList();
            foreach(var item in metas)
            {
                MetaDTO dto = new MetaDTO();
                dto.MetaID = item.ID;
                dto.Name = item.Name;
                dto.MetaContent = item.MetaContent;
                metaDTOs.Add(dto);
            }
            
            return metaDTOs;
        }

        public MetaDTO GetMetaWithID(int iD)
        {
           try
            {
                Meta meta = db.Metas.First(x=> x.ID == iD);
                MetaDTO dto = new MetaDTO();
                if(meta != null)
                {
                    dto.MetaID = meta.ID;
                    dto.Name = meta.Name;
                    dto.MetaContent = meta.MetaContent;
                }
                return dto;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateMeta(MetaDTO model)
        {
            try
            {
                Meta meta = db.Metas.First(x => x.ID == model.MetaID);
                meta.Name = model.Name;
                meta.MetaContent = model.MetaContent;
                meta.LastUpdateDate = DateTime.Now;
                meta.LastUpdateUserID = UserStatic.UserID;

                return meta.ID;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

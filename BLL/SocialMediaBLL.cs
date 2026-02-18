using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dAO = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model)
        {
            SocialMedia social = new SocialMedia();
            social.Name = model.Name;
            social.Link = model.Link;
            social.ImagePath = model.ImagePath;
            social.AddDate = DateTime.Now;
            social.LastUpdateUserID = UserStatic.UserID;
            social.LastUpdateDate = DateTime.Now;
            int socialMediaID = dAO.AddSocialMedia(social);
            if(socialMediaID != 0)
            {
                LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, socialMediaID);
                return true;
            }
            return false;
        }

        public List<SocialMediaDTO> GetSocialMedia()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = dAO.GetSocialMedia();
            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int iD)
        {
            SocialMediaDTO dto = dAO.GetSocialMediaWithID(iD);
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, dto.ID);
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            string oldImagePath = dAO.UpdateSocialMedia(model);
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, model.ID);
            return oldImagePath;
        }
    }
}

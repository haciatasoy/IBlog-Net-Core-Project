using AutoMapper;
using DTOLayer.DTOs.Blog;
using DTOLayer.DTOs.Comment;
using DTOLayer.DTOs.Contact;
using DTOLayer.DTOs.Kategori;
using DTOLayer.DTOs.Login;
using DTOLayer.DTOs.Register;
using DTOLayer.DTOs.Writer;
using EntityLayer.Concreate;

namespace IBlog.Mapping
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<RegisterCreateDto, AppUser>().ReverseMap();
            CreateMap<CreateBlogDto, Blog>().ReverseMap();
            CreateMap<CategoryAddDto,Category>().ReverseMap();
            CreateMap<WriterUpdateDto,AppUser>().ReverseMap();
            CreateMap<AddCommentDto,BlogComment>().ReverseMap();
			CreateMap<AddContactDto, Contact>().ReverseMap();
            CreateMap<LoginViewDto, AppUser>().ReverseMap();
        }
       
    }
}

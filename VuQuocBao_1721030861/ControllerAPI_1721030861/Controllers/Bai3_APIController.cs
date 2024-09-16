using AutoMapper;
using ControllerAPI_1721030861.Database;
using ControllerAPI_1721030861.Database.Models.Bai2;
using ControllerAPI_1721030861.Database.Models.Bai3;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControllerAPI_1721030861.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Bai3_APIController : ControllerBase
    {
        private readonly CmsContext _context;
        private readonly IMapper _mapper;
        public Bai3_APIController(CmsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Image
        [HttpGet]
        public async Task<ActionResult<ImageDTO>> GetImage(int ImageId)
        {
            var image = await _context.Images.FindAsync(ImageId);
            if (image is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }
            return _mapper.Map<ImageDTO>(image);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetAllImages()
        {
            var image = await _context.Images.ToListAsync();
            return _mapper.Map<List<ImageDTO>>(image);
        }
        [HttpPost]
        public async Task<ActionResult<Image>> CreateImage(ImageDTO image)
        {
            var NewImage = _mapper.Map<Image>(image);

            var newImageId = _context.Images.Max(x => x.Id) + 1;
            NewImage.Id = newImageId;

            _context.Images.Add(NewImage);

            await _context.SaveChangesAsync();

            return NewImage;
        }
        [HttpPut]
        public async Task<ActionResult<Image>> EditImage(int ImageId, ImageDTO image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Image = await _context.Images.FindAsync(ImageId);
            if (Image is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewImage = _mapper.Map<Image>(image);
            NewImage.Id = ImageId;

            _context.Images.Update(NewImage);

            await _context.SaveChangesAsync();

            return Image;
        }
        [HttpDelete]
        public async Task<ActionResult<Image>> DeleteImage(int ImageId)
        {
            var Image = await _context.Images.FindAsync(ImageId);
            if (Image is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }
            _context.Images.Remove(Image);

            await _context.SaveChangesAsync();

            return Image;
        }
        #endregion

        #region Portal
        [HttpGet]
        public async Task<ActionResult<PortalDTO>> GetPortal(int PortalId)
        {
            var portal = await _context.Portals.FindAsync(PortalId);
            if (portal is null)
            {
                ModelState.AddModelError("PortalId", "Invalid PortalId");
                return NotFound(ModelState);
            }
            return _mapper.Map<PortalDTO>(portal);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortalDTO>>> GetAllPortals()
        {
            var portal = await _context.Portals.ToListAsync();
            return _mapper.Map<List<PortalDTO>>(portal);
        }
        [HttpPost]
        public async Task<ActionResult<Portal>> CreatePortal(PortalDTO portal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Addresses.FindAsync(portal.AddressId) is null)
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound(ModelState);
            }
            if (await _context.Images.FindAsync(portal.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewPortal = _mapper.Map<Portal>(portal);

            var newPortalId = _context.Portals.Max(x => x.Id) + 1;
            NewPortal.Id = newPortalId;

            _context.Portals.Add(NewPortal);

            await _context.SaveChangesAsync();

            return NewPortal;
        }
        [HttpPut]
        public async Task<ActionResult<Portal>> EditPortal(int PortalId, PortalDTO portal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Portals.FindAsync(PortalId) is null)
            {
                ModelState.AddModelError("PortalId", "Invalid PortalId");
                return NotFound(ModelState);
            }
            if (await _context.Addresses.FindAsync(portal.AddressId) is null)
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound(ModelState);
            }
            if (await _context.Images.FindAsync(portal.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewPortal = _mapper.Map<Portal>(portal);
            NewPortal.Id = PortalId;

            _context.Portals.Update(NewPortal);

            await _context.SaveChangesAsync();

            return NewPortal;
        }
        [HttpDelete]
        public async Task<ActionResult<Portal>> DeletePortal(int PortalId)
        {
            var Portal = await _context.Portals.FindAsync(PortalId);
            if (Portal is null)
            {
                ModelState.AddModelError("PortalId", "Invalid PortalId");
                return NotFound(ModelState);
            }
            _context.Portals.Remove(Portal);

            await _context.SaveChangesAsync();

            return Portal;
        }
        #endregion

        #region Contact
        [HttpGet]
        public async Task<ActionResult<ContactDTO>> GetContact(int ContactId)
        {
            var contact = await _context.Contacts.FindAsync(ContactId);
            if (contact is null)
            {
                ModelState.AddModelError("ContactId", "Invalid ContactId");
                return NotFound(ModelState);
            }
            return _mapper.Map<ContactDTO>(contact);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetAllContacts()
        {
            var contact = await _context.Contacts.ToListAsync();
            return _mapper.Map<List<ContactDTO>>(contact);
        }
        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact(ContactDTO contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Accounts.FindAsync(contact.AccountId) is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }
            if (await _context.Accounts.FindAsync(contact.AdviseId) is null)
            {
                ModelState.AddModelError("AdviseId", "Invalid AdviseId");
                return NotFound(ModelState);
            }

            var NewContact = _mapper.Map<Contact>(contact);

            var newContactId = _context.Contacts.Max(x => x.Id) + 1;
            NewContact.Id = newContactId;

            _context.Contacts.Add(NewContact);

            await _context.SaveChangesAsync();

            return NewContact;
        }
        [HttpPut]
        public async Task<ActionResult<Contact>> EditContact(int ContactId, ContactDTO contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Contacts.FindAsync(ContactId) is null)
            {
                ModelState.AddModelError("ContactId", "Invalid ContactId");
                return NotFound(ModelState);
            }
            if (await _context.Accounts.FindAsync(contact.AccountId) is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }
            if (await _context.Accounts.FindAsync(contact.AdviseId) is null)
            {
                ModelState.AddModelError("AdviseId", "Invalid AdviseId");
                return NotFound(ModelState);
            }

            var NewContact = _mapper.Map<Contact>(contact);
            NewContact.Id = ContactId;

            _context.Contacts.Update(NewContact);

            await _context.SaveChangesAsync();

            return NewContact;
        }
        [HttpDelete]
        public async Task<ActionResult<Contact>> DeleteContact(int ContactId)
        {
            var Contact = await _context.Contacts.FindAsync(ContactId);
            if (Contact is null)
            {
                ModelState.AddModelError("ContactId", "Invalid ContactId");
                return NotFound(ModelState);
            }
            _context.Contacts.Remove(Contact);

            await _context.SaveChangesAsync();

            return Contact;
        }
        #endregion

        #region Content
        [HttpGet]
        public async Task<ActionResult<ContentDTO>> GetContent(int ContentId)
        {
            var content = await _context.Contents.FindAsync(ContentId);
            if (content is null)
            {
                ModelState.AddModelError("ContentId", "Invalid ContentId");
                return NotFound(ModelState);
            }
            return _mapper.Map<ContentDTO>(content);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentDTO>>> GetAllContents()
        {
            var content = await _context.Contents.ToListAsync();
            return _mapper.Map<List<ContentDTO>>(content);
        }
        [HttpPost]
        public async Task<ActionResult<Content>> CreateContent(ContentDTO content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Images.FindAsync(content.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewContent = _mapper.Map<Content>(content);

            var newContentId = _context.Contents.Max(x => x.Id) + 1;
            NewContent.Id = newContentId;

            _context.Contents.Add(NewContent);

            await _context.SaveChangesAsync();

            return NewContent;
        }
        [HttpPut]
        public async Task<ActionResult<Content>> EditContent(int ContentId, ContentDTO content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Contents.FindAsync(ContentId) is null)
            {
                ModelState.AddModelError("ContentId", "Invalid ContentId");
                return NotFound(ModelState);
            }
            if (await _context.Images.FindAsync(content.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewContent = _mapper.Map<Content>(content);
            NewContent.Id = ContentId;

            _context.Contents.Update(NewContent);

            await _context.SaveChangesAsync();

            return NewContent;
        }
        [HttpDelete]
        public async Task<ActionResult<Content>> DeleteContent(int ContentId)
        {
            var Content = await _context.Contents.FindAsync(ContentId);
            if (Content is null)
            {
                ModelState.AddModelError("ContentId", "Invalid ContentId");
                return NotFound(ModelState);
            }
            _context.Contents.Remove(Content);

            await _context.SaveChangesAsync();

            return Content;
        }
        #endregion

        #region Banner
        [HttpGet]
        public async Task<ActionResult<BannerDTO>> GetBanner(int BannerId)
        {
            var banner = await _context.Banners.FindAsync(BannerId);
            if (banner is null)
            {
                ModelState.AddModelError("BannerId", "Invalid BannerId");
                return NotFound(ModelState);
            }
            return _mapper.Map<BannerDTO>(banner);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannerDTO>>> GetAllBanners()
        {
            var banner = await _context.Banners.ToListAsync();
            return _mapper.Map<List<BannerDTO>>(banner);
        }
        [HttpPost]
        public async Task<ActionResult<Banner>> CreateBanner(BannerDTO banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Images.FindAsync(banner.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewBanner = _mapper.Map<Banner>(banner);

            var newBannerId = _context.Banners.Max(x => x.Id) + 1;
            NewBanner.Id = newBannerId;

            _context.Banners.Add(NewBanner);

            await _context.SaveChangesAsync();

            return NewBanner;
        }
        [HttpPut]
        public async Task<ActionResult<Banner>> EditBanner(int BannerId, BannerDTO banner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Banners.FindAsync(BannerId) is null)
            {
                ModelState.AddModelError("BannerId", "Invalid BannerId");
                return NotFound(ModelState);
            }
            if (await _context.Images.FindAsync(banner.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewBanner = _mapper.Map<Banner>(banner);
            NewBanner.Id = BannerId;

            _context.Banners.Update(NewBanner);

            await _context.SaveChangesAsync();

            return NewBanner;
        }
        [HttpDelete]
        public async Task<ActionResult<Banner>> DeleteBanner(int BannerId)
        {
            var Banner = await _context.Banners.FindAsync(BannerId);
            if (Banner is null)
            {
                ModelState.AddModelError("BannerId", "Invalid BannerId");
                return NotFound(ModelState);
            }
            _context.Banners.Remove(Banner);

            await _context.SaveChangesAsync();

            return Banner;
        }
        #endregion

        #region Comment
        [HttpGet]
        public async Task<ActionResult<CommentDTO>> GetComment(int CommentId)
        {
            var comment = await _context.Comments.FindAsync(CommentId);
            if (comment is null)
            {
                ModelState.AddModelError("CommentId", "Invalid CommentId");
                return NotFound(ModelState);
            }
            return _mapper.Map<CommentDTO>(comment);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComments()
        {
            var comment = await _context.Comments.ToListAsync();
            return _mapper.Map<List<CommentDTO>>(comment);
        }
        [HttpPost]
        public async Task<ActionResult<Comment>> CreateComment(CommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Accounts.FindAsync(comment.AccountId) is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }

            var NewComment = _mapper.Map<Comment>(comment);

            var newCommentId = _context.Comments.Max(x => x.Id) + 1;
            NewComment.Id = newCommentId;

            _context.Comments.Add(NewComment);

            await _context.SaveChangesAsync();

            return NewComment;
        }
        [HttpPut]
        public async Task<ActionResult<Comment>> EditComment(int CommentId, CommentDTO comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Comments.FindAsync(CommentId) is null)
            {
                ModelState.AddModelError("CommentId", "Invalid CommentId");
                return NotFound(ModelState);
            }
            if (await _context.Accounts.FindAsync(comment.AccountId) is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }

            var NewComment = _mapper.Map<Comment>(comment);
            NewComment.Id = CommentId;

            _context.Comments.Update(NewComment);

            await _context.SaveChangesAsync();

            return NewComment;
        }
        [HttpDelete]
        public async Task<ActionResult<Comment>> DeleteComment(int CommentId)
        {
            var Comment = await _context.Comments.FindAsync(CommentId);
            if (Comment is null)
            {
                ModelState.AddModelError("CommentId", "Invalid CommentId");
                return NotFound(ModelState);
            }
            _context.Comments.Remove(Comment);

            await _context.SaveChangesAsync();

            return Comment;
        }
        #endregion

        #region Account
        [HttpGet]
        public async Task<ActionResult<AccountDTO>> GetAccount(int AccountId)
        {
            var account = await _context.Accounts.FindAsync(AccountId);
            if (account is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }
            return _mapper.Map<AccountDTO>(account);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountDTO>>> GetAllAccounts()
        {
            var account = await _context.Accounts.ToListAsync();
            return _mapper.Map<List<AccountDTO>>(account);
        }
        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount(AccountDTO account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (account.AddressId is not null && await _context.Addresses.FindAsync(account.AddressId) is null)
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound(ModelState);
            }
            if (account.ImageId is not null && await _context.Images.FindAsync(account.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewAccount = _mapper.Map<Account>(account);

            var newAccountId = _context.Accounts.Max(x => x.Id) + 1;
            NewAccount.Id = newAccountId;

            _context.Accounts.Add(NewAccount);

            await _context.SaveChangesAsync();

            return NewAccount;
        }
        [HttpPut]
        public async Task<ActionResult<Account>> EditAccount(int AccountId, AccountDTO account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _context.Accounts.FindAsync(AccountId) is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }
            if (account.AddressId is not null && await _context.Addresses.FindAsync(account.AddressId) is null)
            {
                ModelState.AddModelError("AddressId", "Invalid AddressId");
                return NotFound(ModelState);
            }
            if (account.ImageId is not null && await _context.Images.FindAsync(account.ImageId) is null)
            {
                ModelState.AddModelError("ImageId", "Invalid ImageId");
                return NotFound(ModelState);
            }

            var NewAccount = _mapper.Map<Account>(account);
            NewAccount.Id = AccountId;

            _context.Accounts.Update(NewAccount);

            await _context.SaveChangesAsync();

            return NewAccount;
        }
        [HttpDelete]
        public async Task<ActionResult<Account>> DeleteAccount(int AccountId)
        {
            var Account = await _context.Accounts.FindAsync(AccountId);
            if (Account is null)
            {
                ModelState.AddModelError("AccountId", "Invalid AccountId");
                return NotFound(ModelState);
            }
            _context.Accounts.Remove(Account);

            await _context.SaveChangesAsync();

            return Account;
        }
        #endregion
    }
}

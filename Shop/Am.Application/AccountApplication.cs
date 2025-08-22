using System.Collections.Generic;
using System.Linq;
using Am.Application.Contracts.Account;
using Am.Domain.AccountAgg;
using Am.Domain.RoleAgg;
using Shop.Application;

namespace Am.Application
{
    public class AccountApplication : IAccountApplication
    {
        private readonly IPasswordHasher _hasher;
        private readonly IAccountRepository _accountRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleRepository _roleRepository;


        public AccountApplication(IPasswordHasher hasher, IAccountRepository accountRepository, IFileUploader fileUploader, IAuthHelper authHelper, IRoleRepository roleRepository)
        {
            _hasher = hasher;
            _accountRepository = accountRepository;
            _fileUploader = fileUploader;
            _authHelper = authHelper;
            _roleRepository = roleRepository;
        }

        public OperationResult ChangePassword(ChangePassword command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.NotFound);

            if (command.Password != command.RePassword)
                return operation.Failed(ApplicationMessages.NotMatch);

            var password = _hasher.Hash(command.Password);
            
            account.ChangePassword(password);
            _accountRepository.SaveChanges();
            
            
            return operation.Succedded();

        }

        public OperationResult Login(Login command)
        {
            var operation = new OperationResult();
            var account = _accountRepository.GetBy(command.UserName);

            if (account == null)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            (bool verified , bool NeedsUpgrade) res = _hasher.Check(account.Password, command.Password);

            if (!res.verified)
                return operation.Failed(ApplicationMessages.WrongUserPass);

            var permissions = _roleRepository.Get(account.RoleId)
                .Permissions
                .Select(x => x.Code)
                .ToList();

            var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.Fullname, account.Username, permissions);
            _authHelper.SignIn(authViewModel);
            
            return operation.Succedded();



        }

        public List<AccountViewModel> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Logout()
        {
            _authHelper.SingOut();
        }

        public OperationResult Create(CreateAccount command)
        {
            var operation = new OperationResult();

            if (_accountRepository.Exists(x => x.Username == command.Username || x.Mobile == command.Mobile))
                return operation.Failed(ApplicationMessages.Duplicate);

            var FileName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePicture");
            var password = _hasher.Hash(command.Password);
            
            var Account = new Account(command.Fullname,command.Username, password, command.Mobile, command.RoleId, FileName);
            _accountRepository.Create(Account);
            _accountRepository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Edit(EditAccount command)
        {
            var operation = new OperationResult();

            var account = _accountRepository.Get(command.Id);
            if (account == null)
                return operation.Failed(ApplicationMessages.NotFound);

            
            var FileName = _fileUploader.Upload(command.ProfilePhoto, "ProfilePicture");
            account.Edit(command.Fullname,command.Username, command.Mobile, command.RoleId, FileName);
            
            _accountRepository.SaveChanges();

            return operation.Succedded();
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            return _accountRepository.Search(searchModel);
        }

        public EditAccount GetDetails(long id)
        {
            return _accountRepository.GetDetails(id);
        }


    }
}
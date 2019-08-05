using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Messages
/// </summary>
public static class Messages
{
    public static string ShowMessage(MessagesEnum msg)
    {
        string Message;
        switch (msg)
        {

            case MessagesEnum.SaveErrorGeneral:
                Message = "!خطا...اطلاعات ذخیره نشد";
                break;
            case MessagesEnum.LoadErrorGeneral:
                Message = "اطلاعات به درستی بارگذاری نشدند.";
                break;
            case MessagesEnum.InvalidBaseID:
                Message = "BaseID اشتباه است.";
                break;
            case MessagesEnum.InvalidID:
                Message = "شناسه صحیح نیست.";
                break;
            case MessagesEnum.SavedSuccessfuly:
                Message = "اطلاعات با موفقیت ذخیره شد.";
                break;
            case MessagesEnum.PrimaryInfomationSavedSuccessfuly:
                Message = "اطلاعات با موفقیت ذخیره شد.";
                break;
            case MessagesEnum.InvalidLogin:
                Message = "نام کاربری یا کلمه عبور اشتباه است";
                break;
            case MessagesEnum.DeletedSuccessfuly:
                Message = "رکورد مورد نظر حذف شد.";
                break;
            case MessagesEnum.PassandConfDoNotMatch:
                Message = "کلمه عبور جدید و تایید کلمه عبور یکی نیستند";
                break;
            case MessagesEnum.InvalidPassword:
                Message = "کلمه عبور اشتباه است";
                break;
            case MessagesEnum.PleaseSelectYourRole:
                Message = "لطفا نقش خود را انتخاب کنید";
                break;
            case MessagesEnum.TraspasserNotSet:
                Message = "شخص متخلف مشخص نشده است";
                break;
            case MessagesEnum.InformationSuccessfullySaved:
                Message = "اطلاعات با موفقیت ذخیره شد";
                break;
            case MessagesEnum.PasswordSuccessfullyChanged:
                Message = "کلمه عبور با موفقیت تغییر کرد";
                break;
            case MessagesEnum.InvalidUsernameORPassword:
                Message = "نام کاربری یا کلمه عبور اشتباه است";
                break;
            case MessagesEnum.UnableToAddDuplicateRecord:
                Message = "خطا در ذخیره اطلاعات - رکورد تکراری";
                break;
            case MessagesEnum.ErrorSavingData:
                Message = "بروز خطا در ذخیره اطلاعات";
                break;
            case MessagesEnum.ContentRateReCalculated:
                Message = "امتیاز محتواهای این نشریه مجددا محاسبه شد";
                break;
            case MessagesEnum.ErrorWhileDelete:
                Message = "رکورد مرتبط دارای اطلاعات مرتبط میباشد و قابل حذف نیست";
                break;
            case MessagesEnum.ErrorInsertDuplicate:
                Message = "ایجاد رکورد تکراری با خطا مواجه شد";
                break;
            case MessagesEnum.ErrorDuplicateJournalContent:
                Message = "محتوایی با این عنوان قبلا وارد شده است";
                break;
            case MessagesEnum.ErrorPleaseFillJournalField:
                Message = "لطفا فیلد نشریه را پر کنید";
                break;
            case MessagesEnum.ErrorPleaseFillCommentField:
                Message = "لطفا فیلد نظر را پر کنید";
                break;
            case MessagesEnum.InfoYourCommentSuccessfullySaved:
                Message = "نظر شما با موفقیت ثبت شد";
                break;
            case MessagesEnum.ErrorNotValidFileName:
                Message = "پسوند فایل معتبر نیست";
                break;
            case MessagesEnum.ErrorNoAccess:
                Message = "شما به اطلاعات این بخش دسترسی ندارید";
                break;
            case MessagesEnum.MoshaverNotConfirmed:
                Message = "وضعیت ثبت نام شما بررسی نشده است. لطفاً با اتحادیه شهرستان خود تماس بگیرید";
                break;
            case MessagesEnum.UserNotConfirmedYet:
                Message = "وضعیت ثبت نام شما بررسی نشده است. لطفاً با شماره (۰۲۱)۶۶۵۹۴۶۱۹ تماس بگیرید";
                break;
            case MessagesEnum.UserIsDisabled:
                Message = "وضعیت ثبت نام شما بررسی نشده است. لطفاً با شماره (۰۲۱)۶۶۵۹۴۶۱۹ تماس بگیرید";
                break;
            case MessagesEnum.UserNotConfirmed:
                Message = "ثبت نام شما تایید نشده است";
                break;
            case MessagesEnum.NoGroupAssigned:
                Message = "اطلاعات کاربری شما ناقص است. لطفاً با مرکز پشتیبانی تماس بگیرید. کد خطا: 120";
                break;

            case MessagesEnum.NoZoneAssigned:
                Message = "اطلاعات کاربری شما ناقص است. لطفاً با مرکز پشتیبانی تماس بگیرید.  کد خطا: 121";
                break;
            case MessagesEnum.NoMoshaverAmlakAssigned:
                Message = "اطلاعات کاربری شما ناقص است. لطفاً با مرکز پشتیبانی تماس بگیرید.  کد خطا: 122";
                break;
            case MessagesEnum.IllegalCharacterDetected:
                Message = "اطلاعاتی که وارد کردید. شامل کاراکترهای غیر مجاز است";
                break;

            case MessagesEnum.BlockedUser:
                Message = "ورود شما به سیستم مسدود می باشد";
                break;

            case MessagesEnum.InvalidLoginCount:
                Message = "سعی بیش از حد مجاز برای ورود به سیستم";
                break;

            case MessagesEnum.InvalidCaptcha:
                Message = "کد امنیتی وارد شده صحیح نیست";
                break;
            case MessagesEnum.IEOnly:
                Message = "جهت ثبت نام لطفاً با مرورگر اینترنت اکسپلورر وارد سایت شوید";
                break;
            case MessagesEnum.uniqueViolation:
                Message = "اين شماره سريالها براي اين نوع قرارداد در اين شهر قبلا ثبت شده است ";
                break;
            default:
                Message = "";
                break;
        }
        return Message;
    }
}
public enum MessagesEnum
{
    None,
    SaveErrorGeneral,
    LoadErrorGeneral,
    InvalidBaseID,
    InvalidID,
    SavedSuccessfuly,
    PrimaryInfomationSavedSuccessfuly,
    InvalidLogin,
    DeletedSuccessfuly,
    PassandConfDoNotMatch,
    PasswordSuccessfullyChanged,
    InvalidPassword,
    PleaseSelectYourRole,
    TraspasserNotSet,
    InformationSuccessfullySaved,
    NewPassAndConfPassDoNotMatch,
    YourCurrentPasswordIsInvalid,
    UsernameTaken,
    FileExtensionIsInvalid,
    FileSizeExeed,
    InvalidUsernameORPassword,
    UnableToAddDuplicateRecord,
    ErrorSavingData,
    ContentRateReCalculated,
    ErrorWhileDelete,
    ErrorInsertDuplicate,
    ErrorDuplicateJournalContent,
    ErrorPleaseFillJournalField,
    ErrorPleaseFillCommentField,
    InfoYourCommentSuccessfullySaved,
    ErrorNotValidFileName,
    ErrorNoAccess,
    MoshaverNotConfirmed,
    UserNotConfirmed,
    UserNotConfirmedYet,
    UserIsDisabled,
    NoGroupAssigned,
    NoZoneAssigned,
    NoMoshaverAmlakAssigned,
    IllegalCharacterDetected,
    BlockedUser,
    InvalidLoginCount,
    InvalidCaptcha,
    IEOnly,
    uniqueViolation
}



function wsPKIlogin(username, passhash, captchaText, RawUrl, UrlHost) {
    var a = PKI.CheckUserValidation(username, passhash, captchaText, RawUrl, UrlHost, OnCheckUserValidationSuccess, OnCheckUserValidationFailure);
}

function OnCheckUserValidationSuccess(result) {

    if (SetOutput(result)) {

        if (!result.NeedCert) {
            PKI.Login(result, OnloginSuccess, OnloginFailure);
        }
        else 
            GetServerTime(result);
        
    }
}

function OnCheckUserValidationFailure(error) {
    ErrorHandlig(error);
}

function OnloginSuccess(result) {
    if (SetOutput(result)) {
        window.location = "../Main.aspx";

    }
    else {
        //        window.location = "/Forms/Users/CorrectionForm.aspx";
        //        window.location.href = "/Forms/Users/CorrectionForm.aspx";
        //        window.location.assign("/Forms/Users/CorrectionForm.aspx");
        //        window.location.reload();
    }
}

function OnloginFailure(error) { 
    ErrorHandlig(error);
}

function OnHashAndcheckvalSuccess(result) {
    if (SetOutput(result))
        window.location = "../Main.aspx";
}

function OnHashAndcheckvalFailure(error) {
    ErrorHandlig(error);
}

function GetServerTime(setting) {
    PKI.GetServerTime(setting, OnSuccess1, OnFailure1);
}

function OnSuccess1(result) {
    SigningProcess(result);
}

function OnFailure1(error) {
    ErrorHandlig(error);
}

//========================Javascript Method============================
function SigningProcess(result) {
    if (result.Tumbprint != null) {

        if (_PKIClass.CheckToken(result.Tumbprint)) {

            var passHash = _PKIClass.DoHash(result.Pass);
            var concat = "<login><Password>" + passHash + "</Password><UserName>" + result.UserName +
            "</UserName><Captcha>" + result.CaptchaText + "</Captcha></login>";
            var sign = _PKIClass.DoSign(concat, result.ServerTime);
            if (sign != '')
                PKI.HashAndcheckval(result, sign, OnHashAndcheckvalSuccess, OnHashAndcheckvalFailure);
        }
        else
        // SetMessage('لطفا امضای دیجیتال خود را به سیستم متصل نمایید');
            return false;
    }
    else
        SetMessage('امضای دیجیتال شما در سامانه تعریف نشده است');
}



function SetOutput(result) {
    if (result.MsgText != "" && result.MsgText != null) {
        if (result.MsgText == "IncorrectData") {
            document.location = "/Forms/Users/CorrectionForm.aspx";
            return false;
        }
        SetMessage(result.MsgText)
        SetVisibility(result.MsgVisible, 'msgBox');
        document.getElementById('captcha_imgCaptcha').src = result.CaptchaImageUrl;
        document.getElementById('captcha_txtCaptcha').value = "";
        return false;
    }
    else {

        return true;
    }
}

function SetMessage(msg) {
    document.getElementById('msgBox').innerText = msg;
}

function SetVisibility(visible, ID) {
    var Visibility = (visible == true) ? "visible" : "hidden";
    document.getElementById(ID).style.visibility = Visibility;
}

function ErrorHandlig(error) {
    var msg = String.format(
    "Stack trace: {0}\nService Error: {0}\nMessage: {1}\nStatus Code: {2}\nException Type: {3}\nTimed out: {4}",
    error.get_stackTrace(),
    error.get_message(),
    error.get_statusCode(),
    error.get_exceptionType(),
    error.get_timedOut()
    );

    alert(msg);
}

//==========================PKI Class==============================

var PKIClass = function () {

    this.CAPICOM_STORE_OPEN_READ_ONLY = 0;
    this.CAPICOM_CURRENT_USER_STORE = 2;
    this.CAPICOM_SMART_CARD_USER_STORE = 4;
    this.CAPICOM_CERTIFICATE_FIND_SHA1_HASH = 0;
    this.CAPICOM_CERTIFICATE_FIND_EXTENDED_PROPERTY = 6;
    this.CAPICOM_CERTIFICATE_FIND_TIME_VALID = 9;
    this.CAPICOM_CERTIFICATE_FIND_KEY_USAGE = 12;
    this.CAPICOM_DIGITAL_SIGNATURE_KEY_USAGE = 0x00000080;
    this.CAPICOM_AUTHENTICATED_ATTRIBUTE_SIGNING_TIME = 0;
    this.CAPICOM_INFO_SUBJECT_SIMPLE_NAME = 0;
    this.CAPICOM_ENCODE_BASE64 = 0;
    this.CAPICOM_E_CANCELLED = -2138568446;
    this.CERT_KEY_SPEC_PROP_ID = 6;
    this.CAPICOM_HASH_ALGORITHM_SHA1 = 0;

    this.CAPICOM_CHECK_NONE = 0;
    this.CAPICOM_CHECK_TRUSTED_ROOT = 1;
    this.CAPICOM_CHECK_TIME_VALIDITY = 2;
    this.CAPICOM_CHECK_SIGNATURE_VALIDITY = 4;
    this.CAPICOM_CHECK_ONLINE_REVOCATION_STATUS = 8;
    this.CAPICOM_CHECK_OFFLINE_REVOCATION_STATUS = 16;

    this.CAPICOM_TRUST_IS_NOT_TIME_VALID = 1;
    this.CAPICOM_TRUST_IS_NOT_TIME_NESTED = 2;
    this.CAPICOM_TRUST_IS_REVOKED = 4;
    this.CAPICOM_TRUST_IS_NOT_SIGNATURE_VALID = 8;
    this.CAPICOM_TRUST_IS_NOT_VALID_FOR_USAGE = 16;
    this.CAPICOM_TRUST_IS_UNTRUSTED_ROOT = 32;
    this.CAPICOM_TRUST_REVOCATION_STATUS_UNKNOWN = 64;
    this.CAPICOM_TRUST_IS_CYCLIC = 128;
    this.CAPICOM_TRUST_IS_PARTIAL_CHAIN = 65536;
    this.CAPICOM_TRUST_CTL_IS_NOT_TIME_VALID = 131072;
    this.CAPICOM_TRUST_CTL_IS_NOT_SIGNATURE_VALID = 262144;
    this.CAPICOM_TRUST_CTL_IS_NOT_VALID_FOR_USAGE = 524288;
    this.CAPICOM_VERIFY_SIGNATURE_ONLY = 0;

    this.CAPICOM_ENCRYPTION_ALGORITHM_RC2 = 0;
    this.CAPICOM_ENCRYPTION_ALGORITHM_RC4 = 1;
    this.CAPICOM_ENCRYPTION_ALGORITHM_DES = 2;
    this.CAPICOM_ENCRYPTION_ALGORITHM_3DES = 3;
    this.CAPICOM_ENCRYPTION_ALGORITHM_AES = 4;
    this.CAPICOM_ENCRYPTION_KEY_LENGTH_MAXIMUM = 0;
    this.CAPICOM_ENCRYPTION_KEY_LENGTH_40_BITS = 1;
    this.CAPICOM_ENCRYPTION_KEY_LENGTH_56_BITS = 2;
    this.CAPICOM_ENCRYPTION_KEY_LENGTH_128_BITS = 3;
    this.CAPICOM_ENCRYPTION_KEY_LENGTH_192_BITS = 4;
    this.CAPICOM_ENCRYPTION_KEY_LENGTH_256_BITS = 5;
    this.CAPICOM_SECRET_PASSWORD = 0;
    this.CAPICOM_ENCODE_BASE64 = 0;
    this.CAPICOM_ENCODE_BINARY = 1;
    this.CAPICOM_ENCODE_ANY = -1;
    this.CAPICOM_VERIFY_SIGNATURE_AND_CERTIFICATE = 1;
    this.CAPICOM_CERTIFICATE_FIND_SHA1_HASH = 0;

    alert(IsCAPICOMInstalled());
    this.MyStore = new ActiveXObject("CAPICOM.Store");
    this.FilteredCertificates = new ActiveXObject("CAPICOM.Certificates");
    this.Certificate = null

    this.req;
    this.sender;
}

PKIClass.prototype = {

    DoHash: function(val) {
        var HashedData = new ActiveXObject("CAPICOM.HashedData");
        HashedData.Algorithm = this.CAPICOM_HASH_ALGORITHM_SHA1;
        HashedData.Hash(val);
        return HashedData.Value;
    },

    IsCAPICOMInstalled: function() {
        if (typeof (this.oCAPICOM) == "object") {
            if ((this.oCAPICOM.object != null)) {
                return true;
            }
        }
    },

    checkCertStatus: function(oCertificate) {
        if (this.IsCAPICOMInstalled) {
            var Certificate = oCertificate;

            window.status = "در حال بررسی وضعیت اعتبار گواهی...";
            Certificate.IsValid().CheckFlag = (this.CAPICOM_CHECK_TIME_VALIDITY | this.CAPICOM_CHECK_SIGNATURE_VALIDITY); //| CAPICOM_CHECK_ONLINE_REVOCATION_STATUS);
            if (Certificate.IsValid().Result == true) {
                window.status = "";
                return true;
            }
            else {
                var Chain = new ActiveXObject("CAPICOM.Chain");
                Chain.Build(Certificate)

                window.status = "";
                if (this.CAPICOM_TRUST_IS_NOT_SIGNATURE_VALID & Chain.Status) {
                    //SetMessage("CryptoAPI found a problem with the signature on '" + Certificate.GetInfo(this.CAPICOM_INFO_SUBJECT_SIMPLE_NAME) + "'");
                    SetMessage('فرآیند امضای دیجیتال امکان پذیر نمی باشد 401')
                    return false;
                }
                if ((this.CAPICOM_TRUST_IS_UNTRUSTED_ROOT & Chain.Status) || (this.CAPICOM_TRUST_IS_PARTIAL_CHAIN & Chain.Status)) {
                    //SetMessage("CryptoAPI was unable to chain '" + Certificate.GetInfo(this.CAPICOM_INFO_SUBJECT_SIMPLE_NAME) + "' to a trusted authority");
                    SetMessage('فرآیند امضای دیجیتال امکان پذیر نمی باشد 402')
                    return false;
                }
                if (this.CAPICOM_TRUST_IS_CYCLIC & Chain.Status) {
                    //SetMessage("CAPICOM_TRUST_IS_CYCLIC");
                    SetMessage('403 فرآیند امضای دیجیتال امکان پذیر نمی باشد')
                    return false;
                }
                if (this.CAPICOM_TRUST_CTL_IS_NOT_TIME_VALID & Chain.Status) {
                    SetMessage("404 تاریخ اعتبار گواهی نامه دیجیتال شما به پایان رسیده است");
                    return false;
                }
                if (this.CAPICOM_TRUST_CTL_IS_NOT_SIGNATURE_VALID & Chain.Status) {
                    SetMessage("گواهی نامه دیجیتال شما اعتبار ندارد 405");
                    return false;
                }
                if (this.CAPICOM_TRUST_CTL_IS_NOT_VALID_FOR_USAGE & Chain.Status) {
                    //SetMessage("CAPICOM_TRUST_CTL_IS_NOT_VALID_FOR_USAGE");
                    SetMessage('فرآیند امضای دیجیتال امکان پذیر نمی باشد 406 ')
                    return false;
                }
                if (this.CAPICOM_TRUST_IS_NOT_TIME_VALID & Chain.Status) {
                    SetMessage("تاریخ اعتبار گواهی نامه دیجیتال شما به پایان رسیده است 407 ");
                    return false;
                }
                if (this.CAPICOM_TRUST_IS_NOT_TIME_NESTED & Chain.Status) {
                    //SetMessage("CAPICOM_TRUST_IS_NOT_TIME_NESTED");
                    SetMessage('408 فرآیند امضای دیجیتال امکان پذیر نمی باشد')
                    return false;
                }
                if (this.CAPICOM_TRUST_IS_NOT_VALID_FOR_USAGE & Chain.Status) {
                    //SetMessage("CAPICOM_TRUST_IS_NOT_VALID_FOR_USAGE");
                    SetMessage('409 فرآیند امضای دیجیتال امکان پذیر نمی باشد')
                    return false;
                }
                if (this.CAPICOM_TRUST_IS_REVOKED & Chain.Status) {
                    //SetMessage("CryptoAPI determined that '" + Certificate.GetInfo(this.CAPICOM_INFO_SUBJECT_SIMPLE_NAME) + "' or one of its issuers was revoked.");
                    SetMessage('410 فرآیند امضای دیجیتال امکان پذیر نمی باشد')
                    return false;
                }
                if (this.CAPICOM_TRUST_REVOCATION_STATUS_UNKNOWN & Chain.Status) {
                    SetMessage('اعتبار گواهي ديجيتال ' + Certificate.GetInfo(this.CAPICOM_INFO_SUBJECT_SIMPLE_NAME) + 'قابل تاييد نمي باشد')
                    return false;
                }
            }
        }
    },

    CheckToken: function(DBThumbPrint) {
        try {
            alert("را وارد نمایید SO PIN در قسمت ورود رمز Yes کاربر گرامی، لطفاً در قسمت بعد پس از کلیک بر روی گزینه");
            
            this.MyStore.Open(2, "My", 0);

            if (this.MyStore.Certificates.Count != 0) {
                this.FilteredCertificates = this.MyStore.Certificates.Find(this.CAPICOM_CERTIFICATE_FIND_SHA1_HASH,
            DBThumbPrint, false);

                this.Certificate = new ActiveXObject("CAPICOM.Certificate");
                this.Certificate = this.FilteredCertificates.Item(1);


                if (this.Certificate != null) {
                    if (this.checkCertStatus(this.Certificate)) {
                        return true;
                    }
                    else
                        return false;
                }
                else {
                    SetMessage('گواهي ديجيتال موجود در سخت افزار متعلق به كد كاربري وارد شده نمي باشد')
                    return false;
                }
            }
            else {
                SetMessage('لطفا سخت افزار حاوي گواهي ديجيتال خود را به كامپيوتر متصل نمائيد')
                return false;
            }

        }
        catch (e) {
            if (e.number != this.CAPICOM_E_CANCELLED) {
                SetMessage("فرآیند امضای دیجیتال امکان پذیر نمی باشد 411");
               // SetMessage(e.description)
                return false;
            }
        }
    },

    CheckSign: function(Message, SignDATA) {
        var Sign = new ActiveXObject("CAPICOM.SignedData");

        // Sign.Content = Message;
        try {
            Sign.Verify(SignDATA, false, this.CAPICOM_VERIFY_SIGNATURE_ONLY)
            //SetMessage('امضا صحيح است');
        } catch (ex) {
            SetMessage(ex.message);
            return false;
        }
        return true;
    },

    DoSign: function(val, ServerTime) {
        var SignedData = new ActiveXObject("CAPICOM.SignedData");
        var Signer = new ActiveXObject("CAPICOM.Signer");
        var TimeAttribute = new ActiveXObject("CAPICOM.Attribute");
        SignedData.Content = val;

        try {
            Signer.Certificate = this.Certificate;
            var Today = ServerTime;
            TimeAttribute.Name = this.CAPICOM_AUTHENTICATED_ATTRIBUTE_SIGNING_TIME;
            TimeAttribute.Value = Today.getVarDate();
            Today = null;
            Signer.AuthenticatedAttributes.Add(TimeAttribute);
            szSignature = SignedData.Sign(Signer, false, this.CAPICOM_ENCODE_BASE64);

            if (this.CheckSign(val, szSignature)) {
                return szSignature
            }
            else
                return '';
        }
        catch (e) {

            SetMessage('فرآیند امضای دیجیتال امکان پذیر نمی باشد')
            return '';
        }
    }
}

var _PKIClass = new PKIClass();



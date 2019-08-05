<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_PageTools" Codebehind="PageTools.ascx.cs" %>
<table border="0" cellpadding="2" cellspacing="0" style="cursor:hand" width="100%">
	<tr>
		<td onclick="CreateBookmarkLink()" style="cursor:hand" width="125">
		<table border="0" cellpadding="2" cellspacing="0" width="100%">
			<tr>
				<td width="5"><img border="0" src="images/newbookmark.jpg" width="12" height="13"></td>
				<td  class=cPersianContent nowrap width="110" align="center"><font color="#466CAE">علامت گذاری صفحه</font></td>
			</tr>
			</table>
		</td>
			<td onclick1="window.print()"  style="cursor:hand" width="60" >
			<table border="0" cellpadding="2" cellspacing="0" width="100%">
			<tr>
				<td><img border="0" src="images/newprint.jpg" width="12" height="13"></td>
				<td  class=cPersianContent align="center"><font color="#466CAE">چاپ</font></td>
			</tr>
			</table>
		</td>
			<td  style="cursor:hand" onclick="window.open('SendEmail.aspx?PagePath=')">
			<table border="0" cellpadding="2" cellspacing="0" width="100%">
			<tr>
				<td valign="bottom" width="5"><img src="images/newcontactus.jpg"></td>
				<td  class=cPersianContent nowrap width="80" align="center"><font color="#466CAE">ارسال e-mail&nbsp;</font></td>
			</tr>
			</table>
			
		</td>
		<td width="99%" style="background-image: url('images/newback6.jpg'); background-repeat: repeat-x; background-position: center bottom"></td>
	</tr>
	<tr><td height=10></td></tr>
</table>

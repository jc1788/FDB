using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
//using System.Text;
using System.DirectoryServices;

using Microsoft.Security.Application;
using System.Net.NetworkInformation;

using System.Data.OleDb;

namespace LdapClass
{
    public class ReadAD
    {
        /* 說明
         * ADAdminUser = 用來查詢AD的帳號;
         * ADAdminPassword = 用來查詢AD的密碼;
         * ADFullPath = "LDAP://" + IP或Domain_Name;
         * ADServer = 由DC所組成的位址，例如DC_Town.DC_City.DC_Root，須拆解成 DC=DC_Root 的形式附加在LDAP查詢字串;
         * */

        ///*以下是 總局AD 的連線資訊*/
        private static string ADAdminUser = "cpcortc";
        private static string ADAdminPassword = "cpc#10~3650D"; //"cpc#10~3650A","ibm#10~36501";
        private static string ADServerName = "10.50.0.11"; //10.50.0.12   ，內網用：10.50.0.47、10.50.0.48
        private static string ADServerContainer = "freeway.gov.tw";
        private static string ADFullPath = "LDAP://" + ADServerName;

        //南區
        private static string ADAdminUser3 = "ad-auth";
        private static string ADAdminPassword3 = "/X11111x@";
        private static string ADServerName3 = "10.53.0.7"; //10.53.0.8
        private static string ADServerContainer3 = "s_expressway.gov";
        private static string ADFullPath3 = "LDAP://" + ADServerName3;

        ////拓建
        //private static string ADAdminUser4 = "Scan";
        //private static string ADAdminPassword4 = "1qaz@WSX";
        //private static string ADServerName4 = "10.55.200.2"; //10.55.200.20
        //private static string ADServerContainer4 = "wfreeway.gov.tw";
        //private static string ADFullPath4 = "LDAP://" + ADServerName4;

        public static bool CheckUser(string sArgU, string sArgP, string deptType)
        {
            bool isPassed = false;
            DirectoryEntry de = GetDe(sArgU, sArgP, deptType);

            if (de != null)
            {
                try
                {
                    string guid = de.NativeGuid;
                    isPassed = true;
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                }
                finally
                {
                    de.Close();
                    de.Dispose();
                }
            }

            return isPassed;
        }

        private static DirectoryEntry GetDe(string deptType)
        {
            string sLdappath = "";
            DirectoryEntry de = null;

            switch (deptType)
            {
                case "南":
                    sLdappath = ADFullPath3 + "/" + GetLDAPDomain("南");
                    de = new DirectoryEntry(sLdappath, ADAdminUser3, ADAdminPassword3);
                    break;
                //case "拓":
                //    sLdappath = ADFullPath4 + "/" + GetLDAPDomain("拓");
                //    de = new DirectoryEntry(sLdappath, ADAdminUser4, ADAdminPassword4);
                //    break;
                default:
                    sLdappath = ADFullPath + "/" + GetLDAPDomain("總");
                    de = new DirectoryEntry(sLdappath, ADAdminUser, ADAdminPassword);
                    break;
            }

            return de;
        }

        private static DirectoryEntry GetDe(string sArgU, string sArgP, string deptType)
        {
            string sLdappath = "";
            DirectoryEntry de = null;

            //sArgU = Encoder.LdapFilterEncode(sArgU);
            //sArgP = Encoder.LdapFilterEncode(sArgP);
            string guidId = "";
            string ldapDomain = "";
            bool isPassed = false;
            string errMsg = "";

            if (string.IsNullOrEmpty(deptType) == true) //如果沒有傳入所在部門資料，則從總局開始判斷，非局內主機登入時會用到
            {
                ldapDomain = GetLDAPDomain("總");
                sLdappath = ADFullPath + "/" + ldapDomain;
                de = new DirectoryEntry(sLdappath, sArgU, sArgP);
                try
                {
                    guidId = de.NativeGuid;
                    isPassed = true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message.ToString();
                    isPassed = false;
                }

                if (de == null || isPassed == false)
                {
                    ldapDomain = GetLDAPDomain("南");
                    sLdappath = ADFullPath3 + "/" + ldapDomain;
                    de = new DirectoryEntry(sLdappath, sArgU, sArgP);
                    try
                    {
                        guidId = de.NativeGuid;
                        isPassed = true;
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message.ToString();
                        isPassed = false;
                    }
                }

                //if (de == null || isPassed == false)
                //{
                //    ldapDomain = GetLDAPDomain("拓");
                //    sLdappath = ADFullPath4 + "/" + ldapDomain;
                //    de = new DirectoryEntry(sLdappath, sArgU, sArgP);
                //    try
                //    {
                //        guidId = de.NativeGuid;
                //        isPassed = true;
                //    }
                //    catch (Exception ex)
                //    {
                //        errMsg = ex.Message.ToString();
                //        isPassed = false;
                //    }
                //}
            }
            else
            {
                ldapDomain = GetLDAPDomain(deptType);

                switch (deptType)
                {
                    case "南":
                        sLdappath = ADFullPath3 + "/" + ldapDomain;
                        break;
                    //case "拓":
                    //    sLdappath = ADFullPath4 + "/" + ldapDomain;
                    //    break;
                    default:
                        sLdappath = ADFullPath + "/" + ldapDomain;
                        break;
                }

                de = new DirectoryEntry(sLdappath, sArgU, sArgP);
                try
                {
                    guidId = de.NativeGuid;
                    isPassed = true;
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message.ToString();
                    isPassed = false;
                }
            }

            return de;
        }

        private static string GetLDAPDomain(string typeData)
        {
            System.Text.StringBuilder LDAPDomain = new System.Text.StringBuilder();
            string[] LDAPDC = null;
            Ping myPing = new Ping();

            switch (typeData)
            {
                case "南":
                    LDAPDC = ADServerContainer3.Split('.');
                    break;
                //case "拓":
                //    PingReply myPingReply = myPing.Send(ADServerName4);
                //    if (myPingReply.Status == IPStatus.Success)
                //    {
                //        LDAPDC = ADServerContainer4.Split('.');
                //    }
                //    break;
                default:
                    LDAPDC = ADServerContainer.Split('.');
                    break;
            }

            if (LDAPDC != null)
            {
                for (int cnt = 0; cnt < LDAPDC.GetUpperBound(0) + 1; cnt++)
                {
                    LDAPDomain.Append("DC=" + LDAPDC[cnt]);
                    if (cnt < LDAPDC.GetUpperBound(0))
                    {
                        LDAPDomain.Append(",");
                    }
                }
            }

            return LDAPDomain.ToString();
        }

        public static string DataExists(string sType, string sData, string deptType, HttpResponse oResponse)
        {
            return ChkDataExists(sType, sData, deptType, oResponse);
        }

        public static string ChkUserNameExists(string userName, string deptType, HttpResponse oResponse)
        {
            string sReturn = "No";

            if (userName != "")
            {
                string oneName = userName[0].ToString();
                string twoName = userName.Substring(1, userName.Length - 1);

                DataTable oDt = getDataInfoOne("sn", oneName, "givenname", twoName, deptType, oResponse);
                if (oDt.Rows.Count == 0)
                {
                    sReturn = ChkDataExists("displayname", userName, deptType, oResponse); //正式 displayname , 測試 givenName
                }
                else
                {
                    sReturn = "Yes";
                }
            }

            return sReturn;
        }

        public static DataTable GetUserDataMuti(string userName, string deptType)
        {
            DataTable backData = new DataTable();
            DataTable dtData = null;
            int lenDt = 0;

            string oneName = userName[0].ToString();
            string twoName = userName.Substring(1, userName.Length - 1);
            dtData = getDataInfoMuti("sn", oneName, "givenname", twoName, deptType);
            backData = dtData.Clone();

            lenDt = dtData.Rows.Count;
            if (lenDt > 0)
            {
                for (int cnt = 0; cnt < lenDt; cnt++)
                {
                    DataRow oAddRow = backData.NewRow();
                    oAddRow.ItemArray = dtData.Rows[cnt].ItemArray;
                    backData.Rows.Add(oAddRow);
                }
            }

            dtData = getDataInfoMuti("displayname", userName, deptType); //正式 displayname , 測試 givenName
            lenDt = dtData.Rows.Count;
            if (lenDt > 0)
            {
                DataRow rowD = null;
                DataRow[] rowS = null;

                for (int cnt = 0; cnt < lenDt; cnt++)
                {
                    rowD = dtData.Rows[cnt];
                    rowS = backData.Select("userLdapNo='" + rowD["userLdapNo"].ToString() + "'");

                    if (rowS.Length == 0)
                    {
                        DataRow oAddRow = backData.NewRow();
                        oAddRow.ItemArray = rowD.ItemArray;
                        backData.Rows.Add(oAddRow);
                    }
                }
            }

            return backData;
        }

        public static string ChkLdapIdExists(string ldapId, string deptType, HttpResponse oResponse)
        {
            string sReturn = "No";

            if (ldapId != "")
            {
                sReturn = ChkDataExists("sAMAccountName", ldapId, deptType, oResponse);
            }

            return sReturn;
        }

        public static DataTable GetLdapIdData(string ldapId, string deptType)
        {
            DataTable dtData = getDataInfoOne("sAMAccountName", ldapId, deptType);
            return dtData;
        }

        private static string ChkDataExists(string sType, string sData, string deptType, HttpResponse oResponse)
        {
            string sReturn = "";

            try
            {
                DataTable oDt = getDataInfoOne(sType, sData, deptType, oResponse);

                if (oDt.Rows.Count == 0)
                {
                    sReturn = "No";
                }
                else
                {
                    sReturn = "Yes";
                }
            }
            catch (DirectoryServicesCOMException ComEx)
            {
                sReturn = "Err";
                if (oResponse != null)
                {
                    oResponse.Write(ComEx.Message + "<br />");
                    oResponse.Write(ComEx.ExtendedError + "<br />");
                    oResponse.Write(ComEx.ExtendedErrorMessage + "<br />");
                }
            }
            catch (Exception Ex)
            {
                sReturn = "Err";
                if (oResponse != null) { oResponse.Write(Ex.Message + "<br />"); }
            }

            return sReturn;
        }

        public static string ReplaceLDAPServ(string sValue)
        {
            //取代字元中輸入LDAP的保留字元：(,),*,¥,NULL
            sValue = sValue.ToUpper().Replace("NULL", "").Replace("(", "").Replace(")", "").Replace("*", "").Replace("¥", "");

            return sValue;
        }

        public static DataTable getDataInfoOne(string sType, string sData, string deptType)
        {
            return getDataInfoOne(sType, sData, "", "", deptType);
        }

        public static DataTable getDataInfoMuti(string sType, string sData, string deptType)
        {
            return getDataInfoMuti(sType, sData, "", "", deptType);
        }

        public static DataTable getDataInfoOne(string sType, string sData, string deptType, HttpResponse oResponse)
        {
            return getDataInfoOne(sType, sData, "", "", deptType, oResponse);
        }

        public static DataTable getDataInfoOne(string sType1, string sData1, string sType2, string sData2, string deptType)
        {
            DataTable oDt = GetLdapSearchData(sType1, sData1, sType2, sData2, "One", deptType, null);
            return oDt;
        }

        public static DataTable getDataInfoMuti(string sType1, string sData1, string sType2, string sData2, string deptType)
        {
            DataTable oDt = GetLdapSearchData(sType1, sData1, sType2, sData2, "Muti", deptType, null);
            return oDt;
        }

        public static DataTable getDataInfoOne(string sType1, string sData1, string sType2, string sData2, string deptType, HttpResponse oResponse)
        {
            DataTable oDt = GetLdapSearchData(sType1, sData1, sType2, sData2, "One", deptType, oResponse);
            return oDt;
        }

        private static DataTable GetLdapSearchData(string sType1, string sData1, string sType2, string sData2, string dataType, string deptType)
        {
            return GetLdapSearchData(sType1, sData1, sType2, sData2, dataType, deptType, null);
        }

        private static DataTable GetLdapSearchData(string sType1, string sData1, string sType2, string sData2, string dataType, string deptType, HttpResponse oResponse)
        {
            SearchResultCollection results = null;

            DataTable oDt = new DataTable();
            oDt.Columns.Add("userLdapNo");
            oDt.Columns.Add("userName");
            oDt.Columns.Add("title");
            oDt.Columns.Add("eMail");
            oDt.Columns.Add("deptName");
            oDt.Columns.Add("deptName1");
            oDt.Columns.Add("deptName2");

            if (string.IsNullOrEmpty(deptType) == true) //如果沒有傳入所在部門資料，則從總局開始判斷，非局內主機登入時會用到
            {
                bool runSearch = true;
                switch (sData1.ToLower()) //此2個帳號在總局有建立測試帳號，其實是南工處的人員
                {
                    case "xyalan":
                    case "maychang":
                        runSearch = false;
                        break;
                }

                if (runSearch == true)
                {
                    results = GetLdapSearch(sType1, sData1, sType2, sData2, "總");
                    if (results != null && results.Count > 0) { oDt = LdapSearchToDataTable(results, oDt, dataType, oResponse); }
                    if (results != null) { results.Dispose(); }

                    if (dataType == "One" && oDt.Rows.Count > 0)
                    {
                        return oDt;
                    }
                }

                results = GetLdapSearch(sType1, sData1, sType2, sData2, "南");
                if (results != null && results.Count > 0) { oDt = LdapSearchToDataTable(results, oDt, dataType, oResponse); }
                if (results != null) { results.Dispose(); }

                if (dataType == "One" && oDt.Rows.Count > 0)
                {
                    return oDt;
                }
            }
            else
            {
                results = GetLdapSearch(sType1, sData1, sType2, sData2, deptType);
                if (results != null && results.Count > 0) { oDt = LdapSearchToDataTable(results, oDt, dataType, oResponse); }
                if (results != null) { results.Dispose(); }

                if (dataType == "One" && oDt.Rows.Count > 0)
                {
                    return oDt;
                }
            }

            return oDt;
        }

        private static SearchResultCollection GetLdapSearch(string sType1, string sData1, string sType2, string sData2, string deptType)
        {
            DirectoryEntry de = GetDe(deptType);
            SearchResultCollection results = null;

            if (de != null)
            {
                sType1 = ReplaceLDAPServ(sType1);
                sData1 = ReplaceLDAPServ(sData1);

                DirectorySearcher deSearch = new DirectorySearcher(de);
                deSearch.SearchRoot = de;
                //deSearch.Filter = "(&(objectCategory=user)(cn=" + sData + "))";
                //deSearch.Filter = "(&(objectCategory=user)(mailnickname=" + sData + "))";

                sType1 = Encoder.LdapFilterEncode(sType1);
                sData1 = Encoder.LdapFilterEncode(sData1);

                string whereType2 = "";
                if (sType2 != "" && sData2 != "")
                {
                    sType2 = Encoder.LdapFilterEncode(sType2);
                    sData2 = Encoder.LdapFilterEncode(sData2);
                    whereType2 = "(" + sType2 + "=" + sData2 + ")";
                }

                deSearch.Filter = "(&(objectCategory=user)(" + sType1 + "=" + sData1 + ")" + whereType2 + ")";
                //deSearch.Filter = "(&(objectCategory=user)(" + sType + "=*" + sData + "*))";

                deSearch.PropertiesToLoad.Add("cn"); //                           陳真芳  B00313      C00215      張晉豪      吳文憲
                deSearch.PropertiesToLoad.Add("company"); //                      無      北區養護工程分局  中區工程處  無          無
                deSearch.PropertiesToLoad.Add("department"); //                   工務組  工務課      西螺服務區  屏東工務段  無
                deSearch.PropertiesToLoad.Add("sn"); //姓氏                       陳      李          黃          張          吳
                deSearch.PropertiesToLoad.Add("givenname"); //名字                真芳    丁寧        筱媁        晉豪        文憲
                deSearch.PropertiesToLoad.Add("displayname"); //                  陳真芳  李丁寧      黃筱媁      張晉豪      吳文憲
                deSearch.PropertiesToLoad.Add("title"); //職稱                    工程司  工程員      工程員      工程員      無
                deSearch.PropertiesToLoad.Add("mail"); //電子郵件 freeway.gov.tw  kengo@  ninglee@    c_acc@      frank511@   wuwenhs@
                deSearch.PropertiesToLoad.Add("name"); //                         陳真芳  B00313      C00215      張晉豪      吳文憲
                deSearch.PropertiesToLoad.Add("sAMAccountName"); //登入帳號       A00598  B00313      C00215      frank511    wuwenhs
                deSearch.PropertiesToLoad.Add("distinguishedName");
                //CN=陳真芳,OU=A10-工務組,OU=局本部帳號,OU=國道高速公路局(套用PC TWGCB原則),DC=Freeway,DC=gov,DC=tw
                //CN=B00404,OU=B03-工務課,OU=北區工程處,DC=Freeway,DC=gov,DC=tw
                //CN=B00221,OU=B010-工務科,OU=北區養護工程分局,DC=Freeway,DC=gov,DC=tw
                //CN=C00019,OU=C03-工務課,OU=中區工程處,DC=Freeway,DC=gov,DC=tw
                //CN=林開湖,OU=使用者,OU=工務課,OU=南區工程處,DC=s_expressway,DC=gov
                //CN=張乃祥,OU=技術課,DC=wfreeway,DC=gov,DC=tw

                //deSearch.PropertiesToLoad.Add("deptName"); //機關名稱  //總局    //北區養護工程分局  //中區工程處  //南區工程處  //拓建工程處
                //deSearch.PropertiesToLoad.Add("deptName1"); //一級單位  //養路組  //工務課      //西螺服務區  //屏東工務段  //機料課
                //deSearch.PropertiesToLoad.Add("deptName2"); //二級單位  //管理科  //            //            //林厝監工站  //保養場
                //deSearch.PropertiesToLoad.Add("userLdapNo"); //員工編號  //A00598  //B00313      //C00215      //frank511    //wuwenhs
                //deSearch.PropertiesToLoad.Add("userName"); //員工編號  //A00598  //B00313      //C00215      //frank511    //wuwenhs

                try
                {
                    //SearchResult results = deSearch.FindOne();
                    results = deSearch.FindAll();
                }
                catch (Exception)
                {

                }
                finally
                {
                    deSearch.Dispose();
                    de.Close();
                    de.Dispose();
                }
            }

            return results;
        }

        private static DataTable LdapSearchToDataTable(SearchResultCollection results, DataTable oDt, string dataType)
        {
            return LdapSearchToDataTable(results, oDt, dataType, null);
        }

        private static DataTable LdapSearchToDataTable(SearchResultCollection results, DataTable oDt, string dataType, HttpResponse oResponse)
        {
            string cn = "";
            string company = "";
            string department1 = "";
            string department2 = "";
            string sn = "";
            string givenname = "";
            string displayname = "";
            string title = "";
            string mail = "";
            string name = "";
            string sAMAccountName = "";
            string distinguishedName = "";

            if (results != null && results.Count > 0)
            {
                foreach (SearchResult result in results)
                {
                    cn = "";
                    company = "";
                    department1 = "";
                    department2 = "";
                    sn = "";
                    givenname = "";
                    displayname = "";
                    title = "";
                    mail = "";
                    name = "";
                    sAMAccountName = "";
                    distinguishedName = "";

                    if (result.Properties["cn"].Count > 0) { cn = result.Properties["cn"][0].ToString(); }
                    if (result.Properties["company"].Count > 0) { company = result.Properties["company"][0].ToString(); }
                    if (result.Properties["department"].Count > 0) { department1 = result.Properties["department"][0].ToString(); }
                    if (result.Properties["sn"].Count > 0) { sn = result.Properties["sn"][0].ToString(); }
                    if (result.Properties["givenname"].Count > 0) { givenname = result.Properties["givenname"][0].ToString(); }
                    if (result.Properties["displayname"].Count > 0) { displayname = result.Properties["displayname"][0].ToString(); }
                    if (result.Properties["title"].Count > 0) { title = result.Properties["title"][0].ToString(); }
                    if (result.Properties["mail"].Count > 0) { mail = result.Properties["mail"][0].ToString(); }
                    if (result.Properties["name"].Count > 0) { name = result.Properties["name"][0].ToString(); }
                    if (result.Properties["sAMAccountName"].Count > 0) { sAMAccountName = result.Properties["sAMAccountName"][0].ToString(); }
                    if (result.Properties["distinguishedName"].Count > 0) { distinguishedName = result.Properties["distinguishedName"][0].ToString(); }

                    if (displayname == "" || (sn != "" && givenname != ""))
                    {
                        displayname = sn + givenname;
                        displayname = displayname.Replace("謝謝", "謝");
                    }

                    if (distinguishedName != "")
                    {
                        if (dataType == "One" && distinguishedName.Contains("停用") == true)
                        {
                            continue;
                        }

                        string[] xPathData = distinguishedName.Split(',');
                        string[] xDeptData1 = null;
                        string[] xDeptData2 = null;
                        string deptNameTmp = "";

                        if (distinguishedName.Contains("DC=Freeway") == true)
                        {
                            if (distinguishedName.Contains("OU=局本部帳號") == true)
                            {
                                company = "總局";

                                if (xPathData[2] == "OU=局本部帳號")
                                {
                                    xDeptData1 = xPathData[1].Split('=');
                                }
                                else if (xPathData[3] == "OU=局本部帳號")
                                {
                                    xDeptData1 = xPathData[2].Split('=');
                                    xDeptData2 = xPathData[1].Split('=');
                                }
                            }
                            else if (distinguishedName.Contains("OU=北區養護工程分局") == true || distinguishedName.Contains("OU=北區工程處") == true)
                            {
                                company = "北區養護工程分局";

                                if (xPathData[2] == "OU=北區養護工程分局" || xPathData[2] == "OU=北區工程處")
                                {
                                    xDeptData1 = xPathData[1].Split('=');
                                }
                                else if (xPathData[3] == "OU=北區養護工程分局" || xPathData[3] == "OU=北區工程處")
                                {
                                    xDeptData1 = xPathData[2].Split('=');
                                }
                            }
                            else if (distinguishedName.Contains("OU=中區養護工程分局") == true || distinguishedName.Contains("OU=中區工程處") == true)
                            {
                                company = "中區養護工程分局";

                                if (xPathData[2] == "OU=中區養護工程分局" || xPathData[2] == "OU=中區工程處")
                                {
                                    xDeptData1 = xPathData[1].Split('=');
                                }
                                else if (xPathData[3] == "OU=中區養護工程分局" || xPathData[3] == "OU=中區工程處")
                                {
                                    xDeptData1 = xPathData[2].Split('=');
                                }
                            }
                            else if (distinguishedName.Contains("OU=新工一處") == true || distinguishedName.Contains("OU=第一新建工程處") == true)
                            {
                                company = "第一新建工程處";

                                if (xPathData[2] == "OU=新工一處" || xPathData[2] == "OU=第一新建工程處")
                                {
                                    xDeptData1 = xPathData[1].Split('=');
                                }
                                else if (xPathData[3] == "OU=新工一處" || xPathData[3] == "OU=第一新建工程處")
                                {
                                    xDeptData1 = xPathData[2].Split('=');
                                }
                            }
                            else if (distinguishedName.Contains("OU=新工二處") == true || distinguishedName.Contains("OU=第二新建工程處") == true)
                            {
                                company = "第二新建工程處";

                                if (xPathData[2] == "OU=新工二處" || xPathData[2] == "OU=第二新建工程處")
                                {
                                    xDeptData1 = xPathData[1].Split('=');
                                }
                                else if (xPathData[3] == "OU=新工二處" || xPathData[3] == "OU=第二新建工程處")
                                {
                                    xDeptData1 = xPathData[2].Split('=');
                                }
                            }

                            if (xDeptData1[1].Contains("-") == true)
                            {
                                deptNameTmp = xDeptData1[1].Split('-')[1];
                            }
                            else
                            {
                                deptNameTmp = xDeptData1[1];
                            }

                            if (department1 != deptNameTmp) { department1 = deptNameTmp; }

                            if (xDeptData2 != null)
                            {
                                if (xDeptData2[1].Contains("-") == true)
                                {
                                    department2 = xDeptData2[1].Split('-')[1];
                                }
                                else
                                {
                                    department2 = xDeptData2[1];
                                }
                            }
                        }
                        else if (distinguishedName.Contains("DC=s_expressway") == true)
                        {
                            company = "南區養護工程分局";

                            if (xPathData[2] == "OU=南區養護工程分局" || xPathData[2] == "OU=高速公路局南區養護工程分局" || xPathData[2] == "OU=南區工程處")
                            {
                                xDeptData1 = xPathData[1].Split('=');
                            }
                            else if (xPathData[3] == "OU=南區養護工程分局" || xPathData[3] == "OU=高速公路局南區養護工程分局" || xPathData[3] == "OU=南區工程處")
                            {
                                if (xPathData[2] == "OU=總務課" || xPathData[2] == "OU=業務科")
                                {
                                    switch (xPathData[1])
                                    {
                                        case "OU=新營服務區D41":
                                        case "OU=仁德服務區D42":
                                        case "OU=東山服務區D43":
                                        case "OU=關廟服務區D44":
                                        case "OU=古坑服務區D45":
                                        case "OU=新營服務區":
                                        case "OU=仁德服務區":
                                        case "OU=東山服務區":
                                        case "OU=關廟服務區":
                                        case "OU=古坑服務區":
                                            xDeptData1 = xPathData[1].Split('=');
                                            break;
                                        default:
                                            xDeptData1 = xPathData[2].Split('=');
                                            break;
                                    }
                                }
                                else
                                {
                                    xDeptData1 = xPathData[2].Split('=');
                                }
                            }
                            else if (xPathData[4] == "OU=南區養護工程分局" || xPathData[4] == "OU=南區工程處")
                            {
                                xDeptData1 = xPathData[3].Split('=');
                            }

                            if (xDeptData1[1].Contains("-") == true)
                            {
                                deptNameTmp = xDeptData1[1].Split('-')[1];
                            }
                            else
                            {
                                deptNameTmp = xDeptData1[1];
                            }

                            if (department1 != deptNameTmp) { department1 = deptNameTmp; }
                        }
                        //else if (distinguishedName.Contains("DC=wfreeway") == true)
                        //{
                        //    company = "拓建工程處";
                        //    if (xPathData[2] == "DC=wfreeway")
                        //    {
                        //        xDeptData1 = xPathData[1].Split('=');
                        //    }
                        //    else if (xPathData[3] == "DC=wfreeway")
                        //    {
                        //        xDeptData1 = xPathData[2].Split('=');
                        //    }

                        //    if (xDeptData1[1].Contains("-") == true)
                        //    {
                        //        deptNameTmp = xDeptData1[1].Split('-')[1];
                        //    }
                        //    else
                        //    {
                        //        deptNameTmp = xDeptData1[1];
                        //    }

                        //    if (department1 != deptNameTmp) { department1 = deptNameTmp; }
                        //}
                    }

                    if (oResponse != null)
                    {
                        oResponse.Write("DataStart<br /><br />");

                        foreach (string key in result.Properties.PropertyNames)
                        {
                            foreach (object propVal in result.Properties[key]) { oResponse.Write(key + "=" + propVal + "<br />"); }
                        }

                        oResponse.Write("DataEnd<br /><br />");
                    }

                    DataRow oNewRow = oDt.NewRow();
                    oNewRow["userLdapNo"] = sAMAccountName;
                    oNewRow["userName"] = displayname;
                    oNewRow["title"] = title;
                    oNewRow["eMail"] = mail;
                    oNewRow["deptName"] = company;
                    oNewRow["deptName1"] = department1;
                    oNewRow["deptName2"] = department2;
                    oDt.Rows.Add(oNewRow);
                }
            }
            return oDt;
        }
    }

    public class ChkUser
    {
        public static string GetClientIP(HttpRequest httpRequest)
        {
            string sIP = null;
            string sFORWARDED = "";
            if (string.IsNullOrEmpty(httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]) == false)
            {
                sFORWARDED = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }

            if (sFORWARDED == "" || sFORWARDED.ToUpper().IndexOf("UNKNOWN") > -1)
            {
                sIP = httpRequest.ServerVariables["REMOTE_ADDR"];
            }
            else if (sFORWARDED.IndexOf(",") > 0)
            {
                sIP = sFORWARDED.Substring(1, sFORWARDED.IndexOf(",") - 1);
            }
            else if (sFORWARDED.IndexOf(";") > 0)
            {
                sIP = sFORWARDED.Substring(1, sFORWARDED.IndexOf(";") - 1);
            }
            else { sIP = sFORWARDED; }

            return sIP.Replace(" ", string.Empty);
        }

        public static string ChkLDAP_UID(string sUserID, string sPassWord, string deptType)
        {
            string connStr = "";
            OleDbConnection DB = new OleDbConnection(connStr);
            OleDbCommand myCommand = null;

            string sExecSql = "";
            string rtn = "";

            //======================以下程式為範例，請依各系統自行修正====================
/*
            string sSql = "Select ID From USER Where ID=?";
            myCommand = new OleDbCommand(sSql, DB);
            string sDbUserID = myCommand.ExecuteScalar().ToString();

            DataTable oDt = ReadAD.GetLdapIdData(sUserID, deptType); //取出使用者資料
            if (oDt == null || oDt.Rows.Count == 0)
            {
                rtn = "登入使用者帳號未在AD建檔";
                return rtn;
            }

            DataRow oRow = oDt.Rows[0];
            string sUSR_NAME = oRow["userName"].ToString(); //名字
            string sTITLE = oRow["title"].ToString(); //職稱
            string sEMAIL = oRow["eMail"].ToString(); //電子郵件
            string deptName = oRow["deptName"].ToString(); //機關名稱
            string deptName1 = oRow["deptName1"].ToString(); //一級單位
            string deptName2 = oRow["deptName2"].ToString(); //二級單位

            string sTEL_O = "";
            string sTEL_M = "";

            string sDeptNo = "";
            string sWhere = "";
            string sErr = "";

            switch (deptName)
            {
                case "總局":
                    sWhere = deptName1;
                    if (deptName2 != "") { sWhere = deptName1 + "." + deptName2; }
                    myCommand = new OleDbCommand("Select NO From DEPT Where NAME='" + sWhere + "'", DB);
                    sDeptNo = myCommand.ExecuteScalar().ToString();
                    break;
                default:
                    sWhere = deptName;
                    if (deptName1 != "") { sWhere = deptName + "." + deptName1; }
                    if (deptName2 != "") { sWhere = deptName + "." + deptName1 + "." + deptName2; }

                    myCommand = new OleDbCommand("Select NO From DEPT Where NAME='" + sWhere + "'", DB);
                    sDeptNo = myCommand.ExecuteScalar().ToString();
                    break;
            }

            //Response.Write(sDeptNo + "<br />");
            //檢查登入的單位是否存在
            if (sDeptNo != "")
            {
                sPassWord = sPassWord.Replace("'", "''");
                //Response.Write(sDbUserID + "<br />");
                //沒有用 Ldap 登入過
                if (sDbUserID == "")
                {
                    //查詢人員流水號
                    myCommand = new OleDbCommand("Select Max(ID) From USER Where ID Like '" + sDeptNo + "'", DB);
                    sDbUserID = myCommand.ExecuteScalar().ToString();

                    if (sDbUserID == "") { sDbUserID = sDeptNo + "001"; }
                    else { sDbUserID = sDeptNo + (Convert.ToDouble(sDbUserID.Substring((sDeptNo).Length)) + 1).ToString("000"); }

                    //新增人員
                    sExecSql = "Insert Into USER (ID,NAME,MAIL,TITLE,UPD_UID,ADD_DT) Values ('{0}',N'{1}',N'{2}',N'{3}','LDAP',getdate())";
                    myCommand = new OleDbCommand(sExecSql, DB);
                    sDbUserID = myCommand.ExecuteScalar().ToString();

                    //新增人員和部門連結資料
                    sExecSql = "Insert Into DPUR (DEPT_NO,USR_ID) Values ('{0}','{1}')";
                    myCommand = new OleDbCommand(sExecSql, DB);
                    sDbUserID = myCommand.ExecuteScalar().ToString();
                }
                else
                {
                    bool bYesHave = false;
                    sExecSql = "Select NO From DPUR Where ID='" + sDbUserID + "'";
                    myCommand = new OleDbCommand(sExecSql, DB);

                    DataTable dtData = new DataTable();
                    dtData.Load(myCommand.ExecuteReader());
                    DataView oOgcDpur = new DataView(dtData);

                    int iDpurCnt = oOgcDpur.Count;
                    if (iDpurCnt > 0)
                    {
                        for (int iCnt = 0; iCnt < iDpurCnt; iCnt++)
                        {
                            if (oOgcDpur[iCnt]["DEPT_NO"].ToString() == sDeptNo)
                            {
                                bYesHave = true;
                            }
                            else
                            {
                                //刪除人員和部門連結資料
                                sExecSql = string.Format("Delete From DPUR Where NO='{0}' And ID='{1}'", oOgcDpur[iCnt]["DEPT_NO"].ToString(), sDbUserID);
                                myCommand = new OleDbCommand(sExecSql, DB);
                                myCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    if (sErr != "")
                    {
                        rtn = sErr;
                    }

                    if (bYesHave == false)
                    {
                        //新增人員和部門連結資料
                        sExecSql = "Insert Into DPUR (NO,ID) Values ('{0}','{1}')";
                        myCommand = new OleDbCommand(sExecSql, DB);
                        myCommand.ExecuteNonQuery();
                    }
                    //Response.Write(sErr);

                    //修改人員檔
                    sExecSql = "Update USER Set NAME=N'{0}',TITLE='{3}' Where ID='" + sDbUserID + "'";
                    myCommand = new OleDbCommand(sExecSql, DB);
                    myCommand.ExecuteNonQuery();
                }
            }
            else
            {
                rtn = "登入的單位尚未授權，請聯絡系統管理員";
            }
*/
            return rtn;
        }
    }
}

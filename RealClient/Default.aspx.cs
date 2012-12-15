using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RealLicenseLibrary;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnLicense_Click(object sender, EventArgs e)
    {
        Client client = new Client();
        RealLicense obj = new RealLicense();
        obj.LicenseNumber = "JJ00131289";
        client.sendAuthenticationRequest(obj);
        Session["User"] = client.receiveAuthenticationResponse();
        if (((Boolean)Session["User"] == true))
        {
            Response.Redirect("~/Success.aspx");
        }
        else
        {
            Response.Redirect("~/Expired.aspx");
        }
    }
}
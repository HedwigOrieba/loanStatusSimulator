<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmRequestSimulator.aspx.cs" Inherits="SimulatorApiAppDFCUBank.frmRequestSimulator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>loanStatusApp</title>
    <meta name="author" content="Hedwig Orieba" />
    <style type="text/css">

        #base {
            width: 99%;
            margin:auto;
            text-align:center;
            }

        #cmdpanel {
            position:relative;
            left:4.5%;
            margin-bottom:5px;
            height:30px;
            line-height:30px;
            text-align:left;
            font-family:'Trebuchet MS';
            font-size:0.8em;
            width:90%;
        }

        .LoadRandomAccList {
            margin-left:2px;
            width:180px;
            font-family:Arial;
            font-size:0.8em;
            margin-right:10px;
        }

        .txtBoxes {
            color:#d38042;
            font-family:'Trebuchet MS';
            font-size:0.7em;
            background-color:#F5F5F5
        }

        .txtjwtok {
                 color:#4169E1;
        }

        p{
            color:#4682B4;
            font-weight:bold;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id ="base">
         <p> API SIMULATOR PORTAL - DFCU BANK </p>

         <div id="cmdpanel">
             <asp:Button ID="LoadRandomList"  cssClass="LoadRandomAccList"  runat ="server" Text="Generate Random Accounts"  Height="25px" OnClick="LoadRandomList_Click" />
             <asp:Button ID="cmdMakeRequest"  cssClass="LoadRandomAccList" runat="server" Text="Invoke Endpoint & Commit" Height="25px" OnClick="callResorurce_Click" Enabled="False" />
         </div>

         <asp:TextBox ID="txtResponse" cssClass="txtBoxes"  runat ="server" TextMode="MultiLine" Width="90%" Height="100px"></asp:TextBox>
         <br/>
         <asp:TextBox ID="txtjwt" cssClass="txtBoxes txtjwtok" runat ="server" TextMode="MultiLine" Width="90%" Height="200px"></asp:TextBox>

    </div>
    </form>
</body>
</html>

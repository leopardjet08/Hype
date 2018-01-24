<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="FinalProject.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.0/jquery.min.js"></script> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/> 
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>﻿
     
</head>
<body>

       <nav class="navbar navbar-light" style="background-color: #f5f5f5; margin-top:0px; ">
            <div class="container-fluid">

                <!-- Logo -->
                <div class="navbar-header">
                    <a href="#" class="navbar-brand"></a>
                    <img src="CompanyLogo.png" alt="Logo" style="height:45%; width:45%; padding-top:10px"/> 
                </div>

                 <!-- Menu Items -->
                <div>
                    <ul class="nav navbar-nav">
                        
                        <li><a href="http://www.niagaracatholic.ca/"><h3>Website</h3></a></li>
                        
                        <li><a href="HomePage.aspx"><h3>Home</h3></a></li>
                        
                        <!-- Dropdown Menu Template1 -->
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><h3> Applications <span class="caret" ></span></h3></a>
                            <ul class="dropdown-menu">
                                <li><a href="HomePage.aspx"><h4>View Applications</h4></a></li>
                            </ul>
                        </li>
                          <!-- Dropdown Menu Template2 -->
                        <li>
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><h3> Postings <span class="caret" ></span></h3></a>
                            <ul class="dropdown-menu">    
                                <li><a href="HomePage.aspx"><h4>View Postings</h4></a></li>
                                <li><a href="HomePage.aspx"><h4>Create Posting</h4></a></li>
                            </ul>
                        </li>
                          <!-- Dropdown Menu Template3 -->
                        <li>
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown"><h3> Dummy List<span class="caret" ></span></h3></a>
                            <ul class="dropdown-menu">   
                                <li><a href="HomePage.aspx"><h4>Dummy Text</h4></a></li>
                                <li><a href="HomePage.aspx"><h4>More Dummy Text</h4></a></li>
                            </ul>
                        </li>
                    </ul>

                <!-- Logout -->
                   <ul class="nav navbar-nav navbar-right">
                       <li><a href="Login.aspx"><h4>Logout</h4></></a></li>
                   </ul>
                    <ul class="nav navbar-nav navbar-right">
                       <li><a href="Register.aspx"><h4>Register</h4></></a></li>
                   </ul>
                </div> 
            </div>
        </nav>
        <form id="form2" runat="server">
             <div class="container well well-lg" style="height: 1000px">
                 <br /><br />
                 <fieldset>
                     

                    <legend style="text-align:center; font-size:30px; color:#337ab7;">Niagara Catholic District School Board</legend>
                     <h3 style="padding-top:20px; text-align:center;">Search Job Postings:</h3>&nbsp;&nbsp;
                     
                     <div class="container well well-lg" style="width: 491px";>
                               <div style="text-align:center">
                                 <asp:TextBox ID="TextBox1" class="form-control" style="padding-bottom:20px; width:500px;" runat="server"></asp:TextBox>
                              </div>
                         </div>
                     
                            
                        

                        
                    <p style="text-align:center">
                    <a href="#"><img src="updatesIcon.png" style="padding-top:100px"/></a>
                    <a href="#"><img src="emailIcon.png" style="padding-left:10%; padding-right:10%"/></a>
                    <a href="#"><img src="staffIcon.png" /></a>
                    </p>
                     <p style="padding-top:20px;">
                         Follow us on social media:
                     </p>
                     <p>              
                        <a href="https://www.facebook.com/NiagaraCatholicDSB/"><img src="facebook.png" style="height:30px; padding-top:10px"/></a>&nbsp;&nbsp;
                        <a href="https://twitter.com/niagaracatholic"><img src="twitter.png" style="height:25px;padding-right:10%"/></a>&nbsp;&nbsp;
                        <a href="https://www.youtube.com/user/NiagaraCatholicDSB"><img src="youtube.png" style="height:25px;"/></a><br />
                     </p>

                  </fieldset>
             </div>              
        </form>
    <div id="footer" class="navbar navbar-light" style="padding:25px 0 25px 0; margin-bottom:0px; border:none; bottom:0; text-align:center; font-size:14px; background-color: #f5f5f5;">
             
        Niagara Catholic District School Board - Copyright © 2018
    </div>

    <div id="footer2" style=" height:15px; margin-bottom:0px; border:none; bottom:0; text-align:center; font-size:14px; background-color: #1b1fa5;"></div>

</body>
</html>

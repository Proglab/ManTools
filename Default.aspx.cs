using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManTools2020
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Utilisateur"] == null)
            {
                logedPanel.Visible = false;
                loginPanel.Visible = true;
            } else
            {
                logedPanel.Visible = true;
                loginPanel.Visible = false;
            }
        }


        protected void Envoyer_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (Login.Text != String.Empty && Password.Text != String.Empty)
                {
                    {
                        //Vérification du login
                        if ((Login.Text != "adm" || Password.Text != "adm") && (Login.Text != "cons" || Password.Text != "cons") &&
                            (Login.Text != "dma" || Password.Text != "dmaa") && (Login.Text != "esa" || Password.Text != "esaa") &&
                            (Login.Text != "ggi" || Password.Text != "ggii") && (Login.Text != "evh" || Password.Text != "evhh") &&
                            (Login.Text != "pha" || Password.Text != "phaa") && (Login.Text != "seh" || Password.Text != "sehh") &&
                            (Login.Text != "tja" || Password.Text != "tjaa") && (Login.Text != "dda" || Password.Text != "ddaa") &&
                            (Login.Text != "mmo" || Password.Text != "mmoo") && (Login.Text != "dhu" || Password.Text != "dhuu") &&
                            (Login.Text != "pke" || Password.Text != "pkee") && (Login.Text != "pdl" || Password.Text != "pdll") &&
                            (Login.Text != "mva" || Password.Text != "mvaa") && (Login.Text != "cel" || Password.Text != "cell"))
                        {
                            RequiredFieldValidator1.IsValid = false;
                            RequiredFieldValidator1.ErrorMessage = "Veuillez entrer le bon Login / Login fout of ...";
                            RequiredFieldValidator2.IsValid = false;
                            RequiredFieldValidator2.ErrorMessage = "Veuillez entrer le bon password / Password fout";
                        }
                        else
                        {
                            //Initialisation de l'utilisateur
                            Session["Utilisateur"] = Login.Text;

                            //Initialisation du Profil
                            if ((Login.Text == "adm") || (Login.Text == "dma") || (Login.Text == "ggi") || (Login.Text == "seh"))
                            {
                                Session["Profil"] = "ADMIN";
                            }
                            if ((Login.Text == "esa") || (Login.Text == "evh") || (Login.Text == "pha"))
                            {
                                Session["Profil"] = "USER";
                            }
                            else
                            {
                                Session["Profil"] = "CONS";
                            }

                            //Initialisation de la langue
                            if ((Login.Text == "adm") || (Login.Text == "cons") || (Login.Text == " dma") || (Login.Text == "esa") || (Login.Text == "tja") ||
                               (Login.Text == "dda") || (Login.Text == "mmo") || (Login.Text == "dhu") || (Login.Text == "cel"))
                            {
                                Session["Langue"] = "FR";
                            }
                            else
                            {
                                Session["Langue"] = "NL";
                            }

                            //Initialisation de la région
                            if ((Login.Text == "adm") || (Login.Text == "cons") || (Login.Text == "dma") || (Login.Text == "esa") ||
                                (Login.Text == "tja") || (Login.Text == "mmo") || (Login.Text == "mva"))
                            {
                                Session["Region"] = "BXL";
                            }
                            if ((Login.Text == "ggi") || (Login.Text == "evh") || (Login.Text == "pha") || (Login.Text == "seh") || (Login.Text == "pke"))
                            {
                                Session["Region"] = "ANV";
                            }
                            else
                            {
                                Session["Region"] = "MAN";
                            }

                            //Response.Redirect("default.aspx");
                            if (Session["Langue"] == "FR")
                            {
                                LblMessage.Text = "Vous êtes bien connecté à présent cher ";
                            }
                            else
                            {
                                LblMessage.Text = "U bent ingelogd als ";
                            }

                            if (Login.Text == "adm")
                            {
                                LblMessage.Text += "Administrateur";
                            }
                            else if (Login.Text == "cons")
                            {
                                LblMessage.Text += "Consultant";
                            }
                            else if (Login.Text == "dma")
                            {
                                LblMessage.Text += "Dirk Maes";
                            }
                            else if (Login.Text == "ggi")
                            {
                                LblMessage.Text += "Geert Gillis";
                            }
                            else if (Login.Text == "esa")
                            {
                                LblMessage.Text += "Eric Salade";
                            }
                            else if (Login.Text == "evh")
                            {
                                LblMessage.Text += "Erik Van Haerenborgh";
                            }
                            else if (Login.Text == "pha")
                            {
                                LblMessage.Text += "Hans Pardon";
                            }
                            else if (Login.Text == "seh")
                            {
                                LblMessage.Text += "Saïd El Hahaoui";
                            }
                            else if (Login.Text == "tja")
                            {
                                LblMessage.Text += "Thierry Jacobs";
                            }
                            else if (Login.Text == "dda")
                            {
                                LblMessage.Text += "Delphine Danneau";
                            }
                            else if (Login.Text == "mmo")
                            {
                                LblMessage.Text += "Mark Moreau";
                            }
                            else if (Login.Text == "dhu")
                            {
                                LblMessage.Text += "Danny Huart";
                            }
                            else if (Login.Text == "pke")
                            {
                                LblMessage.Text += "Peter Keysers";
                            }
                            else if (Login.Text == "pdl")
                            {
                                LblMessage.Text += "Pietro De Luca";
                            }
                            else if (Login.Text == "mva")
                            {
                                LblMessage.Text += "Michael Vancraenenbroeck";
                            }

                            Response.Redirect("default.aspx");
                        }
                    }
                }
            }
        }
    }
}
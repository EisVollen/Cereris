<%@ Page Title="Главная" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Juno.aspx.cs" Inherits="Cereris.Juno" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="body-wrapper">

        <div id="page-header-wrapper">

            <div class="clear40"></div>
            <!--content container-->
            <div id="content_container" class="center-clear">

                <!--carousel-->
                <div id="carousel_container">
                    <!--nivo slider-->
                    <div class="theme-default">
                        <div class="ribbon"></div>
                        <div id="slider3" class="nivoSlider">
                            <a runat="server" id="referenceSlider1" href="">
                                <img id="sliderImage1" runat="server" size="cover" src="" width="970" height="360" alt="" title="#slider3-caption-0" /></a>
                            <a runat="server" id="referenceSlider2" href="">
                                <img id="sliderImage2" runat="server" src="" width="970" height="360" alt="" title="#slider3-caption-1" /></a>
                            <a runat="server" id="referenceSlider3" href="">
                                <img id="sliderImage3" runat="server" src="" width="970" height="360" alt="" title="#slider3-caption-2" /></a>
                        </div>
                    </div>

                    <!--/nivo slider-->
                </div>
                <div id="slider3-caption-0" class="nivo-html-caption">
                    <h1 runat="server" id="TitleImage1"></h1>
                    <p runat="server" id="DsrImage1"></p>
                </div>
                <div id="slider3-caption-1" class="nivo-html-caption">
                    <h1 runat="server" id="TitleImage2"></h1>
                    <p runat="server" id="DsrImage2"></p>
                </div>
                <div id="slider3-caption-2" class="nivo-html-caption">
                    <h1 runat="server" id="TitleImage3"></h1>
                    <p runat="server" id="DsrImage3"></p>
                </div>

                <!-- /carousel -->

                <div class="clear40"></div>

                <!-- three box footer-->
                <div class="box-three-sub-wrapper transparent-background">
                    <div class="box-three featured-box-sub nomargin">
                        <div>
                            <div>
                                <!-- sub featured title -->
                                <h3>О сайте</h3>
                                <div class="clear"></div>
                                <!-- text-->
                                <span>Данный проект реализован в рамках дипломной работы. 
                                </span>
                                <span>В качетве данных используется интерфейс прикладного программирования (API) NASA. 
                                </span>
                                <span></span>
                            </div>
                        </div>
                    </div>
                    <div class="box-three featured-box-sub">
                        <div>
                            <div>
                                <!-- sub featured title -->
                                <h3>Популярные публикации</h3>
                                <div class="clearfix"></div>
                                <!-- list with image -->
                                <ul>
                                    <li>
                                        <img src="images/mostPopular1.png" alt="" width="55" height="55" />
                                        <span><a runat="server" id="MostPopular" href=""></a></span>
                                        <span runat="server" id="MostPopularDate" class="times-gray-italic-13"></span>
                                    </li>
                                    <li>
                                        <img src="images/mostPopular2.png" alt="" width="55" height="55" />
                                        <span><a runat="server" id="MostPopular2" href=""></a></span>
                                        <span runat="server" id="MostPopular2Date" class="times-gray-italic-13"></span>
                                    </li>
                                </ul>
                                <!-- /list with image -->
                            </div>
                        </div>
                    </div>
                    <div class="box-three featured-box-sub">
                        <div>
                            <div>
                                <!-- sub featured title -->
                                <h3>Контактрая информация</h3>
                                <div class="clear"></div>
                                <span>
                                    <span>Казахстан. Астана.</span>
                                    <span></span>
                                    <span></span>
                                    <span>E-mail: e_vollen@bk.ru</span>
                                    <span></span>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /three box footer-->
            </div>
            <!-- /content container-->
        </div>
    </div>
</asp:Content>

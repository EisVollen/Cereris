<%@ Page Language="C#" EnableEventValidation="false"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApodDetail.aspx.cs" Inherits="Cereris.ApodDetail" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function setParrent(id) {
            Parrent_Id.setValue(id);
            document.getElementById("comment_form" + id).style.visibility = "hidden";
        }

    </script>
      <!--body wrapper-->
    <div id="body-wrapper">
    
    	<div id="page-header-wrapper">
           
	        <div class="clear40"></div>
            
            <!--side bar title and bread crumb-->
            <div class="center-clear">
                <div id="breadcrumb" class="page-columns-head-style gray-frame page-column2">
                    <ul>
                        <li><a href="Juno.aspx">Главная</a></li>
                        <li><a href="Blog.aspx" class="active">Публикации</a></li>
                    </ul>
                </div>
                
              <div class="page-columns-head-style gray-frame page-column1 margin-left-10">
                	<h1 class="heading h1">Категории</h1>
                </div>
            </div>
            <!--/side bar title and bread crumb-->
            
        </div>
        <!--/page header wrapper-->
        
        
        <!--page content wrapper-->
        <div id="page-content-wrapper">        
        
            <!--content container-->
          	<div class="center-clear page-content-wrapper">
                
                <!--page content goes here-->
            	<div runat="server" ID="PostDiv" class="page-content gray-frame nopadding blog-content">
	               
					<div class="clear10"></div>
                                        
					<!--post-->
                    <div class="post-box">
						
                        <!--date box-->
                        <div class="date-box alignleft_block">
                            <div>                                        
                            	<div></div>                            
                                <span class="line1" runat="server" ID="day"></span>
                                <span class="line2" runat="server" ID="mounth"></span>
                                <span class="line3" runat="server" ID="years"></span>
                            </div>
                        </div>
                        <!--/date box-->
                        <input hidden="true" runat="server" ID="Post_Id"/>
                        <input hidden="true" runat="server" ID="Parrent_Id"/>
                        <!--title-->
                        <h1 class="alignleft_block margin-left-20 post-title heading" runat="server" ID="title"></h1>
						<!--/title-->
	
    					<!--post content-->
                        <div class="alignleft_block post-content margin-left-20 margin_top10">
                        
							<!--image-->
                            <img runat="server" ID="Picture" src="images/NotFind.png" style="width: 80% ; height: 70%" alt="" />
							<!--/image -->	

                             <iframe runat="server" ID="video" src="" width="500" height="410" frameborder="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>                   
                             <!--/vimeo video-->

                            <div class="clear10"></div>
                            <div class="original" align="right"><a href="<%= referece %>true" class="notextdecoration gray-only">Оригинал</a>                     
                            </div>
                            <div class="clear10"></div>
                            <!--text content-->
                            <p runat="server" ID="Text">
                            </p>
							
                            <!--/text content-->
                        </div>
                        <!--/post content-->
                        
                        <div class="clear10"></div>
                        
                        <!--information bar-->
                        <div class="info-bar alignleft_block" align="center">
                        	<span style="align-content: center">Переведено сервисом <a href="http://translate.yandex.ru/" class="notextdecoration gray-only">«Яндекс.Переводчик»</a></span>                            
                            
                        </div>
                       <!--/information bar-->
						
                          <div class="clear20"></div>
                          <div class="info-bar alignleft_block" runat="server" ID="infobar">
                               </div>
                        <div class="clear10"></div>
                        
                        <!--post comments-->
                        <div class="post-comments comments-trackbacks-list nopadding" runat="server" ID="CommentsPosts">
                        </div>
                        <!--/post comments-->
                        
                        <!--post trackbacks-->
                        <div class="post-trackbacks comments-trackbacks-list nopadding noshow">
                        	<div>
							</div>                                                         
                        </div>
                        <!--/post trackbacks-->                        
                        
                        <!--/comments & trackbacks-->
                        
                        <div class="clear20"></div>
                        
                        <!--post new comment-->
						<h1 class="heading h1 margin_bottom10">Оставить комментарий</h1>
                        <form name="comment_form" id="comment_form" class="generic-form alignleft_block" action="blog-post.html">
                            <p>
                                <input type="text" runat="server"  name="txt_comment_name" style="width:40%" id="txt_comment_name" class="medium user" required />
                                <label for="txt_comment_name">Имя *</label>
                            </p>
                            <p>
                                <input type="email" runat="server" name="txt_comment_email"  style="width:40%" id="txt_comment_email" class="medium email" required />
                                <label for="txt_comment_email">E-mail *</label>
                            </p>
                            <p>
                                <textarea class="xxlarge" runat="server" rows="6" style="width: 60%" cols="4" name="txt_comment_message" id="txt_comment_message" required/> 
                            </p>
    
                            <div id="message_box_place_holder"></div>
                            
                            <div class="clear10"></div>
                            
                            <a onserverclick="SendClick" runat="server" class="button alignleft_block bold_only" id="btnSubmit">Отправить</a>
                            
                        </form> <%----%>
                        
                        <!--/post new comment-->
                        
                        <div class="clear20"></div>

                    </div>                    
                    <!--/post-->                                
                    
                </div>
                <!--/page content goes here-->                
                
                <!--side bar at right-->
             <div class="side_bar gray-frame page-column1 page-column1-blog margin-left-10 no-bottom-padding">
                    
                	 <div class="splitter-h alignleft_block"></div>                    

                    <!--calendar-->
					<div id="inlineDatepicker"></div>
                    <!--/calendar-->
                                   
					<div class="clear20"></div>
                    
                    <!--title-->
                  	<h1 class="heading-with-left-margin h1">Архив</h1>
					<!--/title-->                     
                    
                	<!--categories-->
                    <ul class="double-column-ul page-list-menu">
                        <li><a href="#">Январь 2015</a></li>
                        <li><a href="#">Февраль 2015</a></li>
                        <li><a href="#">Март 2015</a></li>
                        <li><a href="#">Апрель 2015</a></li>
                        <li><a href="#">Май 2015</a></li>
                        <li><a href="#">Июнь 2015</a></li>                        
                    </ul>
                    
                    <ul class="double-column-ul page-list-menu">
                        <li><a href="#">Июль 2015</a></li>
                        <li><a href="#">Август 2015</a></li>
                        <li><a href="#">Сентябрь 2015</a></li>
                        <li><a href="#">Октябрь 2015</a></li>
                        <li><a href="#">Ноябрь 2015</a></li>
                        <li><a href="#">Декабрь 2015</a></li>
                    </ul>                    
                    <!--/categories-->

                    <div class="clear20"></div>                    
                    
                    <!--simple tabs-->

                    <!--tab navigation-->
					<!--title-->
                  	<h1 class="heading-with-left-margin h1">Популярные</h1>
					<!--/title-->                     
                    <!--/tab navigation-->
					
                    <!--tab content-->
                 
						<ul runat="server" id="tabposts" class="tab-posts margin_bottom10">
                        </ul>

                    <!--title-->
                  	<h1 class="heading-with-left-margin h1">Случайное видео</h1>
					<!--/title-->   
                                        
                    <!--vimeo video-->
                    <iframe runat="server" ID="randomeVideo" src="" width="288" height="210" frameborder="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>                    <!--/vimeo video-->

                    <!--title-->
                  	<h1 class="heading-with-left-margin h1">Случайное фото</h1>
					<!--/title-->   

					<div class="clear"></div>

                    <!--flickr-->
                 <img id="randomeImages" runat="server" width="288" height="210" src="" />
                 <!--/flickr-->

                   
           	  	</div>
                <!--/side bar at right-->

            </div>
            <!-- /content container-->

	        <div class="clear40"></div>
	
        </div>
        <!--/page content wrapper-->
      
    </div>
    <!--/body wrapper-->    

</asp:Content>

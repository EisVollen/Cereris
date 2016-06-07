<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Blog.aspx.cs" Inherits="Cereris.Blog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Публикации</title>
<link href='http://fonts.googleapis.com/css?family=Terminal+Dosis:400,700,800,600,500,300,200' rel='stylesheet' type='text/css'>
<link rel="stylesheet" type="text/css" href="css/reset.css"/>
<link rel="stylesheet" type="text/css" href="css/style.css"/>
<link rel="stylesheet" type="text/css" href="css/default.css"/>
<!--[if lte IE 7]>
<link rel="stylesheet" type="text/css" href="css/ie7.css" />
<![endif]-->

<!--jquery library. 1.4.1 preffered-->
<script type="text/javascript" src="scripts/jquery-1.7.1.min.js"></script>

<!--jquery easing-->
<script type="text/javascript" src="scripts/jquery.easing.1.3.js"></script>
<!--/jquery easing-->

<!--nivo slider plugin for jquery-->
<script type="text/javascript" src="scripts/nivo-slider/jquery.nivo.slider.js"></script>
<link rel="stylesheet" type="text/css" href="scripts/nivo-slider/nivo-slider.css"/>
<!--/nivo slider plugin for jquery-->

<!--superfish dropdown-->
<link rel="stylesheet" type="text/css" href="scripts/superfish/css/superfish.css"/>
<script type="text/javascript" src="scripts/superfish/js/superfish.js"></script>
<script type="text/javascript" src="scripts/superfish/js/hoverIntent.js"></script>
<script type="text/javascript" src="scripts/superfish/js/jquery.bgiframe.min.js"></script>
<script type="text/javascript" src="scripts/superfish/js/supersubs.js"></script>
<!--/superfish dropdown-->

<!--auto complete-->
<script type='text/javascript' src='scripts/autocomplete/jquery.autocomplete.min.js'></script>
<link rel="stylesheet" type="text/css" href="scripts/autocomplete/jquery.autocomplete.css" />
<!--/auto complete-->

<!--misc. scripts-->
<script type="text/javascript" src="scripts/scripts.js"></script>
<!--/misc. scripts-->

<!--jquery color-->
<!--required for changing border colors etc.-->
<script type="text/javascript" src="scripts/jquery-color/jquery.color.js"></script>
<!--/jquery color-->

<!--flash & html5 media player-->
<script type='text/javascript' src='scripts/mediaplayer-5.8/jwplayer.js'></script>
<!--/flash & html5 media player-->

<!--jquery calendar-->
<script type="text/javascript" src="scripts/calendar/date.js"></script>
<script type="text/javascript" src="scripts/calendar/jquery.datePicker.js"></script>
<link rel="stylesheet" type="text/css" href="scripts/calendar/datePicker.css"/>
<!--/jquery calendar-->

<script type="text/javascript">
<!--
    var selectedDate;
$(document).ready(function() {
	
	$(".post-box").hover(function(){
		$(this).find(".date-box>div>div").stop(true, true).fadeIn("slow");
		
		$(this).stop().animate({ "borderTopColor": "#e53400","borderBottomColor": "#e53400","borderLeftColor": "#e53400","borderRightColor": "#e53400" }, 'slow');
	},function(){
		$(this).find(".date-box>div>div").stop(true, true).fadeOut("slow");
				
		$(this).stop().animate({ "borderTopColor": "#e1e1e1","borderBottomColor": "#e1e1e1","borderLeftColor": "#e1e1e1","borderRightColor": "#e1e1e1" }, 'slow');
	});	
	
	$('#inlineDatepicker').datePicker({
	    inline: true,
	    startDate: '16/06/1995',
	    endDate: (new Date()).asString()
        })
        .bind(
			'dateSelected',
			function (e, selectedDate, $td) {
			    var dateS = selectedDate.asString();
			    window.open("ApodDetail.aspx?date=" + dateS, '_self');
			    }
		);;

});
//-->
</script>

<!--add this script-->
<script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=xa-4edd10d035201268"></script>
<!--/add this script-->

</head>

<!--body-->
<body>
     <div id="header-contanier">    
        
            <!--header wrapper-->
            <div id="header" class="center-clear">   
           
                <!--header place holder-->
                <div class="header-place-holder"></div>
                
              
                <!--menu-->
                <ul id="top_menu" class="sf-menu">
                    <li><a href="Juno.aspx">Главная</a></li>
                    <li><a href="Blog.aspx">Публикации</a></li>                                                                
                    <li><a href="About.aspx">Контактная информация</a></li>           
                </ul>
                <!-- /menu-->
               
            </div>
            
        </div>
	<%--<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">--%>
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
            	<div runat="server" ID="postStore" class="page-content gray-frame nopadding blog-content" style="height: auto">
				 </div>   
                
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
                 <ul runat = "server" id="archeveLeftSide" class="double-column-ul page-list-menu" />

                 <ul runat="server" id="archeveRightSide" class="double-column-ul page-list-menu"/>
                        
                                        
                    <!--/categories-->

                    <div class="clear20"></div>                    
                    
                    <!--simple tabs-->

                    <!--tab navigation-->
					<ul class="tabs alignleft_block">
	                    <li><a href="#" class="defaulttab" rel="tabs1">Популярные</a></li>
                    </ul>
                    <!--/tab navigation-->
					
                    <!--tab content-->
                    <div class="tab-content" id="tabs1" style="height: auto">
						<ul runat="server" id="tabposts" style="height: auto" class="tab-posts margin_bottom10">
                        </ul>
                    </div>
                    
                  

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
                    <img id="randomeImages" runat="server" width="288" height="210" src=""/>  
                    <!--/flickr-->

                   
           	  	</div>
                <!--/side bar at right-->
               </div>
            <!-- /content container-->
           
	        <div class="clear40"></div>
	
        </div>
        <!--/page content wrapper-->
	</div>
    
     </asp:Content>
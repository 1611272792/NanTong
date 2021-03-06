(function ($) {
    $.nfinetab = {
        refreshTab: function () {            
            var currentId = $('.page-tabs-content').find('.active').attr('data-id');            
            var target = $('.NFine_iframe[data-id="' + currentId + '"]');            
            var url = target.attr('src');            
            $.loading(true);            
            target.attr('src', url).load(function () {               
                $.loading(false);
            });            
        },
        activeTab: function () {            
            var currentId = $(this).data('id');            
            if (!$(this).hasClass('active')) {                
                $('.mainContent .NFine_iframe').each(function () {                    
                    if ($(this).data('id') == currentId) {                        
                        $(this).show().siblings('.NFine_iframe').hide();
                        return false;
                    }
                });                
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                //$.nfinetab.scrollToTab(this);
            }
        },
        closeOtherTabs: function () {
            $('.page-tabs-content').children("[data-id]").find('.fa-remove').parents('a').not(".active").each(function () {
                $('.NFine_iframe[data-id="' + $(this).data('id') + '"]').remove();
                $(this).remove();
            });
            $('.page-tabs-content').css("margin-left", "0");
        },
        closeTab: function () {
            var closeTabId = $(this).parents('.menuTab').data('id');           
            var currentWidth = $(this).parents('.menuTab').width();           
            if ($(this).parents('.menuTab').hasClass('active')) {                
                if ($(this).parents('.menuTab').next('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').next('.menuTab:eq(0)').data('id');
                    $(this).parents('.menuTab').next('.menuTab:eq(0)').addClass('active');
                    $('.mainContent .NFine_iframe').each(function () {                        
                        if ($(this).data('id') == activeId) {                            
                            $(this).show().siblings('.NFine_iframe').hide();
                            return false;
                        }
                    });
                    var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'));
                    if (marginLeftVal < 0) {
                        $('.page-tabs-content').animate({
                            marginLeft: (marginLeftVal + currentWidth) + 'px'
                        }, "fast");
                    }
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .NFine_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
                if ($(this).parents('.menuTab').prev('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').prev('.menuTab:last').data('id');
                    $(this).parents('.menuTab').prev('.menuTab:last').addClass('active');
                    $('.mainContent .NFine_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.NFine_iframe').hide();
                            return false;
                        }
                    });
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .NFine_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }                
            }
            else {                
                $(this).parents('.menuTab').remove();
                $('.mainContent .NFine_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
                //$.nfinetab.scrollToTab($('.menuTab.active'));                
            }
            return false;
        },
        addTab: function () {           
            $("#header-nav>ul>li.open").removeClass("open");
            var dataId = $(this).attr('data-id');
            var dataUrl = $(this).attr('href');
            
            var menuName = $.trim($(this).text());
            var flag = true;
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                alert("Not Found");
                return false;
            }
            $('.menuTab').each(function () {                
                if ($(this).data('id') == dataUrl) {                    
                    if (!$(this).hasClass('active')) {                        
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $('.mainContent .NFine_iframe').each(function () {                            
                            if ($(this).data('id') == dataUrl) {                                
                                $(this).show().siblings('.NFine_iframe').hide();
                                return false;
                            }                            
                        });
                    }
                    flag = false;
                    return false;
                }                
            });
            if (flag) {                
                var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-remove"></i></a>';                
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="NFine_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';                
                $('.mainContent').find('iframe.NFine_iframe').hide();                
                $('.mainContent').append(str1);                
                $.loading(true);                
                $('.mainContent iframe:visible').load(function () {
                    $.loading(false);
                });                
                $('.menuTabs .page-tabs-content').append(str);

                var oUl = document.getElementById('menu');
                var list = document.getElementsByClassName("menuTab");
                //循环给每一个class为.menuTab的a标签添加右键菜单
                for (var i = 0; i < list.length; i++) {
                    list[i].oncontextmenu = function (ev) {
                        //右键选择当前tab选项卡
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        var oEvent = ev || event;
                        //一定要加px，要不然chrom不认
                        oUl.style.top = oEvent.clientY + 'px';
                        oUl.style.left = oEvent.clientX + 'px';
                        oUl.style.display = 'block';
                        return false;
                    }
                }
                document.onclick = function () {
                    oUl.style.display = 'none';
                };

                var iframe = document.getElementsByClassName('NFine_iframe');
                for (var i = 0; i < iframe.length; i++) {
                    iframe[i].onload = function () {
                        iframe[i-1].contentDocument.onclick = function () {
                            var oUl = document.getElementById('menu');
                            oUl.style.display = 'none';
                        };
                    }
                }


            }            
            return false;
        },        
        init: function () {
            $('.menuItem').on('click', $.nfinetab.addTab);
            $('.menuTabs').on('click', '.menuTab i', $.nfinetab.closeTab);
            $('.menuTabs').on('click', '.menuTab', $.nfinetab.activeTab);            
            $('.tabReload').on('click', $.nfinetab.refreshTab);            
            $('.tabCloseCurrent').on('click', function () {
                $('.page-tabs-content').find('.active i').trigger("click");
            });            
            $('.tabCloseAll').on('click', function () {                
                $('.page-tabs-content').children("[data-id]").find('.fa-remove').each(function () {
                    $('.NFine_iframe[data-id="' + $(this).data('id') + '"]').remove();
                    $(this).parents('a').remove();
                });
                $('.page-tabs-content').children("[data-id]:first").each(function () {
                    $('.NFine_iframe[data-id="' + $(this).data('id') + '"]').show();
                    $(this).addClass("active");
                });
                $('.page-tabs-content').css("margin-left", "0");               
            });
            $('.tabCloseOther').on('click', $.nfinetab.closeOtherTabs);            
        }
    };    
    $(function () {        
        $.nfinetab.init();        
    });
})(jQuery);
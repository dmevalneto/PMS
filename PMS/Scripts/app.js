/*!
 *
 * Angle - Bootstrap Admin Template
 *
 * Version: 4.0
 * Author: @themicon_co
 * Website: http://themicon.co
 * License: https://wrapbootstrap.com/help/licenses
 *
 */


(function(window, document, $, undefined) {
    'use strict';

    if (typeof $ === 'undefined') {
        throw new Error('This application\'s JavaScript requires jQuery');
    }

    $(function() {

        // Restore body classes
        // -----------------------------------
        var $body = $('body');
        new StateToggler().restoreState($body);

        // enable settings toggle after restore
        $('#chk-fixed').prop('checked', $body.hasClass('layout-fixed'));
        $('#chk-collapsed').prop('checked', $body.hasClass('aside-collapsed'));
        $('#chk-collapsed-text').prop('checked', $body.hasClass('aside-collapsed-text'));
        $('#chk-boxed').prop('checked', $body.hasClass('layout-boxed'));
        $('#chk-float').prop('checked', $body.hasClass('aside-float'));
        $('#chk-hover').prop('checked', $body.hasClass('aside-hover'));

        // When ready display the offsidebar
        $('.offsidebar.d-none').removeClass('d-none');

        // Disable warning "Synchronous XMLHttpRequest on the main thread is deprecated.."
        $.ajaxPrefilter(function(options, originalOptions, jqXHR) {
            options.async = true;
        });

    }); // doc ready

})(window, document, window.jQuery);
// Start Bootstrap JS
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initBootstrap);

    function initBootstrap() {

        // POPOVER
        // -----------------------------------

        $('[data-toggle="popover"]').popover();

        // TOOLTIP
        // -----------------------------------

        $('[data-toggle="tooltip"]').tooltip({
            container: 'body'
        });

        // DROPDOWN INPUTS
        // -----------------------------------
        $('.dropdown input').on('click focus', function(event) {
            event.stopPropagation();
        });

    }

})(window, document, window.jQuery);
// Module: card-tools
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initCardDismiss);
    $(initCardCollapse);
    $(initCardRefresh);


    /**
     * Dismiss cards
     * [data-tool="card-dismiss"]
     *
     * Requires animo.js
     */
    function initCardDismiss() {
        var cardSelector = '[data-tool="card-dismiss"]',
            removeEvent = 'card.remove',
            removedEvent = 'card.removed';

        $(document).on('click', cardSelector, function() {

            // find the first parent card
            var parent = $(this).closest('.card');
            var deferred = new $.Deferred();

            // Trigger the event and finally remove the element
            parent.trigger(removeEvent, [parent, deferred]);
            // needs resolve() to be called
            deferred.done(removeElement);

            function removeElement() {
                parent.animo({ animation: 'bounceOut' }, destroyCard);
            }

            function destroyCard() {
                var col = parent.parent();

                $.when(parent.trigger(removedEvent, [parent]))
                    .done(function() {
                        parent.remove();
                        // remove the parent if it is a row and is empty and not a sortable (portlet)
                        col
                            .trigger(removedEvent) // An event to catch when the card has been removed from DOM
                            .filter(function() {
                                var el = $(this);
                                return (el.is('[class*="col-"]:not(.sortable)') && el.children('*').length === 0);
                            }).remove();
                    });
            }
        });
    }


    /**
     * Collapse cards
     * [data-tool="card-collapse"]
     *
     * Also uses browser storage to keep track
     * of cards collapsed state
     */
    function initCardCollapse() {
        var cardSelector = '[data-tool="card-collapse"]',
            storageKeyName = 'jq-cardState';

        // Prepare the card to be collapsable and its events
        $(cardSelector).each(function() {
            // find the first parent card
            var $this = $(this),
                parent = $this.closest('.card'),
                wrapper = parent.find('.card-wrapper'),
                collapseOpts = { toggle: false },
                iconElement = $this.children('em'),
                cardId = parent.attr('id');

            // if wrapper not added, add it
            // we need a wrapper to avoid jumping due to the paddings
            if (!wrapper.length) {
                wrapper =
                    parent.children('.card-heading').nextAll() //find('.card-body, .card-footer')
                    .wrapAll('<div/>')
                    .parent()
                    .addClass('card-wrapper');
                collapseOpts = {};
            }

            // Init collapse and bind events to switch icons
            wrapper
                .collapse(collapseOpts)
                .on('hide.bs.collapse', function() {
                    setIconHide(iconElement);
                    saveCardState(cardId, 'hide');
                    wrapper.prev('.card-heading').addClass('card-heading-collapsed');
                })
                .on('show.bs.collapse', function() {
                    setIconShow(iconElement);
                    saveCardState(cardId, 'show');
                    wrapper.prev('.card-heading').removeClass('card-heading-collapsed');
                });

            // Load the saved state if exists
            var currentState = loadCardState(cardId);
            if (currentState) {
                setTimeout(function() { wrapper.collapse(currentState); }, 50);
                saveCardState(cardId, currentState);
            }

        });

        // finally catch clicks to toggle card collapse
        $(document).on('click', cardSelector, function() {

            var parent = $(this).closest('.card');
            var wrapper = parent.find('.card-wrapper');

            wrapper.collapse('toggle');

        });

        /////////////////////////////////////////////
        // Common use functions for card collapse //
        /////////////////////////////////////////////
        function setIconShow(iconEl) {
            iconEl.removeClass('fa-plus').addClass('fa-minus');
        }

        function setIconHide(iconEl) {
            iconEl.removeClass('fa-minus').addClass('fa-plus');
        }

        function saveCardState(id, state) {
            var data = Storages.localStorage.get(storageKeyName);
            if (!data) { data = {}; }
            data[id] = state;
            Storages.localStorage.set(storageKeyName, data);
        }

        function loadCardState(id) {
            var data = Storages.localStorage.get(storageKeyName);
            if (data) {
                return data[id] || false;
            }
        }
    }


    /**
     * Refresh cards
     * [data-tool="card-refresh"]
     * [data-spinner="standard"]
     */
    function initCardRefresh() {
        var cardSelector = '[data-tool="card-refresh"]',
            refreshEvent = 'card.refresh',
            whirlClass = 'whirl',
            defaultSpinner = 'standard';

        // method to clear the spinner when done
        function removeSpinner() {
            this.removeClass(whirlClass);
        }

        // catch clicks to toggle card refresh
        $(document).on('click', cardSelector, function() {
            var $this = $(this),
                card = $this.parents('.card').eq(0),
                spinner = $this.data('spinner') || defaultSpinner;

            // start showing the spinner
            card.addClass(whirlClass + ' ' + spinner);

            // attach as public method
            card.removeSpinner = removeSpinner;

            // Trigger the event and send the card object
            $this.trigger(refreshEvent, [card]);

        });
    }

})(window, document, window.jQuery);



// GLOBAL CONSTANTS
// -----------------------------------

(function(window, document, $, undefined){

    window.APP_COLORS = {
        'primary':                '#5d9cec',
        'success':                '#27c24c',
        'info':                   '#23b7e5',
        'warning':                '#ff902b',
        'danger':                 '#f05050',
        'inverse':                '#131e26',
        'green':                  '#37bc9b',
        'pink':                   '#f532e5',
        'purple':                 '#7266ba',
        'dark':                   '#3a3f51',
        'yellow':                 '#fad732',
        'gray-darker':            '#232735',
        'gray-dark':              '#3a3f51',
        'gray':                   '#dde6e9',
        'gray-light':             '#e4eaec',
        'gray-lighter':           '#edf1f2'
    };

    window.APP_MEDIAQUERY = {
        'desktopLG':             1200,
        'desktop':                992,
        'tablet':                 768,
        'mobile':                 480
    };

})(window, document, window.jQuery);
// FULLSCREEN
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initScreenFull);

    function initScreenFull() {
        if (typeof screenfull === 'undefined') return;

        var $doc = $(document);
        var $fsToggler = $('[data-toggle-fullscreen]');

        // Not supported under IE
        var ua = window.navigator.userAgent;
        if (ua.indexOf("MSIE ") > 0 || !!ua.match(/Trident.*rv\:11\./)) {
            $fsToggler.addClass('hide');
        }

        if (!$fsToggler.is(':visible')) // hidden on mobiles or IE
            return;

        $fsToggler.on('click', function(e) {
            e.preventDefault();

            if (screenfull.enabled) {

                screenfull.toggle();

                // Switch icon indicator
                toggleFSIcon($fsToggler);

            } else {
                console.log('Fullscreen not enabled');
            }
        });

        if (screenfull.raw && screenfull.raw.fullscreenchange)
            $doc.on(screenfull.raw.fullscreenchange, function() {
                toggleFSIcon($fsToggler);
            });

        function toggleFSIcon($element) {
            if (screenfull.isFullscreen)
                $element.children('em').removeClass('fa-expand').addClass('fa-compress');
            else
                $element.children('em').removeClass('fa-compress').addClass('fa-expand');
        }

    }

})(window, document, window.jQuery);
// LOAD CUSTOM CSS
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initLoadCSS);

    function initLoadCSS() {

        $('[data-load-css]').on('click', function(e) {

            var element = $(this);

            if (element.is('a'))
                e.preventDefault();

            var uri = element.data('loadCss'),
                link;

            if (uri) {
                link = createLink(uri);
                if (!link) {
                    $.error('Error creating stylesheet link element.');
                }
            } else {
                $.error('No stylesheet location defined.');
            }

        });
    }

    function createLink(uri) {
        var linkId = 'autoloaded-stylesheet',
            oldLink = $('#' + linkId).attr('id', linkId + '-old');

        $('head').append($('<link/>').attr({
            'id': linkId,
            'rel': 'stylesheet',
            'href': uri
        }));

        if (oldLink.length) {
            oldLink.remove();
        }

        return $('#' + linkId);
    }

})(window, document, window.jQuery);
// TRANSLATION
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initTranslation);


    var preferredLang = 'en';
    var pathPrefix = '/Content/i18n'; // folder of json files
    var packName = 'site';
    var storageKey = 'jq-appLang';

    function initTranslation() {

        if (!$.fn.localize) return;

        // detect saved language or use default
        var currLang = Storages.localStorage.get(storageKey) || preferredLang;
        // set initial options
        var opts = {
            language: currLang,
            pathPrefix: pathPrefix,
            callback: function(data, defaultCallback) {
                Storages.localStorage.set(storageKey, currLang); // save the language
                defaultCallback(data);
            }
        };

        // Set initial language
        setLanguage(opts);

        // Listen for changes
        $('[data-set-lang]').on('click', function() {

            currLang = $(this).data('setLang');

            if (currLang) {

                opts.language = currLang;

                setLanguage(opts);

                activateDropdown($(this));
            }

        });


        function setLanguage(options) {
            $("[data-localize]").localize(packName, options);
        }

        // Set the current clicked text as the active dropdown text
        function activateDropdown(elem) {
            var menu = elem.parents('.dropdown-menu');
            if (menu.length) {
                var toggle = menu.prev('button, a');
                toggle.text(elem.text());
            }
        }

    }

})(window, document, window.jQuery);
// NAVBAR SEARCH
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initNavbarSearch);

    function initNavbarSearch() {

        var navSearch = new navbarSearchInput();

        // Open search input
        var $searchOpen = $('[data-search-open]');

        $searchOpen
            .on('click', function(e) { e.stopPropagation(); })
            .on('click', navSearch.toggle);

        // Close search input
        var $searchDismiss = $('[data-search-dismiss]');
        var inputSelector = '.navbar-form input[type="text"]';

        $(inputSelector)
            .on('click', function(e) { e.stopPropagation(); })
            .on('keyup', function(e) {
                if (e.keyCode == 27) // ESC
                    navSearch.dismiss();
            });

        // click anywhere closes the search
        $(document).on('click', navSearch.dismiss);
        // dismissable options
        $searchDismiss
            .on('click', function(e) { e.stopPropagation(); })
            .on('click', navSearch.dismiss);

    }

    var navbarSearchInput = function() {
        var navbarFormSelector = 'form.navbar-form';
        return {
            toggle: function() {

                var navbarForm = $(navbarFormSelector);

                navbarForm.toggleClass('open');

                var isOpen = navbarForm.hasClass('open');

                navbarForm.find('input')[isOpen ? 'focus' : 'blur']();

            },

            dismiss: function() {
                $(navbarFormSelector)
                    .removeClass('open') // Close control
                    .find('input[type="text"]').blur() // remove focus
                // .val('')                    // Empty input
                ;
            }
        };

    }

})(window, document, window.jQuery);
// NOW TIMER
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initNowTimer);

    function initNowTimer() {

        $('[data-now]').each(function() {
            var element = $(this),
                format = element.data('format');

            function updateTime() {
                var dt = moment(new Date()).format(format);
                element.text(dt);
            }

            updateTime();
            setInterval(updateTime, 1000);

        });
    }

})(window, document, window.jQuery);
// Toggle RTL mode for demo
// -----------------------------------


(function(window, document, $, undefined) {
    'use strict';

    $(initRTL);

    function initRTL() {
        var maincss = $('#maincss');
        var bscss = $('#bscss');
        $('#chk-rtl').on('change', function() {
            // app rtl check
            maincss.attr('href', this.checked ? '/Content/css/app-rtl.css' : '/Content/css/app.css');
            // bootstrap rtl check
            bscss.attr('href', this.checked ? '/Content/css/bootstrap-rtl.css' : '/Content/css/bootstrap.css');
        });
    }

})(window, document, window.jQuery);
// SIDEBAR
// -----------------------------------


(function(window, document, $, undefined) {
    'use strict';

    $(initSidebar);


    var $win;
    var $html;
    var $body;
    var $sidebar;

    function initSidebar() {

        $win = $(window);
        $html = $('html');
        $body = $('body');
        $sidebar = $('.sidebar');

        // AUTOCOLLAPSE ITEMS
        // -----------------------------------

        var sidebarCollapse = $sidebar.find('.collapse');
        sidebarCollapse.on('show.bs.collapse', function(event) {

            event.stopPropagation();
            if ($(this).parents('.collapse').length === 0)
                sidebarCollapse.filter('.show').collapse('hide');

        });

        // SIDEBAR ACTIVE STATE
        // -----------------------------------

        // Find current active item
        var currentItem = $('.sidebar .active').parents('li');

        // hover mode don't try to expand active collapse
        if (!useAsideHover())
            currentItem
            .addClass('active') // activate the parent
            .children('.collapse') // find the collapse
            .collapse('show'); // and show it

        // remove this if you use only collapsible sidebar items
        $sidebar.find('li > a + ul').on('show.bs.collapse', function(e) {
            if (useAsideHover()) e.preventDefault();
        });

        // SIDEBAR COLLAPSED ITEM HANDLER
        // -----------------------------------


        var eventName = isTouch() ? 'click' : 'mouseenter';
        var subNav = $();
        $sidebar.on(eventName, '.sidebar-nav > li', function() {

            if (isSidebarCollapsed() || useAsideHover()) {

                subNav.trigger('mouseleave');
                subNav = toggleMenuItem($(this));

                // Used to detect click and touch events outside the sidebar
                sidebarAddBackdrop();
            }

        });

        var sidebarAnyclickClose = $sidebar.data('sidebarAnyclickClose');

        // Allows to close
        if (typeof sidebarAnyclickClose !== 'undefined') {

            $('.wrapper').on('click.sidebar', function(e) {
                // don't check if sidebar not visible
                if (!$body.hasClass('aside-toggled')) return;

                var $target = $(e.target);
                if (!$target.parents('.aside-container').length && // if not child of sidebar
                    !$target.is('#user-block-toggle') && // user block toggle anchor
                    !$target.parent().is('#user-block-toggle') // user block toggle icon
                ) {
                    $body.removeClass('aside-toggled');
                }

            });
        }
    }

    function sidebarAddBackdrop() {
        var $backdrop = $('<div/>', { 'class': 'dropdown-backdrop' });
        $backdrop.insertAfter('.aside-container').on("click mouseenter", function() {
            removeFloatingNav();
        });
    }

    // Open the collapse sidebar submenu items when on touch devices
    // - desktop only opens on hover
    function toggleTouchItem($element) {
        $element
            .siblings('li')
            .removeClass('open')
            .end()
            .toggleClass('open');
    }

    // Handles hover to open items under collapsed menu
    // -----------------------------------
    function toggleMenuItem($listItem) {

        removeFloatingNav();

        var ul = $listItem.children('ul');

        if (!ul.length) return $();
        if ($listItem.hasClass('open')) {
            toggleTouchItem($listItem);
            return $();
        }

        var $aside = $('.aside-container');
        var $asideInner = $('.aside-inner'); // for top offset calculation
        // float aside uses extra padding on aside
        var mar = parseInt($asideInner.css('padding-top'), 0) + parseInt($aside.css('padding-top'), 0);

        var subNav = ul.clone().appendTo($aside);

        toggleTouchItem($listItem);

        var itemTop = ($listItem.position().top + mar) - $sidebar.scrollTop();
        var vwHeight = $win.height();

        subNav
            .addClass('nav-floating')
            .css({
                position: isFixed() ? 'fixed' : 'absolute',
                top: itemTop,
                bottom: (subNav.outerHeight(true) + itemTop > vwHeight) ? 0 : 'auto'
            });

        subNav.on('mouseleave', function() {
            toggleTouchItem($listItem);
            subNav.remove();
        });

        return subNav;
    }

    function removeFloatingNav() {
        $('.sidebar-subnav.nav-floating').remove();
        $('.dropdown-backdrop').remove();
        $('.sidebar li.open').removeClass('open');
    }

    function isTouch() {
        return $html.hasClass('touch');
    }

    function isSidebarCollapsed() {
        return $body.hasClass('aside-collapsed') || $body.hasClass('aside-collapsed-text');
    }

    function isSidebarToggled() {
        return $body.hasClass('aside-toggled');
    }

    function isMobile() {
        return $win.width() < APP_MEDIAQUERY.tablet;
    }

    function isFixed() {
        return $body.hasClass('layout-fixed');
    }

    function useAsideHover() {
        return $body.hasClass('aside-hover');
    }

})(window, document, window.jQuery);
// SLIMSCROLL
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initSlimsSroll);

    function initSlimsSroll() {

        $('[data-scrollable]').each(function() {

            var element = $(this),
                defaultHeight = 250;

            element.slimScroll({
                height: (element.data('height') || defaultHeight)
            });

        });
    }

})(window, document, window.jQuery);
// Custom jQuery
// -----------------------------------


(function(window, document, $, undefined) {
    'use strict';

    $(initTableCheckAll);

    function initTableCheckAll() {

        $('[data-check-all]').on('change', function() {
            var $this = $(this),
                index = $this.index() + 1,
                checkbox = $this.find('input[type="checkbox"]'),
                table = $this.parents('table');
            // Make sure to affect only the correct checkbox column
            table.find('tbody > tr > td:nth-child(' + index + ') input[type="checkbox"]')
                .prop('checked', checkbox[0].checked);

        });

    }

})(window, document, window.jQuery);
// TOGGLE STATE
// -----------------------------------

(function(window, document, $, undefined) {
    'use strict';

    $(initToggleState);

    function initToggleState() {

        var $body = $('body');
        var toggle = new StateToggler();

        $('[data-toggle-state]')
            .on('click', function(e) {
                // e.preventDefault();
                e.stopPropagation();
                var element = $(this),
                    classname = element.data('toggleState'),
                    target = element.data('target'),
                    noPersist = (element.attr('data-no-persist') !== undefined);

                // Specify a target selector to toggle classname
                // use body by default
                var $target = target ? $(target) : $body;

                if (classname) {
                    if ($target.hasClass(classname)) {
                        $target.removeClass(classname);
                        if (!noPersist)
                            toggle.removeState(classname);
                    } else {
                        $target.addClass(classname);
                        if (!noPersist)
                            toggle.addState(classname);
                    }

                }

                // some elements may need this when toggled class change the content size
                $(window).resize();

            });

    }

    // Handle states to/from localstorage
    var StateToggler = function() {

        var STORAGE_KEY_NAME = 'jq-toggleState';

        /** Add a state to the browser storage to be restored later */
        this.addState = function(classname) {
            var data = Storages.localStorage.get(STORAGE_KEY_NAME);
            if (data instanceof Array) data.push(classname);
            else data = [classname];
            Storages.localStorage.set(STORAGE_KEY_NAME, data);
        };
        /** Remove a state from the browser storage */
        this.removeState = function(classname) {
            var data = Storages.localStorage.get(STORAGE_KEY_NAME);
            if (data) {
                var index = data.indexOf(classname);
                if (index !== -1) data.splice(index, 1);
                Storages.localStorage.set(STORAGE_KEY_NAME, data);
            }
        };
        /** Load the state string and restore the classlist */
        this.restoreState = function($elem) {
            var data = Storages.localStorage.get(STORAGE_KEY_NAME);
            if (data instanceof Array)
                $elem.addClass(data.join(' '));
        };
    };

    window.StateToggler = StateToggler;

})(window, document, window.jQuery);
/**=========================================================
 * Module: trigger-resize.js
 * Triggers a window resize event from any element
 =========================================================*/

(function(window, document, $, undefined) {
    'use strict';

    $(initTriggerResize);

    function initTriggerResize() {
        var element = $('[data-trigger-resize]');
        var value = element.data('triggerResize')
        element.on('click', function() {
            setTimeout(function() {
                // all IE friendly dispatchEvent
                var evt = document.createEvent('UIEvents');
                evt.initUIEvent('resize', true, false, window, 0);
                window.dispatchEvent(evt);
                // modern dispatchEvent way
                // window.dispatchEvent(new Event('resize'));
            }, value || 300);
        });
    }

})(window, document, window.jQuery);
// Custom jQuery
// -----------------------------------


(function(window, document, $, undefined) {
    'use strict';

    $(initCustom);

    function initCustom() {

        // custom code

    }

})(window, document, window.jQuery);
//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC5pbml0LmpzIiwiY29tbW9uL2Jvb3RzdHJhcC1zdGFydC5qcyIsImNvbW1vbi9jYXJkLXRvb2xzLmpzIiwiY29tbW9uL2NvbnN0YW50cy5qcyIsImNvbW1vbi9mdWxsc2NyZWVuLmpzIiwiY29tbW9uL2xvYWQtY3NzLmpzIiwiY29tbW9uL2xvY2FsaXplLmpzIiwiY29tbW9uL25hdmJhci1zZWFyY2guanMiLCJjb21tb24vbm93LmpzIiwiY29tbW9uL3J0bC5qcyIsImNvbW1vbi9zaWRlYmFyLmpzIiwiY29tbW9uL3NsaW1zY3JvbGwuanMiLCJjb21tb24vdGFibGUtY2hlY2thbGwuanMiLCJjb21tb24vdG9nZ2xlLXN0YXRlLmpzIiwiY29tbW9uL3RyaWdnZXItcmVzaXplLmpzIiwiY3VzdG9tLmpzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiJBQUFBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQzVDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQzlCQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUN6TEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQy9CQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FDcERBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUNqREE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FDakVBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQ2pFQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FDekJBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQ3BCQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FDckxBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUN0QkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUN4QkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FDN0VBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUN6QkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EiLCJmaWxlIjoiYXBwLmpzIiwic291cmNlc0NvbnRlbnQiOlsiLyohXHJcbiAqXHJcbiAqIEFuZ2xlIC0gQm9vdHN0cmFwIEFkbWluIFRlbXBsYXRlXHJcbiAqXHJcbiAqIFZlcnNpb246IEB2ZXJzaW9uQFxyXG4gKiBBdXRob3I6IEBhdXRob3JAXHJcbiAqIFdlYnNpdGU6IEB1cmxAXHJcbiAqIExpY2Vuc2U6IEBsaWNlbnNlQFxyXG4gKlxyXG4gKi9cclxuXHJcblxyXG4oZnVuY3Rpb24od2luZG93LCBkb2N1bWVudCwgJCwgdW5kZWZpbmVkKSB7XHJcbiAgICAndXNlIHN0cmljdCc7XHJcblxyXG4gICAgaWYgKHR5cGVvZiAkID09PSAndW5kZWZpbmVkJykge1xyXG4gICAgICAgIHRocm93IG5ldyBFcnJvcignVGhpcyBhcHBsaWNhdGlvblxcJ3MgSmF2YVNjcmlwdCByZXF1aXJlcyBqUXVlcnknKTtcclxuICAgIH1cclxuXHJcbiAgICAkKGZ1bmN0aW9uKCkge1xyXG5cclxuICAgICAgICAvLyBSZXN0b3JlIGJvZHkgY2xhc3Nlc1xyXG4gICAgICAgIC8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbiAgICAgICAgdmFyICRib2R5ID0gJCgnYm9keScpO1xyXG4gICAgICAgIG5ldyBTdGF0ZVRvZ2dsZXIoKS5yZXN0b3JlU3RhdGUoJGJvZHkpO1xyXG5cclxuICAgICAgICAvLyBlbmFibGUgc2V0dGluZ3MgdG9nZ2xlIGFmdGVyIHJlc3RvcmVcclxuICAgICAgICAkKCcjY2hrLWZpeGVkJykucHJvcCgnY2hlY2tlZCcsICRib2R5Lmhhc0NsYXNzKCdsYXlvdXQtZml4ZWQnKSk7XHJcbiAgICAgICAgJCgnI2Noay1jb2xsYXBzZWQnKS5wcm9wKCdjaGVja2VkJywgJGJvZHkuaGFzQ2xhc3MoJ2FzaWRlLWNvbGxhcHNlZCcpKTtcclxuICAgICAgICAkKCcjY2hrLWNvbGxhcHNlZC10ZXh0JykucHJvcCgnY2hlY2tlZCcsICRib2R5Lmhhc0NsYXNzKCdhc2lkZS1jb2xsYXBzZWQtdGV4dCcpKTtcclxuICAgICAgICAkKCcjY2hrLWJveGVkJykucHJvcCgnY2hlY2tlZCcsICRib2R5Lmhhc0NsYXNzKCdsYXlvdXQtYm94ZWQnKSk7XHJcbiAgICAgICAgJCgnI2Noay1mbG9hdCcpLnByb3AoJ2NoZWNrZWQnLCAkYm9keS5oYXNDbGFzcygnYXNpZGUtZmxvYXQnKSk7XHJcbiAgICAgICAgJCgnI2Noay1ob3ZlcicpLnByb3AoJ2NoZWNrZWQnLCAkYm9keS5oYXNDbGFzcygnYXNpZGUtaG92ZXInKSk7XHJcblxyXG4gICAgICAgIC8vIFdoZW4gcmVhZHkgZGlzcGxheSB0aGUgb2Zmc2lkZWJhclxyXG4gICAgICAgICQoJy5vZmZzaWRlYmFyLmQtbm9uZScpLnJlbW92ZUNsYXNzKCdkLW5vbmUnKTtcclxuXHJcbiAgICAgICAgLy8gRGlzYWJsZSB3YXJuaW5nIFwiU3luY2hyb25vdXMgWE1MSHR0cFJlcXVlc3Qgb24gdGhlIG1haW4gdGhyZWFkIGlzIGRlcHJlY2F0ZWQuLlwiXHJcbiAgICAgICAgJC5hamF4UHJlZmlsdGVyKGZ1bmN0aW9uKG9wdGlvbnMsIG9yaWdpbmFsT3B0aW9ucywganFYSFIpIHtcclxuICAgICAgICAgICAgb3B0aW9ucy5hc3luYyA9IHRydWU7XHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgfSk7IC8vIGRvYyByZWFkeVxyXG5cclxufSkod2luZG93LCBkb2N1bWVudCwgd2luZG93LmpRdWVyeSk7IiwiLy8gU3RhcnQgQm9vdHN0cmFwIEpTXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4oZnVuY3Rpb24od2luZG93LCBkb2N1bWVudCwgJCwgdW5kZWZpbmVkKSB7XHJcbiAgICAndXNlIHN0cmljdCc7XHJcblxyXG4gICAgJChpbml0Qm9vdHN0cmFwKTtcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0Qm9vdHN0cmFwKCkge1xyXG5cclxuICAgICAgICAvLyBQT1BPVkVSXHJcbiAgICAgICAgLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbiAgICAgICAgJCgnW2RhdGEtdG9nZ2xlPVwicG9wb3ZlclwiXScpLnBvcG92ZXIoKTtcclxuXHJcbiAgICAgICAgLy8gVE9PTFRJUFxyXG4gICAgICAgIC8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4gICAgICAgICQoJ1tkYXRhLXRvZ2dsZT1cInRvb2x0aXBcIl0nKS50b29sdGlwKHtcclxuICAgICAgICAgICAgY29udGFpbmVyOiAnYm9keSdcclxuICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgLy8gRFJPUERPV04gSU5QVVRTXHJcbiAgICAgICAgLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuICAgICAgICAkKCcuZHJvcGRvd24gaW5wdXQnKS5vbignY2xpY2sgZm9jdXMnLCBmdW5jdGlvbihldmVudCkge1xyXG4gICAgICAgICAgICBldmVudC5zdG9wUHJvcGFnYXRpb24oKTtcclxuICAgICAgICB9KTtcclxuXHJcbiAgICB9XHJcblxyXG59KSh3aW5kb3csIGRvY3VtZW50LCB3aW5kb3cualF1ZXJ5KTsiLCIvLyBNb2R1bGU6IGNhcmQtdG9vbHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbihmdW5jdGlvbih3aW5kb3csIGRvY3VtZW50LCAkLCB1bmRlZmluZWQpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuXHJcbiAgICAkKGluaXRDYXJkRGlzbWlzcyk7XHJcbiAgICAkKGluaXRDYXJkQ29sbGFwc2UpO1xyXG4gICAgJChpbml0Q2FyZFJlZnJlc2gpO1xyXG5cclxuXHJcbiAgICAvKipcclxuICAgICAqIERpc21pc3MgY2FyZHNcclxuICAgICAqIFtkYXRhLXRvb2w9XCJjYXJkLWRpc21pc3NcIl1cclxuICAgICAqXHJcbiAgICAgKiBSZXF1aXJlcyBhbmltby5qc1xyXG4gICAgICovXHJcbiAgICBmdW5jdGlvbiBpbml0Q2FyZERpc21pc3MoKSB7XHJcbiAgICAgICAgdmFyIGNhcmRTZWxlY3RvciA9ICdbZGF0YS10b29sPVwiY2FyZC1kaXNtaXNzXCJdJyxcclxuICAgICAgICAgICAgcmVtb3ZlRXZlbnQgPSAnY2FyZC5yZW1vdmUnLFxyXG4gICAgICAgICAgICByZW1vdmVkRXZlbnQgPSAnY2FyZC5yZW1vdmVkJztcclxuXHJcbiAgICAgICAgJChkb2N1bWVudCkub24oJ2NsaWNrJywgY2FyZFNlbGVjdG9yLCBmdW5jdGlvbigpIHtcclxuXHJcbiAgICAgICAgICAgIC8vIGZpbmQgdGhlIGZpcnN0IHBhcmVudCBjYXJkXHJcbiAgICAgICAgICAgIHZhciBwYXJlbnQgPSAkKHRoaXMpLmNsb3Nlc3QoJy5jYXJkJyk7XHJcbiAgICAgICAgICAgIHZhciBkZWZlcnJlZCA9IG5ldyAkLkRlZmVycmVkKCk7XHJcblxyXG4gICAgICAgICAgICAvLyBUcmlnZ2VyIHRoZSBldmVudCBhbmQgZmluYWxseSByZW1vdmUgdGhlIGVsZW1lbnRcclxuICAgICAgICAgICAgcGFyZW50LnRyaWdnZXIocmVtb3ZlRXZlbnQsIFtwYXJlbnQsIGRlZmVycmVkXSk7XHJcbiAgICAgICAgICAgIC8vIG5lZWRzIHJlc29sdmUoKSB0byBiZSBjYWxsZWRcclxuICAgICAgICAgICAgZGVmZXJyZWQuZG9uZShyZW1vdmVFbGVtZW50KTtcclxuXHJcbiAgICAgICAgICAgIGZ1bmN0aW9uIHJlbW92ZUVsZW1lbnQoKSB7XHJcbiAgICAgICAgICAgICAgICBwYXJlbnQuYW5pbW8oeyBhbmltYXRpb246ICdib3VuY2VPdXQnIH0sIGRlc3Ryb3lDYXJkKTtcclxuICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgZnVuY3Rpb24gZGVzdHJveUNhcmQoKSB7XHJcbiAgICAgICAgICAgICAgICB2YXIgY29sID0gcGFyZW50LnBhcmVudCgpO1xyXG5cclxuICAgICAgICAgICAgICAgICQud2hlbihwYXJlbnQudHJpZ2dlcihyZW1vdmVkRXZlbnQsIFtwYXJlbnRdKSlcclxuICAgICAgICAgICAgICAgICAgICAuZG9uZShmdW5jdGlvbigpIHtcclxuICAgICAgICAgICAgICAgICAgICAgICAgcGFyZW50LnJlbW92ZSgpO1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAvLyByZW1vdmUgdGhlIHBhcmVudCBpZiBpdCBpcyBhIHJvdyBhbmQgaXMgZW1wdHkgYW5kIG5vdCBhIHNvcnRhYmxlIChwb3J0bGV0KVxyXG4gICAgICAgICAgICAgICAgICAgICAgICBjb2xcclxuICAgICAgICAgICAgICAgICAgICAgICAgICAgIC50cmlnZ2VyKHJlbW92ZWRFdmVudCkgLy8gQW4gZXZlbnQgdG8gY2F0Y2ggd2hlbiB0aGUgY2FyZCBoYXMgYmVlbiByZW1vdmVkIGZyb20gRE9NXHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAuZmlsdGVyKGZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIHZhciBlbCA9ICQodGhpcyk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgcmV0dXJuIChlbC5pcygnW2NsYXNzKj1cImNvbC1cIl06bm90KC5zb3J0YWJsZSknKSAmJiBlbC5jaGlsZHJlbignKicpLmxlbmd0aCA9PT0gMCk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB9KS5yZW1vdmUoKTtcclxuICAgICAgICAgICAgICAgICAgICB9KTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG5cclxuXHJcbiAgICAvKipcclxuICAgICAqIENvbGxhcHNlIGNhcmRzXHJcbiAgICAgKiBbZGF0YS10b29sPVwiY2FyZC1jb2xsYXBzZVwiXVxyXG4gICAgICpcclxuICAgICAqIEFsc28gdXNlcyBicm93c2VyIHN0b3JhZ2UgdG8ga2VlcCB0cmFja1xyXG4gICAgICogb2YgY2FyZHMgY29sbGFwc2VkIHN0YXRlXHJcbiAgICAgKi9cclxuICAgIGZ1bmN0aW9uIGluaXRDYXJkQ29sbGFwc2UoKSB7XHJcbiAgICAgICAgdmFyIGNhcmRTZWxlY3RvciA9ICdbZGF0YS10b29sPVwiY2FyZC1jb2xsYXBzZVwiXScsXHJcbiAgICAgICAgICAgIHN0b3JhZ2VLZXlOYW1lID0gJ2pxLWNhcmRTdGF0ZSc7XHJcblxyXG4gICAgICAgIC8vIFByZXBhcmUgdGhlIGNhcmQgdG8gYmUgY29sbGFwc2FibGUgYW5kIGl0cyBldmVudHNcclxuICAgICAgICAkKGNhcmRTZWxlY3RvcikuZWFjaChmdW5jdGlvbigpIHtcclxuICAgICAgICAgICAgLy8gZmluZCB0aGUgZmlyc3QgcGFyZW50IGNhcmRcclxuICAgICAgICAgICAgdmFyICR0aGlzID0gJCh0aGlzKSxcclxuICAgICAgICAgICAgICAgIHBhcmVudCA9ICR0aGlzLmNsb3Nlc3QoJy5jYXJkJyksXHJcbiAgICAgICAgICAgICAgICB3cmFwcGVyID0gcGFyZW50LmZpbmQoJy5jYXJkLXdyYXBwZXInKSxcclxuICAgICAgICAgICAgICAgIGNvbGxhcHNlT3B0cyA9IHsgdG9nZ2xlOiBmYWxzZSB9LFxyXG4gICAgICAgICAgICAgICAgaWNvbkVsZW1lbnQgPSAkdGhpcy5jaGlsZHJlbignZW0nKSxcclxuICAgICAgICAgICAgICAgIGNhcmRJZCA9IHBhcmVudC5hdHRyKCdpZCcpO1xyXG5cclxuICAgICAgICAgICAgLy8gaWYgd3JhcHBlciBub3QgYWRkZWQsIGFkZCBpdFxyXG4gICAgICAgICAgICAvLyB3ZSBuZWVkIGEgd3JhcHBlciB0byBhdm9pZCBqdW1waW5nIGR1ZSB0byB0aGUgcGFkZGluZ3NcclxuICAgICAgICAgICAgaWYgKCF3cmFwcGVyLmxlbmd0aCkge1xyXG4gICAgICAgICAgICAgICAgd3JhcHBlciA9XHJcbiAgICAgICAgICAgICAgICAgICAgcGFyZW50LmNoaWxkcmVuKCcuY2FyZC1oZWFkaW5nJykubmV4dEFsbCgpIC8vZmluZCgnLmNhcmQtYm9keSwgLmNhcmQtZm9vdGVyJylcclxuICAgICAgICAgICAgICAgICAgICAud3JhcEFsbCgnPGRpdi8+JylcclxuICAgICAgICAgICAgICAgICAgICAucGFyZW50KClcclxuICAgICAgICAgICAgICAgICAgICAuYWRkQ2xhc3MoJ2NhcmQtd3JhcHBlcicpO1xyXG4gICAgICAgICAgICAgICAgY29sbGFwc2VPcHRzID0ge307XHJcbiAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgIC8vIEluaXQgY29sbGFwc2UgYW5kIGJpbmQgZXZlbnRzIHRvIHN3aXRjaCBpY29uc1xyXG4gICAgICAgICAgICB3cmFwcGVyXHJcbiAgICAgICAgICAgICAgICAuY29sbGFwc2UoY29sbGFwc2VPcHRzKVxyXG4gICAgICAgICAgICAgICAgLm9uKCdoaWRlLmJzLmNvbGxhcHNlJywgZnVuY3Rpb24oKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgc2V0SWNvbkhpZGUoaWNvbkVsZW1lbnQpO1xyXG4gICAgICAgICAgICAgICAgICAgIHNhdmVDYXJkU3RhdGUoY2FyZElkLCAnaGlkZScpO1xyXG4gICAgICAgICAgICAgICAgICAgIHdyYXBwZXIucHJldignLmNhcmQtaGVhZGluZycpLmFkZENsYXNzKCdjYXJkLWhlYWRpbmctY29sbGFwc2VkJyk7XHJcbiAgICAgICAgICAgICAgICB9KVxyXG4gICAgICAgICAgICAgICAgLm9uKCdzaG93LmJzLmNvbGxhcHNlJywgZnVuY3Rpb24oKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgc2V0SWNvblNob3coaWNvbkVsZW1lbnQpO1xyXG4gICAgICAgICAgICAgICAgICAgIHNhdmVDYXJkU3RhdGUoY2FyZElkLCAnc2hvdycpO1xyXG4gICAgICAgICAgICAgICAgICAgIHdyYXBwZXIucHJldignLmNhcmQtaGVhZGluZycpLnJlbW92ZUNsYXNzKCdjYXJkLWhlYWRpbmctY29sbGFwc2VkJyk7XHJcbiAgICAgICAgICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgICAgIC8vIExvYWQgdGhlIHNhdmVkIHN0YXRlIGlmIGV4aXN0c1xyXG4gICAgICAgICAgICB2YXIgY3VycmVudFN0YXRlID0gbG9hZENhcmRTdGF0ZShjYXJkSWQpO1xyXG4gICAgICAgICAgICBpZiAoY3VycmVudFN0YXRlKSB7XHJcbiAgICAgICAgICAgICAgICBzZXRUaW1lb3V0KGZ1bmN0aW9uKCkgeyB3cmFwcGVyLmNvbGxhcHNlKGN1cnJlbnRTdGF0ZSk7IH0sIDUwKTtcclxuICAgICAgICAgICAgICAgIHNhdmVDYXJkU3RhdGUoY2FyZElkLCBjdXJyZW50U3RhdGUpO1xyXG4gICAgICAgICAgICB9XHJcblxyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICAvLyBmaW5hbGx5IGNhdGNoIGNsaWNrcyB0byB0b2dnbGUgY2FyZCBjb2xsYXBzZVxyXG4gICAgICAgICQoZG9jdW1lbnQpLm9uKCdjbGljaycsIGNhcmRTZWxlY3RvciwgZnVuY3Rpb24oKSB7XHJcblxyXG4gICAgICAgICAgICB2YXIgcGFyZW50ID0gJCh0aGlzKS5jbG9zZXN0KCcuY2FyZCcpO1xyXG4gICAgICAgICAgICB2YXIgd3JhcHBlciA9IHBhcmVudC5maW5kKCcuY2FyZC13cmFwcGVyJyk7XHJcblxyXG4gICAgICAgICAgICB3cmFwcGVyLmNvbGxhcHNlKCd0b2dnbGUnKTtcclxuXHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIC8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vL1xyXG4gICAgICAgIC8vIENvbW1vbiB1c2UgZnVuY3Rpb25zIGZvciBjYXJkIGNvbGxhcHNlIC8vXHJcbiAgICAgICAgLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vXHJcbiAgICAgICAgZnVuY3Rpb24gc2V0SWNvblNob3coaWNvbkVsKSB7XHJcbiAgICAgICAgICAgIGljb25FbC5yZW1vdmVDbGFzcygnZmEtcGx1cycpLmFkZENsYXNzKCdmYS1taW51cycpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgZnVuY3Rpb24gc2V0SWNvbkhpZGUoaWNvbkVsKSB7XHJcbiAgICAgICAgICAgIGljb25FbC5yZW1vdmVDbGFzcygnZmEtbWludXMnKS5hZGRDbGFzcygnZmEtcGx1cycpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgZnVuY3Rpb24gc2F2ZUNhcmRTdGF0ZShpZCwgc3RhdGUpIHtcclxuICAgICAgICAgICAgdmFyIGRhdGEgPSBTdG9yYWdlcy5sb2NhbFN0b3JhZ2UuZ2V0KHN0b3JhZ2VLZXlOYW1lKTtcclxuICAgICAgICAgICAgaWYgKCFkYXRhKSB7IGRhdGEgPSB7fTsgfVxyXG4gICAgICAgICAgICBkYXRhW2lkXSA9IHN0YXRlO1xyXG4gICAgICAgICAgICBTdG9yYWdlcy5sb2NhbFN0b3JhZ2Uuc2V0KHN0b3JhZ2VLZXlOYW1lLCBkYXRhKTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGZ1bmN0aW9uIGxvYWRDYXJkU3RhdGUoaWQpIHtcclxuICAgICAgICAgICAgdmFyIGRhdGEgPSBTdG9yYWdlcy5sb2NhbFN0b3JhZ2UuZ2V0KHN0b3JhZ2VLZXlOYW1lKTtcclxuICAgICAgICAgICAgaWYgKGRhdGEpIHtcclxuICAgICAgICAgICAgICAgIHJldHVybiBkYXRhW2lkXSB8fCBmYWxzZTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcblxyXG4gICAgLyoqXHJcbiAgICAgKiBSZWZyZXNoIGNhcmRzXHJcbiAgICAgKiBbZGF0YS10b29sPVwiY2FyZC1yZWZyZXNoXCJdXHJcbiAgICAgKiBbZGF0YS1zcGlubmVyPVwic3RhbmRhcmRcIl1cclxuICAgICAqL1xyXG4gICAgZnVuY3Rpb24gaW5pdENhcmRSZWZyZXNoKCkge1xyXG4gICAgICAgIHZhciBjYXJkU2VsZWN0b3IgPSAnW2RhdGEtdG9vbD1cImNhcmQtcmVmcmVzaFwiXScsXHJcbiAgICAgICAgICAgIHJlZnJlc2hFdmVudCA9ICdjYXJkLnJlZnJlc2gnLFxyXG4gICAgICAgICAgICB3aGlybENsYXNzID0gJ3doaXJsJyxcclxuICAgICAgICAgICAgZGVmYXVsdFNwaW5uZXIgPSAnc3RhbmRhcmQnO1xyXG5cclxuICAgICAgICAvLyBtZXRob2QgdG8gY2xlYXIgdGhlIHNwaW5uZXIgd2hlbiBkb25lXHJcbiAgICAgICAgZnVuY3Rpb24gcmVtb3ZlU3Bpbm5lcigpIHtcclxuICAgICAgICAgICAgdGhpcy5yZW1vdmVDbGFzcyh3aGlybENsYXNzKTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIC8vIGNhdGNoIGNsaWNrcyB0byB0b2dnbGUgY2FyZCByZWZyZXNoXHJcbiAgICAgICAgJChkb2N1bWVudCkub24oJ2NsaWNrJywgY2FyZFNlbGVjdG9yLCBmdW5jdGlvbigpIHtcclxuICAgICAgICAgICAgdmFyICR0aGlzID0gJCh0aGlzKSxcclxuICAgICAgICAgICAgICAgIGNhcmQgPSAkdGhpcy5wYXJlbnRzKCcuY2FyZCcpLmVxKDApLFxyXG4gICAgICAgICAgICAgICAgc3Bpbm5lciA9ICR0aGlzLmRhdGEoJ3NwaW5uZXInKSB8fCBkZWZhdWx0U3Bpbm5lcjtcclxuXHJcbiAgICAgICAgICAgIC8vIHN0YXJ0IHNob3dpbmcgdGhlIHNwaW5uZXJcclxuICAgICAgICAgICAgY2FyZC5hZGRDbGFzcyh3aGlybENsYXNzICsgJyAnICsgc3Bpbm5lcik7XHJcblxyXG4gICAgICAgICAgICAvLyBhdHRhY2ggYXMgcHVibGljIG1ldGhvZFxyXG4gICAgICAgICAgICBjYXJkLnJlbW92ZVNwaW5uZXIgPSByZW1vdmVTcGlubmVyO1xyXG5cclxuICAgICAgICAgICAgLy8gVHJpZ2dlciB0aGUgZXZlbnQgYW5kIHNlbmQgdGhlIGNhcmQgb2JqZWN0XHJcbiAgICAgICAgICAgICR0aGlzLnRyaWdnZXIocmVmcmVzaEV2ZW50LCBbY2FyZF0pO1xyXG5cclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpO1xyXG5cclxuXHJcbiIsIi8vIEdMT0JBTCBDT05TVEFOVFNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbihmdW5jdGlvbih3aW5kb3csIGRvY3VtZW50LCAkLCB1bmRlZmluZWQpe1xyXG5cclxuICAgIHdpbmRvdy5BUFBfQ09MT1JTID0ge1xyXG4gICAgICAgICdwcmltYXJ5JzogICAgICAgICAgICAgICAgJyM1ZDljZWMnLFxyXG4gICAgICAgICdzdWNjZXNzJzogICAgICAgICAgICAgICAgJyMyN2MyNGMnLFxyXG4gICAgICAgICdpbmZvJzogICAgICAgICAgICAgICAgICAgJyMyM2I3ZTUnLFxyXG4gICAgICAgICd3YXJuaW5nJzogICAgICAgICAgICAgICAgJyNmZjkwMmInLFxyXG4gICAgICAgICdkYW5nZXInOiAgICAgICAgICAgICAgICAgJyNmMDUwNTAnLFxyXG4gICAgICAgICdpbnZlcnNlJzogICAgICAgICAgICAgICAgJyMxMzFlMjYnLFxyXG4gICAgICAgICdncmVlbic6ICAgICAgICAgICAgICAgICAgJyMzN2JjOWInLFxyXG4gICAgICAgICdwaW5rJzogICAgICAgICAgICAgICAgICAgJyNmNTMyZTUnLFxyXG4gICAgICAgICdwdXJwbGUnOiAgICAgICAgICAgICAgICAgJyM3MjY2YmEnLFxyXG4gICAgICAgICdkYXJrJzogICAgICAgICAgICAgICAgICAgJyMzYTNmNTEnLFxyXG4gICAgICAgICd5ZWxsb3cnOiAgICAgICAgICAgICAgICAgJyNmYWQ3MzInLFxyXG4gICAgICAgICdncmF5LWRhcmtlcic6ICAgICAgICAgICAgJyMyMzI3MzUnLFxyXG4gICAgICAgICdncmF5LWRhcmsnOiAgICAgICAgICAgICAgJyMzYTNmNTEnLFxyXG4gICAgICAgICdncmF5JzogICAgICAgICAgICAgICAgICAgJyNkZGU2ZTknLFxyXG4gICAgICAgICdncmF5LWxpZ2h0JzogICAgICAgICAgICAgJyNlNGVhZWMnLFxyXG4gICAgICAgICdncmF5LWxpZ2h0ZXInOiAgICAgICAgICAgJyNlZGYxZjInXHJcbiAgICB9O1xyXG5cclxuICAgIHdpbmRvdy5BUFBfTUVESUFRVUVSWSA9IHtcclxuICAgICAgICAnZGVza3RvcExHJzogICAgICAgICAgICAgMTIwMCxcclxuICAgICAgICAnZGVza3RvcCc6ICAgICAgICAgICAgICAgIDk5MixcclxuICAgICAgICAndGFibGV0JzogICAgICAgICAgICAgICAgIDc2OCxcclxuICAgICAgICAnbW9iaWxlJzogICAgICAgICAgICAgICAgIDQ4MFxyXG4gICAgfTtcclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyIsIi8vIEZVTExTQ1JFRU5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbihmdW5jdGlvbih3aW5kb3csIGRvY3VtZW50LCAkLCB1bmRlZmluZWQpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuXHJcbiAgICAkKGluaXRTY3JlZW5GdWxsKTtcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0U2NyZWVuRnVsbCgpIHtcclxuICAgICAgICBpZiAodHlwZW9mIHNjcmVlbmZ1bGwgPT09ICd1bmRlZmluZWQnKSByZXR1cm47XHJcblxyXG4gICAgICAgIHZhciAkZG9jID0gJChkb2N1bWVudCk7XHJcbiAgICAgICAgdmFyICRmc1RvZ2dsZXIgPSAkKCdbZGF0YS10b2dnbGUtZnVsbHNjcmVlbl0nKTtcclxuXHJcbiAgICAgICAgLy8gTm90IHN1cHBvcnRlZCB1bmRlciBJRVxyXG4gICAgICAgIHZhciB1YSA9IHdpbmRvdy5uYXZpZ2F0b3IudXNlckFnZW50O1xyXG4gICAgICAgIGlmICh1YS5pbmRleE9mKFwiTVNJRSBcIikgPiAwIHx8ICEhdWEubWF0Y2goL1RyaWRlbnQuKnJ2XFw6MTFcXC4vKSkge1xyXG4gICAgICAgICAgICAkZnNUb2dnbGVyLmFkZENsYXNzKCdoaWRlJyk7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBpZiAoISRmc1RvZ2dsZXIuaXMoJzp2aXNpYmxlJykpIC8vIGhpZGRlbiBvbiBtb2JpbGVzIG9yIElFXHJcbiAgICAgICAgICAgIHJldHVybjtcclxuXHJcbiAgICAgICAgJGZzVG9nZ2xlci5vbignY2xpY2snLCBmdW5jdGlvbihlKSB7XHJcbiAgICAgICAgICAgIGUucHJldmVudERlZmF1bHQoKTtcclxuXHJcbiAgICAgICAgICAgIGlmIChzY3JlZW5mdWxsLmVuYWJsZWQpIHtcclxuXHJcbiAgICAgICAgICAgICAgICBzY3JlZW5mdWxsLnRvZ2dsZSgpO1xyXG5cclxuICAgICAgICAgICAgICAgIC8vIFN3aXRjaCBpY29uIGluZGljYXRvclxyXG4gICAgICAgICAgICAgICAgdG9nZ2xlRlNJY29uKCRmc1RvZ2dsZXIpO1xyXG5cclxuICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgIGNvbnNvbGUubG9nKCdGdWxsc2NyZWVuIG5vdCBlbmFibGVkJyk7XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9KTtcclxuXHJcbiAgICAgICAgaWYgKHNjcmVlbmZ1bGwucmF3ICYmIHNjcmVlbmZ1bGwucmF3LmZ1bGxzY3JlZW5jaGFuZ2UpXHJcbiAgICAgICAgICAgICRkb2Mub24oc2NyZWVuZnVsbC5yYXcuZnVsbHNjcmVlbmNoYW5nZSwgZnVuY3Rpb24oKSB7XHJcbiAgICAgICAgICAgICAgICB0b2dnbGVGU0ljb24oJGZzVG9nZ2xlcik7XHJcbiAgICAgICAgICAgIH0pO1xyXG5cclxuICAgICAgICBmdW5jdGlvbiB0b2dnbGVGU0ljb24oJGVsZW1lbnQpIHtcclxuICAgICAgICAgICAgaWYgKHNjcmVlbmZ1bGwuaXNGdWxsc2NyZWVuKVxyXG4gICAgICAgICAgICAgICAgJGVsZW1lbnQuY2hpbGRyZW4oJ2VtJykucmVtb3ZlQ2xhc3MoJ2ZhLWV4cGFuZCcpLmFkZENsYXNzKCdmYS1jb21wcmVzcycpO1xyXG4gICAgICAgICAgICBlbHNlXHJcbiAgICAgICAgICAgICAgICAkZWxlbWVudC5jaGlsZHJlbignZW0nKS5yZW1vdmVDbGFzcygnZmEtY29tcHJlc3MnKS5hZGRDbGFzcygnZmEtZXhwYW5kJyk7XHJcbiAgICAgICAgfVxyXG5cclxuICAgIH1cclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyIsIi8vIExPQUQgQ1VTVE9NIENTU1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdExvYWRDU1MpO1xyXG5cclxuICAgIGZ1bmN0aW9uIGluaXRMb2FkQ1NTKCkge1xyXG5cclxuICAgICAgICAkKCdbZGF0YS1sb2FkLWNzc10nKS5vbignY2xpY2snLCBmdW5jdGlvbihlKSB7XHJcblxyXG4gICAgICAgICAgICB2YXIgZWxlbWVudCA9ICQodGhpcyk7XHJcblxyXG4gICAgICAgICAgICBpZiAoZWxlbWVudC5pcygnYScpKVxyXG4gICAgICAgICAgICAgICAgZS5wcmV2ZW50RGVmYXVsdCgpO1xyXG5cclxuICAgICAgICAgICAgdmFyIHVyaSA9IGVsZW1lbnQuZGF0YSgnbG9hZENzcycpLFxyXG4gICAgICAgICAgICAgICAgbGluaztcclxuXHJcbiAgICAgICAgICAgIGlmICh1cmkpIHtcclxuICAgICAgICAgICAgICAgIGxpbmsgPSBjcmVhdGVMaW5rKHVyaSk7XHJcbiAgICAgICAgICAgICAgICBpZiAoIWxpbmspIHtcclxuICAgICAgICAgICAgICAgICAgICAkLmVycm9yKCdFcnJvciBjcmVhdGluZyBzdHlsZXNoZWV0IGxpbmsgZWxlbWVudC4nKTtcclxuICAgICAgICAgICAgICAgIH1cclxuICAgICAgICAgICAgfSBlbHNlIHtcclxuICAgICAgICAgICAgICAgICQuZXJyb3IoJ05vIHN0eWxlc2hlZXQgbG9jYXRpb24gZGVmaW5lZC4nKTtcclxuICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbiAgICBmdW5jdGlvbiBjcmVhdGVMaW5rKHVyaSkge1xyXG4gICAgICAgIHZhciBsaW5rSWQgPSAnYXV0b2xvYWRlZC1zdHlsZXNoZWV0JyxcclxuICAgICAgICAgICAgb2xkTGluayA9ICQoJyMnICsgbGlua0lkKS5hdHRyKCdpZCcsIGxpbmtJZCArICctb2xkJyk7XHJcblxyXG4gICAgICAgICQoJ2hlYWQnKS5hcHBlbmQoJCgnPGxpbmsvPicpLmF0dHIoe1xyXG4gICAgICAgICAgICAnaWQnOiBsaW5rSWQsXHJcbiAgICAgICAgICAgICdyZWwnOiAnc3R5bGVzaGVldCcsXHJcbiAgICAgICAgICAgICdocmVmJzogdXJpXHJcbiAgICAgICAgfSkpO1xyXG5cclxuICAgICAgICBpZiAob2xkTGluay5sZW5ndGgpIHtcclxuICAgICAgICAgICAgb2xkTGluay5yZW1vdmUoKTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIHJldHVybiAkKCcjJyArIGxpbmtJZCk7XHJcbiAgICB9XHJcblxyXG59KSh3aW5kb3csIGRvY3VtZW50LCB3aW5kb3cualF1ZXJ5KTsiLCIvLyBUUkFOU0xBVElPTlxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdFRyYW5zbGF0aW9uKTtcclxuXHJcblxyXG4gICAgdmFyIHByZWZlcnJlZExhbmcgPSAnZW4nO1xyXG4gICAgdmFyIHBhdGhQcmVmaXggPSAnL0NvbnRlbnQvaTE4bic7IC8vIGZvbGRlciBvZiBqc29uIGZpbGVzXHJcbiAgICB2YXIgcGFja05hbWUgPSAnc2l0ZSc7XHJcbiAgICB2YXIgc3RvcmFnZUtleSA9ICdqcS1hcHBMYW5nJztcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0VHJhbnNsYXRpb24oKSB7XHJcblxyXG4gICAgICAgIGlmICghJC5mbi5sb2NhbGl6ZSkgcmV0dXJuO1xyXG5cclxuICAgICAgICAvLyBkZXRlY3Qgc2F2ZWQgbGFuZ3VhZ2Ugb3IgdXNlIGRlZmF1bHRcclxuICAgICAgICB2YXIgY3VyckxhbmcgPSBTdG9yYWdlcy5sb2NhbFN0b3JhZ2UuZ2V0KHN0b3JhZ2VLZXkpIHx8IHByZWZlcnJlZExhbmc7XHJcbiAgICAgICAgLy8gc2V0IGluaXRpYWwgb3B0aW9uc1xyXG4gICAgICAgIHZhciBvcHRzID0ge1xyXG4gICAgICAgICAgICBsYW5ndWFnZTogY3VyckxhbmcsXHJcbiAgICAgICAgICAgIHBhdGhQcmVmaXg6IHBhdGhQcmVmaXgsXHJcbiAgICAgICAgICAgIGNhbGxiYWNrOiBmdW5jdGlvbihkYXRhLCBkZWZhdWx0Q2FsbGJhY2spIHtcclxuICAgICAgICAgICAgICAgIFN0b3JhZ2VzLmxvY2FsU3RvcmFnZS5zZXQoc3RvcmFnZUtleSwgY3VyckxhbmcpOyAvLyBzYXZlIHRoZSBsYW5ndWFnZVxyXG4gICAgICAgICAgICAgICAgZGVmYXVsdENhbGxiYWNrKGRhdGEpO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgfTtcclxuXHJcbiAgICAgICAgLy8gU2V0IGluaXRpYWwgbGFuZ3VhZ2VcclxuICAgICAgICBzZXRMYW5ndWFnZShvcHRzKTtcclxuXHJcbiAgICAgICAgLy8gTGlzdGVuIGZvciBjaGFuZ2VzXHJcbiAgICAgICAgJCgnW2RhdGEtc2V0LWxhbmddJykub24oJ2NsaWNrJywgZnVuY3Rpb24oKSB7XHJcblxyXG4gICAgICAgICAgICBjdXJyTGFuZyA9ICQodGhpcykuZGF0YSgnc2V0TGFuZycpO1xyXG5cclxuICAgICAgICAgICAgaWYgKGN1cnJMYW5nKSB7XHJcblxyXG4gICAgICAgICAgICAgICAgb3B0cy5sYW5ndWFnZSA9IGN1cnJMYW5nO1xyXG5cclxuICAgICAgICAgICAgICAgIHNldExhbmd1YWdlKG9wdHMpO1xyXG5cclxuICAgICAgICAgICAgICAgIGFjdGl2YXRlRHJvcGRvd24oJCh0aGlzKSk7XHJcbiAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgfSk7XHJcblxyXG5cclxuICAgICAgICBmdW5jdGlvbiBzZXRMYW5ndWFnZShvcHRpb25zKSB7XHJcbiAgICAgICAgICAgICQoXCJbZGF0YS1sb2NhbGl6ZV1cIikubG9jYWxpemUocGFja05hbWUsIG9wdGlvbnMpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgLy8gU2V0IHRoZSBjdXJyZW50IGNsaWNrZWQgdGV4dCBhcyB0aGUgYWN0aXZlIGRyb3Bkb3duIHRleHRcclxuICAgICAgICBmdW5jdGlvbiBhY3RpdmF0ZURyb3Bkb3duKGVsZW0pIHtcclxuICAgICAgICAgICAgdmFyIG1lbnUgPSBlbGVtLnBhcmVudHMoJy5kcm9wZG93bi1tZW51Jyk7XHJcbiAgICAgICAgICAgIGlmIChtZW51Lmxlbmd0aCkge1xyXG4gICAgICAgICAgICAgICAgdmFyIHRvZ2dsZSA9IG1lbnUucHJldignYnV0dG9uLCBhJyk7XHJcbiAgICAgICAgICAgICAgICB0b2dnbGUudGV4dChlbGVtLnRleHQoKSk7XHJcbiAgICAgICAgICAgIH1cclxuICAgICAgICB9XHJcblxyXG4gICAgfVxyXG5cclxufSkod2luZG93LCBkb2N1bWVudCwgd2luZG93LmpRdWVyeSk7IiwiLy8gTkFWQkFSIFNFQVJDSFxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdE5hdmJhclNlYXJjaCk7XHJcblxyXG4gICAgZnVuY3Rpb24gaW5pdE5hdmJhclNlYXJjaCgpIHtcclxuXHJcbiAgICAgICAgdmFyIG5hdlNlYXJjaCA9IG5ldyBuYXZiYXJTZWFyY2hJbnB1dCgpO1xyXG5cclxuICAgICAgICAvLyBPcGVuIHNlYXJjaCBpbnB1dFxyXG4gICAgICAgIHZhciAkc2VhcmNoT3BlbiA9ICQoJ1tkYXRhLXNlYXJjaC1vcGVuXScpO1xyXG5cclxuICAgICAgICAkc2VhcmNoT3BlblxyXG4gICAgICAgICAgICAub24oJ2NsaWNrJywgZnVuY3Rpb24oZSkgeyBlLnN0b3BQcm9wYWdhdGlvbigpOyB9KVxyXG4gICAgICAgICAgICAub24oJ2NsaWNrJywgbmF2U2VhcmNoLnRvZ2dsZSk7XHJcblxyXG4gICAgICAgIC8vIENsb3NlIHNlYXJjaCBpbnB1dFxyXG4gICAgICAgIHZhciAkc2VhcmNoRGlzbWlzcyA9ICQoJ1tkYXRhLXNlYXJjaC1kaXNtaXNzXScpO1xyXG4gICAgICAgIHZhciBpbnB1dFNlbGVjdG9yID0gJy5uYXZiYXItZm9ybSBpbnB1dFt0eXBlPVwidGV4dFwiXSc7XHJcblxyXG4gICAgICAgICQoaW5wdXRTZWxlY3RvcilcclxuICAgICAgICAgICAgLm9uKCdjbGljaycsIGZ1bmN0aW9uKGUpIHsgZS5zdG9wUHJvcGFnYXRpb24oKTsgfSlcclxuICAgICAgICAgICAgLm9uKCdrZXl1cCcsIGZ1bmN0aW9uKGUpIHtcclxuICAgICAgICAgICAgICAgIGlmIChlLmtleUNvZGUgPT0gMjcpIC8vIEVTQ1xyXG4gICAgICAgICAgICAgICAgICAgIG5hdlNlYXJjaC5kaXNtaXNzKCk7XHJcbiAgICAgICAgICAgIH0pO1xyXG5cclxuICAgICAgICAvLyBjbGljayBhbnl3aGVyZSBjbG9zZXMgdGhlIHNlYXJjaFxyXG4gICAgICAgICQoZG9jdW1lbnQpLm9uKCdjbGljaycsIG5hdlNlYXJjaC5kaXNtaXNzKTtcclxuICAgICAgICAvLyBkaXNtaXNzYWJsZSBvcHRpb25zXHJcbiAgICAgICAgJHNlYXJjaERpc21pc3NcclxuICAgICAgICAgICAgLm9uKCdjbGljaycsIGZ1bmN0aW9uKGUpIHsgZS5zdG9wUHJvcGFnYXRpb24oKTsgfSlcclxuICAgICAgICAgICAgLm9uKCdjbGljaycsIG5hdlNlYXJjaC5kaXNtaXNzKTtcclxuXHJcbiAgICB9XHJcblxyXG4gICAgdmFyIG5hdmJhclNlYXJjaElucHV0ID0gZnVuY3Rpb24oKSB7XHJcbiAgICAgICAgdmFyIG5hdmJhckZvcm1TZWxlY3RvciA9ICdmb3JtLm5hdmJhci1mb3JtJztcclxuICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICB0b2dnbGU6IGZ1bmN0aW9uKCkge1xyXG5cclxuICAgICAgICAgICAgICAgIHZhciBuYXZiYXJGb3JtID0gJChuYXZiYXJGb3JtU2VsZWN0b3IpO1xyXG5cclxuICAgICAgICAgICAgICAgIG5hdmJhckZvcm0udG9nZ2xlQ2xhc3MoJ29wZW4nKTtcclxuXHJcbiAgICAgICAgICAgICAgICB2YXIgaXNPcGVuID0gbmF2YmFyRm9ybS5oYXNDbGFzcygnb3BlbicpO1xyXG5cclxuICAgICAgICAgICAgICAgIG5hdmJhckZvcm0uZmluZCgnaW5wdXQnKVtpc09wZW4gPyAnZm9jdXMnIDogJ2JsdXInXSgpO1xyXG5cclxuICAgICAgICAgICAgfSxcclxuXHJcbiAgICAgICAgICAgIGRpc21pc3M6IGZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICAgICAgJChuYXZiYXJGb3JtU2VsZWN0b3IpXHJcbiAgICAgICAgICAgICAgICAgICAgLnJlbW92ZUNsYXNzKCdvcGVuJykgLy8gQ2xvc2UgY29udHJvbFxyXG4gICAgICAgICAgICAgICAgICAgIC5maW5kKCdpbnB1dFt0eXBlPVwidGV4dFwiXScpLmJsdXIoKSAvLyByZW1vdmUgZm9jdXNcclxuICAgICAgICAgICAgICAgIC8vIC52YWwoJycpICAgICAgICAgICAgICAgICAgICAvLyBFbXB0eSBpbnB1dFxyXG4gICAgICAgICAgICAgICAgO1xyXG4gICAgICAgICAgICB9XHJcbiAgICAgICAgfTtcclxuXHJcbiAgICB9XHJcblxyXG59KSh3aW5kb3csIGRvY3VtZW50LCB3aW5kb3cualF1ZXJ5KTsiLCIvLyBOT1cgVElNRVJcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbihmdW5jdGlvbih3aW5kb3csIGRvY3VtZW50LCAkLCB1bmRlZmluZWQpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuXHJcbiAgICAkKGluaXROb3dUaW1lcik7XHJcblxyXG4gICAgZnVuY3Rpb24gaW5pdE5vd1RpbWVyKCkge1xyXG5cclxuICAgICAgICAkKCdbZGF0YS1ub3ddJykuZWFjaChmdW5jdGlvbigpIHtcclxuICAgICAgICAgICAgdmFyIGVsZW1lbnQgPSAkKHRoaXMpLFxyXG4gICAgICAgICAgICAgICAgZm9ybWF0ID0gZWxlbWVudC5kYXRhKCdmb3JtYXQnKTtcclxuXHJcbiAgICAgICAgICAgIGZ1bmN0aW9uIHVwZGF0ZVRpbWUoKSB7XHJcbiAgICAgICAgICAgICAgICB2YXIgZHQgPSBtb21lbnQobmV3IERhdGUoKSkuZm9ybWF0KGZvcm1hdCk7XHJcbiAgICAgICAgICAgICAgICBlbGVtZW50LnRleHQoZHQpO1xyXG4gICAgICAgICAgICB9XHJcblxyXG4gICAgICAgICAgICB1cGRhdGVUaW1lKCk7XHJcbiAgICAgICAgICAgIHNldEludGVydmFsKHVwZGF0ZVRpbWUsIDEwMDApO1xyXG5cclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyIsIi8vIFRvZ2dsZSBSVEwgbW9kZSBmb3IgZGVtb1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuXHJcbihmdW5jdGlvbih3aW5kb3csIGRvY3VtZW50LCAkLCB1bmRlZmluZWQpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuXHJcbiAgICAkKGluaXRSVEwpO1xyXG5cclxuICAgIGZ1bmN0aW9uIGluaXRSVEwoKSB7XHJcbiAgICAgICAgdmFyIG1haW5jc3MgPSAkKCcjbWFpbmNzcycpO1xyXG4gICAgICAgIHZhciBic2NzcyA9ICQoJyNic2NzcycpO1xyXG4gICAgICAgICQoJyNjaGstcnRsJykub24oJ2NoYW5nZScsIGZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICAvLyBhcHAgcnRsIGNoZWNrXHJcbiAgICAgICAgICAgIG1haW5jc3MuYXR0cignaHJlZicsIHRoaXMuY2hlY2tlZCA/ICcvQ29udGVudC9jc3MvYXBwLXJ0bC5jc3MnIDogJy9Db250ZW50L2Nzcy9hcHAuY3NzJyk7XHJcbiAgICAgICAgICAgIC8vIGJvb3RzdHJhcCBydGwgY2hlY2tcclxuICAgICAgICAgICAgYnNjc3MuYXR0cignaHJlZicsIHRoaXMuY2hlY2tlZCA/ICcvQ29udGVudC9jc3MvYm9vdHN0cmFwLXJ0bC5jc3MnIDogJy9Db250ZW50L2Nzcy9ib290c3RyYXAuY3NzJyk7XHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG59KSh3aW5kb3csIGRvY3VtZW50LCB3aW5kb3cualF1ZXJ5KTsiLCIvLyBTSURFQkFSXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdFNpZGViYXIpO1xyXG5cclxuXHJcbiAgICB2YXIgJHdpbjtcclxuICAgIHZhciAkaHRtbDtcclxuICAgIHZhciAkYm9keTtcclxuICAgIHZhciAkc2lkZWJhcjtcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0U2lkZWJhcigpIHtcclxuXHJcbiAgICAgICAgJHdpbiA9ICQod2luZG93KTtcclxuICAgICAgICAkaHRtbCA9ICQoJ2h0bWwnKTtcclxuICAgICAgICAkYm9keSA9ICQoJ2JvZHknKTtcclxuICAgICAgICAkc2lkZWJhciA9ICQoJy5zaWRlYmFyJyk7XHJcblxyXG4gICAgICAgIC8vIEFVVE9DT0xMQVBTRSBJVEVNU1xyXG4gICAgICAgIC8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4gICAgICAgIHZhciBzaWRlYmFyQ29sbGFwc2UgPSAkc2lkZWJhci5maW5kKCcuY29sbGFwc2UnKTtcclxuICAgICAgICBzaWRlYmFyQ29sbGFwc2Uub24oJ3Nob3cuYnMuY29sbGFwc2UnLCBmdW5jdGlvbihldmVudCkge1xyXG5cclxuICAgICAgICAgICAgZXZlbnQuc3RvcFByb3BhZ2F0aW9uKCk7XHJcbiAgICAgICAgICAgIGlmICgkKHRoaXMpLnBhcmVudHMoJy5jb2xsYXBzZScpLmxlbmd0aCA9PT0gMClcclxuICAgICAgICAgICAgICAgIHNpZGViYXJDb2xsYXBzZS5maWx0ZXIoJy5zaG93JykuY29sbGFwc2UoJ2hpZGUnKTtcclxuXHJcbiAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIC8vIFNJREVCQVIgQUNUSVZFIFNUQVRFXHJcbiAgICAgICAgLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbiAgICAgICAgLy8gRmluZCBjdXJyZW50IGFjdGl2ZSBpdGVtXHJcbiAgICAgICAgdmFyIGN1cnJlbnRJdGVtID0gJCgnLnNpZGViYXIgLmFjdGl2ZScpLnBhcmVudHMoJ2xpJyk7XHJcblxyXG4gICAgICAgIC8vIGhvdmVyIG1vZGUgZG9uJ3QgdHJ5IHRvIGV4cGFuZCBhY3RpdmUgY29sbGFwc2VcclxuICAgICAgICBpZiAoIXVzZUFzaWRlSG92ZXIoKSlcclxuICAgICAgICAgICAgY3VycmVudEl0ZW1cclxuICAgICAgICAgICAgLmFkZENsYXNzKCdhY3RpdmUnKSAvLyBhY3RpdmF0ZSB0aGUgcGFyZW50XHJcbiAgICAgICAgICAgIC5jaGlsZHJlbignLmNvbGxhcHNlJykgLy8gZmluZCB0aGUgY29sbGFwc2VcclxuICAgICAgICAgICAgLmNvbGxhcHNlKCdzaG93Jyk7IC8vIGFuZCBzaG93IGl0XHJcblxyXG4gICAgICAgIC8vIHJlbW92ZSB0aGlzIGlmIHlvdSB1c2Ugb25seSBjb2xsYXBzaWJsZSBzaWRlYmFyIGl0ZW1zXHJcbiAgICAgICAgJHNpZGViYXIuZmluZCgnbGkgPiBhICsgdWwnKS5vbignc2hvdy5icy5jb2xsYXBzZScsIGZ1bmN0aW9uKGUpIHtcclxuICAgICAgICAgICAgaWYgKHVzZUFzaWRlSG92ZXIoKSkgZS5wcmV2ZW50RGVmYXVsdCgpO1xyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICAvLyBTSURFQkFSIENPTExBUFNFRCBJVEVNIEhBTkRMRVJcclxuICAgICAgICAvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuXHJcbiAgICAgICAgdmFyIGV2ZW50TmFtZSA9IGlzVG91Y2goKSA/ICdjbGljaycgOiAnbW91c2VlbnRlcic7XHJcbiAgICAgICAgdmFyIHN1Yk5hdiA9ICQoKTtcclxuICAgICAgICAkc2lkZWJhci5vbihldmVudE5hbWUsICcuc2lkZWJhci1uYXYgPiBsaScsIGZ1bmN0aW9uKCkge1xyXG5cclxuICAgICAgICAgICAgaWYgKGlzU2lkZWJhckNvbGxhcHNlZCgpIHx8IHVzZUFzaWRlSG92ZXIoKSkge1xyXG5cclxuICAgICAgICAgICAgICAgIHN1Yk5hdi50cmlnZ2VyKCdtb3VzZWxlYXZlJyk7XHJcbiAgICAgICAgICAgICAgICBzdWJOYXYgPSB0b2dnbGVNZW51SXRlbSgkKHRoaXMpKTtcclxuXHJcbiAgICAgICAgICAgICAgICAvLyBVc2VkIHRvIGRldGVjdCBjbGljayBhbmQgdG91Y2ggZXZlbnRzIG91dHNpZGUgdGhlIHNpZGViYXJcclxuICAgICAgICAgICAgICAgIHNpZGViYXJBZGRCYWNrZHJvcCgpO1xyXG4gICAgICAgICAgICB9XHJcblxyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICB2YXIgc2lkZWJhckFueWNsaWNrQ2xvc2UgPSAkc2lkZWJhci5kYXRhKCdzaWRlYmFyQW55Y2xpY2tDbG9zZScpO1xyXG5cclxuICAgICAgICAvLyBBbGxvd3MgdG8gY2xvc2VcclxuICAgICAgICBpZiAodHlwZW9mIHNpZGViYXJBbnljbGlja0Nsb3NlICE9PSAndW5kZWZpbmVkJykge1xyXG5cclxuICAgICAgICAgICAgJCgnLndyYXBwZXInKS5vbignY2xpY2suc2lkZWJhcicsIGZ1bmN0aW9uKGUpIHtcclxuICAgICAgICAgICAgICAgIC8vIGRvbid0IGNoZWNrIGlmIHNpZGViYXIgbm90IHZpc2libGVcclxuICAgICAgICAgICAgICAgIGlmICghJGJvZHkuaGFzQ2xhc3MoJ2FzaWRlLXRvZ2dsZWQnKSkgcmV0dXJuO1xyXG5cclxuICAgICAgICAgICAgICAgIHZhciAkdGFyZ2V0ID0gJChlLnRhcmdldCk7XHJcbiAgICAgICAgICAgICAgICBpZiAoISR0YXJnZXQucGFyZW50cygnLmFzaWRlLWNvbnRhaW5lcicpLmxlbmd0aCAmJiAvLyBpZiBub3QgY2hpbGQgb2Ygc2lkZWJhclxyXG4gICAgICAgICAgICAgICAgICAgICEkdGFyZ2V0LmlzKCcjdXNlci1ibG9jay10b2dnbGUnKSAmJiAvLyB1c2VyIGJsb2NrIHRvZ2dsZSBhbmNob3JcclxuICAgICAgICAgICAgICAgICAgICAhJHRhcmdldC5wYXJlbnQoKS5pcygnI3VzZXItYmxvY2stdG9nZ2xlJykgLy8gdXNlciBibG9jayB0b2dnbGUgaWNvblxyXG4gICAgICAgICAgICAgICAgKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgJGJvZHkucmVtb3ZlQ2xhc3MoJ2FzaWRlLXRvZ2dsZWQnKTtcclxuICAgICAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgIH0pO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxuXHJcbiAgICBmdW5jdGlvbiBzaWRlYmFyQWRkQmFja2Ryb3AoKSB7XHJcbiAgICAgICAgdmFyICRiYWNrZHJvcCA9ICQoJzxkaXYvPicsIHsgJ2NsYXNzJzogJ2Ryb3Bkb3duLWJhY2tkcm9wJyB9KTtcclxuICAgICAgICAkYmFja2Ryb3AuaW5zZXJ0QWZ0ZXIoJy5hc2lkZS1jb250YWluZXInKS5vbihcImNsaWNrIG1vdXNlZW50ZXJcIiwgZnVuY3Rpb24oKSB7XHJcbiAgICAgICAgICAgIHJlbW92ZUZsb2F0aW5nTmF2KCk7XHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgLy8gT3BlbiB0aGUgY29sbGFwc2Ugc2lkZWJhciBzdWJtZW51IGl0ZW1zIHdoZW4gb24gdG91Y2ggZGV2aWNlc1xyXG4gICAgLy8gLSBkZXNrdG9wIG9ubHkgb3BlbnMgb24gaG92ZXJcclxuICAgIGZ1bmN0aW9uIHRvZ2dsZVRvdWNoSXRlbSgkZWxlbWVudCkge1xyXG4gICAgICAgICRlbGVtZW50XHJcbiAgICAgICAgICAgIC5zaWJsaW5ncygnbGknKVxyXG4gICAgICAgICAgICAucmVtb3ZlQ2xhc3MoJ29wZW4nKVxyXG4gICAgICAgICAgICAuZW5kKClcclxuICAgICAgICAgICAgLnRvZ2dsZUNsYXNzKCdvcGVuJyk7XHJcbiAgICB9XHJcblxyXG4gICAgLy8gSGFuZGxlcyBob3ZlciB0byBvcGVuIGl0ZW1zIHVuZGVyIGNvbGxhcHNlZCBtZW51XHJcbiAgICAvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4gICAgZnVuY3Rpb24gdG9nZ2xlTWVudUl0ZW0oJGxpc3RJdGVtKSB7XHJcblxyXG4gICAgICAgIHJlbW92ZUZsb2F0aW5nTmF2KCk7XHJcblxyXG4gICAgICAgIHZhciB1bCA9ICRsaXN0SXRlbS5jaGlsZHJlbigndWwnKTtcclxuXHJcbiAgICAgICAgaWYgKCF1bC5sZW5ndGgpIHJldHVybiAkKCk7XHJcbiAgICAgICAgaWYgKCRsaXN0SXRlbS5oYXNDbGFzcygnb3BlbicpKSB7XHJcbiAgICAgICAgICAgIHRvZ2dsZVRvdWNoSXRlbSgkbGlzdEl0ZW0pO1xyXG4gICAgICAgICAgICByZXR1cm4gJCgpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgdmFyICRhc2lkZSA9ICQoJy5hc2lkZS1jb250YWluZXInKTtcclxuICAgICAgICB2YXIgJGFzaWRlSW5uZXIgPSAkKCcuYXNpZGUtaW5uZXInKTsgLy8gZm9yIHRvcCBvZmZzZXQgY2FsY3VsYXRpb25cclxuICAgICAgICAvLyBmbG9hdCBhc2lkZSB1c2VzIGV4dHJhIHBhZGRpbmcgb24gYXNpZGVcclxuICAgICAgICB2YXIgbWFyID0gcGFyc2VJbnQoJGFzaWRlSW5uZXIuY3NzKCdwYWRkaW5nLXRvcCcpLCAwKSArIHBhcnNlSW50KCRhc2lkZS5jc3MoJ3BhZGRpbmctdG9wJyksIDApO1xyXG5cclxuICAgICAgICB2YXIgc3ViTmF2ID0gdWwuY2xvbmUoKS5hcHBlbmRUbygkYXNpZGUpO1xyXG5cclxuICAgICAgICB0b2dnbGVUb3VjaEl0ZW0oJGxpc3RJdGVtKTtcclxuXHJcbiAgICAgICAgdmFyIGl0ZW1Ub3AgPSAoJGxpc3RJdGVtLnBvc2l0aW9uKCkudG9wICsgbWFyKSAtICRzaWRlYmFyLnNjcm9sbFRvcCgpO1xyXG4gICAgICAgIHZhciB2d0hlaWdodCA9ICR3aW4uaGVpZ2h0KCk7XHJcblxyXG4gICAgICAgIHN1Yk5hdlxyXG4gICAgICAgICAgICAuYWRkQ2xhc3MoJ25hdi1mbG9hdGluZycpXHJcbiAgICAgICAgICAgIC5jc3Moe1xyXG4gICAgICAgICAgICAgICAgcG9zaXRpb246IGlzRml4ZWQoKSA/ICdmaXhlZCcgOiAnYWJzb2x1dGUnLFxyXG4gICAgICAgICAgICAgICAgdG9wOiBpdGVtVG9wLFxyXG4gICAgICAgICAgICAgICAgYm90dG9tOiAoc3ViTmF2Lm91dGVySGVpZ2h0KHRydWUpICsgaXRlbVRvcCA+IHZ3SGVpZ2h0KSA/IDAgOiAnYXV0bydcclxuICAgICAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIHN1Yk5hdi5vbignbW91c2VsZWF2ZScsIGZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICB0b2dnbGVUb3VjaEl0ZW0oJGxpc3RJdGVtKTtcclxuICAgICAgICAgICAgc3ViTmF2LnJlbW92ZSgpO1xyXG4gICAgICAgIH0pO1xyXG5cclxuICAgICAgICByZXR1cm4gc3ViTmF2O1xyXG4gICAgfVxyXG5cclxuICAgIGZ1bmN0aW9uIHJlbW92ZUZsb2F0aW5nTmF2KCkge1xyXG4gICAgICAgICQoJy5zaWRlYmFyLXN1Ym5hdi5uYXYtZmxvYXRpbmcnKS5yZW1vdmUoKTtcclxuICAgICAgICAkKCcuZHJvcGRvd24tYmFja2Ryb3AnKS5yZW1vdmUoKTtcclxuICAgICAgICAkKCcuc2lkZWJhciBsaS5vcGVuJykucmVtb3ZlQ2xhc3MoJ29wZW4nKTtcclxuICAgIH1cclxuXHJcbiAgICBmdW5jdGlvbiBpc1RvdWNoKCkge1xyXG4gICAgICAgIHJldHVybiAkaHRtbC5oYXNDbGFzcygndG91Y2gnKTtcclxuICAgIH1cclxuXHJcbiAgICBmdW5jdGlvbiBpc1NpZGViYXJDb2xsYXBzZWQoKSB7XHJcbiAgICAgICAgcmV0dXJuICRib2R5Lmhhc0NsYXNzKCdhc2lkZS1jb2xsYXBzZWQnKSB8fCAkYm9keS5oYXNDbGFzcygnYXNpZGUtY29sbGFwc2VkLXRleHQnKTtcclxuICAgIH1cclxuXHJcbiAgICBmdW5jdGlvbiBpc1NpZGViYXJUb2dnbGVkKCkge1xyXG4gICAgICAgIHJldHVybiAkYm9keS5oYXNDbGFzcygnYXNpZGUtdG9nZ2xlZCcpO1xyXG4gICAgfVxyXG5cclxuICAgIGZ1bmN0aW9uIGlzTW9iaWxlKCkge1xyXG4gICAgICAgIHJldHVybiAkd2luLndpZHRoKCkgPCBBUFBfTUVESUFRVUVSWS50YWJsZXQ7XHJcbiAgICB9XHJcblxyXG4gICAgZnVuY3Rpb24gaXNGaXhlZCgpIHtcclxuICAgICAgICByZXR1cm4gJGJvZHkuaGFzQ2xhc3MoJ2xheW91dC1maXhlZCcpO1xyXG4gICAgfVxyXG5cclxuICAgIGZ1bmN0aW9uIHVzZUFzaWRlSG92ZXIoKSB7XHJcbiAgICAgICAgcmV0dXJuICRib2R5Lmhhc0NsYXNzKCdhc2lkZS1ob3ZlcicpO1xyXG4gICAgfVxyXG5cclxufSkod2luZG93LCBkb2N1bWVudCwgd2luZG93LmpRdWVyeSk7IiwiLy8gU0xJTVNDUk9MTFxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdFNsaW1zU3JvbGwpO1xyXG5cclxuICAgIGZ1bmN0aW9uIGluaXRTbGltc1Nyb2xsKCkge1xyXG5cclxuICAgICAgICAkKCdbZGF0YS1zY3JvbGxhYmxlXScpLmVhY2goZnVuY3Rpb24oKSB7XHJcblxyXG4gICAgICAgICAgICB2YXIgZWxlbWVudCA9ICQodGhpcyksXHJcbiAgICAgICAgICAgICAgICBkZWZhdWx0SGVpZ2h0ID0gMjUwO1xyXG5cclxuICAgICAgICAgICAgZWxlbWVudC5zbGltU2Nyb2xsKHtcclxuICAgICAgICAgICAgICAgIGhlaWdodDogKGVsZW1lbnQuZGF0YSgnaGVpZ2h0JykgfHwgZGVmYXVsdEhlaWdodClcclxuICAgICAgICAgICAgfSk7XHJcblxyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG5cclxufSkod2luZG93LCBkb2N1bWVudCwgd2luZG93LmpRdWVyeSk7IiwiLy8gQ3VzdG9tIGpRdWVyeVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuXHJcbihmdW5jdGlvbih3aW5kb3csIGRvY3VtZW50LCAkLCB1bmRlZmluZWQpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuXHJcbiAgICAkKGluaXRUYWJsZUNoZWNrQWxsKTtcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0VGFibGVDaGVja0FsbCgpIHtcclxuXHJcbiAgICAgICAgJCgnW2RhdGEtY2hlY2stYWxsXScpLm9uKCdjaGFuZ2UnLCBmdW5jdGlvbigpIHtcclxuICAgICAgICAgICAgdmFyICR0aGlzID0gJCh0aGlzKSxcclxuICAgICAgICAgICAgICAgIGluZGV4ID0gJHRoaXMuaW5kZXgoKSArIDEsXHJcbiAgICAgICAgICAgICAgICBjaGVja2JveCA9ICR0aGlzLmZpbmQoJ2lucHV0W3R5cGU9XCJjaGVja2JveFwiXScpLFxyXG4gICAgICAgICAgICAgICAgdGFibGUgPSAkdGhpcy5wYXJlbnRzKCd0YWJsZScpO1xyXG4gICAgICAgICAgICAvLyBNYWtlIHN1cmUgdG8gYWZmZWN0IG9ubHkgdGhlIGNvcnJlY3QgY2hlY2tib3ggY29sdW1uXHJcbiAgICAgICAgICAgIHRhYmxlLmZpbmQoJ3Rib2R5ID4gdHIgPiB0ZDpudGgtY2hpbGQoJyArIGluZGV4ICsgJykgaW5wdXRbdHlwZT1cImNoZWNrYm94XCJdJylcclxuICAgICAgICAgICAgICAgIC5wcm9wKCdjaGVja2VkJywgY2hlY2tib3hbMF0uY2hlY2tlZCk7XHJcblxyXG4gICAgICAgIH0pO1xyXG5cclxuICAgIH1cclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyIsIi8vIFRPR0dMRSBTVEFURVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdFRvZ2dsZVN0YXRlKTtcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0VG9nZ2xlU3RhdGUoKSB7XHJcblxyXG4gICAgICAgIHZhciAkYm9keSA9ICQoJ2JvZHknKTtcclxuICAgICAgICB2YXIgdG9nZ2xlID0gbmV3IFN0YXRlVG9nZ2xlcigpO1xyXG5cclxuICAgICAgICAkKCdbZGF0YS10b2dnbGUtc3RhdGVdJylcclxuICAgICAgICAgICAgLm9uKCdjbGljaycsIGZ1bmN0aW9uKGUpIHtcclxuICAgICAgICAgICAgICAgIC8vIGUucHJldmVudERlZmF1bHQoKTtcclxuICAgICAgICAgICAgICAgIGUuc3RvcFByb3BhZ2F0aW9uKCk7XHJcbiAgICAgICAgICAgICAgICB2YXIgZWxlbWVudCA9ICQodGhpcyksXHJcbiAgICAgICAgICAgICAgICAgICAgY2xhc3NuYW1lID0gZWxlbWVudC5kYXRhKCd0b2dnbGVTdGF0ZScpLFxyXG4gICAgICAgICAgICAgICAgICAgIHRhcmdldCA9IGVsZW1lbnQuZGF0YSgndGFyZ2V0JyksXHJcbiAgICAgICAgICAgICAgICAgICAgbm9QZXJzaXN0ID0gKGVsZW1lbnQuYXR0cignZGF0YS1uby1wZXJzaXN0JykgIT09IHVuZGVmaW5lZCk7XHJcblxyXG4gICAgICAgICAgICAgICAgLy8gU3BlY2lmeSBhIHRhcmdldCBzZWxlY3RvciB0byB0b2dnbGUgY2xhc3NuYW1lXHJcbiAgICAgICAgICAgICAgICAvLyB1c2UgYm9keSBieSBkZWZhdWx0XHJcbiAgICAgICAgICAgICAgICB2YXIgJHRhcmdldCA9IHRhcmdldCA/ICQodGFyZ2V0KSA6ICRib2R5O1xyXG5cclxuICAgICAgICAgICAgICAgIGlmIChjbGFzc25hbWUpIHtcclxuICAgICAgICAgICAgICAgICAgICBpZiAoJHRhcmdldC5oYXNDbGFzcyhjbGFzc25hbWUpKSB7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgICR0YXJnZXQucmVtb3ZlQ2xhc3MoY2xhc3NuYW1lKTtcclxuICAgICAgICAgICAgICAgICAgICAgICAgaWYgKCFub1BlcnNpc3QpXHJcbiAgICAgICAgICAgICAgICAgICAgICAgICAgICB0b2dnbGUucmVtb3ZlU3RhdGUoY2xhc3NuYW1lKTtcclxuICAgICAgICAgICAgICAgICAgICB9IGVsc2Uge1xyXG4gICAgICAgICAgICAgICAgICAgICAgICAkdGFyZ2V0LmFkZENsYXNzKGNsYXNzbmFtZSk7XHJcbiAgICAgICAgICAgICAgICAgICAgICAgIGlmICghbm9QZXJzaXN0KVxyXG4gICAgICAgICAgICAgICAgICAgICAgICAgICAgdG9nZ2xlLmFkZFN0YXRlKGNsYXNzbmFtZSk7XHJcbiAgICAgICAgICAgICAgICAgICAgfVxyXG5cclxuICAgICAgICAgICAgICAgIH1cclxuXHJcbiAgICAgICAgICAgICAgICAvLyBzb21lIGVsZW1lbnRzIG1heSBuZWVkIHRoaXMgd2hlbiB0b2dnbGVkIGNsYXNzIGNoYW5nZSB0aGUgY29udGVudCBzaXplXHJcbiAgICAgICAgICAgICAgICAkKHdpbmRvdykucmVzaXplKCk7XHJcblxyXG4gICAgICAgICAgICB9KTtcclxuXHJcbiAgICB9XHJcblxyXG4gICAgLy8gSGFuZGxlIHN0YXRlcyB0by9mcm9tIGxvY2Fsc3RvcmFnZVxyXG4gICAgdmFyIFN0YXRlVG9nZ2xlciA9IGZ1bmN0aW9uKCkge1xyXG5cclxuICAgICAgICB2YXIgU1RPUkFHRV9LRVlfTkFNRSA9ICdqcS10b2dnbGVTdGF0ZSc7XHJcblxyXG4gICAgICAgIC8qKiBBZGQgYSBzdGF0ZSB0byB0aGUgYnJvd3NlciBzdG9yYWdlIHRvIGJlIHJlc3RvcmVkIGxhdGVyICovXHJcbiAgICAgICAgdGhpcy5hZGRTdGF0ZSA9IGZ1bmN0aW9uKGNsYXNzbmFtZSkge1xyXG4gICAgICAgICAgICB2YXIgZGF0YSA9IFN0b3JhZ2VzLmxvY2FsU3RvcmFnZS5nZXQoU1RPUkFHRV9LRVlfTkFNRSk7XHJcbiAgICAgICAgICAgIGlmIChkYXRhIGluc3RhbmNlb2YgQXJyYXkpIGRhdGEucHVzaChjbGFzc25hbWUpO1xyXG4gICAgICAgICAgICBlbHNlIGRhdGEgPSBbY2xhc3NuYW1lXTtcclxuICAgICAgICAgICAgU3RvcmFnZXMubG9jYWxTdG9yYWdlLnNldChTVE9SQUdFX0tFWV9OQU1FLCBkYXRhKTtcclxuICAgICAgICB9O1xyXG4gICAgICAgIC8qKiBSZW1vdmUgYSBzdGF0ZSBmcm9tIHRoZSBicm93c2VyIHN0b3JhZ2UgKi9cclxuICAgICAgICB0aGlzLnJlbW92ZVN0YXRlID0gZnVuY3Rpb24oY2xhc3NuYW1lKSB7XHJcbiAgICAgICAgICAgIHZhciBkYXRhID0gU3RvcmFnZXMubG9jYWxTdG9yYWdlLmdldChTVE9SQUdFX0tFWV9OQU1FKTtcclxuICAgICAgICAgICAgaWYgKGRhdGEpIHtcclxuICAgICAgICAgICAgICAgIHZhciBpbmRleCA9IGRhdGEuaW5kZXhPZihjbGFzc25hbWUpO1xyXG4gICAgICAgICAgICAgICAgaWYgKGluZGV4ICE9PSAtMSkgZGF0YS5zcGxpY2UoaW5kZXgsIDEpO1xyXG4gICAgICAgICAgICAgICAgU3RvcmFnZXMubG9jYWxTdG9yYWdlLnNldChTVE9SQUdFX0tFWV9OQU1FLCBkYXRhKTtcclxuICAgICAgICAgICAgfVxyXG4gICAgICAgIH07XHJcbiAgICAgICAgLyoqIExvYWQgdGhlIHN0YXRlIHN0cmluZyBhbmQgcmVzdG9yZSB0aGUgY2xhc3NsaXN0ICovXHJcbiAgICAgICAgdGhpcy5yZXN0b3JlU3RhdGUgPSBmdW5jdGlvbigkZWxlbSkge1xyXG4gICAgICAgICAgICB2YXIgZGF0YSA9IFN0b3JhZ2VzLmxvY2FsU3RvcmFnZS5nZXQoU1RPUkFHRV9LRVlfTkFNRSk7XHJcbiAgICAgICAgICAgIGlmIChkYXRhIGluc3RhbmNlb2YgQXJyYXkpXHJcbiAgICAgICAgICAgICAgICAkZWxlbS5hZGRDbGFzcyhkYXRhLmpvaW4oJyAnKSk7XHJcbiAgICAgICAgfTtcclxuICAgIH07XHJcblxyXG4gICAgd2luZG93LlN0YXRlVG9nZ2xlciA9IFN0YXRlVG9nZ2xlcjtcclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyIsIi8qKj09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PVxyXG4gKiBNb2R1bGU6IHRyaWdnZXItcmVzaXplLmpzXHJcbiAqIFRyaWdnZXJzIGEgd2luZG93IHJlc2l6ZSBldmVudCBmcm9tIGFueSBlbGVtZW50XHJcbiA9PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT09PT0qL1xyXG5cclxuKGZ1bmN0aW9uKHdpbmRvdywgZG9jdW1lbnQsICQsIHVuZGVmaW5lZCkge1xyXG4gICAgJ3VzZSBzdHJpY3QnO1xyXG5cclxuICAgICQoaW5pdFRyaWdnZXJSZXNpemUpO1xyXG5cclxuICAgIGZ1bmN0aW9uIGluaXRUcmlnZ2VyUmVzaXplKCkge1xyXG4gICAgICAgIHZhciBlbGVtZW50ID0gJCgnW2RhdGEtdHJpZ2dlci1yZXNpemVdJyk7XHJcbiAgICAgICAgdmFyIHZhbHVlID0gZWxlbWVudC5kYXRhKCd0cmlnZ2VyUmVzaXplJylcclxuICAgICAgICBlbGVtZW50Lm9uKCdjbGljaycsIGZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICBzZXRUaW1lb3V0KGZ1bmN0aW9uKCkge1xyXG4gICAgICAgICAgICAgICAgLy8gYWxsIElFIGZyaWVuZGx5IGRpc3BhdGNoRXZlbnRcclxuICAgICAgICAgICAgICAgIHZhciBldnQgPSBkb2N1bWVudC5jcmVhdGVFdmVudCgnVUlFdmVudHMnKTtcclxuICAgICAgICAgICAgICAgIGV2dC5pbml0VUlFdmVudCgncmVzaXplJywgdHJ1ZSwgZmFsc2UsIHdpbmRvdywgMCk7XHJcbiAgICAgICAgICAgICAgICB3aW5kb3cuZGlzcGF0Y2hFdmVudChldnQpO1xyXG4gICAgICAgICAgICAgICAgLy8gbW9kZXJuIGRpc3BhdGNoRXZlbnQgd2F5XHJcbiAgICAgICAgICAgICAgICAvLyB3aW5kb3cuZGlzcGF0Y2hFdmVudChuZXcgRXZlbnQoJ3Jlc2l6ZScpKTtcclxuICAgICAgICAgICAgfSwgdmFsdWUgfHwgMzAwKTtcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyIsIi8vIEN1c3RvbSBqUXVlcnlcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcblxyXG4oZnVuY3Rpb24od2luZG93LCBkb2N1bWVudCwgJCwgdW5kZWZpbmVkKSB7XHJcbiAgICAndXNlIHN0cmljdCc7XHJcblxyXG4gICAgJChpbml0Q3VzdG9tKTtcclxuXHJcbiAgICBmdW5jdGlvbiBpbml0Q3VzdG9tKCkge1xyXG5cclxuICAgICAgICAvLyBjdXN0b20gY29kZVxyXG5cclxuICAgIH1cclxuXHJcbn0pKHdpbmRvdywgZG9jdW1lbnQsIHdpbmRvdy5qUXVlcnkpOyJdfQ==

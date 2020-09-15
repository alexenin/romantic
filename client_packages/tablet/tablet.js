let browser = null;

mp.keys.bind(0x4F, false, function () {
    if (browser != null) {
        browser.destroy();
        browser = null;
        mp.gui.cursor.show(false, false);
        mp.game.ui.displayRadar(true);
        return
    }
    if (mp.gui.cursor.visible)
        return;
    
    browser = mp.browsers.new('package://tablet/tablet.html');

    mp.gui.cursor.show(true, true);
    mp.game.ui.displayRadar(false);
    
    
})
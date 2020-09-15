setInterval(function () {
    var name = (global.localplayer.getVariable('REMOTE_ID') == undefined) ? `Не авторизован` : `Игрок №${global.localplayer.getVariable("REMOTE_ID")}`; 
	mp.discord.update('Astrovskiy.roleplay', name);
}, 10000);
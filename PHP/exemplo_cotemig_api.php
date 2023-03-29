<?php
// Documentação disponível em: https://api.cotemig.com.br/v1/doc

include('./httpful.phar'); // Cliente REST para PHP: http://phphttpclient.com

$url = 'https://api.cotemig.com.br/v1/';

$response = \Httpful\Request::get($url.'perfil')
    ->expectsJson()
	->authenticateWith('USUARIO', 'SENHA') // substitua pelos dados do usuário (serão criptografados pelo HTTPS)
    ->send();

if($response->code != 200) {
	echo "<p>Erro {$response->body->erro}: {$response->body->detalhes}</p>";
}
else {
	echo "<p>Código: {$response->body->id}<br/>";
	echo "Nome: {$response->body->nome}</p>";
}
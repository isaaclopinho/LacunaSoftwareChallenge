# LacunaSoftwareChallenge

> In order to demonstrate that the misuse of cryptography can lead into major security flaws, this challenge will consist of a server that uses One-Time Pad cipher (applying bit-wise XOR for each character) to encrypt and decrypt its authentication tokens, but always reuses the secret key.
Your objective, as challenger, is to forge a valid token for the user “master” — by exploiting the server’s misuse of OTP cipher, mentioned above — and then access an endpoint which only the master has access to.

# Como foi feito:

> Primeiro, foram criadas 4 contas com 32 caracteres cada com caracteres repetidos para uma primeira avaliação do padrão do token que é gerado.

> As contas foram:
- 000000000000000000000000000000
- 000000000000000000000000001111
- 111100000000000000000000000000
- 111100000000000000000000001111

> Convertendo-os para hexadecimal, foi possível verificar que os 12 primeiros caracteres do token gerado sempre eram iguais, enquanto que as partes com 0000 ou 1111 (convertidos para hexadecimal) também continham partes parecidas e, assim, verificou-se que os próximos 64 caracteres constituia o nome do usuário.

> Com o usuário criptografado e com o nome de usuário em hexadecimal, foi possível descobrir a chave de criptografia com uma simples operação XOR bit a bit. Dessa maneira, a key encontrada foi: BA8403B04475857897E04CFD7A6423ADFEF618EB12EC7A3AE5D1AFCA1BCEFC9F.

> Para criptografar o usuário "master", foi necessário criar uma conta com menos de 32 caracteres para descobrir qual o caracter que é utilizado para preencher os caracteres faltantes. Então foi criado a conta "00000000", onde utilizando a operação XOR, da conta criptografada e da key encontrada, descobriu-se que o caracter utilizado era "#" ou "23" em hexadecimal. Assim, o nome "master##########################" simplesmente poderia ser convertido para o hexadecimal e utilizando a operação XOR com a key encontramos a chave para substituir no token.

- Usuário Master Criptografado: 7E570C42107A65BB4C36FDE5947008EDDD53BC831CF5919C6F28CE938EDDFBC

> Por último, foi feito uma requisição de login para gerar um token válido. Com esse, substituimos a parte do usuário (a partir do 13 caractere) com o Usuário Master e inserimos um Bearer Token de autenticação no header para fazer requisções para a parte secreta. Em que é mostrado a seguinte mensagem:

```
{"secret":"The System Master secret is: Science is not good or bad, but it can be used both ways.","status":"Success"}
```


# ersion 1.0.0
### License: MIT

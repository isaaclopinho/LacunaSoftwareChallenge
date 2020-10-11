# LacunaSoftwareChallenge

> In order to demonstrate that the misuse of cryptography can lead into major security flaws, this challenge will consist of a server that uses One-Time Pad cipher (applying bit-wise XOR for each character) to encrypt and decrypt its authentication tokens, but always reuses the secret key.
Your objective, as challenger, is to forge a valid token for the user “master” — by exploiting the server’s misuse of OTP cipher, mentioned above — and then access an endpoint which only the master has access to.

# Resolução do Desafio:

> Primeiro, criou-se 4 contas com 32 caracteres contendo padrões repetidos para inspecionar o token gerado.

> As contas foram:
- 000000000000000000000000000000
- 000000000000000000000000001111
- 111100000000000000000000000000
- 111100000000000000000000001111

> Ao converter para hexadecimal, verificou-se que os 12 primeiros caracteres eram iguais, enquanto que as partes com 0000 ou 1111 eram bastante parecidas em todo token gerado. Assim, concluiu-se nome do usuário possuia 64 caracteres de comprimento e localizava-se após os 12 primeiros.

> Após isso, foi feito uma operação XOR entre a parte do usuário extraída no token e o nome de usuário equivalente em hexadecimal. Dessa maneira, a key encontrada foi: BA8403B04475857897E04CFD7A6423ADFEF618EB12EC7A3AE5D1AFCA1BCEFC9F.

> Para criptografar o usuário "master", criou-se um usuário novo com menos de 32 caracteres de comprimento para descobrir o caracterer usado para o "padding". Criou-se a conta  "00000000" e utilizou-se novamente da operação XOR entre a conta criptografada e da key encontrada anteriormente. Descobriu-se que o caracter utilizado era "#" ou "23" em hexadecimal. Assim, a conta "master##########################" foi convertida para hexadecimal e utilizou-se a operação XOR com a key e obteve-se a chave para substituir no token.

- Usuário Master Criptografado: 7E570C42107A65BB4C36FDE5947008EDDD53BC831CF5919C6F28CE938EDDFBC

> Por último, foi feito uma requisição de login para gerar um token válido. Com esse, substituimos a parte do usuário com o Usuário Master e inserimos um Bearer Token de autenticação no header para fazer requisições para a parte secreta. Ao fim, obteve-se a resposta:

```
{"secret":"The System Master secret is: Science is not good or bad, but it can be used both ways.","status":"Success"}
```


# Version 1.0.0
### License: MIT

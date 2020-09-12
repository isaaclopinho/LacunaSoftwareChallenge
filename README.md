# LacunaSoftwareChallenge

> In order to demonstrate that the misuse of cryptography can lead into major security flaws, this challenge will consist of a server that uses One-Time Pad cipher (applying bit-wise XOR for each character) to encrypt and decrypt its authentication tokens, but always reuses the secret key.
Your objective, as challenger, is to forge a valid token for the user “master” — by exploiting the server’s misuse of OTP cipher, mentioned above — and then access an endpoint which only the master has access to.

- Version 1.0.0
### License: MIT

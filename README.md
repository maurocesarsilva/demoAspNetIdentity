
#gerar chave privada 2048
openssl genpkey -algorithm RSA -out private_key.pem -pkeyopt rsa_keygen_bits:2048

#gerar chave publica 2048
openssl rsa -pubout -in private_key.pem -out public_key.pem



#gerar chave privada 256
openssl ecparam -name prime256v1 -genkey -noout -out private-key.pem


#gerar chave publica 256
openssl ec -in private-key.pem -pubout -out public-key.pem

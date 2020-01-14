package main
//"sselph"
//ROMHasher20160916
//sselph-scraper
import (
    "fmt"
	"crypto/aes"
	"crypto/cipher"
	"crypto/rand"
	"encoding/base64"
	"encoding/json"
	"io"
)

const key = "Ym+lFlDfNS/Ud2kRTitrzA54qKBlAru5vD3k6fUqrCc="
const data = "tyaeVitrQtwVtlZdKN1htjwLYrLgSezPt2edn8o1ayryAQmCHnIK3BKCbGZfMA+5FGbFMrgR1UD1p+vHkuUzgby/oR0kKzHcryJe/2OuzjMsy6K7CYaCCWzFWPny0i8XU3w="

func main() {

dev := DeobfuscateDevInfo()
fmt.Printf("%q", dev.Name)
return

}

// DevInfo is the information about the developer and used across APIs.
type DevInfo struct {
	ID       string
	Password string
	Name     string
}

func ObfuscateDevInfo(info DevInfo) (string){
	k, err := base64.StdEncoding.DecodeString(key)
	if err != nil {
		return ""
	}
	b, err := json.Marshal(info)
	if err != nil {
		return ""
	}
	block, err := aes.NewCipher(k[:])
	if err != nil {
		return ""
	}
	gcm, err := cipher.NewGCM(block)
	if err != nil {
		return ""
	}
	nonce := make([]byte, 12)
	if _, err := io.ReadFull(rand.Reader, nonce); err != nil {
		panic(err.Error())
	}
	ciphertext := gcm.Seal(nil, nonce, b, nil)
	return base64.StdEncoding.EncodeToString(append(nonce, ciphertext...))
}

func DeobfuscateDevInfo() (info DevInfo) {
	out := DevInfo{}
	k, err := base64.StdEncoding.DecodeString(key)
	if err != nil {
		return out
	}
	d, err := base64.StdEncoding.DecodeString(data)
	if err != nil {
		return out
	}
	block, err := aes.NewCipher(k[:])
	if err != nil {
		return out
	}
	gcm, err := cipher.NewGCM(block)
	if err != nil {
		return out
	}
	if len(d) < gcm.NonceSize() {
		return out
	}
	p, err := gcm.Open(nil, d[:gcm.NonceSize()], d[gcm.NonceSize():], nil)
	if err != nil {
		return out
	}
	err = json.Unmarshal(p, &out)
	return out
}
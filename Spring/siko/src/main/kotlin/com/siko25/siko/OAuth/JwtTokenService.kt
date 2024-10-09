package com.siko25.siko.OAuth

import io.jsonwebtoken.*
import io.jsonwebtoken.Claims
import io.jsonwebtoken.security.Keys
import java.util.*
import javax.crypto.SecretKey
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken
import org.springframework.security.core.Authentication
import org.springframework.security.core.authority.SimpleGrantedAuthority
import org.springframework.security.core.userdetails.User
import org.springframework.stereotype.Service

@Service
class JwtTokenService {
    private val secretKey =
            System.getenv("JWT_SECRET_KEY")
                    ?: "TestKeyTestKeyTestKeyTestKeyTestKeyTestKeyTestKeyTestKey"
    private val expirationTime = 1000L * 60 * 60 * 24 * 30 // 30 days

    fun generateToken(playerId: String): TokenInfo {
        val now = Date()
        val expiration = Date(now.time + expirationTime)

        val jwts =
                Jwts.builder()
                        .subject(playerId)
                        .claim("playerId", playerId) // Add this line
                        .issuedAt(now)
                        .expiration(expiration)
                        .signWith(getKey())
                        .compact()

        return TokenInfo(jwts)
    }
    fun getKey(): SecretKey {
        return Keys.hmacShaKeyFor(secretKey.toByteArray())
    }

    private fun getClaims(jwt: String): Claims {
        return Jwts.parser().verifyWith(getKey()).build().parseSignedClaims(jwt).payload
    }

    fun getAuthentication(jwt: String): Authentication {
        val claims = getClaims(jwt)
        val authorities = listOf(SimpleGrantedAuthority("ROLE_USER")) // Default role
        val playerId = claims.subject // Use the subject as playerId
        val principal = User(playerId, "", authorities)
        return UsernamePasswordAuthenticationToken(principal, jwt, authorities)
    }

    fun validateToken(jwt: String): Boolean {
        try {
            getClaims(jwt)
            return true
        } catch (e: Exception) {
            if (e is SecurityException) {
                throw JwtException("JWT is invalid")
            }
            if (e is MalformedJwtException) {
                throw JwtException("JWT is malformed")
            }
            if (e is ExpiredJwtException) {
                throw JwtException("JWT is expired")
            }
            if (e is UnsupportedJwtException) {
                throw JwtException("JWT is unsupported")
            }
            if (e is IllegalArgumentException) {
                throw JwtException("JWT is invalid")
            } else {
                throw JwtException("JWT is invalid")
            }
        }
    }
}

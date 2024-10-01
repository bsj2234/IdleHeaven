package com.siko25.siko.OAuth

import org.springframework.security.oauth2.client.userinfo.DefaultOAuth2UserService
import org.springframework.security.oauth2.client.userinfo.OAuth2UserRequest
import org.springframework.security.oauth2.client.userinfo.OAuth2UserService
import org.springframework.security.oauth2.core.user.OAuth2User
import org.springframework.stereotype.Service

@Service
class CustomOAuth2UserService(private val jwtTokenService: JwtTokenService) :
        OAuth2UserService<OAuth2UserRequest, OAuth2User> {
    val tokenMap = mutableMapOf<String, String>()
    override fun loadUser(userRequest: OAuth2UserRequest): OAuth2User {
        val delegate = DefaultOAuth2UserService()
        val oAuth2User = delegate.loadUser(userRequest)

        return oAuth2User
    }

    fun generateToken(playerId: String): String {
        val token = jwtTokenService.generateToken(playerId)
        tokenMap[token.accessToken] = playerId
        return token.accessToken
    }
}

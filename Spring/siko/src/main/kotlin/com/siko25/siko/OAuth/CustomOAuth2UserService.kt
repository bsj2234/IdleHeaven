package com.siko25.siko.OAuth

import com.siko25.siko.character.player.playerId.PlayerIdService
import org.springframework.security.oauth2.client.userinfo.DefaultOAuth2UserService
import org.springframework.security.oauth2.client.userinfo.OAuth2UserRequest
import org.springframework.security.oauth2.client.userinfo.OAuth2UserService
import org.springframework.security.oauth2.core.user.OAuth2User
import org.springframework.stereotype.Service

@Service
class CustomOAuth2UserService(
        private val jwtTokenService: JwtTokenService,
        private val playerIdService: PlayerIdService
) : OAuth2UserService<OAuth2UserRequest, OAuth2User> {
    override fun loadUser(userRequest: OAuth2UserRequest): OAuth2User {
        val delegate = DefaultOAuth2UserService()
        val oAuth2User = delegate.loadUser(userRequest)

        return oAuth2User
    }

    fun validateUser(playerId: String, token: String): Boolean {
        val storedToken = playerIdService.getToken(playerId)
        if (storedToken == null) {
            return false
        }
        return storedToken == token
    }
}

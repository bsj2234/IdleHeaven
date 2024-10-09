package com.siko25.siko.OAuth

import com.siko25.siko.character.player.playerId.PlayerIdService
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import java.time.Duration
import org.springframework.http.HttpHeaders
import org.springframework.http.ResponseCookie
import org.springframework.security.core.Authentication
import org.springframework.security.oauth2.core.user.OAuth2User
import org.springframework.security.web.DefaultRedirectStrategy
import org.springframework.security.web.RedirectStrategy
import org.springframework.security.web.authentication.SimpleUrlAuthenticationSuccessHandler
import org.springframework.stereotype.Component

@Component
class OAuth2AuthenticationSuccessHandler(
        private val customOAuth2UserService: CustomOAuth2UserService,
        private val playerIdService: PlayerIdService,
        private val jwtTokenService: JwtTokenService
) : SimpleUrlAuthenticationSuccessHandler() {

    init {
        setDefaultTargetUrl(defaultTargetUrl)
    }

    private val redirectStrategy: RedirectStrategy = DefaultRedirectStrategy()
    override fun onAuthenticationSuccess(
            request: HttpServletRequest,
            response: HttpServletResponse,
            authentication: Authentication,
    ) {
        val oAuth2User = authentication.principal as OAuth2User
        var token = playerIdService.getToken(oAuth2User.name)
        if (token == null) {
            token = jwtTokenService.generateToken(oAuth2User.name).accessToken
            playerIdService.createPlayerId(oAuth2User.name, token)
        }

        val cookie =
                ResponseCookie.from("auth_token", token)
                        .httpOnly(true)
                        .secure(true) // set to true if using HTTPS
                        .path("/")
                        .maxAge(Duration.ofDays(7))
                        .build()

        response.addHeader(HttpHeaders.SET_COOKIE, cookie.toString())

        // Redirect to home page or a specific URL
        super.setDefaultTargetUrl("/oauth2-callback")
        super.onAuthenticationSuccess(request, response, authentication)
    }
}

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
import org.springframework.security.web.authentication.AuthenticationSuccessHandler
import org.springframework.security.web.authentication.SimpleUrlAuthenticationSuccessHandler
import org.springframework.stereotype.Component

@Component
class OAuth2AuthenticationSuccessHandler(
        private val customOAuth2UserService: CustomOAuth2UserService,
        private val playerIdService: PlayerIdService,
        private val jwtTokenService: JwtTokenService
) : SimpleUrlAuthenticationSuccessHandler(), AuthenticationSuccessHandler {

    private val redirectStrategy: RedirectStrategy = DefaultRedirectStrategy()
    override fun onAuthenticationSuccess(
            request: HttpServletRequest,
            response: HttpServletResponse,
            authentication: Authentication,
    ) {
        println("OAuth2 authentication success")
        try {
            val oAuth2User = authentication.principal as OAuth2User

            var token = playerIdService.getToken(oAuth2User.name)
            if (token == null) {
                token = jwtTokenService.generateToken(oAuth2User.name).accessToken
                playerIdService.createPlayerId(oAuth2User.name, token)
            }
            println("Generated token: $token")

            val cookie =
                    ResponseCookie.from("auth_token", token)
                            .httpOnly(true)
                            .secure(true)
                            .path("/")
                            .maxAge(Duration.ofHours(1))
                            .build()

            response.addHeader(HttpHeaders.SET_COOKIE, cookie.toString())

            // Redirect to the callback URL
            redirectStrategy.sendRedirect(request, response, "/oauth2-callback")
        } catch (e: Exception) {
            println("Error in OAuth2AuthenticationSuccessHandler: ${e.message}")
            e.printStackTrace()
            // Redirect to an error page or login page
            redirectStrategy.sendRedirect(request, response, "/login?error=authentication_failed")
        }
    }
}

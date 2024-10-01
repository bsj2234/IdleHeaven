package com.siko25.siko.OAuth

import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity
import org.springframework.security.config.annotation.web.invoke
import org.springframework.security.core.Authentication
import org.springframework.security.oauth2.core.user.OAuth2User
import org.springframework.security.web.SecurityFilterChain
import org.springframework.security.web.authentication.SimpleUrlAuthenticationSuccessHandler
import org.springframework.stereotype.Component

@Configuration
@EnableWebSecurity
class SecurityConfig(
        private val customOAuth2UserService: CustomOAuth2UserService,
        private val oAuth2AuthenticationSuccessHandler: OAuth2AuthenticationSuccessHandler
) {
    @Bean
    fun securityFilterChain(http: HttpSecurity): SecurityFilterChain {
        http
                .authorizeHttpRequests { authorizeRequests ->
                    authorizeRequests
                            .requestMatchers("/login", "/auth/callback", "/error")
                            .permitAll()
                            .anyRequest()
                            .authenticated()
                }
                .oauth2Login { oauth2Login ->
                    oauth2Login
                            .loginPage("/login")
                            .userInfoEndpoint { userInfoEndpoint ->
                                userInfoEndpoint.userService(customOAuth2UserService)
                            }
                            .successHandler(oAuth2AuthenticationSuccessHandler)
                }
                .csrf { csrf -> csrf.disable() } // For testing purposes. Enable in production.

        return http.build()
    }
}

@Component
class OAuth2AuthenticationSuccessHandler(
        private val customOAuth2UserService: CustomOAuth2UserService
) : SimpleUrlAuthenticationSuccessHandler() {
    override fun onAuthenticationSuccess(
            request: HttpServletRequest,
            response: HttpServletResponse,
            authentication: Authentication
    ) {
        val oAuth2User = authentication.principal as OAuth2User
        val token = customOAuth2UserService.generateToken(oAuth2User.name)

        println("Generated token: $token") // Add this line for debugging

        val redirectUrl = "http://localhost:8080/oauth2-callback?token=$token"
        println("Redirecting to: $redirectUrl") // Add this line for debugging

        response.sendRedirect(redirectUrl)
    }
}

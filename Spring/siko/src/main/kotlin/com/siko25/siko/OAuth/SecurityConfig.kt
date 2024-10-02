package com.siko25.siko.OAuth

import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity
import org.springframework.security.config.annotation.web.invoke
import org.springframework.security.web.SecurityFilterChain

@Configuration
@EnableWebSecurity
class SecurityConfig(
        private val customOAuth2UserService: CustomOAuth2UserService,
        private val oAuth2AuthenticationSuccessHandler: OAuth2AuthenticationSuccessHandler
) {
    @Bean
    fun securityFilterChain(http: HttpSecurity): SecurityFilterChain {
        http
                // .requiresChannel { channel -> channel.anyRequest().requiresSecure() }
                .requiresChannel { channel -> channel.anyRequest().requiresInsecure() }
                .authorizeHttpRequests { authorize ->
                    authorize
                            .requestMatchers(
                                    "/login",
                                    "/auth/callback",
                                    "/error",
                                    "/login/oauth2/code/*"
                            )
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

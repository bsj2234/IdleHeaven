package com.siko25.siko.OAuth

import jakarta.servlet.http.HttpServletRequest
import org.springframework.security.core.annotation.AuthenticationPrincipal
import org.springframework.security.oauth2.core.user.OAuth2User
import org.springframework.stereotype.Controller
import org.springframework.ui.Model
import org.springframework.web.bind.annotation.GetMapping

@Controller
class LoginController {

    @GetMapping("/login")
    fun login(): String {
        return "login"
    }

    @GetMapping("/oauth2-callback")
    fun oauth2Callback(
            request: HttpServletRequest,
            @AuthenticationPrincipal user: OAuth2User?,
            model: Model
    ): String {
        // Retrieve the token from the session
        // val token = request.session.getAttribute("oauth2_token") as? String
        // request.session.removeAttribute("oauth2_token") // Remove the token from the session
        println("All cookies: ${request.cookies?.joinToString { "${it.name}=${it.value}" }}")
        val token = request.cookies?.find { it.name == "auth_token" }?.value
        println("Found auth_token: $token")

        if (token == null) {
            println("No token provided")
            return "redirect:/login"
        }

        user?.let {
            model.addAttribute("name", it.getAttribute("name"))
            model.addAttribute("email", it.getAttribute("email"))
        }
        model.addAttribute("token", token)
        return "oauth2-callback"
    }
}

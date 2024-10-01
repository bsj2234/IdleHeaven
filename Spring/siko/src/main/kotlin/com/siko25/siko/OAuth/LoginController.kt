package com.siko25.siko.OAuth

import org.springframework.security.core.annotation.AuthenticationPrincipal
import org.springframework.security.oauth2.core.user.OAuth2User
import org.springframework.stereotype.Controller
import org.springframework.ui.Model
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestParam

@Controller
class LoginController {
    @GetMapping("/login")
    fun login(): String {
        return "login"
    }

    @GetMapping("/oauth2-callback")
    fun oauth2Callback(
            @RequestParam token: String?,
            @AuthenticationPrincipal user: OAuth2User?,
            model: Model
    ): String {
        println("Received callback with token: $token") // Add this line for debugging
        user?.let {
            model.addAttribute("name", it.getAttribute("name"))
            model.addAttribute("email", it.getAttribute("email"))
        }
        model.addAttribute("token", token ?: "No token provided")
        return "oauth2-callback"
    }
}

package com.siko25.siko.OAuth

import jakarta.servlet.*
import jakarta.servlet.http.*
import org.springframework.security.core.context.SecurityContextHolder
import org.springframework.web.filter.OncePerRequestFilter

class JwtTokenFilter(private val jwtTokenService: JwtTokenService) : OncePerRequestFilter() {
    override fun doFilterInternal(
            request: HttpServletRequest,
            response: HttpServletResponse,
            filterChain: FilterChain
    ) {
        val token = extractTokenFromRequest(request)
        if (token != null && jwtTokenService.validateToken(token)) {
            val authentication = jwtTokenService.getAuthentication(token)
            SecurityContextHolder.getContext().authentication = authentication
        }
        filterChain.doFilter(request, response)
    }

    private fun extractTokenFromRequest(request: HttpServletRequest): String? {
        val cookieToken = request.cookies?.find { it.name == "auth_token" }?.value
        val headerToken = request.getHeader("auth_token")
        return headerToken ?: cookieToken
    }
}

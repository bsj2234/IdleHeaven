package com.siko25.siko.OAuth

import jakarta.servlet.FilterChain
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.slf4j.LoggerFactory
import org.springframework.security.core.context.SecurityContextHolder
import org.springframework.stereotype.Component
import org.springframework.web.filter.OncePerRequestFilter

@Component
class RequestLoggingFilter : OncePerRequestFilter() {
    private val log = LoggerFactory.getLogger(RequestLoggingFilter::class.java)

    override fun doFilterInternal(
            request: HttpServletRequest,
            response: HttpServletResponse,
            filterChain: FilterChain
    ) {
        log.debug("Processing request: ${request.method} ${request.requestURI}")
        log.debug("Authentication: ${SecurityContextHolder.getContext().authentication}")
        filterChain.doFilter(request, response)
        log.debug("Response status: ${response.status}")
    }
}

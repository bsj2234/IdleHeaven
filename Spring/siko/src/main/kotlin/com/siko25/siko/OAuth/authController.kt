import com.siko25.siko.OAuth.JwtTokenService
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api/auth")
class AuthController(private val jwtTokenService: JwtTokenService) {

    @PostMapping("/login")
    fun login(@RequestBody loginRequest: LoginRequest): LoginResponse {
        // 여기서 사용자 인증 로직을 구현합니다 (예: 데이터베이스에서 사용자 확인)
        // 인증이 성공했다고 가정합니다

        val playerId = "player123" // 실제로는 인증된 플레이어의 ID를 사용해야 합니다
        val token = jwtTokenService.generateToken(playerId)

        return LoginResponse(token.accessToken)
    }
}

data class LoginRequest(val username: String, val password: String)

data class LoginResponse(val token: String)




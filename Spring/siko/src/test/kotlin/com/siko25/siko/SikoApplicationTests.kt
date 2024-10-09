package com.siko25.siko

import org.junit.jupiter.api.Test
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.test.context.TestPropertySource

@SpringBootTest
@TestPropertySource(properties = [
    "logging.level.org.springframework=DEBUG",
    "logging.level.com.siko25.siko=DEBUG"
])
class SikoApplicationTests {

    @Test fun contextLoads() {}
}

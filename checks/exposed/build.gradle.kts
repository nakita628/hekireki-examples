// Compiles the Kotlin hekireki-exposed wrote (copied to src/main/kotlin/models by check.sh)
// against the real Exposed, with every warning an error.
plugins {
    kotlin("jvm") version "2.3.20"
}

repositories {
    mavenCentral()
}

dependencies {
    val exposed = "1.5.0"
    implementation("org.jetbrains.exposed:exposed-core:$exposed")
    implementation("org.jetbrains.exposed:exposed-jdbc:$exposed")
    implementation("org.jetbrains.exposed:exposed-dao:$exposed")
    implementation("org.jetbrains.exposed:exposed-json:$exposed")
    implementation("org.postgresql:postgresql:42.7.13")
    // cuid() / cuid(2) and ulid() defaults are generated with these.
    implementation("io.github.thibaultmeyer:cuid:2.0.5")
    implementation("com.github.f4b6a3:ulid-creator:5.2.4")
    runtimeOnly("org.slf4j:slf4j-nop:2.0.17")
}

kotlin {
    jvmToolchain(21)
    compilerOptions {
        allWarningsAsErrors.set(true)
    }
}

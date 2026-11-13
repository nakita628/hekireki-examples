package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<Int>("users") {
    override val id = integer("id").autoIncrement().entityId()
    val email = text("email").uniqueIndex("users_email_key")
    val passwordHash = text("password_hash").nullable()
    val isVerified = integer("is_verified").default(0)
    val verificationToken = text("verification_token").nullable()
    val verificationTokenExpiresAt = long("verification_token_expires_at").nullable()
    val verificationResendCount = integer("verification_resend_count").default(0)
    val resetPasswordToken = text("reset_password_token").nullable()
    val resetPasswordTokenExpiresAt = long("reset_password_token_expires_at").nullable()
    val passkeyRecoveryToken = text("passkey_recovery_token").nullable()
    val passkeyRecoveryTokenExpiresAt = long("passkey_recovery_token_expires_at").nullable()
    val loginToken = text("login_token").nullable()
    val loginTokenExpiresAt = long("login_token_expires_at").nullable()
    val failedLoginAttempts = integer("failed_login_attempts").default(0)
    val lockedUntil = long("locked_until").nullable()
    val tokenVersion = integer("token_version").default(0)
    val termsAcceptedAt = long("terms_accepted_at").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val storageQuotaBytes = long("storage_quota_bytes").default(104857600L)
    val storageUsedBytes = long("storage_used_bytes").default(0L)
    override val primaryKey = PrimaryKey(id, name = "users_pkey")

    init {
        index("users_verification_token_idx", false, verificationToken)
        index("users_reset_password_token_idx", false, resetPasswordToken)
        index("users_passkey_recovery_token_idx", false, passkeyRecoveryToken)
        index("users_login_token_idx", false, loginToken)
    }
}

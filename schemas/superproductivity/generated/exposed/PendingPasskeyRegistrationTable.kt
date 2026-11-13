package superproductivity.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PendingPasskeyRegistrationTable : IdTable<String>("pending_passkey_registrations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val verificationToken = text("verification_token").uniqueIndex("pending_passkey_registrations_verification_token_key")
    val verificationTokenExpiresAt = long("verification_token_expires_at")
    val credentialId = binary("credential_id")
    val publicKey = binary("public_key")
    val counter = long("counter").default(0L)
    val transports = text("transports").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "pending_passkey_registrations_user_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "pending_passkey_registrations_pkey")

    init {
        index("pending_passkey_registrations_user_id_idx", false, userId)
    }
}

package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PasskeyTable : IdTable<String>("\"Passkey\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val name = text("name").nullable()
    val publicKey = text("publicKey")
    val userId = text("userId")
    val credentialID = text("credentialID")
    val counter = integer("counter").default(0)
    val deviceType = text("deviceType")
    val backedUp = bool("backedUp").default(false)
    val transports = text("transports").nullable()
    val aaguid = text("aaguid").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Passkey_pkey\"")

    init {
        index("\"Passkey_userId_idx\"", false, userId)
        index("\"Passkey_credentialID_idx\"", false, credentialID)
    }
}

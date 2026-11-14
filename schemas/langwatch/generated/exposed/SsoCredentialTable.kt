package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SsoCredentialTable : IdTable<String>("\"SsoCredential\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val connectionId = text("connectionId")
    val kind = text("kind")
    val ciphertext = text("ciphertext")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SsoCredential_pkey\"")

    init {
        index("\"SsoCredential_organizationId_connectionId_idx\"", false, organizationId, connectionId)
    }
}

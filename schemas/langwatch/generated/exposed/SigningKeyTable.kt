package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SigningKeyTable : IdTable<String>("\"SigningKey\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val name = text("name")
    val publicId = text("publicId").uniqueIndex("\"SigningKey_publicId_key\"")
    val secretCiphertext = text("secretCiphertext")
    val createdByUserId = text("createdByUserId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val rotatedAt = pgTimestamp("rotatedAt", 3).nullable()
    val verifiesUntil = pgTimestamp("verifiesUntil", 3).nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SigningKey_pkey\"")

    init {
        index("\"SigningKey_projectId_idx\"", false, projectId)
        index("\"SigningKey_organizationId_idx\"", false, organizationId)
    }
}

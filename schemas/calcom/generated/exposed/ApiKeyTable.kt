package calcom.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyTable : IdTable<String>("\"ApiKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_teamId_fkey\"",
        )
    val note = text("note").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val hashedKey = text("hashedKey").uniqueIndex("\"ApiKey_hashedKey_key\"")
    val appId =
        optReference(
            "appId",
            AppTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_appId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ApiKey_pkey\"")

    init {
        uniqueIndex("\"ApiKey_id_key\"", id)
        index("\"ApiKey_userId_idx\"", false, userId)
    }
}

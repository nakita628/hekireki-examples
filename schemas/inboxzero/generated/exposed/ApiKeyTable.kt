package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyTable : IdTable<String>("\"ApiKey\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name").nullable()
    val hashedKey = text("hashedKey").uniqueIndex("\"ApiKey_hashedKey_key\"")
    val isActive = bool("isActive").default(true)
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val scopes =
        array("scopes", PgEnumColumnType("\"ApiKeyScope\"", ApiKeyScope.entries, ApiKeyScope::dbName))
            .nullable()
            .databaseDefault("'{}'") { emptyList() }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_userId_fkey\"",
        )
    val emailAccountId =
        optReference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiKey_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ApiKey_pkey\"")

    init {
        index("\"ApiKey_userId_isActive_idx\"", false, userId, isActive)
        index("\"ApiKey_emailAccountId_isActive_idx\"", false, emailAccountId, isActive)
    }
}

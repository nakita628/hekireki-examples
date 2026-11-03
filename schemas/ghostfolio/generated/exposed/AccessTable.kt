package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AccessTable : IdTable<String>("\"Access\"") {
    val alias = text("alias").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    val granteeUserId =
        optReference(
            "granteeUserId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Access_granteeUserId_fkey\"",
        )
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val scopes = array("scopes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val settings = jsonb<String>("settings", { it }, { it }).default("{}")
    val type = pgEnum("type", "\"AccessType\"", AccessType.entries, AccessType::dbName).default(AccessType.PRIVATE)
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Access_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Access_pkey\"")

    init {
        index("\"Access_alias_idx\"", false, alias)
        index("\"Access_granteeUserId_idx\"", false, granteeUserId)
        index("\"Access_userId_idx\"", false, userId)
    }
}

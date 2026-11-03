package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiTokenTable : IdTable<Int>("\"ApiToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val token = text("token").uniqueIndex("\"ApiToken_token_key\"")
    val algorithm =
        pgEnum("algorithm", "\"ApiTokenAlgorithm\"", ApiTokenAlgorithm.entries, ApiTokenAlgorithm::dbName).default(ApiTokenAlgorithm.SHA512)
    val expires = pgTimestamp("expires", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiToken_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiToken_teamId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ApiToken_pkey\"")
}

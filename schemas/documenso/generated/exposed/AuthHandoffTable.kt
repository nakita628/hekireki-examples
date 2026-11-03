package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuthHandoffTable : IdTable<String>("\"AuthHandoff\"") {
    override val id = text("id").entityId()
    val type = text("type")
    val payload = text("payload")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"AuthHandoff_pkey\"")

    init {
        index("\"AuthHandoff_expiresAt_idx\"", false, expiresAt)
    }
}

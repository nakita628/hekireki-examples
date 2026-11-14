package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserErasureTable : IdTable<String>("\"UserErasure\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val accountCreatedAt = pgTimestamp("accountCreatedAt", 3)
    val erasedAt = pgTimestamp("erasedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserErasure_pkey\"")

    init {
        index("\"UserErasure_accountCreatedAt_idx\"", false, accountCreatedAt)
    }
}

package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SignInProviderMountTable : IdTable<String>("\"SignInProviderMount\"") {
    override val id = text("id").default("self").entityId()
    val provider = text("provider").nullable()
    val mounted = bool("mounted")
    val lastAppliedEventId = text("lastAppliedEventId")
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SignInProviderMount_pkey\"")
}

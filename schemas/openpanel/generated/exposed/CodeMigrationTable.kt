package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CodeMigrationTable : IdTable<String>("__code_migrations") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val name = text("name").uniqueIndex("__code_migrations_name_key")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "__code_migrations_pkey")
}

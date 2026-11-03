package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RegisteredInstanceTable : IdTable<String>("registered_instances") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val instanceId = text("instance_id").uniqueIndex("registered_instances_instance_id_key")
    val version = text("version")
    val firstSeenAt = pgTimestamp("first_seen_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastSeenAt = pgTimestamp("last_seen_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "registered_instances_pkey")
}

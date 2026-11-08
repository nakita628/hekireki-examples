package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LicenseTable : IdTable<String>("licenses") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val key = text("key").uniqueIndex("licenses_key_key")
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"licenses_userId_fkey\"",
        )
    val validFrom = pgTimestamp("validFrom", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val validUntil = pgTimestamp("validUntil", 3).nullable()
    val maxUsers = integer("maxUsers").nullable().default(1)
    val features = jsonb<String>("features", { it }, { it }).nullable()
    val activatedAt = pgTimestamp("activatedAt", 3).nullable()
    val lastCheckedAt = pgTimestamp("lastCheckedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "licenses_pkey")
}

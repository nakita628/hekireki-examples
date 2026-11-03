package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RuntimeEnvironmentSessionTable : IdTable<String>("\"RuntimeEnvironmentSession\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val ipAddress = text("ipAddress")
    val environmentId =
        reference(
            "environmentId",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RuntimeEnvironmentSession_environmentId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val disconnectedAt = pgTimestamp("disconnectedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"RuntimeEnvironmentSession_pkey\"")
}

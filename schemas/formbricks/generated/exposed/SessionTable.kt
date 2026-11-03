package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionTable : IdTable<String>("\"Session\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sessionToken = text("sessionToken").uniqueIndex("\"Session_sessionToken_key\"")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Session_userId_fkey\"",
        )
    val expires = pgTimestamp("expires", 3)
    val ipAddress = text("ipAddress").nullable()
    val userAgent = text("userAgent").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Session_pkey\"")

    init {
        index("\"Session_userId_idx\"", false, userId)
    }
}

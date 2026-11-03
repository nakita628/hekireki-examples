package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DocumentAuditLogTable : IdTable<String>("\"DocumentAuditLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val envelopeId =
        optReference(
            "envelopeId",
            EnvelopeTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DocumentAuditLog_envelopeId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = text("type")
    val data = jsonb<String>("data", { it }, { it })
    val name = text("name").nullable()
    val email = text("email").nullable()
    val userId = integer("userId").nullable()
    val userAgent = text("userAgent").nullable()
    val ipAddress = text("ipAddress").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"DocumentAuditLog_pkey\"")

    init {
        index("\"DocumentAuditLog_envelopeId_idx\"", false, envelopeId)
    }
}

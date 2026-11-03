package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AuditActorTable : IdTable<UUID>("\"AuditActor\"") {
    override val id = javaUUID("id").clientDefault { uuidV7() }.entityId()
    val type = pgEnum("type", "\"AuditActorType\"", AuditActorType.entries, AuditActorType::dbName)
    val userUuid = javaUUID("userUuid").nullable().uniqueIndex("\"AuditActor_userUuid_key\"")
    val attendeeId = integer("attendeeId").nullable().uniqueIndex("\"AuditActor_attendeeId_key\"")
    val credentialId = integer("credentialId").nullable().uniqueIndex("\"AuditActor_credentialId_key\"")
    val email = text("email").nullable().uniqueIndex("\"AuditActor_email_key\"")
    val phone = text("phone").nullable().uniqueIndex("\"AuditActor_phone_key\"")
    val name = text("name").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AuditActor_pkey\"")

    init {
        index("\"AuditActor_email_idx\"", false, email)
        index("\"AuditActor_userUuid_idx\"", false, userUuid)
        index("\"AuditActor_attendeeId_idx\"", false, attendeeId)
        index("\"AuditActor_credentialId_idx\"", false, credentialId)
    }
}

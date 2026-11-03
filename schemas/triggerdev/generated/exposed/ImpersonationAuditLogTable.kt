package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ImpersonationAuditLogTable : IdTable<String>("\"ImpersonationAuditLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val action =
        pgEnum("action", "\"ImpersonationAuditLogAction\"", ImpersonationAuditLogAction.entries, ImpersonationAuditLogAction::dbName)
    val adminId =
        reference(
            "adminId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ImpersonationAuditLog_adminId_fkey\"",
        )
    val targetId =
        reference(
            "targetId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ImpersonationAuditLog_targetId_fkey\"",
        )
    val ipAddress = text("ipAddress").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ImpersonationAuditLog_pkey\"")

    init {
        index("\"ImpersonationAuditLog_adminId_idx\"", false, adminId)
        index("\"ImpersonationAuditLog_targetId_idx\"", false, targetId)
        index("\"ImpersonationAuditLog_createdAt_idx\"", false, createdAt)
    }
}

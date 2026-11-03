package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserSecurityAuditLogTable : IdTable<Int>("\"UserSecurityAuditLog\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserSecurityAuditLog_userId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = pgEnum("type", "\"UserSecurityAuditLogType\"", UserSecurityAuditLogType.entries, UserSecurityAuditLogType::dbName)
    val userAgent = text("userAgent").nullable()
    val ipAddress = text("ipAddress").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"UserSecurityAuditLog_pkey\"")
}

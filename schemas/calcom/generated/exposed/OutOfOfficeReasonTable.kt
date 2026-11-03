package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OutOfOfficeReasonTable : IdTable<Int>("\"OutOfOfficeReason\"") {
    override val id = integer("id").autoIncrement().entityId()
    val emoji = text("emoji")
    val reason = text("reason").uniqueIndex("\"OutOfOfficeReason_reason_key\"")
    val enabled = bool("enabled").default(true)
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OutOfOfficeReason_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"OutOfOfficeReason_pkey\"")
}

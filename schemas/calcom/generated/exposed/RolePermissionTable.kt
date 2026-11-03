package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object RolePermissionTable : IdTable<String>("\"RolePermission\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val roleId =
        reference(
            "roleId",
            RoleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RolePermission_roleId_fkey\"",
        )
    val resource = text("resource")
    val action = text("action")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RolePermission_pkey\"")

    init {
        index("\"RolePermission_roleId_idx\"", false, roleId)
        index("\"RolePermission_action_idx\"", false, action)
        uniqueIndex("\"RolePermission_roleId_resource_action_key\"", roleId, resource, action)
    }
}

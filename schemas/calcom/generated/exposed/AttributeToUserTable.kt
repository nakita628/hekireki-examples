package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AttributeToUserTable : IdTable<String>("\"AttributeToUser\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val memberId =
        reference(
            "memberId",
            MembershipTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeToUser_memberId_fkey\"",
        )
    val attributeOptionId =
        reference(
            "attributeOptionId",
            AttributeOptionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeToUser_attributeOptionId_fkey\"",
        )
    val weight = integer("weight").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById =
        optReference(
            "createdById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeToUser_createdById_fkey\"",
        )
    val createdByDSyncId =
        text("createdByDSyncId")
            .references(
                DSyncDataTable.directoryId,
                onDelete = ReferenceOption.SET_NULL,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"AttributeToUser_createdByDSyncId_fkey\"",
            ).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedById =
        optReference(
            "updatedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttributeToUser_updatedById_fkey\"",
        )
    val updatedByDSyncId =
        text("updatedByDSyncId")
            .references(
                DSyncDataTable.directoryId,
                onDelete = ReferenceOption.SET_NULL,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"AttributeToUser_updatedByDSyncId_fkey\"",
            ).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AttributeToUser_pkey\"")

    init {
        uniqueIndex("\"AttributeToUser_memberId_attributeOptionId_key\"", memberId, attributeOptionId)
    }
}

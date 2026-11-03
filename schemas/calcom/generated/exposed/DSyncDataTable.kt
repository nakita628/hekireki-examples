package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object DSyncDataTable : IdTable<Int>("\"DSyncData\"") {
    override val id = integer("id").autoIncrement().entityId()
    val directoryId = text("directoryId").uniqueIndex("\"DSyncData_directoryId_key\"")
    val tenant = text("tenant")
    val organizationId =
        integer("organizationId")
            .references(
                OrganizationSettingsTable.organizationId,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"DSyncData_organizationId_fkey\"",
            ).nullable()
            .uniqueIndex("\"DSyncData_organizationId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"DSyncData_pkey\"")
}

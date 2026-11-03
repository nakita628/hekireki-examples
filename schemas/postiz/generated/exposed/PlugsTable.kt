package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object PlugsTable : IdTable<String>("\"Plugs\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Plugs_organizationId_fkey\"",
        )
    val plugFunction = text("plugFunction")
    val data = text("data")
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Plugs_integrationId_fkey\"",
        )
    val activated = bool("activated").default(true)
    override val primaryKey = PrimaryKey(id, name = "\"Plugs_pkey\"")

    init {
        index("\"Plugs_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"Plugs_plugFunction_integrationId_key\"", plugFunction, integrationId)
    }
}

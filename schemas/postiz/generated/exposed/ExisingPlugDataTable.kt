package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object ExisingPlugDataTable : IdTable<String>("\"ExisingPlugData\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ExisingPlugData_integrationId_fkey\"",
        )
    val methodName = text("methodName")
    val value = text("value")
    override val primaryKey = PrimaryKey(id, name = "\"ExisingPlugData_pkey\"")

    init {
        uniqueIndex("\"ExisingPlugData_integrationId_methodName_value_key\"", integrationId, methodName, value)
    }
}

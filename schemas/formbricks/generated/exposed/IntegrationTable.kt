package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object IntegrationTable : IdTable<String>("\"Integration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = pgEnum("type", "\"IntegrationType\"", IntegrationType.entries, IntegrationType::dbName)
    val config = jsonb<String>("config", { it }, { it })
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Integration_workspaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Integration_pkey\"")

    init {
        index("\"Integration_workspaceId_idx\"", false, workspaceId)
        uniqueIndex("\"Integration_type_workspaceId_key\"", type, workspaceId)
    }
}

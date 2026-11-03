package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object EventTypeCustomInputTable : IdTable<Int>("\"EventTypeCustomInput\"") {
    override val id = integer("id").autoIncrement().entityId()
    val eventTypeId =
        reference(
            "eventTypeId",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EventTypeCustomInput_eventTypeId_fkey\"",
        )
    val label = text("label")
    val type = pgEnum("type", "\"EventTypeCustomInputType\"", EventTypeCustomInputType.entries, EventTypeCustomInputType::dbName)
    val options1 = jsonb<String>("options", { it }, { it }).nullable()
    val required = bool("required")
    val placeholder = text("placeholder").default("")
    override val primaryKey = PrimaryKey(id, name = "\"EventTypeCustomInput_pkey\"")

    init {
        index("\"EventTypeCustomInput_eventTypeId_idx\"", false, eventTypeId)
    }
}

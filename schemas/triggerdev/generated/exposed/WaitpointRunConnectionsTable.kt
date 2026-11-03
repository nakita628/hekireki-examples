package triggerdev.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object WaitpointRunConnectionsTable : Table("\"_WaitpointRunConnections\"") {
    val a =
        reference(
            "\"A\"",
            TaskRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_WaitpointRunConnections_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            WaitpointTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_WaitpointRunConnections_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_WaitpointRunConnections_AB_pkey\"")

    init {
        index("\"_WaitpointRunConnections_B_index\"", false, b)
    }
}

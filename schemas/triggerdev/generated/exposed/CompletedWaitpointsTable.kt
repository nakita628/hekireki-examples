package triggerdev.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object CompletedWaitpointsTable : Table("\"_completedWaitpoints\"") {
    val a =
        reference(
            "\"A\"",
            TaskRunExecutionSnapshotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_completedWaitpoints_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            WaitpointTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_completedWaitpoints_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_completedWaitpoints_AB_pkey\"")

    init {
        index("\"_completedWaitpoints_B_index\"", false, b)
    }
}

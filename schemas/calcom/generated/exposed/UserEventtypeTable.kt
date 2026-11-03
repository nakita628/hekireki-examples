package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object UserEventtypeTable : Table("_user_eventtype") {
    val a =
        reference(
            "\"A\"",
            EventTypeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_user_eventtype_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_user_eventtype_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_user_eventtype_AB_pkey\"")

    init {
        index("\"_user_eventtype_B_index\"", false, b)
    }
}

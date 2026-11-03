package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object OrderToTagTable : Table("\"_OrderToTag\"") {
    val a =
        reference(
            "\"A\"",
            OrderTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_OrderToTag_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            TagTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_OrderToTag_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_OrderToTag_AB_pkey\"")

    init {
        index("\"_OrderToTag_B_index\"", false, b)
    }
}

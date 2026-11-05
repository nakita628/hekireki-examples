package inboxzero.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object CategoryToRuleTable : Table("\"_CategoryToRule\"") {
    val a =
        reference(
            "\"A\"",
            CategoryTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_CategoryToRule_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            RuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_CategoryToRule_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_CategoryToRule_AB_pkey\"")

    init {
        index("\"_CategoryToRule_B_index\"", false, b)
    }
}

package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object IntegrationToNotificationRuleTable : Table("\"_IntegrationToNotificationRule\"") {
    val a =
        reference(
            "\"A\"",
            IntegrationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_IntegrationToNotificationRule_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            NotificationRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_IntegrationToNotificationRule_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_IntegrationToNotificationRule_AB_pkey\"")

    init {
        index("\"_IntegrationToNotificationRule_B_index\"", false, b)
    }
}

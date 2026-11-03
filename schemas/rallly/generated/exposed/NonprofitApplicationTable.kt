package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object NonprofitApplicationTable : IdTable<String>("nonprofit_applications") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "nonprofit_applications_space_id_fkey",
        )
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "nonprofit_applications_user_id_fkey",
        )
    val organizationName = text("organization_name")
    val website = text("website")
    val emailDomain = text("email_domain")
    val status =
        pgEnum("status", "\"nonprofit_application_status\"", NonprofitApplicationStatus.entries, NonprofitApplicationStatus::dbName)
    val reason = text("reason").nullable()
    val model = text("model").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "nonprofit_applications_pkey")

    init {
        index("nonprofit_applications_space_id_created_at_idx", false, spaceId, createdAt)
    }
}

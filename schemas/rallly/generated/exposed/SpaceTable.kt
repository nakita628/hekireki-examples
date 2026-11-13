package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SpaceTable : IdTable<String>("spaces") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val image = text("image").nullable()
    val ownerId =
        reference(
            "owner_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "spaces_owner_id_fkey",
        )
    val tier = pgEnum("tier", "\"space_tiers\"", SpaceTier.entries, SpaceTier::dbName).default(SpaceTier.HOBBY)
    val spaceType = pgEnum("space_type", "\"space_types\"", SpaceType.entries, SpaceType::dbName).nullable()
    val industry = text("industry").nullable()
    val primaryColor = text("primary_color").nullable()
    val showBranding = bool("show_branding").default(false)
    val hideAttribution = bool("hide_attribution").default(false)
    val shared = bool("shared").default(false)
    val nonprofitDiscountGrantedAt = pgTimestamp("nonprofit_discount_granted_at", 3).nullable()
    val apiPollQuotaOverride = integer("api_poll_quota_override").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "spaces_pkey")

    init {
        index("spaces_owner_id_idx", false, ownerId)
    }
}

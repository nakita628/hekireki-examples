package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SocialMediaAgencyTable : IdTable<String>("\"SocialMediaAgency\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SocialMediaAgency_userId_fkey\"",
        ).uniqueIndex("\"SocialMediaAgency_userId_key\"")
    val name = text("name")
    val logoId =
        optReference(
            "logoId",
            MediaTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SocialMediaAgency_logoId_fkey\"",
        )
    val website = text("website").nullable()
    val slug = text("slug").nullable()
    val facebook = text("facebook").nullable()
    val instagram = text("instagram").nullable()
    val twitter = text("twitter").nullable()
    val linkedIn = text("linkedIn").nullable()
    val youtube = text("youtube").nullable()
    val tiktok = text("tiktok").nullable()
    val otherSocialMedia = text("otherSocialMedia").nullable()
    val shortDescription = text("shortDescription")
    val description = text("description")
    val approved = bool("approved").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SocialMediaAgency_pkey\"")

    init {
        index("\"SocialMediaAgency_userId_idx\"", false, userId)
        index("\"SocialMediaAgency_deletedAt_idx\"", false, deletedAt)
        index("\"SocialMediaAgency_id_idx\"", false, id)
    }
}

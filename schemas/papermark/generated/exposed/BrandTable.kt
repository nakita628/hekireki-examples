package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BrandTable : IdTable<String>("\"Brand\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val logo = text("logo").nullable()
    val hideLogo = bool("hideLogo").default(false)
    val banner = text("banner").nullable()
    val brandColor = text("brandColor").nullable()
    val accentColor = text("accentColor").nullable()
    val accentButtonColor = text("accentButtonColor").nullable()
    val applyAccentColorToDataroomView = bool("applyAccentColorToDataroomView").default(false)
    val welcomeMessage = text("welcomeMessage").nullable()
    val ctaLabel = text("ctaLabel").nullable()
    val ctaUrl = text("ctaUrl").nullable()
    val privacyPolicyUrl = text("privacyPolicyUrl").nullable()
    val cardLayout = text("cardLayout").default("LIST")
    val showFolderTree = bool("showFolderTree").default(true)
    val viewerLayoutPreset = text("viewerLayoutPreset").default("STANDARD")
    val viewerHeaderStyle = text("viewerHeaderStyle").default("DEFAULT")
    val hideFolderIconsInMain = bool("hideFolderIconsInMain").default(false)
    val customLinkPreviewEnabled = bool("customLinkPreviewEnabled").default(false)
    val linkPreviewTitle = text("linkPreviewTitle").nullable()
    val linkPreviewDescription = text("linkPreviewDescription").nullable()
    val linkPreviewImage = text("linkPreviewImage").nullable()
    val linkPreviewFavicon = text("linkPreviewFavicon").nullable()
    val defaultLanguage = text("defaultLanguage").default("en")
    val name = text("name").default("Default")
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Brand_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Brand_pkey\"")

    init {
        index("\"Brand_teamId_idx\"", false, teamId)
        uniqueIndex("\"Brand_teamId_name_key\"", teamId, name)
    }
}

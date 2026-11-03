package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataroomBrandTable : IdTable<String>("\"DataroomBrand\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val logo = text("logo").nullable()
    val hideLogo = bool("hideLogo").nullable()
    val banner = text("banner").nullable()
    val brandColor = text("brandColor").nullable()
    val accentColor = text("accentColor").nullable()
    val accentButtonColor = text("accentButtonColor").nullable()
    val applyAccentColorToDataroomView = bool("applyAccentColorToDataroomView").default(false)
    val welcomeMessage = text("welcomeMessage").nullable()
    val cardLayout = text("cardLayout").default("LIST")
    val showFolderTree = bool("showFolderTree").default(true)
    val viewerLayoutPreset = text("viewerLayoutPreset").default("STANDARD")
    val viewerHeaderStyle = text("viewerHeaderStyle").default("DEFAULT")
    val hideFolderIconsInMain = bool("hideFolderIconsInMain").default(false)
    val ctaLabel = text("ctaLabel").nullable()
    val ctaUrl = text("ctaUrl").nullable()
    val customLinkPreviewEnabled = bool("customLinkPreviewEnabled").default(false)
    val linkPreviewTitle = text("linkPreviewTitle").nullable()
    val linkPreviewDescription = text("linkPreviewDescription").nullable()
    val linkPreviewImage = text("linkPreviewImage").nullable()
    val linkPreviewFavicon = text("linkPreviewFavicon").nullable()
    val defaultLanguage = text("defaultLanguage").default("en")
    val dataroomId =
        reference(
            "dataroomId",
            DataroomTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DataroomBrand_dataroomId_fkey\"",
        ).uniqueIndex("\"DataroomBrand_dataroomId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataroomBrand_pkey\"")
}

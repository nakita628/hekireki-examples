package typebot.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"ChatProvider\" AS ENUM ('WHATSAPP', 'DIALOG360')",
            "CREATE TYPE \"WorkspaceRole\" AS ENUM ('ADMIN', 'MEMBER', 'GUEST')",
            "CREATE TYPE \"GraphNavigation\" AS ENUM ('MOUSE', 'TRACKPAD')",
            "CREATE TYPE \"Plan\" AS ENUM ('FREE', 'STARTER', 'PRO', 'LIFETIME', 'OFFERED', 'CUSTOM', 'UNLIMITED', 'ENTERPRISE')",
            "CREATE TYPE \"CollaborationType\" AS ENUM ('READ', 'WRITE', 'FULL_ACCESS')",
        )
    val tables: List<Table> =
        listOf(
            AccountTable,
            SessionTable,
            UserTable,
            ApiTokenTable,
            WorkspaceTable,
            SpaceTable,
            MemberInWorkspaceTable,
            WorkspaceInvitationTable,
            CustomDomainTable,
            CredentialsTable,
            UserCredentialsTable,
            VerificationTokenTable,
            DashboardFolderTable,
            TypebotTable,
            InvitationTable,
            CollaboratorsOnTypebotsTable,
            PublicTypebotTable,
            RuntimeMediaIdCacheTable,
            ResultTable,
            SetVariableHistoryItemTable,
            VisitedEdgeTable,
            LogTable,
            AnswerTable,
            AnswerV2Table,
            CouponTable,
            WebhookTable,
            ClaimableCustomPlanTable,
            ChatSessionTable,
            ThemeTemplateTable,
            BannedIpTable,
            SuppressedEmailTable,
        )
}

use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    pub username: Option<String>,
    pub password: String,
    #[sea_orm(column_name = "pfpFilename")]
    pub pfp_filename: Option<String>,
    #[sea_orm(default_value = "default")]
    pub role: String,
    #[sea_orm(default_value = 0)]
    pub suspended: i32,
    #[sea_orm(default_value = false)]
    pub seen_recovery_codes: Option<bool>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
    #[sea_orm(column_name = "lastUpdatedAt")]
    pub last_updated_at: PrismaDateTime,
    #[sea_orm(column_name = "dailyMessageLimit")]
    pub daily_message_limit: Option<i32>,
    #[sea_orm(default_value = "")]
    pub bio: Option<String>,
    pub web_push_subscription_config: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::workspace_chats::Entity")]
    WorkspaceChats,
    #[sea_orm(has_many = "super::workspace_users::Entity")]
    WorkspaceUsers,
    #[sea_orm(has_many = "super::embed_configs::Entity")]
    EmbedConfigs,
    #[sea_orm(has_many = "super::embed_chats::Entity")]
    EmbedChats,
    #[sea_orm(has_many = "super::workspace_threads::Entity")]
    Threads,
    #[sea_orm(has_many = "super::recovery_codes::Entity")]
    RecoveryCodes,
    #[sea_orm(has_many = "super::password_reset_tokens::Entity")]
    PasswordResetTokens,
    #[sea_orm(has_many = "super::workspace_agent_invocations::Entity")]
    WorkspaceAgentInvocations,
    #[sea_orm(has_many = "super::slash_command_presets::Entity")]
    SlashCommandPresets,
    #[sea_orm(has_many = "super::browser_extension_api_keys::Entity")]
    BrowserExtensionApiKeys,
    #[sea_orm(has_many = "super::temporary_auth_tokens::Entity")]
    TemporaryAuthTokens,
    #[sea_orm(has_many = "super::system_prompt_variables::Entity")]
    SystemPromptVariables,
    #[sea_orm(has_many = "super::prompt_history::Entity")]
    PromptHistory,
    #[sea_orm(has_many = "super::desktop_mobile_devices::Entity")]
    DesktopMobileDevices,
    #[sea_orm(has_many = "super::workspace_parsed_files::Entity")]
    WorkspaceParsedFiles,
    #[sea_orm(has_many = "super::memories::Entity")]
    Memories,
}

impl Related<super::workspace_chats::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceChats.def()
    }
}

impl Related<super::workspace_users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceUsers.def()
    }
}

impl Related<super::embed_configs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbedConfigs.def()
    }
}

impl Related<super::embed_chats::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbedChats.def()
    }
}

impl Related<super::workspace_threads::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Threads.def()
    }
}

impl Related<super::recovery_codes::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RecoveryCodes.def()
    }
}

impl Related<super::password_reset_tokens::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PasswordResetTokens.def()
    }
}

impl Related<super::workspace_agent_invocations::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceAgentInvocations.def()
    }
}

impl Related<super::slash_command_presets::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SlashCommandPresets.def()
    }
}

impl Related<super::browser_extension_api_keys::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BrowserExtensionApiKeys.def()
    }
}

impl Related<super::temporary_auth_tokens::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TemporaryAuthTokens.def()
    }
}

impl Related<super::system_prompt_variables::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SystemPromptVariables.def()
    }
}

impl Related<super::prompt_history::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PromptHistory.def()
    }
}

impl Related<super::desktop_mobile_devices::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DesktopMobileDevices.def()
    }
}

impl Related<super::workspace_parsed_files::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkspaceParsedFiles.def()
    }
}

impl Related<super::memories::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Memories.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.into());
        }
        if insert && self.last_updated_at.is_not_set() {
            self.last_updated_at = Set(now.into());
        }
        Ok(self)
    }
}

use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Organization")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub name: String,
    #[sea_orm(default_value = "{}")]
    pub whitelabel: Json,
    #[sea_orm(column_name = "isAISmartToolsEnabled", default_value = false)]
    pub is_aismart_tools_enabled: bool,
    #[sea_orm(column_name = "displayTimeZone")]
    pub display_time_zone: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::membership::Entity")]
    Memberships,
    #[sea_orm(has_many = "super::workspace::Entity")]
    Workspaces,
    #[sea_orm(has_many = "super::invite::Entity")]
    Invites,
    #[sea_orm(has_many = "super::team::Entity")]
    Teams,
    #[sea_orm(has_many = "super::api_key::Entity")]
    ApiKeys,
    #[sea_orm(has_many = "super::feedback_directory::Entity")]
    FeedbackDirectories,
    #[sea_orm(has_many = "super::retention_policy::Entity")]
    RetentionPolicies,
    #[sea_orm(has_many = "super::retention_exemption::Entity")]
    RetentionExemptions,
    #[sea_orm(has_many = "super::retention_run::Entity")]
    RetentionRuns,
    #[sea_orm(has_many = "super::retention_notice::Entity")]
    RetentionNotices,
    #[sea_orm(has_one = "super::organization_billing::Entity")]
    Billing,
}

impl Related<super::membership::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Memberships.def()
    }
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspaces.def()
    }
}

impl Related<super::invite::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Invites.def()
    }
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Teams.def()
    }
}

impl Related<super::api_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiKeys.def()
    }
}

impl Related<super::feedback_directory::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FeedbackDirectories.def()
    }
}

impl Related<super::retention_policy::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionPolicies.def()
    }
}

impl Related<super::retention_exemption::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionExemptions.def()
    }
}

impl Related<super::retention_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionRuns.def()
    }
}

impl Related<super::retention_notice::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RetentionNotices.def()
    }
}

impl Related<super::organization_billing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Billing.def()
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
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
